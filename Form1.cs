using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private int MyCount;

        private short beforeKey1AsyncState;
        private short beforeKey2AsyncState;
        private short beforeKey3AsyncState;
        private short beforeKey4AsyncState;

        AsioOut asioOut;
        String asioDriver;
        WaveOutEvent outputDevice;
        private static WaveMixerStream32 mixer = new WaveMixerStream32();

        AudioFileReader afr1 = new AudioFileReader(@"dong.wav");
        AudioFileReader afr2 = new AudioFileReader(@"ka.wav");

        public Form1()
        {
            InitializeComponent();

            //タイマー初期化
            MyTimer = new DispatcherTimer();
            MyTimer.Tick += MyTimer_Tick;
            //時間間隔、8ミリ秒にしてみた
            MyTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            MyTimer.Start();

            string[] DriverList = AsioOut.GetDriverNames();
            foreach (string s in DriverList)
            {
                comboBox1.Items.Add(s);
                asioDriver = s;
                asioOut = new AsioOut(asioDriver);


                mixer.AutoStop = false;

                mixer.AddInputStream(afr1);
                mixer.AddInputStream(afr2);
                afr1.Volume = (float)(trackBar1.Value / 500f);
                afr2.Volume = (float)(trackBar1.Value / 500f);
                asioOut.Init(mixer);
                asioOut.Play();



            }

        }

        //一定時間間隔でキーの状態を取得して表示
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            //KeyをWindowsAPIで使う仮想キーコードに変換
            var vKey1 = KeyInterop.VirtualKeyFromKey(Key.F);
            var vKey2 = KeyInterop.VirtualKeyFromKey(Key.J);
            var vKey3 = KeyInterop.VirtualKeyFromKey(Key.D);
            var vKey4 = KeyInterop.VirtualKeyFromKey(Key.K);

            //GetAsyncKeyStateでキーの状態を取得して値を表示
            short key1AsyncState = GetAsyncKeyState(vKey1);
            short key2AsyncState = GetAsyncKeyState(vKey2);
            short key3AsyncState = GetAsyncKeyState(vKey3);
            short key4AsyncState = GetAsyncKeyState(vKey4);
            //Console.WriteLine("AsyncKey= " + key1AsyncState.ToString());
            //Console.WriteLine("AsyncKey= " + key2AsyncState.ToString());

            if (beforeKey1AsyncState == 0 && key1AsyncState != 0)
            {
                afr1.Volume = (float)(trackBar1.Value / 500f);
                afr1.Position = 0;


                //AudioFileReader afr = new AudioFileReader(@"dong.wav");
                //outputDevice = new WaveOutEvent();
                //afr.Volume = (float)(trackBar1.Value / 100f);
                //outputDevice.Init(afr);
                //outputDevice.Play();
            }
            beforeKey1AsyncState = key1AsyncState;

            if (beforeKey2AsyncState == 0 && key2AsyncState != 0)
            {
                afr1.Volume = (float)(trackBar1.Value / 500f);
                afr1.Position = 0;
            }
            beforeKey2AsyncState = key2AsyncState;


            if (beforeKey3AsyncState == 0 && key3AsyncState != 0)
            {
                afr2.Volume = (float)(trackBar1.Value / 500f);
                afr2.Position = 0;
            }
            beforeKey3AsyncState = key3AsyncState;


            if (beforeKey4AsyncState == 0 && key4AsyncState != 0)
            {
                afr2.Volume = (float)(trackBar1.Value / 500f);
                afr2.Position = 0;
            }
            beforeKey4AsyncState = key4AsyncState;

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



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


            //Console.WriteLine(asioDriver);


            //Console.WriteLine(asioOut.PlaybackState);
            //asioOut.Play();

            //AudioFileReader afr = new AudioFileReader(@"ka.wav");
            //outputDevice = new WaveOutEvent();
            //afr.Volume = (float)(trackBar1.Value / 100f);
            //outputDevice.Init(afr);
            //outputDevice.Play();




            switch (e.KeyCode)
            {
                case Keys.D:

                    break;
                case Keys.F:

                    break;
                case Keys.J:

                    break;
                case Keys.K:

                    break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();

        }
    }
}
