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
using System.Windows.Input;
using System.Windows.Threading;

namespace KeyboardSounder
{
    public partial class Form1 : Form
    {
        //必要なAPIはGetAsyncKeyStateだけなんだけど
        //似たようなGetKeyStateも使ってみた
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        [DllImport("user32.dll")]

        private static extern short GetKeyState(int vKey);

        private DispatcherTimer MyTimer;

        private short[] beforeKeyAsyncState = new short[9];

        AsioOut asioOut;
        string asioDriver;
        private static WaveMixerStream32 mixer = new WaveMixerStream32();

        AudioFileReader[] afr = new AudioFileReader[9];

        string iniPath = "./setting.ini"; //Iniファイルのパス

        Setting setting = new Setting();


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


            //タイマー初期化
            MyTimer = new DispatcherTimer();
            MyTimer.Tick += MyTimer_Tick;
            //時間間隔、1ミリ秒にしてみた
            MyTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            MyTimer.Start();

            // 音声ファイルの取得
            initialAudioSetting();

            // ドライバー
            string[] DriverList = AsioOut.GetDriverNames();
            foreach (string s in DriverList)
            {
                comboBoxDevice.Items.Add(s);
            }

            if (DriverList.Length > 0)
            {
                comboBoxDevice.SelectedIndex = 0;
                InitializeDriver(DriverList[0]);
            }



        }

        // iniファイルがない場合の初期設定保存
        private void initialDefaultSetting()
        {
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
            checkBoxA.Checked = setting.isEnabled[0];
            checkBoxS.Checked = setting.isEnabled[1];
            checkBoxD.Checked = setting.isEnabled[2];
            checkBoxF.Checked = setting.isEnabled[3];
            checkBoxSpace.Checked = setting.isEnabled[4];
            checkBoxJ.Checked = setting.isEnabled[5];
            checkBoxK.Checked = setting.isEnabled[6];
            checkBoxL.Checked = setting.isEnabled[7];
            checkBoxSemi.Checked = setting.isEnabled[8];
            textBoxA.Text = setting.soundPath[0];
            textBoxS.Text = setting.soundPath[1];
            textBoxD.Text = setting.soundPath[2];
            textBoxF.Text = setting.soundPath[3];
            textBoxSpace.Text = setting.soundPath[4];
            textBoxJ.Text = setting.soundPath[5];
            textBoxK.Text = setting.soundPath[6];
            textBoxL.Text = setting.soundPath[7];
            textBoxSemi.Text = setting.soundPath[8];

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

        //一定時間間隔でキーの状態を取得して表示
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            //KeyをWindowsAPIで使う仮想キーコードに変換
            int[] vKey = new int[9];
            vKey[0] = KeyInterop.VirtualKeyFromKey(Key.A);
            vKey[1] = KeyInterop.VirtualKeyFromKey(Key.S);
            vKey[2] = KeyInterop.VirtualKeyFromKey(Key.D);
            vKey[3] = KeyInterop.VirtualKeyFromKey(Key.F);
            vKey[4] = KeyInterop.VirtualKeyFromKey(Key.Space);
            vKey[5] = KeyInterop.VirtualKeyFromKey(Key.J);
            vKey[6] = KeyInterop.VirtualKeyFromKey(Key.K);
            vKey[7] = KeyInterop.VirtualKeyFromKey(Key.L);
            vKey[8] = KeyInterop.VirtualKeyFromKey(Key.OemSemicolon);

            //GetAsyncKeyStateでキーの状態を取得して値を表示
            short[] keyAsyncState = new short[9];
            for (int i = 0; i < 9; i++)
            {
                if (setting.isEnabled[i] && afr[i] != null)
                {
                    keyAsyncState[i] = GetAsyncKeyState(vKey[i]);
                    if (beforeKeyAsyncState[i] == 0 && keyAsyncState[i] != 0)
                    {
                        afr[i].Position = 0;
                    }
                    beforeKeyAsyncState[i] = keyAsyncState[i];
                }
            }


            //GetKeyStateでキーの状態を取得して値を表示
            //short key1State = GetKeyState(vKey1);
            //short key2State = GetKeyState(vKey2);
            //Console.WriteLine("Key= " + key1State.ToString());
            //Console.WriteLine("Key= " + key2State.ToString());

            //Key1が押された状態で、Key2も押されていたらの判定
            // != 0 この判定の仕方は雑だけど問題なさそう
            //if (key1AsyncState != 0 & (key2AsyncState & 1) == 1)
            //{
            //    //カウントを増やして回数の表示を更新
            //    MyCount++;
            //    MyTextBlockCount.Text = MyCount.ToString() + "回";
            //}

            //↑の雑じゃない判定版
            //if (((key1AsyncState & 0x8000) >> 15 == 1) & ((key2AsyncState & 1) == 1)) { }

            //GetKeyStateとGetAsyncKeyState版でもできた
            //if ((key1State & 0x8000) >> 15 == 1 & (key2AsyncState & 1)== 1)
            //{
            //    MyCount++;
            //    MyTextBlockCount.Text = MyCount.ToString() + "回";
            //}

            //GetkeyStateだけだとできなかった
            //両キーとも押しっぱなしのときしか判定されない
            //if ((key1State & 0x8000) >> 15 == 1 & (key2State & 0x80) >> 7 == 1)
            //{
            //    MyCount++;
            //    MyTextBlockCount.Text = MyCount.ToString() + "回";
            //}

            //GetAsynckeyStateだけ → できた
            //GetkeyStateとGetAsynckeyState → できた
            //GetkeyStateだけ → むり？

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
            InitializeDriver(driver);
            Console.WriteLine(driver);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string name = ((CheckBox)sender).Name;
            switch (name)
            {
                case "checkBoxA":
                    setting.isEnabled[0] = checkBoxA.Checked;
                    break;
                case "checkBoxS":
                    setting.isEnabled[1] = checkBoxS.Checked;
                    break;
                case "checkBoxD":
                    setting.isEnabled[2] = checkBoxD.Checked;
                    break;
                case "checkBoxF":
                    setting.isEnabled[3] = checkBoxF.Checked;
                    break;
                case "checkBoxSpace":
                    setting.isEnabled[4] = checkBoxSpace.Checked;
                    break;
                case "checkBoxJ":
                    setting.isEnabled[5] = checkBoxJ.Checked;
                    break;
                case "checkBoxK":
                    setting.isEnabled[6] = checkBoxK.Checked;
                    break;
                case "checkBoxL":
                    setting.isEnabled[7] = checkBoxL.Checked;
                    break;
                case "checkBoxSemi":
                    setting.isEnabled[8] = checkBoxSemi.Checked;
                    break;
            }
            saveAllSettings();


        }

        private void button_Click(object sender, EventArgs e)
        {
            var o = openFileDialog1;
            DialogResult dr = o.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string name = ((Button)sender).Name;
                switch (name)
                {
                    case "buttonA":
                        textBoxA.Text = o.FileName;
                        setting.soundPath[0] = o.FileName;
                        break;
                    case "buttonS":
                        textBoxS.Text = o.FileName;
                        setting.soundPath[1] = o.FileName;
                        break;
                    case "buttonD":
                        textBoxD.Text = o.FileName;
                        setting.soundPath[2] = o.FileName;
                        break;
                    case "buttonF":
                        textBoxF.Text = o.FileName;
                        setting.soundPath[3] = o.FileName;
                        break;
                    case "buttonSpace":
                        textBoxSpace.Text = o.FileName;
                        setting.soundPath[4] = o.FileName;
                        break;
                    case "buttonJ":
                        textBoxJ.Text = o.FileName;
                        setting.soundPath[5] = o.FileName;
                        break;
                    case "buttonK":
                        textBoxK.Text = o.FileName;
                        setting.soundPath[6] = o.FileName;
                        break;
                    case "buttonL":
                        textBoxL.Text = o.FileName;
                        setting.soundPath[7] = o.FileName;
                        break;
                    case "buttonSemi":
                        textBoxSemi.Text = o.FileName;
                        setting.soundPath[8] = o.FileName;
                        break;
                }

                saveAllSettings();
                initialAudioSetting();
            }
        }


    }


    enum SettingSection
    {
        DeviceSetting,
        SoundSetting
    }

    enum SettingKey
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
