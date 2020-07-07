namespace WindowsFormsApp1
{
    partial class BeiXianShuaiHao
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
            System.Windows.Forms.DataVisualization.Charting.Chart chart3;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.route_comboBox3 = new System.Windows.Forms.ComboBox();
            this.光纤链路 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.date1_comboBox1 = new System.Windows.Forms.ComboBox();
            this.date2_comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_beixian = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart3
            // 
            chartArea5.AxisX.MajorGrid.Enabled = false;
            chartArea5.AxisX.Title = "日期";
            chartArea5.AxisY.MajorGrid.Enabled = false;
            chartArea5.AxisY.Title = "光功率/dBm";
            chartArea5.Name = "ChartArea1";
            chart3.ChartAreas.Add(chartArea5);
            chart3.Enabled = false;
            legend5.Name = "Legend1";
            chart3.Legends.Add(legend5);
            chart3.Location = new System.Drawing.Point(427, 126);
            chart3.Name = "chart3";
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Color = System.Drawing.Color.Green;
            series5.IsValueShownAsLabel = true;
            series5.Legend = "Legend1";
            series5.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series5.Name = "光功率预测值";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTimeOffset;
            chart3.Series.Add(series5);
            chart3.Size = new System.Drawing.Size(0, 0);
            chart3.TabIndex = 9;
            chart3.Text = "chart3";
            title5.Name = "备纤衰耗曲线";
            chart3.Titles.Add(title5);
            // 
            // route_comboBox3
            // 
            this.route_comboBox3.FormattingEnabled = true;
            this.route_comboBox3.Location = new System.Drawing.Point(67, 12);
            this.route_comboBox3.Name = "route_comboBox3";
            this.route_comboBox3.Size = new System.Drawing.Size(121, 20);
            this.route_comboBox3.TabIndex = 3;
            this.route_comboBox3.SelectedIndexChanged += new System.EventHandler(this.route_comboBox3_SelectedIndexChanged);
            // 
            // 光纤链路
            // 
            this.光纤链路.AutoSize = true;
            this.光纤链路.Location = new System.Drawing.Point(8, 15);
            this.光纤链路.Name = "光纤链路";
            this.光纤链路.Size = new System.Drawing.Size(53, 12);
            this.光纤链路.TabIndex = 4;
            this.光纤链路.Text = "光纤线路";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择日期";
            // 
            // date1_comboBox1
            // 
            this.date1_comboBox1.FormattingEnabled = true;
            this.date1_comboBox1.Location = new System.Drawing.Point(310, 12);
            this.date1_comboBox1.Name = "date1_comboBox1";
            this.date1_comboBox1.Size = new System.Drawing.Size(121, 20);
            this.date1_comboBox1.TabIndex = 6;
            this.date1_comboBox1.SelectedIndexChanged += new System.EventHandler(this.date1_comboBox1_SelectedIndexChanged);
            // 
            // date2_comboBox1
            // 
            this.date2_comboBox1.FormattingEnabled = true;
            this.date2_comboBox1.Location = new System.Drawing.Point(460, 12);
            this.date2_comboBox1.Name = "date2_comboBox1";
            this.date2_comboBox1.Size = new System.Drawing.Size(121, 20);
            this.date2_comboBox1.TabIndex = 6;
            this.date2_comboBox1.SelectedIndexChanged += new System.EventHandler(this.date2_comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "至";
            // 
            // Btn_beixian
            // 
            this.Btn_beixian.Location = new System.Drawing.Point(610, 10);
            this.Btn_beixian.Name = "Btn_beixian";
            this.Btn_beixian.Size = new System.Drawing.Size(75, 23);
            this.Btn_beixian.TabIndex = 8;
            this.Btn_beixian.Text = "分析";
            this.Btn_beixian.UseVisualStyleBackColor = true;
            this.Btn_beixian.Click += new System.EventHandler(this.Btn_beixian_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "备纤衰耗曲线";
            // 
            // chart1
            // 
            chartArea6.AxisX.MajorGrid.Enabled = false;
            chartArea6.AxisX.Title = "日期";
            chartArea6.AxisY.MajorGrid.Enabled = false;
            chartArea6.AxisY.Title = "衰耗值/dB";
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(12, 136);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "备纤衰耗值";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTimeOffset;
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(721, 469);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            title6.Name = "备纤衰耗曲线";
            this.chart1.Titles.Add(title6);
            // 
            // BeiXianShuaiHao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 617);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label3);
            this.Controls.Add(chart3);
            this.Controls.Add(this.Btn_beixian);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.date2_comboBox1);
            this.Controls.Add(this.date1_comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.光纤链路);
            this.Controls.Add(this.route_comboBox3);
            this.Name = "BeiXianShuaiHao";
            this.Text = "备纤衰耗";
            ((System.ComponentModel.ISupportInitialize)(chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox route_comboBox3;
        private System.Windows.Forms.Label 光纤链路;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox date1_comboBox1;
        private System.Windows.Forms.ComboBox date2_comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_beixian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
