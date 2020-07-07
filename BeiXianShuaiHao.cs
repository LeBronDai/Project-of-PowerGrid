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


namespace WindowsFormsApp1
{
    public partial class BeiXianShuaiHao : Form
    {
        string rn = null;
        double avg = 0;
        double sum = 0;
        DateTime time1;//选择第一个时间点
        DateTime time2;//选择第二个时间点
        int rqcz = 0;//日期差值，用于预测部分
        DateTime[] x = new DateTime[50];//时间数组，作为x轴
        double[] y = new double[50]; //备纤衰耗，作为y轴
        public BeiXianShuaiHao()
        {
            InitializeComponent();
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
                route_comboBox3.Items.Clear();
                for (int i = 0; i < route.route_list.Count; i++) 
                    route_comboBox3.Items.Add(route.route_list[i].ToString());
                //   已有节点显示下拉列表3.Items.Add(point_data.pname[i].ToString());
                #endregion         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }

        }
//选择线路
        private void route_comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                date1_comboBox1.Items.Clear();
                date2_comboBox1.Items.Clear();
                DBlink db = new DBlink();
                rn = route_comboBox3.Text;

                if (db.DBcon())  //填充日期
                {
                    db.Get_date1(rn);//得到某一线路光纤衰耗日期信息
                }
                db.DBclose();
                date1_comboBox1.Items.Clear();//清空日期再刷新
                date2_comboBox1.Items.Clear();
                for (int i = 0; i < date.dateset.Count; i++)
                {
                    date1_comboBox1.Items.Add(date.dateset[i]);
                    date2_comboBox1.Items.Add(date.dateset[i]);
                }
                // 已有节点显示下拉列表3.Items.Clear();//清空再刷新

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Btn_beixian_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            label3.Visible = true;
            try
            {
                chart1.Series.Clear();//每次输入之后清空
                Series ser = new Series(rn);// 实例化序列图对象
                ser.IsValueShownAsLabel = true;//显示每个节点信息

                chart1.Series.Add(ser);//向chart1中添加序列对象

                #region 数据库连接操作,得到日期差值和光功率数值
                DBlink db1 = new DBlink();
                if (db1.DBcon())
                {
                    db1.Get_datediff(time2, time1); //输入线路名，得到对应的光功率和日期
                }
                db1.DBclose();
                DBlink db = new DBlink();
                if (db.DBcon())
                {
                    db.Get_Beixian(rn, time1, time2);//输入线路名，起止时间得到对应的光功率和日期
                }
                db.DBclose();
                #endregion
                rqcz = date.rqcz[0] + 1;//得到选中的日期差值 比如10月20日与10月19
                for (int i = 0; i < rqcz; i++)//统计日期的个数
                {
                    x[i] = (DateTime)date.dateset[i];//日期
                    y[i] = date.beixian[i];//将光功率赋给y轴
                    sum = sum + y[i];
                }
                //预测部分 得到预测的光功率值
                for (int i = 0; i < rqcz; i++)//rqcz=7 x[0-6] x[7] y[7]预测
                {
                    ser.Points.AddXY(x[i], y[i]);
                }//向xy轴添加元素}
                avg = sum / rqcz;
               
               

                chart1.Series[0].ChartType = SeriesChartType.Spline; //第一条样条图类型
                                                                     //设置标题
                chart1.Titles[0].Text = string.Format("{0}备线衰耗值显示", rn);
                chart1.Titles[0].ForeColor = Color.RoyalBlue;
                chart1.Titles[0].ForeColor = Color.RoyalBlue;
                if (avg > 3)
                {
                    MessageBox.Show("备纤状态异常，可能出现拉伸、弯曲，请检查备纤!");
                }
                else
                {
                    if (avg > 10)
                    { MessageBox.Show("光缆已经断了，请前去排除故障!"); }
                    else
                    {
                        MessageBox.Show("光缆平均备纤衰耗值：" + avg);
                    }
                }
            }
            catch (System.Exception ex) 
            { }
        }

        private void date1_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(date1_comboBox1.Text, out time1))
                MessageBox.Show("日期不正确");
            time1 = time1.Date;
        }

        private void date2_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(date2_comboBox1.Text, out time2))
                MessageBox.Show("日期不正确");
            time2 = time2.Date;
        }
    }
//分析

        //private void date1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    //private void BeiXianShuaiHao_Load(object sender, EventArgs e)
       //{

       // }
    }

