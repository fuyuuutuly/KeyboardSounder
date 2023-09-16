using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace KeyboardSounder
{
    public partial class Form1 : Form
    {
        private Dictionary<int, bool> keyStateDic = new Dictionary<int, bool>();
        private List<TextBox> textBoxList = new List<TextBox>();
        private Color defaultColor;
        private List<CheckBox> checkBoxList = new List<CheckBox>();

        private List<string> checkBoxNameList = new List<string>() { "checkBoxA", "checkBoxS", "checkBoxD", "checkBoxF", "checkBoxSpace", "checkBoxJ", "checkBoxK", "checkBoxL", "checkBoxSemi" };
        private List<string> buttonNameList = new List<string>() { "buttonA", "buttonS", "buttonD", "buttonF", "buttonSpace", "buttonJ", "buttonK", "buttonL", "buttonSemi" };

        private string[] driverList;
        private AsioOut asioOut;
        private string asioDriver;
        private static WaveMixerStream32 mixer = new WaveMixerStream32();

        private AudioFileReader[] afr = new AudioFileReader[9];

        private string iniPath = "./setting.ini"; //Iniファイルのパス

        private Setting setting = new Setting();

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
         string lpApplicationName,
         string lpKeyName,
         string lpDefault,
         StringBuilder lpReturnedstring,
         int nSize,
         string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpstring,
            string lpFileName);

        public Form1()
        {
            InitializeComponent();

            // 初期設定
            initialDefaultSetting();

            // 音声ファイルの取得
            initialAudioSetting();

            // ドライバー
            driverList = AsioOut.GetDriverNames();
            foreach (string s in driverList)
            {
                comboBoxDevice.Items.Add(s);
            }

            if (driverList.Length > 0)
            {
                if (driverList.Contains(setting.device))
                {
                    // 保存されているデバイスがあればそれを選択
                    int idx = driverList.ToList().IndexOf(setting.device);
                    comboBoxDevice.SelectedIndex = idx;
                }
                else
                {
                    // 保存されているデバイスがなければ0番目を選択
                    comboBoxDevice.SelectedIndex = 0;
                }
                // 選択されているデバイスで初期化
                InitializeDriver(driverList[comboBoxDevice.SelectedIndex]);
            }
            else
            {
                MessageBox.Show("対応するASIOデバイスがありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private KeyboardHook keyboardHook = new KeyboardHook();

        protected override void OnLoad(EventArgs e)
        {
            keyboardHook.KeyDownEvent += KeyboardHook_KeyDownEvent;
            keyboardHook.KeyUpEvent += KeyboardHook_KeyUpEvent;
            keyboardHook.Hook();
        }

        private void KeyboardHook_KeyDownEvent(object sender, KeyEventArg e)
        {
            // キーが押されたときに音声再生
            if (keyStateDic.ContainsKey(e.KeyCode))
            {
                if (keyStateDic[e.KeyCode] == false)
                {
                    keyStateDic[e.KeyCode] = true;

                    int i = keyStateDic.Keys.ToList().IndexOf(e.KeyCode);
                    textBoxList[i].BackColor = Color.Yellow;
                    if (setting.isEnabled[i] && afr[i] != null)
                    {
                        afr[i].Position = 0;
                    }
                }
            }
        }

        private void KeyboardHook_KeyUpEvent(object sender, KeyEventArg e)
        {
            // キーが離されたとき
            if (keyStateDic.ContainsKey(e.KeyCode))
            {
                if (keyStateDic[e.KeyCode] == true)
                {
                    keyStateDic[e.KeyCode] = false;

                    int i = keyStateDic.Keys.ToList().IndexOf(e.KeyCode);
                    textBoxList[i].BackColor = defaultColor;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            keyboardHook.UnHook();
        }

        // iniファイルがない場合の初期設定保存
        private void initialDefaultSetting()
        {
            // リストの初期化
            keyStateDic.Add((int)Keys.A, false);
            keyStateDic.Add((int)Keys.S, false);
            keyStateDic.Add((int)Keys.D, false);
            keyStateDic.Add((int)Keys.F, false);
            keyStateDic.Add((int)Keys.Space, false);
            keyStateDic.Add((int)Keys.J, false);
            keyStateDic.Add((int)Keys.K, false);
            keyStateDic.Add((int)Keys.L, false);
            keyStateDic.Add((int)Keys.Oemplus, false);

            textBoxList.Add(textBoxA);
            textBoxList.Add(textBoxS);
            textBoxList.Add(textBoxD);
            textBoxList.Add(textBoxF);
            textBoxList.Add(textBoxSpace);
            textBoxList.Add(textBoxJ);
            textBoxList.Add(textBoxK);
            textBoxList.Add(textBoxL);
            textBoxList.Add(textBoxSemi);

            defaultColor = textBoxA.BackColor;

            checkBoxList.Add(checkBoxA);
            checkBoxList.Add(checkBoxS);
            checkBoxList.Add(checkBoxD);
            checkBoxList.Add(checkBoxF);
            checkBoxList.Add(checkBoxSpace);
            checkBoxList.Add(checkBoxJ);
            checkBoxList.Add(checkBoxK);
            checkBoxList.Add(checkBoxL);
            checkBoxList.Add(checkBoxSemi);

            if (!File.Exists(iniPath))
            {
                // iniファイルがない場合の初期設定保存
                SaveSetting(SettingSection.DeviceSetting.ToString(), SettingKey.Device.ToString(), "");
                SaveSetting(SettingSection.DeviceSetting.ToString(), SettingKey.Volume.ToString(), "15");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_A.ToString(), "false");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_S.ToString(), "false");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_D.ToString(), "true");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_F.ToString(), "true");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_Space.ToString(), "false");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_J.ToString(), "true");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_K.ToString(), "true");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_L.ToString(), "false");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_Semi.ToString(), "false");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_A.ToString(), "");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_S.ToString(), "");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_D.ToString(), "./ka.wav");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_F.ToString(), "./dong.wav");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_Space.ToString(), "");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_J.ToString(), "./dong.wav");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_K.ToString(), "./ka.wav");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_L.ToString(), "");
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_Semi.ToString(), "");
            }

            // iniファイルのデータを一括ロード
            setting.device = LoadSetting(SettingSection.DeviceSetting.ToString(), SettingKey.Device.ToString(), "");
            setting.volume = int.Parse(LoadSetting(SettingSection.DeviceSetting.ToString(), SettingKey.Volume.ToString(), "15"));
            setting.isEnabled[0] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_A.ToString(), "false"));
            setting.isEnabled[1] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_S.ToString(), "false"));
            setting.isEnabled[2] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_D.ToString(), "true"));
            setting.isEnabled[3] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_F.ToString(), "true"));
            setting.isEnabled[4] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_Space.ToString(), "false"));
            setting.isEnabled[5] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_J.ToString(), "true"));
            setting.isEnabled[6] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_K.ToString(), "true"));
            setting.isEnabled[7] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_L.ToString(), "false"));
            setting.isEnabled[8] = bool.Parse(LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_Semi.ToString(), "false"));
            setting.soundPath[0] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_A.ToString(), "");
            setting.soundPath[1] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_S.ToString(), "");
            setting.soundPath[2] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_D.ToString(), "./ka.wav");
            setting.soundPath[3] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_F.ToString(), "./dong.wav");
            setting.soundPath[4] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_Space.ToString(), "");
            setting.soundPath[5] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_J.ToString(), "./dong.wav");
            setting.soundPath[6] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_K.ToString(), "./ka.wav");
            setting.soundPath[7] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_L.ToString(), "");
            setting.soundPath[8] = LoadSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_Semi.ToString(), "");

            // 画面に反映
            trackBarVolume.Value = setting.volume;

            for (int i = 0; i < 9; i++)
            {
                checkBoxList[i].Checked = setting.isEnabled[i];
                textBoxList[i].Text = setting.soundPath[i];
            }
        }

        // 全データ保存
        private void saveAllSettings()
        {
            if (File.Exists(iniPath))
            {
                SaveSetting(SettingSection.DeviceSetting.ToString(), SettingKey.Device.ToString(), setting.device);
                SaveSetting(SettingSection.DeviceSetting.ToString(), SettingKey.Volume.ToString(), setting.volume.ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_A.ToString(), setting.isEnabled[0].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_S.ToString(), setting.isEnabled[1].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_D.ToString(), setting.isEnabled[2].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_F.ToString(), setting.isEnabled[3].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_Space.ToString(), setting.isEnabled[4].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_J.ToString(), setting.isEnabled[5].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_K.ToString(), setting.isEnabled[6].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_L.ToString(), setting.isEnabled[7].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.IsEnabled_Semi.ToString(), setting.isEnabled[8].ToString());
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_A.ToString(), setting.soundPath[0]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_S.ToString(), setting.soundPath[1]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_D.ToString(), setting.soundPath[2]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_F.ToString(), setting.soundPath[3]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_Space.ToString(), setting.soundPath[4]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_J.ToString(), setting.soundPath[5]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_K.ToString(), setting.soundPath[6]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_L.ToString(), setting.soundPath[7]);
                SaveSetting(SettingSection.SoundSetting.ToString(), SettingKey.SoundPath_Semi.ToString(), setting.soundPath[8]);
            }
        }

        private void initialAudioSetting()
        {
            for (int i = 0; i < 9; i++)
            {
                mixer.RemoveInputStream(afr[i]);
            }

            for (int i = 0; i < 9; i++)
            {
                if (File.Exists(setting.soundPath[i]) && setting.soundPath[i].EndsWith(".wav"))
                {
                    try
                    {
                        afr[i] = new AudioFileReader(setting.soundPath[i]);
                        mixer.AddInputStream(afr[i]);
                        afr[i].Position = afr[i].Length;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(setting.soundPath[i]);
                        afr[i] = null;
                    }
                }
                else
                {
                    afr[i] = null;
                }
            }

            mixer.AutoStop = false;

            setVolume();
        }

        private string LoadSetting(string section, string key, string strDef)
        {
            //取得値を格納する変数
            StringBuilder sbResult = new StringBuilder(1024);
            try
            {
                //Iniファイル情報取得
                int intRet = GetPrivateProfileString(section
                                    , key
                                    , strDef
                                    , sbResult
                                    , sbResult.Capacity - 1
                                    , iniPath);

                return sbResult.ToString();
            }
            catch (Exception ex)
            {
                return strDef;
            }
        }

        private bool SaveSetting(string section, string key, string data)
        {
            try
            {
                //エスケープ文字変換
                //string strCnv = SetEscape(strSet);
                //Iniファイルに保存
                int intRet = WritePrivateProfileString(section, key, data, iniPath);

                if (intRet > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void InitializeDriver(string driver)
        {
            asioDriver = driver;
            asioOut?.Dispose();
            asioOut = new AsioOut(asioDriver);
            asioOut.Init(mixer);
            asioOut.Play();
        }

        private void trackBarVolume_Change(object sender, EventArgs e)
        {
            labelVolume.Text = trackBarVolume.Value.ToString();
            setting.volume = trackBarVolume.Value;
            setVolume();
        }

        private void trackBarVolume_MouseUp(object sender, MouseEventArgs e)
        {
            saveAllSettings();
        }

        private void setVolume()
        {
            for (int i = 0; i < 9; i++)
            {
                if (afr[i] != null)
                {
                    afr[i].Volume = (float)(setting.volume / 100f);
                }
            }
        }

        private void comboBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string driver = comboBoxDevice.SelectedItem.ToString();
            setting.device = driver;
            saveAllSettings();

            InitializeDriver(driver);
            Console.WriteLine(driver);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string name = ((CheckBox)sender).Name;

            int i = checkBoxNameList.IndexOf(name);
            setting.isEnabled[i] = checkBoxList[i].Checked;

            saveAllSettings();
        }

        private void button_Click(object sender, EventArgs e)
        {
            var o = openFileDialog1;
            DialogResult dr = o.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string name = ((Button)sender).Name;

                int i = buttonNameList.IndexOf(name);
                textBoxList[i].Text = o.FileName;
                setting.soundPath[i] = o.FileName;

                saveAllSettings();
                initialAudioSetting();
            }
        }
    }

    internal enum SettingSection
    {
        DeviceSetting,
        SoundSetting
    }

    internal enum SettingKey
    {
        Device,
        Volume,
        IsEnabled_A,
        IsEnabled_S,
        IsEnabled_D,
        IsEnabled_F,
        IsEnabled_Space,
        IsEnabled_J,
        IsEnabled_K,
        IsEnabled_L,
        IsEnabled_Semi,
        SoundPath_A,
        SoundPath_S,
        SoundPath_D,
        SoundPath_F,
        SoundPath_Space,
        SoundPath_J,
        SoundPath_K,
        SoundPath_L,
        SoundPath_Semi,
    }

    public class Setting
    {
        public string device;
        public int volume;
        public bool[] isEnabled = new bool[9];
        public string[] soundPath = new string[9];
    }
}