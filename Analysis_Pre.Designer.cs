namespace WindowsFormsApp1
{
    partial class Analysis_Pre
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label1 = new System.Windows.Forms.Label();
            this.光纤链路 = new System.Windows.Forms.Label();
            this.route_comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.date1_comboBox = new System.Windows.Forms.ComboBox();
            this.date2_comboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.线路名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.光功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guanggonglv_dataGridView = new System.Windows.Forms.DataGridView();
            this.线路名称1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.光功率1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataSet1 = new System.Data.DataSet();
            this.rb3_all = new System.Windows.Forms.RadioButton();
            this.rb_special = new System.Windows.Forms.RadioButton();
            this.zhanshi_title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.guanggonglv_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // 光纤链路
            // 
            this.光纤链路.AutoSize = true;
            this.光纤链路.Location = new System.Drawing.Point(24, 9);
            this.光纤链路.Name = "光纤链路";
            this.光纤链路.Size = new System.Drawing.Size(53, 12);
            this.光纤链路.TabIndex = 1;
            this.光纤链路.Text = "光纤链路";
            // 
            // route_comboBox1
            // 
            this.route_comboBox1.FormattingEnabled = true;
            this.route_comboBox1.Location = new System.Drawing.Point(83, 6);
            this.route_comboBox1.Name = "route_comboBox1";
            this.route_comboBox1.Size = new System.Drawing.Size(121, 20);
            this.route_comboBox1.TabIndex = 2;
            this.route_comboBox1.SelectedIndexChanged += new System.EventHandler(this.Route_comboBox1_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(551, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "至";
            // 
            // date1_comboBox
            // 
            this.date1_comboBox.FormattingEnabled = true;
            this.date1_comboBox.Location = new System.Drawing.Point(405, 12);
            this.date1_comboBox.Name = "date1_comboBox";
            this.date1_comboBox.Size = new System.Drawing.Size(121, 20);
            this.date1_comboBox.TabIndex = 2;
            this.date1_comboBox.SelectedIndexChanged += new System.EventHandler(this.Date1_comboBox_SelectedIndexChanged);
            // 
            // date2_comboBox
            // 
            this.date2_comboBox.FormattingEnabled = true;
            this.date2_comboBox.Location = new System.Drawing.Point(588, 12);
            this.date2_comboBox.Name = "date2_comboBox";
            this.date2_comboBox.Size = new System.Drawing.Size(121, 20);
            this.date2_comboBox.TabIndex = 2;
            this.date2_comboBox.SelectedIndexChanged += new System.EventHandler(this.Date2_comboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "预测";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.CursorChanged += new System.EventHandler(this.Button1_CursorChanged);
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // 线路名称
            // 
            this.线路名称.HeaderText = "Column1";
            this.线路名称.Name = "线路名称";
            // 
            // 日期
            // 
            this.日期.HeaderText = "Column1";
            this.日期.Name = "日期";
            // 
            // 光功率
            // 
            this.光功率.HeaderText = "Column1";
            this.光功率.Name = "光功率";
            // 
            // guanggonglv_dataGridView
            // 
            this.guanggonglv_dataGridView.AllowUserToAddRows = false;
            this.guanggonglv_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.guanggonglv_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.guanggonglv_dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.guanggonglv_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guanggonglv_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.线路名称1,
            this.日期1,
            this.光功率1});
            this.guanggonglv_dataGridView.Location = new System.Drawing.Point(843, 129);
            this.guanggonglv_dataGridView.Name = "guanggonglv_dataGridView";
            this.guanggonglv_dataGridView.RowTemplate.Height = 23;
            this.guanggonglv_dataGridView.Size = new System.Drawing.Size(394, 363);
            this.guanggonglv_dataGridView.TabIndex = 5;
            // 
            // 线路名称1
            // 
            this.线路名称1.HeaderText = "线路名称";
            this.线路名称1.Name = "线路名称1";
            // 
            // 日期1
            // 
            this.日期1.HeaderText = "日期";
            this.日期1.Name = "日期1";
            // 
            // 光功率1
            // 
            this.光功率1.HeaderText = "光功率";
            this.光功率1.Name = "光功率1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "光功率预测曲线";
            // 
            // chart1
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Title = "日期";
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.Title = "光功率/dBm";
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(26, 129);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Green;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Name = "光功率预测值";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTimeOffset;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(721, 469);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            title1.Name = "光功率预测曲线";
            this.chart1.Titles.Add(title1);
            this.chart1.Click += new System.EventHandler(this.Chart1_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // rb3_all
            // 
            this.rb3_all.AutoSize = true;
            this.rb3_all.Location = new System.Drawing.Point(270, 7);
            this.rb3_all.Name = "rb3_all";
            this.rb3_all.Size = new System.Drawing.Size(83, 16);
            this.rb3_all.TabIndex = 13;
            this.rb3_all.TabStop = true;
            this.rb3_all.Text = "全部时间段";
            this.rb3_all.UseVisualStyleBackColor = true;
            this.rb3_all.CheckedChanged += new System.EventHandler(this.Rb3_all_CheckedChanged);
            // 
            // rb_special
            // 
            this.rb_special.AutoSize = true;
            this.rb_special.Location = new System.Drawing.Point(270, 29);
            this.rb_special.Name = "rb_special";
            this.rb_special.Size = new System.Drawing.Size(95, 16);
            this.rb_special.TabIndex = 14;
            this.rb_special.TabStop = true;
            this.rb_special.Text = "选中的时间段";
            this.rb_special.UseVisualStyleBackColor = true;
            this.rb_special.CheckedChanged += new System.EventHandler(this.Rb_special_CheckedChanged);
            // 
            // zhanshi_title
            // 
            this.zhanshi_title.AutoSize = true;
            this.zhanshi_title.Location = new System.Drawing.Point(841, 99);
            this.zhanshi_title.Name = "zhanshi_title";
            this.zhanshi_title.Size = new System.Drawing.Size(113, 12);
            this.zhanshi_title.TabIndex = 15;
            this.zhanshi_title.Text = "光功率预测信息展示";
            this.zhanshi_title.Visible = false;
            // 
            // Analysis_Pre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 617);
            this.Controls.Add(this.zhanshi_title);
            this.Controls.Add(this.rb_special);
            this.Controls.Add(this.rb3_all);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.guanggonglv_dataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.date2_comboBox);
            this.Controls.Add(this.date1_comboBox);
            this.Controls.Add(this.route_comboBox1);
            this.Controls.Add(this.光纤链路);
            this.Controls.Add(this.label1);
            this.Name = "Analysis_Pre";
            this.Text = "光功率分析与预测";
            this.Load += new System.EventHandler(this.Analysis_Pre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guanggonglv_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label 光纤链路;
        private System.Windows.Forms.ComboBox route_comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox date1_comboBox;
        private System.Windows.Forms.ComboBox date2_comboBox;
        private System.Windows.Forms.Button button1;
        
        private System.Windows.Forms.DataGridViewTextBoxColumn 线路名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 光功率;
        private System.Windows.Forms.DataGridView guanggonglv_dataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.RadioButton rb3_all;
        private System.Windows.Forms.RadioButton rb_special;
        private System.Windows.Forms.Label zhanshi_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线路名称1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 光功率1;
    }
}
