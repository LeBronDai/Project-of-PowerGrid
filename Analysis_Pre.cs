using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

using System.Windows.Forms;
using fitting_work;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Demo.WindowsForms.CustomMarkers;
using System.Reflection;
using MySql.Data.MySqlClient;
using WindowsFormsApp1.工具类;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility; 
using WindowsFormsApp1.数据实体类;
using huatuTest;
using ydpj_yuce;

namespace WindowsFormsApp1
{
    public partial class Analysis_Pre : Form
    {

        string rn = null;
        string guangyuce=null;
        DateTime[] x = new DateTime[50];//时间数组，作为x轴
        DateTime start;
        DateTime end;
        double[] y = new double[50]; //功率数组，作为y轴
        double[] y1 = new double[50];//预测转换数组；
        string[] x1 = new string[50];//转换x数组
        double yucezhi;//预测值
        DateTime time1;//选择第一个时间点
        DateTime time2;//选择第二个时间点
        int rqcz = 0;//日期差值，用于预测部分
        /// <summary>
        /// 分析预测初始化界面
        /// </summary>
        public Analysis_Pre()
        {
            InitializeComponent();
# region 注释
            //#region 绑定数据源显示使用到的数据和预测点值,用两个线段画的曲线
            //Series ser = new Series();// 实例化序列图对象
            //chart1.Series.Add(ser);//向chart1中添加序列对象
            //for (int i = 0; i < 10; i++)
            //{
            //    ser.Points.AddXY(DateTime.Now.Date.AddDays(i + 1), outRes[i]);
            //}

            //chart1.Series[0].ChartType = SeriesChartType.Spline; //第一条样条图类型
            //ChartArea chartArea = chart1.ChartAreas[0];
            //chart1.ChartAreas[0].AxisX.Interval = 1;   //轴间距为1
            //chart1.ChartAreas[0].AxisX.IntervalOffset = 1;//偏移量为1
            //chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;//决定是否标签待遇偏移量

            //chartArea.AxisY.Minimum = Min(outRes) - 0.001;  //设置y轴的最小值
            //chartArea.AxisY.Maximum = Max(outRes) + 0.001;

            ////label5.Visible = true;
            //int value = (date1.Value.Date - DateTime.Now.Date.AddDays(1)).Days;//差值

            //Series ser1 = new Series();//第二个序列
            //ser1.Color = Color.Red;//红色
            //chart1.Series.Add(ser1);//chart中添加第二个序列
            //for (int i = 0; i < TK; i++)
            //{
            //    ser1.Points.AddXY(date1.Value.Date.AddDays(i), outRes[value + i]);//添加指定的对象到集合的末尾
            //}
            //chart1.Series[1].ChartType = SeriesChartType.Spline;//图标类型样条图类型
            //ChartArea chartArea1 = chart1.ChartAreas[0];
            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
            //chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            //#endregion
 

            //#region 调用并获取预测点结果
            //double[] y1 = new double[90];
            //y1 = a;
            //int day = DateTime.Now.Day;
            //float Nday = 10;//时间周期为10天

            //MWNumericArray yy1 = y1;
            //MWNumericArray xx1 = Nday;

            //fitting_work.Class1 myZXEC = new fitting_work.Class1();

            //MWArray result = myZXEC.fittingFunction1(yy1, xx1);//fittingFunction1调用
            //MWNumericArray temp = (MWNumericArray)result;//强制转换成MWNumericArray

            //mm = temp.ToArray(MWArrayComponent.Real); //temp转化成Array类型的mm对象
            //double[] outRes = new double[mm.Length];  //计算mm的长度作为输出的个数
            //for (int i = 0; i < mm.Length; i++)
            //{
            //    string kk = string.Format("{0:N4}", mm.GetValue(0, i));//?
            //    outRes[i] = Convert.ToDouble(kk);

            //    //outRes[i] = (float)mm.GetValue(0, i);
            //}
           #endregion
            try
            {

                #region 得到光纤链路表
                DBlink db = new DBlink();
                if (db.DBcon())  //填充Pname 数组 删除后显示combox1.text
                {
                    db.Get_route1();//得到光纤链路名称
                }
                db.DBclose();

                // 已有节点显示下拉列表3.Items.Clear();//清空再刷新
                route_comboBox1.Items.Clear();
                for (int i = 0; i < route.route_list.Count; i++)
                    route_comboBox1.Items.Add(route.route_list[i].ToString());
                //   已有节点显示下拉列表3.Items.Add(point_data.pname[i].ToString());
                #endregion
                

                //if (db.DBcon())  //填充日期
                //{
                //    db.Get_date(rn);//得到某一线路光功率表日期信息
                //}
                //db.DBclose();
                //date1_comboBox.Items.Clear();//清空日期再刷新
                //for (int i = 0; i < date.dateset.Count; i++)
                //    date1_comboBox.Items.Add(date.dateset[i].ToString());
                //// 已有节点显示下拉列表3.Items.Clear();//清空再刷新
                //route_comboBox1.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            
        }
        
       /// <summary>
       /// 展示光功率函数
       /// </summary>
        private void show_now_gglv_dataGridView1(DateTime time1,DateTime time2)//展示现有光功率 
        {
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                //db.Get_dataGridView3_gonglv_data("select rn,date(date) as date,optical_power from optical_power");//光功率表中选择数据
                db.Get_dataGridView3_gonglv_data("select rn,date(date) as date,optical_power from optical_power where date >='"+time1+"'"+"and date <='"+time2+"'and rn='"+ rn+"';");
            }
            db.DBclose();
            guanggonglv_dataGridView.Rows.Clear();// 光功率展示
            for (int i = 0; i <= date.rn.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.guanggonglv_dataGridView.Rows.Add();//索引递加
                if (i < date.rn.Count)
                {
                    this.guanggonglv_dataGridView.Rows[index].Cells[0].Value = date.rn[i];        //线路名
                    this.guanggonglv_dataGridView.Rows[index].Cells[1].Value = date.dateset[i].Date;      //线路日期
                    this.guanggonglv_dataGridView.Rows[index].Cells[2].Value = date.gglv[i];      //光功率
                }
                if(i==date.rn.Count)
                {

                    this.guanggonglv_dataGridView.Rows[index].Cells[0].Value = date.rn[i-1];        //线路名
                    this.guanggonglv_dataGridView.Rows[index].Cells[1].Value = date.dateset[i - 1].AddDays(1).Date;      //线路日期
                    this.guanggonglv_dataGridView.Rows[index].Cells[2].Value = yucezhi;      //光功率
                }
            }
        
        }

      
        private void Route_comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
            try
            {
                date1_comboBox.Items.Clear();
                date2_comboBox.Items.Clear();
                DBlink db = new DBlink();
                rn = route_comboBox1.Text;

                if (db.DBcon())  //填充日期
                {
                    db.Get_date(rn);//得到某一线路光功率表日期信息
                }
                db.DBclose();
                date1_comboBox.Items.Clear();//清空日期再刷新
                date2_comboBox.Items.Clear();
                for (int i = 0; i < date.dateset.Count; i++)
                {
                    date1_comboBox.Items.Add(date.dateset[i]);
                    date2_comboBox.Items.Add(date.dateset[i]);
                }
                // 已有节点显示下拉列表3.Items.Clear();//清空再刷新

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        
        /// <summary>
        /// 预测功能，首先选定线路，再选全部时间段还是局部时间段，选定日期区间在需在7日以上
        /// 能够完成预测功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click_1(object sender, EventArgs e)//光功率预测
        {
            this.Cursor = Cursors.WaitCursor;//鼠标进入等待状态
            chart1.Visible = true;
            guanggonglv_dataGridView.Visible = true;
            zhanshi_title.Visible = true;
            label2.Visible = true;
            try
            {
                chart1.Series.Clear();//每次输入之后清空
                Series ser = new Series(rn);// 实例化序列图对象
                ser.IsValueShownAsLabel = true;//显示每个节点信息
               
                chart1.Series.Add(ser);//向chart1中添加序列对象
                zhanshi_title.Visible = true;
                if (rb_special.Checked)//特定时间被选中
                {
                    #region 数据库连接操作,得到日期差值和光功率数值
                    DBlink db1 = new DBlink();
                    if (db1.DBcon())
                    {
                        db1.Get_datediff(time2,time1); //输入线路名，得到对应的光功率和日期
                    }
                    db1.DBclose();
                    DBlink db = new DBlink();
                    if (db.DBcon())
                    {
                        db.Get_gonglv1(rn,time1,time2);//输入线路名，起止时间得到对应的光功率和日期
                    }
                    db.DBclose();
                    #endregion
                    rqcz = date.rqcz[0]+1;//得到选中的日期差值 比如10月20日与10月19日差值为1 但是2天所以差值应该加一
                    if (rqcz <= 1)
                    {
                        MessageBox.Show("日期选择有误请重新选择!", "!");
                    }

                    if (rqcz < 4)
                    {
                        MessageBox.Show("日期区间选择过短，不能预测，请选择大于4天!", "日期选择问题!");

                    }
                    else
                    {
                        for (int i = 0; i < rqcz; i++)//统计日期的个数
                        {
                            x[i] = (DateTime)date.dateset[i];//日期
                            y[i] = date.gglv[i];//将光功率赋给y轴
                        }
                        //预测部分 得到预测的光功率值
                        int Nday = rqcz;//时间周期为10天
                        ydpj_yuce.yuce myyuce = new yuce();
                        MWNumericArray yy1 = y;
                        MWNumericArray xx1 = Nday;
                        MWArray result = myyuce.ydpj_yuce(yy1, xx1);
                        MWNumericArray temp = (MWNumericArray)result;//强制转换成MWNumericArray
                        Array mm = temp.ToArray(MWArrayComponent.Real); //temp转化成Array类型的mm对象
                        double[] outRes = new double[mm.Length];  //计算mm的长度作为输出的个数
                        for (int i = 0; i < mm.Length; i++)
                        {
                            guangyuce = string.Format("{0:N4}", mm.GetValue(0, i));//?
                            outRes[i] = Convert.ToDouble(guangyuce);        
                        }
                        yucezhi = outRes[0];//得到预测的光功率值

                        //绘制预测曲线
                        for (int i = 0; i < rqcz+1; i++)//统计日期的个数
                        {
                            if (i < rqcz)//将当前的选择的数据存储
                            {
                                x[i] = (DateTime)date.dateset[i];
                                y[i] = date.gglv[i];//将光功率赋给y轴
                            }
                            else//预测的数据，主要要时间
                            {
                                x[i] = (DateTime)date.dateset[i-1].AddDays(1);
                                y[i] = yucezhi;
                            }
                        }
                        for (int i = 0; i < rqcz+1; i++)//rqcz=7 x[0-6] x[7] y[7]预测
                        {
                            ser.Points.AddXY(x[i], y[i]);
                        }//向xy轴添加元素}
                        show_now_gglv_dataGridView1(time1,time2);//展示光功率;
                                                      ///图样式设计
                        chart1.Series[0].ChartType = SeriesChartType.Spline; //第一条样条图类型 
                        chart1.Series[0].Color = Color.Red;//颜色设为红色局部
                        chart1.Titles[0].Text = string.Format("{0}光功率显示", rn); //设置标题
                        chart1.Titles[0].ForeColor = Color.RoyalBlue;
                        MessageBox.Show(x[rqcz]+"预测的光功率值为：" + guangyuce, "当前所选区间预测值");
                        #region 调用并获取预测点结果
                    }
                    #endregion
                }

                ///<summary>
                ///所有时间段的光功率
                ///</summary>
                if (rb3_all.Checked)
                {
                    DBlink db = new DBlink();
                    if (db.DBcon())
                    {
                        db.Get_gonglv(rn);//输入线路名，得到对应的光功率和日期
                    }
                    db.DBclose();
                    //所有日期及光功率
                    for (int i = 0; i < date.dateset.Count; i++)//统计日期的个数
                        {
                            x[i] = (DateTime)date.dateset[i];
                            y[i] = date.gglv[i];//将光功率赋给y轴
                            start=x[0];
                            end = x[date.dateset.Count - 1];
                        }
                    date1_comboBox.Text = Convert.ToString(start.Date);
                    date2_comboBox.Text = Convert.ToString(end.Date);

                    //预测部分
                    int Nday = date.dateset.Count;//时间全部的日子
                   
                    if (Nday < 4)
                    {
                        MessageBox.Show("当前日期区间小于4天，不能预测!", "日期选择问题!");

                    }
                    else
                    {
                        ydpj_yuce.yuce myyuce1 = new yuce();
                        MWNumericArray yy1 = y;
                        MWNumericArray xx1 = Nday;
                        MWArray result = myyuce1.ydpj_yuce(yy1, xx1);
                        MWNumericArray temp = (MWNumericArray)result;//强制转换成MWNumericArray
                        Array mm = temp.ToArray(MWArrayComponent.Real); //temp转化成Array类型的mm对象
                        double[] outRes = new double[mm.Length];  //计算mm的长度作为输出的个数
                        for (int i = 0; i < mm.Length; i++)
                        {
                            guangyuce = string.Format("{0:N4}", mm.GetValue(0, i));//?
                            outRes[i] = Convert.ToDouble(guangyuce);
                        }
                        yucezhi = outRes[0];//得到预测的光功率值

                        //绘制预测曲线
                        for (int i = 0; i < Nday + 1; i++)//统计日期的个数
                        {
                            if (i < Nday)//将当前的选择的数据存储
                            {
                                x[i] = (DateTime)date.dateset[i];
                                y[i] = date.gglv[i];//将光功率赋给y轴
                            }
                            else//预测的数据，主要要时间
                            {
                                x[i] = (DateTime)date.dateset[i - 1].AddDays(1);
                                y[i] = yucezhi;
                            }
                        }
                        for (int i = 0; i < Nday + 1; i++)
                        {
                            ser.Points.AddXY(x[i], y[i]);//向xy轴添加元素
                        }
                        show_now_gglv_dataGridView1(start, end);
                        //chart样式
                        chart1.Series[0].ChartType = SeriesChartType.Spline; //第一条样条图类型
                                                                             //设置标题
                        chart1.Titles[0].Text = string.Format("{0}光功率显示", rn);
                        chart1.Titles[0].ForeColor = Color.RoyalBlue;
                        chart1.Titles[0].ForeColor = Color.RoyalBlue;

                        //设置图标显示样式

                        MessageBox.Show(x[Nday] + "预测的光功率值为：" + guangyuce, "总体预测值");
                    }
                }//将时间赋给x轴}强制类型转换
                 //draw(x,y);//调用函数，画出折线图}
                guanggonglv_dataGridView.Visible = true;
                this.Cursor = Cursors.Default;
            }
            catch (System.Exception ex) { }
        }

        
        private void Date1_comboBox_SelectedIndexChanged(object sender, EventArgs e)//单击日期1的下拉列表将string类型转换成Datetime类型
        {
         
            if (!DateTime.TryParse(date1_comboBox.Text, out time1))
                MessageBox.Show("日期不正确"); 
            time1 = time1.Date;
            // time1 = Convert.ToDateTime(date1_comboBox.Text);//将选中的日期赋给time1


        }
        
        private void Date2_comboBox_SelectedIndexChanged(object sender, EventArgs e)//单击日期2的下拉列表将string类型转换成Datetime类型
        {

            if (!DateTime.TryParse(date2_comboBox.Text, out time2))
                MessageBox.Show("日期不正确");
            time2 = time2.Date;
        }
       
        private void Rb3_all_CheckedChanged(object sender, EventArgs e)//选择全选按钮后日期变得不可编辑
        {
            ChartArea chartArea = chart1.ChartAreas[0];//     表示图表图像上的图表区域。
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea.AxisY.Minimum = -40.00;//光功率范围最小值
            chartArea.AxisY.Maximum = 40.00;//光功率范围最大值
            chartArea.AxisX.Title = "日期";
            chartArea.AxisY.Title = "光功率";
            date1_comboBox.Enabled = false;
            date2_comboBox.Enabled = false;
        }

        private void Rb_special_CheckedChanged(object sender, EventArgs e)//选择部分日期按钮，可选择相应日期
        {
            //设置图标显示样式
            ChartArea chartArea = chart1.ChartAreas[0];//     表示图表图像上的图表区域。
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea.AxisY.Minimum = -40.00;//光功率范围最小值
            chartArea.AxisY.Maximum = 40.00;//光功率范围最大值
            chartArea.AxisX.Title = "日期";
            chartArea.AxisY.Title = "光功率";
            date1_comboBox.Enabled = true;
            date2_comboBox.Enabled = true;
        }

        private void Analysis_Pre_Load(object sender, EventArgs e)
        {
            chart1.Visible = false;
            guanggonglv_dataGridView.Visible = false;
            zhanshi_title.Visible = false;
            label2.Visible = false;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_CursorChanged(object sender, EventArgs e)
        {
            
        }
    }


}
