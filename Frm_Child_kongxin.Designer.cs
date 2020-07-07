namespace WindowsFormsApp1
{
    partial class Frm_Child_kongxin
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_routenum = new System.Windows.Forms.RadioButton();
            this.btn_count = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txt2_range = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt1_range = new System.Windows.Forms.TextBox();
            this.btn_fenxi = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart2);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Location = new System.Drawing.Point(18, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 656);
            this.panel1.TabIndex = 1;
            // 
            // chart2
            // 
            chartArea1.AxisY.Title = "条";
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(72, 80);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(816, 532);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "饼状图";
            // 
            // chart1
            // 
            this.chart1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Angle = -60;
            chartArea2.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Stacked;
            chartArea2.AxisY.Title = "条数";
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(72, 80);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.IsValueShownAsLabel = true;
            series2.Legend = "Legend1";
            series2.Name = "空芯数";
            series3.ChartArea = "ChartArea1";
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.Name = "纤芯数";
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(816, 532);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btn_routenum
            // 
            this.btn_routenum.AutoSize = true;
            this.btn_routenum.Location = new System.Drawing.Point(18, 12);
            this.btn_routenum.Name = "btn_routenum";
            this.btn_routenum.Size = new System.Drawing.Size(95, 16);
            this.btn_routenum.TabIndex = 3;
            this.btn_routenum.TabStop = true;
            this.btn_routenum.Text = "线路数量统计";
            this.btn_routenum.UseVisualStyleBackColor = true;
            this.btn_routenum.CheckedChanged += new System.EventHandler(this.Btn_routenum_CheckedChanged);
            // 
            // btn_count
            // 
            this.btn_count.AutoSize = true;
            this.btn_count.Location = new System.Drawing.Point(132, 12);
            this.btn_count.Name = "btn_count";
            this.btn_count.Size = new System.Drawing.Size(95, 16);
            this.btn_count.TabIndex = 4;
            this.btn_count.TabStop = true;
            this.btn_count.Text = "线路负载统计";
            this.btn_count.UseVisualStyleBackColor = true;
            this.btn_count.CheckedChanged += new System.EventHandler(this.Btn_count_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "至";
            // 
            // txt2_range
            // 
            this.txt2_range.Location = new System.Drawing.Point(464, 12);
            this.txt2_range.Name = "txt2_range";
            this.txt2_range.Size = new System.Drawing.Size(100, 21);
            this.txt2_range.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "请输入空芯率范围";
            // 
            // txt1_range
            // 
            this.txt1_range.Location = new System.Drawing.Point(335, 12);
            this.txt1_range.Name = "txt1_range";
            this.txt1_range.Size = new System.Drawing.Size(100, 21);
            this.txt1_range.TabIndex = 10;
            // 
            // btn_fenxi
            // 
            this.btn_fenxi.Location = new System.Drawing.Point(587, 12);
            this.btn_fenxi.Name = "btn_fenxi";
            this.btn_fenxi.Size = new System.Drawing.Size(75, 23);
            this.btn_fenxi.TabIndex = 9;
            this.btn_fenxi.Text = "分析";
            this.btn_fenxi.UseVisualStyleBackColor = true;
            this.btn_fenxi.Click += new System.EventHandler(this.Btn_fenxi_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(714, 15);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 16);
            this.radioButton3.TabIndex = 14;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "饼状图";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // Frm_Child3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 617);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt2_range);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt1_range);
            this.Controls.Add(this.btn_fenxi);
            this.Controls.Add(this.btn_count);
            this.Controls.Add(this.btn_routenum);
            this.Controls.Add(this.panel1);
            this.Name = "Frm_Child3";
            this.Text = "空芯率统计分析";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RadioButton btn_routenum;
        private System.Windows.Forms.RadioButton btn_count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt2_range;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt1_range;
        private System.Windows.Forms.Button btn_fenxi;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}