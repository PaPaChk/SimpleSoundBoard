namespace Soundboard
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            comboBox1 = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            button2 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            linkLabel1 = new LinkLabel();
            checkBox1 = new CheckBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("新細明體", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(39, 104);
            comboBox1.Margin = new Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(857, 32);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("新細明體", 24F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label1.Location = new Point(29, 27);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(412, 48);
            label1.TabIndex = 2;
            label1.Text = "Select Output Device";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Font = new Font("新細明體", 24F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button1.Location = new Point(741, 326);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(156, 86);
            button1.TabIndex = 3;
            button1.Text = "Stop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(39, 242);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(857, 30);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("新細明體", 16F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label2.Location = new Point(32, 179);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(230, 32);
            label2.TabIndex = 5;
            label2.Text = "Input Sound URL";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(39, 374);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(362, 30);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("新細明體", 16F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label3.Location = new Point(32, 304);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(318, 32);
            label3.TabIndex = 7;
            label3.Text = "Custom Name (Optional)";
            // 
            // button2
            // 
            button2.Font = new Font("新細明體", 24F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button2.Location = new Point(577, 326);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(156, 86);
            button2.TabIndex = 8;
            button2.Text = "Load";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(39, 463);
            flowLayoutPanel1.Margin = new Padding(4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(858, 719);
            flowLayoutPanel1.TabIndex = 10;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("新細明體", 16F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label4.Location = new Point(428, 326);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(118, 32);
            label4.TabIndex = 11;
            label4.Text = "Volume:";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("新細明體", 16F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label5.Location = new Point(460, 367);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(60, 32);
            label5.TabIndex = 12;
            label5.Text = "null";
            label5.Click += label5_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("新細明體", 16F, FontStyle.Regular, GraphicsUnit.Point, 136);
            linkLabel1.Location = new Point(504, 179);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(322, 32);
            linkLabel1.TabIndex = 13;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Find Sound Effects Here!";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(39, 143);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(262, 27);
            checkBox1.TabIndex = 14;
            checkBox1.TabStop = false;
            checkBox1.Text = "Playback on default device";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.Control;
            label6.Location = new Point(577, 290);
            label6.Name = "label6";
            label6.Size = new Size(233, 23);
            label6.TabIndex = 0;
            label6.Text = "Not playing any sound yet";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(935, 1216);
            Controls.Add(label6);
            Controls.Add(checkBox1);
            Controls.Add(linkLabel1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "Form1";
            Text = "SoundBoard";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private CheckBox checkBox1;
        private Label label6;
    }
}

