namespace WindowsFormsApp1
{
    partial class Frm_Child
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.备纤衰耗分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.纤芯数录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备纤衰耗量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备纤使用率分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.纤芯总数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备纤剩余量分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.测试JS = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.备纤衰耗分析ToolStripMenuItem,
            this.备纤使用率分析ToolStripMenuItem,
            this.备纤剩余量分析ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(866, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 备纤衰耗分析ToolStripMenuItem
            // 
            this.备纤衰耗分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.纤芯数录入ToolStripMenuItem,
            this.备纤衰耗量ToolStripMenuItem});
            this.备纤衰耗分析ToolStripMenuItem.Name = "备纤衰耗分析ToolStripMenuItem";
            this.备纤衰耗分析ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.备纤衰耗分析ToolStripMenuItem.Text = "备纤衰耗分析";
            this.备纤衰耗分析ToolStripMenuItem.Click += new System.EventHandler(this.备纤衰耗分析ToolStripMenuItem_Click);
            // 
            // 纤芯数录入ToolStripMenuItem
            // 
            this.纤芯数录入ToolStripMenuItem.Name = "纤芯数录入ToolStripMenuItem";
            this.纤芯数录入ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.纤芯数录入ToolStripMenuItem.Text = "纤芯数录入";
            // 
            // 备纤衰耗量ToolStripMenuItem
            // 
            this.备纤衰耗量ToolStripMenuItem.Name = "备纤衰耗量ToolStripMenuItem";
            this.备纤衰耗量ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.备纤衰耗量ToolStripMenuItem.Text = "备纤衰耗量";
            // 
            // 备纤使用率分析ToolStripMenuItem
            // 
            this.备纤使用率分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.纤芯总数ToolStripMenuItem});
            this.备纤使用率分析ToolStripMenuItem.Name = "备纤使用率分析ToolStripMenuItem";
            this.备纤使用率分析ToolStripMenuItem.Size = new System.Drawing.Size(104, 21);
            this.备纤使用率分析ToolStripMenuItem.Text = "备纤使用率分析";
            // 
            // 纤芯总数ToolStripMenuItem
            // 
            this.纤芯总数ToolStripMenuItem.Name = "纤芯总数ToolStripMenuItem";
            this.纤芯总数ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.纤芯总数ToolStripMenuItem.Text = "纤芯总数";
            // 
            // 备纤剩余量分析ToolStripMenuItem
            // 
            this.备纤剩余量分析ToolStripMenuItem.Name = "备纤剩余量分析ToolStripMenuItem";
            this.备纤剩余量分析ToolStripMenuItem.Size = new System.Drawing.Size(104, 21);
            this.备纤剩余量分析ToolStripMenuItem.Text = "备纤剩余量分析";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(102, 28);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(759, 549);
            this.webBrowser1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "加载列表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // 测试JS
            // 
            this.测试JS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.测试JS.Location = new System.Drawing.Point(4, 85);
            this.测试JS.Name = "测试JS";
            this.测试JS.Size = new System.Drawing.Size(75, 23);
            this.测试JS.TabIndex = 6;
            this.测试JS.Text = "测试JS";
            this.测试JS.UseVisualStyleBackColor = true;
            this.测试JS.Click += new System.EventHandler(this.Button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 114);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "JS访问";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click_1);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(4, 170);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(4, 197);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "双环";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(4, 255);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "性别";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(4, 226);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "份额";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Frm_Child
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 579);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.测试JS);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_Child";
            this.Text = "光缆智能分析";
            this.Load += new System.EventHandler(this.Frm_Child_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 备纤衰耗分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 纤芯数录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备纤衰耗量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备纤使用率分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 纤芯总数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备纤剩余量分析ToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button 测试JS;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}