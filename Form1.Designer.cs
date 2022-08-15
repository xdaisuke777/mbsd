
namespace MBSD
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SQL_radioButton = new System.Windows.Forms.RadioButton();
            this.SQL_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.XSS_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.XSS_radioButton = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DT_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.DT_radioButton = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.DR_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.DR_radioButton = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CSRF_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.CSRF_radioButton = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.other_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.other_radioButton = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.OSCI_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.OSCI_radioButton = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.HTTPHI_OFF_radioButton = new System.Windows.Forms.RadioButton();
            this.HTTPHI_radioButton = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ONOFFbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.warn = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.Location = new System.Drawing.Point(558, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(374, 19);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "http://";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.Location = new System.Drawing.Point(720, 551);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "実行";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "SQLインジェクション";
            // 
            // SQL_radioButton
            // 
            this.SQL_radioButton.AutoSize = true;
            this.SQL_radioButton.Checked = true;
            this.SQL_radioButton.Location = new System.Drawing.Point(120, 8);
            this.SQL_radioButton.Name = "SQL_radioButton";
            this.SQL_radioButton.Size = new System.Drawing.Size(39, 16);
            this.SQL_radioButton.TabIndex = 15;
            this.SQL_radioButton.TabStop = true;
            this.SQL_radioButton.Text = "ON";
            this.SQL_radioButton.UseVisualStyleBackColor = true;
            // 
            // SQL_OFF_radioButton
            // 
            this.SQL_OFF_radioButton.AutoSize = true;
            this.SQL_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.SQL_OFF_radioButton.Name = "SQL_OFF_radioButton";
            this.SQL_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.SQL_OFF_radioButton.TabIndex = 16;
            this.SQL_OFF_radioButton.TabStop = true;
            this.SQL_OFF_radioButton.Text = "OFF";
            this.SQL_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.SQL_OFF_radioButton);
            this.panel1.Controls.Add(this.SQL_radioButton);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(603, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 36);
            this.panel1.TabIndex = 17;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Controls.Add(this.XSS_OFF_radioButton);
            this.panel2.Controls.Add(this.XSS_radioButton);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(603, 169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 36);
            this.panel2.TabIndex = 18;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // XSS_OFF_radioButton
            // 
            this.XSS_OFF_radioButton.AutoSize = true;
            this.XSS_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.XSS_OFF_radioButton.Name = "XSS_OFF_radioButton";
            this.XSS_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.XSS_OFF_radioButton.TabIndex = 16;
            this.XSS_OFF_radioButton.Text = "OFF";
            this.XSS_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // XSS_radioButton
            // 
            this.XSS_radioButton.AutoSize = true;
            this.XSS_radioButton.Checked = true;
            this.XSS_radioButton.Location = new System.Drawing.Point(120, 8);
            this.XSS_radioButton.Name = "XSS_radioButton";
            this.XSS_radioButton.Size = new System.Drawing.Size(39, 16);
            this.XSS_radioButton.TabIndex = 15;
            this.XSS_radioButton.TabStop = true;
            this.XSS_radioButton.Text = "ON";
            this.XSS_radioButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "XSS";
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel3.Controls.Add(this.DT_OFF_radioButton);
            this.panel3.Controls.Add(this.DT_radioButton);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(603, 211);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(224, 36);
            this.panel3.TabIndex = 18;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // DT_OFF_radioButton
            // 
            this.DT_OFF_radioButton.AutoSize = true;
            this.DT_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.DT_OFF_radioButton.Name = "DT_OFF_radioButton";
            this.DT_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.DT_OFF_radioButton.TabIndex = 16;
            this.DT_OFF_radioButton.Text = "OFF";
            this.DT_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // DT_radioButton
            // 
            this.DT_radioButton.AutoSize = true;
            this.DT_radioButton.Checked = true;
            this.DT_radioButton.Location = new System.Drawing.Point(120, 8);
            this.DT_radioButton.Name = "DT_radioButton";
            this.DT_radioButton.Size = new System.Drawing.Size(39, 16);
            this.DT_radioButton.TabIndex = 15;
            this.DT_radioButton.TabStop = true;
            this.DT_radioButton.Text = "ON";
            this.DT_radioButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "ディレトラ";
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.Controls.Add(this.DR_OFF_radioButton);
            this.panel5.Controls.Add(this.DR_radioButton);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Location = new System.Drawing.Point(603, 253);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(224, 36);
            this.panel5.TabIndex = 20;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // DR_OFF_radioButton
            // 
            this.DR_OFF_radioButton.AutoSize = true;
            this.DR_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.DR_OFF_radioButton.Name = "DR_OFF_radioButton";
            this.DR_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.DR_OFF_radioButton.TabIndex = 16;
            this.DR_OFF_radioButton.Text = "OFF";
            this.DR_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // DR_radioButton
            // 
            this.DR_radioButton.AutoSize = true;
            this.DR_radioButton.Checked = true;
            this.DR_radioButton.Location = new System.Drawing.Point(120, 8);
            this.DR_radioButton.Name = "DR_radioButton";
            this.DR_radioButton.Size = new System.Drawing.Size(39, 16);
            this.DR_radioButton.TabIndex = 15;
            this.DR_radioButton.TabStop = true;
            this.DR_radioButton.Text = "ON";
            this.DR_radioButton.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "ディレリス";
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Controls.Add(this.CSRF_OFF_radioButton);
            this.panel4.Controls.Add(this.CSRF_radioButton);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(603, 295);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(224, 36);
            this.panel4.TabIndex = 21;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // CSRF_OFF_radioButton
            // 
            this.CSRF_OFF_radioButton.AutoSize = true;
            this.CSRF_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.CSRF_OFF_radioButton.Name = "CSRF_OFF_radioButton";
            this.CSRF_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.CSRF_OFF_radioButton.TabIndex = 16;
            this.CSRF_OFF_radioButton.Text = "OFF";
            this.CSRF_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // CSRF_radioButton
            // 
            this.CSRF_radioButton.AutoSize = true;
            this.CSRF_radioButton.Checked = true;
            this.CSRF_radioButton.Location = new System.Drawing.Point(120, 8);
            this.CSRF_radioButton.Name = "CSRF_radioButton";
            this.CSRF_radioButton.Size = new System.Drawing.Size(39, 16);
            this.CSRF_radioButton.TabIndex = 15;
            this.CSRF_radioButton.TabStop = true;
            this.CSRF_radioButton.Text = "ON";
            this.CSRF_radioButton.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "CSRF";
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel6.Controls.Add(this.other_OFF_radioButton);
            this.panel6.Controls.Add(this.other_radioButton);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Location = new System.Drawing.Point(603, 465);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(224, 36);
            this.panel6.TabIndex = 24;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // other_OFF_radioButton
            // 
            this.other_OFF_radioButton.AutoSize = true;
            this.other_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.other_OFF_radioButton.Name = "other_OFF_radioButton";
            this.other_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.other_OFF_radioButton.TabIndex = 16;
            this.other_OFF_radioButton.Text = "OFF";
            this.other_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // other_radioButton
            // 
            this.other_radioButton.AutoSize = true;
            this.other_radioButton.Checked = true;
            this.other_radioButton.Location = new System.Drawing.Point(120, 8);
            this.other_radioButton.Name = "other_radioButton";
            this.other_radioButton.Size = new System.Drawing.Size(39, 16);
            this.other_radioButton.TabIndex = 15;
            this.other_radioButton.TabStop = true;
            this.other_radioButton.Text = "ON";
            this.other_radioButton.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "意図しないリダ";
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel7.Controls.Add(this.OSCI_OFF_radioButton);
            this.panel7.Controls.Add(this.OSCI_radioButton);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Location = new System.Drawing.Point(603, 335);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(224, 36);
            this.panel7.TabIndex = 22;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // OSCI_OFF_radioButton
            // 
            this.OSCI_OFF_radioButton.AutoSize = true;
            this.OSCI_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.OSCI_OFF_radioButton.Name = "OSCI_OFF_radioButton";
            this.OSCI_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.OSCI_OFF_radioButton.TabIndex = 16;
            this.OSCI_OFF_radioButton.Text = "OFF";
            this.OSCI_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // OSCI_radioButton
            // 
            this.OSCI_radioButton.AutoSize = true;
            this.OSCI_radioButton.Checked = true;
            this.OSCI_radioButton.Location = new System.Drawing.Point(120, 8);
            this.OSCI_radioButton.Name = "OSCI_radioButton";
            this.OSCI_radioButton.Size = new System.Drawing.Size(39, 16);
            this.OSCI_radioButton.TabIndex = 15;
            this.OSCI_radioButton.TabStop = true;
            this.OSCI_radioButton.Text = "ON";
            this.OSCI_radioButton.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "OSCI";
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel8.Controls.Add(this.HTTPHI_OFF_radioButton);
            this.panel8.Controls.Add(this.HTTPHI_radioButton);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Location = new System.Drawing.Point(603, 424);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(224, 36);
            this.panel8.TabIndex = 23;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // HTTPHI_OFF_radioButton
            // 
            this.HTTPHI_OFF_radioButton.AutoSize = true;
            this.HTTPHI_OFF_radioButton.Location = new System.Drawing.Point(165, 8);
            this.HTTPHI_OFF_radioButton.Name = "HTTPHI_OFF_radioButton";
            this.HTTPHI_OFF_radioButton.Size = new System.Drawing.Size(45, 16);
            this.HTTPHI_OFF_radioButton.TabIndex = 16;
            this.HTTPHI_OFF_radioButton.Text = "OFF";
            this.HTTPHI_OFF_radioButton.UseVisualStyleBackColor = true;
            // 
            // HTTPHI_radioButton
            // 
            this.HTTPHI_radioButton.AutoSize = true;
            this.HTTPHI_radioButton.Checked = true;
            this.HTTPHI_radioButton.Location = new System.Drawing.Point(120, 8);
            this.HTTPHI_radioButton.Name = "HTTPHI_radioButton";
            this.HTTPHI_radioButton.Size = new System.Drawing.Size(39, 16);
            this.HTTPHI_radioButton.TabIndex = 15;
            this.HTTPHI_radioButton.TabStop = true;
            this.HTTPHI_radioButton.Text = "ON";
            this.HTTPHI_radioButton.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 12);
            this.label13.TabIndex = 14;
            this.label13.Text = "HTTPHI";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "テキスト文書|*.csv|すべてのファイル|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ONOFFbutton
            // 
            this.ONOFFbutton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ONOFFbutton.Location = new System.Drawing.Point(707, 510);
            this.ONOFFbutton.Name = "ONOFFbutton";
            this.ONOFFbutton.Size = new System.Drawing.Size(120, 35);
            this.ONOFFbutton.TabIndex = 25;
            this.ONOFFbutton.Text = "一括 OFF";
            this.ONOFFbutton.UseVisualStyleBackColor = true;
            this.ONOFFbutton.Click += new System.EventHandler(this.ONOFFbutton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(301, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 37);
            this.button1.TabIndex = 26;
            this.button1.Text = "前回の記録";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(556, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "http://で始まるURLを入力してください";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // warn
            // 
            this.warn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.warn.AutoSize = true;
            this.warn.Location = new System.Drawing.Point(556, 79);
            this.warn.Name = "warn";
            this.warn.Size = new System.Drawing.Size(0, 12);
            this.warn.TabIndex = 28;
            // 
            // panel9
            // 
            this.panel9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel9.Location = new System.Drawing.Point(699, 424);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(159, 79);
            this.panel9.TabIndex = 29;
            this.panel9.Paint += new System.Windows.Forms.PaintEventHandler(this.panel9_Paint);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(623, 380);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "「以下の2つの項目について」";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(627, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "頑張りましたが、間に合いませんでした。";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MBSD.Properties.Resources.namakemono1;
            this.pictureBox1.Location = new System.Drawing.Point(165, 114);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(432, 431);
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1587, 664);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.warn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ONOFFbutton);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton SQL_radioButton;
        private System.Windows.Forms.RadioButton SQL_OFF_radioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton XSS_OFF_radioButton;
        private System.Windows.Forms.RadioButton XSS_radioButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton DT_OFF_radioButton;
        private System.Windows.Forms.RadioButton DT_radioButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton DR_OFF_radioButton;
        private System.Windows.Forms.RadioButton DR_radioButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton CSRF_OFF_radioButton;
        private System.Windows.Forms.RadioButton CSRF_radioButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton other_OFF_radioButton;
        private System.Windows.Forms.RadioButton other_radioButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton OSCI_OFF_radioButton;
        private System.Windows.Forms.RadioButton OSCI_radioButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton HTTPHI_OFF_radioButton;
        private System.Windows.Forms.RadioButton HTTPHI_radioButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ONOFFbutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label warn;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

