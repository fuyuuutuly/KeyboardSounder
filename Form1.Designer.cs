
namespace KeyboardSounder
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxDevice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.labelVolume = new System.Windows.Forms.Label();
            this.checkBoxA = new System.Windows.Forms.CheckBox();
            this.checkBoxS = new System.Windows.Forms.CheckBox();
            this.checkBoxD = new System.Windows.Forms.CheckBox();
            this.checkBoxF = new System.Windows.Forms.CheckBox();
            this.checkBoxSpace = new System.Windows.Forms.CheckBox();
            this.checkBoxJ = new System.Windows.Forms.CheckBox();
            this.checkBoxK = new System.Windows.Forms.CheckBox();
            this.checkBoxL = new System.Windows.Forms.CheckBox();
            this.checkBoxSemi = new System.Windows.Forms.CheckBox();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.textBoxD = new System.Windows.Forms.TextBox();
            this.textBoxF = new System.Windows.Forms.TextBox();
            this.textBoxSpace = new System.Windows.Forms.TextBox();
            this.textBoxJ = new System.Windows.Forms.TextBox();
            this.textBoxK = new System.Windows.Forms.TextBox();
            this.textBoxL = new System.Windows.Forms.TextBox();
            this.textBoxSemi = new System.Windows.Forms.TextBox();
            this.buttonA = new System.Windows.Forms.Button();
            this.buttonS = new System.Windows.Forms.Button();
            this.buttonD = new System.Windows.Forms.Button();
            this.buttonF = new System.Windows.Forms.Button();
            this.buttonSpace = new System.Windows.Forms.Button();
            this.buttonJ = new System.Windows.Forms.Button();
            this.buttonK = new System.Windows.Forms.Button();
            this.buttonL = new System.Windows.Forms.Button();
            this.buttonSemi = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxDevice
            // 
            this.comboBoxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevice.FormattingEnabled = true;
            this.comboBoxDevice.Location = new System.Drawing.Point(110, 22);
            this.comboBoxDevice.Name = "comboBoxDevice";
            this.comboBoxDevice.Size = new System.Drawing.Size(224, 20);
            this.comboBoxDevice.TabIndex = 3;
            this.comboBoxDevice.SelectedIndexChanged += new System.EventHandler(this.comboBoxDevice_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(45, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "出力先";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(57, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "音量";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(110, 54);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(224, 45);
            this.trackBarVolume.TabIndex = 1;
            this.trackBarVolume.Value = 15;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            // 
            // labelVolume
            // 
            this.labelVolume.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVolume.Location = new System.Drawing.Point(52, 66);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(39, 18);
            this.labelVolume.TabIndex = 2;
            this.labelVolume.Text = "15";
            this.labelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxA
            // 
            this.checkBoxA.AutoSize = true;
            this.checkBoxA.Location = new System.Drawing.Point(16, 30);
            this.checkBoxA.Name = "checkBoxA";
            this.checkBoxA.Size = new System.Drawing.Size(32, 16);
            this.checkBoxA.TabIndex = 6;
            this.checkBoxA.Text = "A";
            this.checkBoxA.UseVisualStyleBackColor = true;
            this.checkBoxA.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxS
            // 
            this.checkBoxS.AutoSize = true;
            this.checkBoxS.Location = new System.Drawing.Point(16, 56);
            this.checkBoxS.Name = "checkBoxS";
            this.checkBoxS.Size = new System.Drawing.Size(31, 16);
            this.checkBoxS.TabIndex = 7;
            this.checkBoxS.Text = "S";
            this.checkBoxS.UseVisualStyleBackColor = true;
            this.checkBoxS.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxD
            // 
            this.checkBoxD.AutoSize = true;
            this.checkBoxD.Location = new System.Drawing.Point(16, 82);
            this.checkBoxD.Name = "checkBoxD";
            this.checkBoxD.Size = new System.Drawing.Size(32, 16);
            this.checkBoxD.TabIndex = 8;
            this.checkBoxD.Text = "D";
            this.checkBoxD.UseVisualStyleBackColor = true;
            this.checkBoxD.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxF
            // 
            this.checkBoxF.AutoSize = true;
            this.checkBoxF.Location = new System.Drawing.Point(16, 108);
            this.checkBoxF.Name = "checkBoxF";
            this.checkBoxF.Size = new System.Drawing.Size(31, 16);
            this.checkBoxF.TabIndex = 9;
            this.checkBoxF.Text = "F";
            this.checkBoxF.UseVisualStyleBackColor = true;
            this.checkBoxF.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxSpace
            // 
            this.checkBoxSpace.AutoSize = true;
            this.checkBoxSpace.Location = new System.Drawing.Point(16, 134);
            this.checkBoxSpace.Name = "checkBoxSpace";
            this.checkBoxSpace.Size = new System.Drawing.Size(55, 16);
            this.checkBoxSpace.TabIndex = 10;
            this.checkBoxSpace.Text = "Space";
            this.checkBoxSpace.UseVisualStyleBackColor = true;
            this.checkBoxSpace.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxJ
            // 
            this.checkBoxJ.AutoSize = true;
            this.checkBoxJ.Location = new System.Drawing.Point(16, 160);
            this.checkBoxJ.Name = "checkBoxJ";
            this.checkBoxJ.Size = new System.Drawing.Size(31, 16);
            this.checkBoxJ.TabIndex = 11;
            this.checkBoxJ.Text = "J";
            this.checkBoxJ.UseVisualStyleBackColor = true;
            this.checkBoxJ.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxK
            // 
            this.checkBoxK.AutoSize = true;
            this.checkBoxK.Location = new System.Drawing.Point(16, 186);
            this.checkBoxK.Name = "checkBoxK";
            this.checkBoxK.Size = new System.Drawing.Size(31, 16);
            this.checkBoxK.TabIndex = 12;
            this.checkBoxK.Text = "K";
            this.checkBoxK.UseVisualStyleBackColor = true;
            this.checkBoxK.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxL
            // 
            this.checkBoxL.AutoSize = true;
            this.checkBoxL.Location = new System.Drawing.Point(16, 212);
            this.checkBoxL.Name = "checkBoxL";
            this.checkBoxL.Size = new System.Drawing.Size(30, 16);
            this.checkBoxL.TabIndex = 13;
            this.checkBoxL.Text = "L";
            this.checkBoxL.UseVisualStyleBackColor = true;
            this.checkBoxL.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxSemi
            // 
            this.checkBoxSemi.AutoSize = true;
            this.checkBoxSemi.Location = new System.Drawing.Point(16, 238);
            this.checkBoxSemi.Name = "checkBoxSemi";
            this.checkBoxSemi.Size = new System.Drawing.Size(26, 16);
            this.checkBoxSemi.TabIndex = 14;
            this.checkBoxSemi.Text = ";";
            this.checkBoxSemi.UseVisualStyleBackColor = true;
            this.checkBoxSemi.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // textBoxA
            // 
            this.textBoxA.Location = new System.Drawing.Point(87, 27);
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.ReadOnly = true;
            this.textBoxA.Size = new System.Drawing.Size(242, 19);
            this.textBoxA.TabIndex = 15;
            // 
            // textBoxS
            // 
            this.textBoxS.Location = new System.Drawing.Point(87, 53);
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.ReadOnly = true;
            this.textBoxS.Size = new System.Drawing.Size(242, 19);
            this.textBoxS.TabIndex = 16;
            // 
            // textBoxD
            // 
            this.textBoxD.Location = new System.Drawing.Point(87, 79);
            this.textBoxD.Name = "textBoxD";
            this.textBoxD.ReadOnly = true;
            this.textBoxD.Size = new System.Drawing.Size(242, 19);
            this.textBoxD.TabIndex = 17;
            // 
            // textBoxF
            // 
            this.textBoxF.Location = new System.Drawing.Point(87, 105);
            this.textBoxF.Name = "textBoxF";
            this.textBoxF.ReadOnly = true;
            this.textBoxF.Size = new System.Drawing.Size(242, 19);
            this.textBoxF.TabIndex = 18;
            // 
            // textBoxSpace
            // 
            this.textBoxSpace.Location = new System.Drawing.Point(87, 131);
            this.textBoxSpace.Name = "textBoxSpace";
            this.textBoxSpace.ReadOnly = true;
            this.textBoxSpace.Size = new System.Drawing.Size(242, 19);
            this.textBoxSpace.TabIndex = 19;
            // 
            // textBoxJ
            // 
            this.textBoxJ.Location = new System.Drawing.Point(87, 157);
            this.textBoxJ.Name = "textBoxJ";
            this.textBoxJ.ReadOnly = true;
            this.textBoxJ.Size = new System.Drawing.Size(242, 19);
            this.textBoxJ.TabIndex = 20;
            // 
            // textBoxK
            // 
            this.textBoxK.Location = new System.Drawing.Point(87, 183);
            this.textBoxK.Name = "textBoxK";
            this.textBoxK.ReadOnly = true;
            this.textBoxK.Size = new System.Drawing.Size(242, 19);
            this.textBoxK.TabIndex = 21;
            // 
            // textBoxL
            // 
            this.textBoxL.Location = new System.Drawing.Point(87, 209);
            this.textBoxL.Name = "textBoxL";
            this.textBoxL.ReadOnly = true;
            this.textBoxL.Size = new System.Drawing.Size(242, 19);
            this.textBoxL.TabIndex = 22;
            // 
            // textBoxSemi
            // 
            this.textBoxSemi.Location = new System.Drawing.Point(87, 235);
            this.textBoxSemi.Name = "textBoxSemi";
            this.textBoxSemi.ReadOnly = true;
            this.textBoxSemi.Size = new System.Drawing.Size(242, 19);
            this.textBoxSemi.TabIndex = 23;
            // 
            // buttonA
            // 
            this.buttonA.Location = new System.Drawing.Point(335, 26);
            this.buttonA.Name = "buttonA";
            this.buttonA.Size = new System.Drawing.Size(20, 22);
            this.buttonA.TabIndex = 24;
            this.buttonA.Text = "...";
            this.buttonA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonA.UseVisualStyleBackColor = true;
            // 
            // buttonS
            // 
            this.buttonS.Location = new System.Drawing.Point(335, 52);
            this.buttonS.Name = "buttonS";
            this.buttonS.Size = new System.Drawing.Size(20, 22);
            this.buttonS.TabIndex = 25;
            this.buttonS.Text = "...";
            this.buttonS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonS.UseVisualStyleBackColor = true;
            // 
            // buttonD
            // 
            this.buttonD.Location = new System.Drawing.Point(335, 78);
            this.buttonD.Name = "buttonD";
            this.buttonD.Size = new System.Drawing.Size(20, 22);
            this.buttonD.TabIndex = 26;
            this.buttonD.Text = "...";
            this.buttonD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonD.UseVisualStyleBackColor = true;
            // 
            // buttonF
            // 
            this.buttonF.Location = new System.Drawing.Point(335, 104);
            this.buttonF.Name = "buttonF";
            this.buttonF.Size = new System.Drawing.Size(20, 22);
            this.buttonF.TabIndex = 27;
            this.buttonF.Text = "...";
            this.buttonF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonF.UseVisualStyleBackColor = true;
            // 
            // buttonSpace
            // 
            this.buttonSpace.Location = new System.Drawing.Point(335, 130);
            this.buttonSpace.Name = "buttonSpace";
            this.buttonSpace.Size = new System.Drawing.Size(20, 22);
            this.buttonSpace.TabIndex = 28;
            this.buttonSpace.Text = "...";
            this.buttonSpace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSpace.UseVisualStyleBackColor = true;
            // 
            // buttonJ
            // 
            this.buttonJ.Location = new System.Drawing.Point(335, 156);
            this.buttonJ.Name = "buttonJ";
            this.buttonJ.Size = new System.Drawing.Size(20, 22);
            this.buttonJ.TabIndex = 29;
            this.buttonJ.Text = "...";
            this.buttonJ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonJ.UseVisualStyleBackColor = true;
            // 
            // buttonK
            // 
            this.buttonK.Location = new System.Drawing.Point(335, 182);
            this.buttonK.Name = "buttonK";
            this.buttonK.Size = new System.Drawing.Size(20, 22);
            this.buttonK.TabIndex = 30;
            this.buttonK.Text = "...";
            this.buttonK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonK.UseVisualStyleBackColor = true;
            // 
            // buttonL
            // 
            this.buttonL.Location = new System.Drawing.Point(335, 208);
            this.buttonL.Name = "buttonL";
            this.buttonL.Size = new System.Drawing.Size(20, 22);
            this.buttonL.TabIndex = 31;
            this.buttonL.Text = "...";
            this.buttonL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonL.UseVisualStyleBackColor = true;
            // 
            // buttonSemi
            // 
            this.buttonSemi.Location = new System.Drawing.Point(335, 234);
            this.buttonSemi.Name = "buttonSemi";
            this.buttonSemi.Size = new System.Drawing.Size(20, 22);
            this.buttonSemi.TabIndex = 32;
            this.buttonSemi.Text = "...";
            this.buttonSemi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSemi.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSemi);
            this.groupBox1.Controls.Add(this.buttonL);
            this.groupBox1.Controls.Add(this.buttonK);
            this.groupBox1.Controls.Add(this.buttonJ);
            this.groupBox1.Controls.Add(this.buttonSpace);
            this.groupBox1.Controls.Add(this.buttonF);
            this.groupBox1.Controls.Add(this.buttonD);
            this.groupBox1.Controls.Add(this.buttonS);
            this.groupBox1.Controls.Add(this.buttonA);
            this.groupBox1.Controls.Add(this.textBoxSemi);
            this.groupBox1.Controls.Add(this.textBoxL);
            this.groupBox1.Controls.Add(this.textBoxK);
            this.groupBox1.Controls.Add(this.textBoxJ);
            this.groupBox1.Controls.Add(this.textBoxSpace);
            this.groupBox1.Controls.Add(this.textBoxF);
            this.groupBox1.Controls.Add(this.textBoxD);
            this.groupBox1.Controls.Add(this.textBoxS);
            this.groupBox1.Controls.Add(this.textBoxA);
            this.groupBox1.Controls.Add(this.checkBoxSemi);
            this.groupBox1.Controls.Add(this.checkBoxL);
            this.groupBox1.Controls.Add(this.checkBoxK);
            this.groupBox1.Controls.Add(this.checkBoxJ);
            this.groupBox1.Controls.Add(this.checkBoxSpace);
            this.groupBox1.Controls.Add(this.checkBoxF);
            this.groupBox1.Controls.Add(this.checkBoxD);
            this.groupBox1.Controls.Add(this.checkBoxS);
            this.groupBox1.Controls.Add(this.checkBoxA);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(12, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 270);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SoundSetting";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxDevice);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.labelVolume);
            this.groupBox2.Controls.Add(this.trackBarVolume);
            this.groupBox2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 103);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DeviceSetting";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 403);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "KeyboardSounder";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.CheckBox checkBoxA;
        private System.Windows.Forms.CheckBox checkBoxS;
        private System.Windows.Forms.CheckBox checkBoxD;
        private System.Windows.Forms.CheckBox checkBoxF;
        private System.Windows.Forms.CheckBox checkBoxSpace;
        private System.Windows.Forms.CheckBox checkBoxJ;
        private System.Windows.Forms.CheckBox checkBoxK;
        private System.Windows.Forms.CheckBox checkBoxL;
        private System.Windows.Forms.CheckBox checkBoxSemi;
        private System.Windows.Forms.TextBox textBoxA;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.TextBox textBoxD;
        private System.Windows.Forms.TextBox textBoxF;
        private System.Windows.Forms.TextBox textBoxSpace;
        private System.Windows.Forms.TextBox textBoxJ;
        private System.Windows.Forms.TextBox textBoxK;
        private System.Windows.Forms.TextBox textBoxL;
        private System.Windows.Forms.TextBox textBoxSemi;
        private System.Windows.Forms.Button buttonA;
        private System.Windows.Forms.Button buttonS;
        private System.Windows.Forms.Button buttonD;
        private System.Windows.Forms.Button buttonF;
        private System.Windows.Forms.Button buttonSpace;
        private System.Windows.Forms.Button buttonJ;
        private System.Windows.Forms.Button buttonK;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonSemi;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

