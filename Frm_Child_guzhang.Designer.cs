namespace WindowsFormsApp1
{
    partial class Frm_Child_guzhang
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendCellColumn legendCellColumn1 = new System.Windows.Forms.DataVisualization.Charting.LegendCellColumn();
            System.Windows.Forms.DataVisualization.Charting.LegendCellColumn legendCellColumn2 = new System.Windows.Forms.DataVisualization.Charting.LegendCellColumn();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.备纤衰耗分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.纤芯数录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备纤衰耗量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.故障统计分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleroute = new System.Windows.Forms.ToolStripMenuItem();
            this.allroute = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.year_comboBox1 = new System.Windows.Forms.ComboBox();
            this.DataAnalysis = new System.Windows.Forms.Button();
            this.SelectedYear = new System.Windows.Forms.Label();
            this.route_comboBox1 = new System.Windows.Forms.ComboBox();
            this.selectedRoute = new System.Windows.Forms.Label();
            this.guzhang_bar_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guzhang_pie_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rbtnBar = new System.Windows.Forms.RadioButton();
            this.rbtnPie = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guzhang_bar_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guzhang_pie_chart)).BeginInit();
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
            this.故障统计分析ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1256, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 备纤衰耗分析ToolStripMenuItem
            // 
            this.备纤衰耗分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.纤芯数录入ToolStripMenuItem,
            this.备纤衰耗量ToolStripMenuItem});
            this.备纤衰耗分析ToolStripMenuItem.Name = "备纤衰耗分析ToolStripMenuItem";
            this.备纤衰耗分析ToolStripMenuItem.Size = new System.Drawing.Size(104, 21);
            this.备纤衰耗分析ToolStripMenuItem.Text = "空芯率统计分析";
            this.备纤衰耗分析ToolStripMenuItem.Click += new System.EventHandler(this.备纤衰耗分析ToolStripMenuItem_Click);
            // 
            // 纤芯数录入ToolStripMenuItem
            // 
            this.纤芯数录入ToolStripMenuItem.Name = "纤芯数录入ToolStripMenuItem";
            this.纤芯数录入ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.纤芯数录入ToolStripMenuItem.Text = "纤芯数录入";
            this.纤芯数录入ToolStripMenuItem.Click += new System.EventHandler(this.纤芯数录入ToolStripMenuItem_Click);
            // 
            // 备纤衰耗量ToolStripMenuItem
            // 
            this.备纤衰耗量ToolStripMenuItem.Name = "备纤衰耗量ToolStripMenuItem";
            this.备纤衰耗量ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.备纤衰耗量ToolStripMenuItem.Text = "备纤衰耗量";
            // 
            // 故障统计分析ToolStripMenuItem
            // 
            this.故障统计分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleroute,
            this.allroute});
            this.故障统计分析ToolStripMenuItem.Name = "故障统计分析ToolStripMenuItem";
            this.故障统计分析ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.故障统计分析ToolStripMenuItem.Text = "故障统计分析";
            this.故障统计分析ToolStripMenuItem.Click += new System.EventHandler(this.故障统计分析ToolStripMenuItem_Click);
            // 
            // singleroute
            // 
            this.singleroute.Name = "singleroute";
            this.singleroute.Size = new System.Drawing.Size(124, 22);
            this.singleroute.Text = "单条线路";
            this.singleroute.Click += new System.EventHandler(this.Piechart_Click);
            // 
            // allroute
            // 
            this.allroute.Name = "allroute";
            this.allroute.Size = new System.Drawing.Size(124, 22);
            this.allroute.Text = "全部线路";
            this.allroute.Click += new System.EventHandler(this.Barchart_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.year_comboBox1);
            this.panel1.Controls.Add(this.DataAnalysis);
            this.panel1.Controls.Add(this.SelectedYear);
            this.panel1.Controls.Add(this.route_comboBox1);
            this.panel1.Controls.Add(this.selectedRoute);
            this.panel1.Controls.Add(this.guzhang_bar_chart);
            this.panel1.Controls.Add(this.guzhang_pie_chart);
            this.panel1.Controls.Add(this.rbtnBar);
            this.panel1.Controls.Add(this.rbtnPie);
            this.panel1.Location = new System.Drawing.Point(27, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 656);
            this.panel1.TabIndex = 13;
            // 
            // year_comboBox1
            // 
            this.year_comboBox1.FormattingEnabled = true;
            this.year_comboBox1.Location = new System.Drawing.Point(455, 17);
            this.year_comboBox1.Name = "year_comboBox1";
            this.year_comboBox1.Size = new System.Drawing.Size(121, 20);
            this.year_comboBox1.TabIndex = 20;
            this.year_comboBox1.SelectedIndexChanged += new System.EventHandler(this.Year_comboBox1_SelectedIndexChanged);
            // 
            // DataAnalysis
            // 
            this.DataAnalysis.Location = new System.Drawing.Point(833, 14);
            this.DataAnalysis.Name = "DataAnalysis";
            this.DataAnalysis.Size = new System.Drawing.Size(75, 23);
            this.DataAnalysis.TabIndex = 18;
            this.DataAnalysis.Text = "分析";
            this.DataAnalysis.UseVisualStyleBackColor = true;
            this.DataAnalysis.Click += new System.EventHandler(this.DataAnalysis_Click);
            // 
            // SelectedYear
            // 
            this.SelectedYear.AutoSize = true;
            this.SelectedYear.Location = new System.Drawing.Point(398, 21);
            this.SelectedYear.Name = "SelectedYear";
            this.SelectedYear.Size = new System.Drawing.Size(65, 12);
            this.SelectedYear.TabIndex = 16;
            this.SelectedYear.Text = "选择年份：";
            // 
            // route_comboBox1
            // 
            this.route_comboBox1.FormattingEnabled = true;
            this.route_comboBox1.Location = new System.Drawing.Point(266, 17);
            this.route_comboBox1.Name = "route_comboBox1";
            this.route_comboBox1.Size = new System.Drawing.Size(121, 20);
            this.route_comboBox1.TabIndex = 17;
            this.route_comboBox1.SelectedIndexChanged += new System.EventHandler(this.Route_comboBox1_SelectedIndexChanged);
            // 
            // selectedRoute
            // 
            this.selectedRoute.AutoSize = true;
            this.selectedRoute.Location = new System.Drawing.Point(195, 21);
            this.selectedRoute.Name = "selectedRoute";
            this.selectedRoute.Size = new System.Drawing.Size(65, 12);
            this.selectedRoute.TabIndex = 16;
            this.selectedRoute.Text = "选择线路：";
            // 
            // guzhang_bar_chart
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Maximum = 12D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "月份";
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.Title = "故障次数";
            chartArea1.Name = "ChartArea1";
            this.guzhang_bar_chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.guzhang_bar_chart.Legends.Add(legend1);
            this.guzhang_bar_chart.Location = new System.Drawing.Point(72, 80);
            this.guzhang_bar_chart.Name = "guzhang_bar_chart";
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "断裂";
            series2.ChartArea = "ChartArea1";
            series2.IsValueShownAsLabel = true;
            series2.Legend = "Legend1";
            series2.Name = "拉伸";
            series3.ChartArea = "ChartArea1";
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.Name = "接头盒进水";
            series4.ChartArea = "ChartArea1";
            series4.IsValueShownAsLabel = true;
            series4.Legend = "Legend1";
            series4.Name = "异常";
            series5.ChartArea = "ChartArea1";
            series5.IsValueShownAsLabel = true;
            series5.Legend = "Legend1";
            series5.Name = "其他";
            this.guzhang_bar_chart.Series.Add(series1);
            this.guzhang_bar_chart.Series.Add(series2);
            this.guzhang_bar_chart.Series.Add(series3);
            this.guzhang_bar_chart.Series.Add(series4);
            this.guzhang_bar_chart.Series.Add(series5);
            this.guzhang_bar_chart.Size = new System.Drawing.Size(816, 532);
            this.guzhang_bar_chart.TabIndex = 14;
            this.guzhang_bar_chart.Text = "guzhang_bar_chart";
            title1.Name = "线路故障统计信息";
            this.guzhang_bar_chart.Titles.Add(title1);
            // 
            // guzhang_pie_chart
            // 
            chartArea2.Name = "ChartArea1";
            this.guzhang_pie_chart.ChartAreas.Add(chartArea2);
            legendCellColumn1.ColumnType = System.Windows.Forms.DataVisualization.Charting.LegendCellColumnType.SeriesSymbol;
            legendCellColumn1.Name = "Column1";
            legendCellColumn2.Name = "Column2";
            legendCellColumn2.Text = "#VALX";
            legend2.CellColumns.Add(legendCellColumn1);
            legend2.CellColumns.Add(legendCellColumn2);
            legend2.Name = "Legend1";
            this.guzhang_pie_chart.Legends.Add(legend2);
            this.guzhang_pie_chart.Location = new System.Drawing.Point(72, 80);
            this.guzhang_pie_chart.Name = "guzhang_pie_chart";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series6.IsValueShownAsLabel = true;
            series6.Label = "#VALX ：#VAL{P2}";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.guzhang_pie_chart.Series.Add(series6);
            this.guzhang_pie_chart.Size = new System.Drawing.Size(804, 532);
            this.guzhang_pie_chart.TabIndex = 14;
            this.guzhang_pie_chart.Text = "guzhang_pie_chart";
            // 
            // rbtnBar
            // 
            this.rbtnBar.AutoSize = true;
            this.rbtnBar.Location = new System.Drawing.Point(721, 19);
            this.rbtnBar.Name = "rbtnBar";
            this.rbtnBar.Size = new System.Drawing.Size(95, 16);
            this.rbtnBar.TabIndex = 1;
            this.rbtnBar.TabStop = true;
            this.rbtnBar.Text = "月份故障统计";
            this.rbtnBar.UseVisualStyleBackColor = true;
            this.rbtnBar.CheckedChanged += new System.EventHandler(this.RbtnBar_CheckedChanged);
            // 
            // rbtnPie
            // 
            this.rbtnPie.AutoSize = true;
            this.rbtnPie.Location = new System.Drawing.Point(596, 19);
            this.rbtnPie.Name = "rbtnPie";
            this.rbtnPie.Size = new System.Drawing.Size(119, 16);
            this.rbtnPie.TabIndex = 0;
            this.rbtnPie.TabStop = true;
            this.rbtnPie.Text = "年份故障类型占比";
            this.rbtnPie.UseVisualStyleBackColor = true;
            this.rbtnPie.CheckedChanged += new System.EventHandler(this.RbtnPie_CheckedChanged);
            // 
            // Frm_Child_guzhang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 617);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_Child_guzhang";
            this.Text = "光缆智能分析";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Child_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Child_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guzhang_bar_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guzhang_pie_chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 备纤衰耗分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 纤芯数录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备纤衰耗量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 故障统计分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleroute;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnBar;
        private System.Windows.Forms.RadioButton rbtnPie;
        private System.Windows.Forms.DataVisualization.Charting.Chart guzhang_pie_chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart guzhang_bar_chart;
        private System.Windows.Forms.ToolStripMenuItem allroute;
        private System.Windows.Forms.Label selectedRoute;
        private System.Windows.Forms.ComboBox route_comboBox1;
        private System.Windows.Forms.Button DataAnalysis;
        private System.Windows.Forms.Label SelectedYear;
        private System.Windows.Forms.ComboBox year_comboBox1;
    }
}
