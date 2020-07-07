using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSScriptControl;
using MySql.Data.MySqlClient;
using WindowsFormsApp1.数据实体类;
using WindowsFormsApp1.工具类;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using MathWorks;

namespace WindowsFormsApp1
{
    public partial class Frm_Child_guzhang : Form
    {

        //故障名
       
        //List<Double> yData = new List<double>();
        string rn;
        double year;
        ChartTemplate charttemp = new ChartTemplate();
        bool single_or_all = false;//默认绘制单条线路数据
        public Frm_Child_kongxin frm2;
        public Frm_Child_guzhang()
        {
            InitializeComponent();
        }

        private void Frm_Child_Load(object sender, EventArgs e)
        {

            DBlink db = new DBlink();
            if (db.DBcon())  //填充Pname 数组
            {
                db.Get_guzhang_route();//得到故障线路
            }
            db.DBclose();
            route_comboBox1.Items.Clear();//需要修改
            int i;
            for (i = 0; i < guzhang.rn.Count; i++)
                route_comboBox1.Items.Add(guzhang.rn[i].ToString());//添加线路名
            this.WindowState = FormWindowState.Maximized;

        }

        private void 备纤衰耗分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (frm2 != null)
            {
                if (frm2.IsDisposed)
                    frm2 = new Frm_Child_kongxin();//如果已经销毁，则重新创建子窗口对象
                frm2.Show();

            }
            else
            {
                frm2 = new Frm_Child_kongxin();
                frm2.Show();

            }
        }
        
        

        private void 故障统计分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加线路
            
            
            

        }

        

        private void RbtnPie_CheckedChanged(object sender, EventArgs e)//
        {
            if (!single_or_all)//绘制单条线路上的数据
            {
                guzhang_bar_chart.Visible = false;
                guzhang_pie_chart.Visible = true;
                double s = 0;//总
                             //List<String> xData = new List<String>();//故障类型就5种
                             //List<Double> yData = new List<Double>();
                double[] sum = new double[5];
                double[] pro = new double[5];
                string[] guzhang_Type = new string[] { "断裂", "拉伸", "接头盒进水", "异常", "其他" };
                double[] yData = new double[5];//故障类型的数量5中

                DBlink db = new DBlink();
                if (db.DBcon())
                {
                    db.Gettype_count(rn, year);//输入线路名得到故障类型和数目
                }
                db.DBclose();

                for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                {
                    sum[i] = double.Parse(guzhang.count[i].ToString());//得到故障数量
                                                                       //  guzhang_Type[i] = guzhang.guzhang_type[i].ToString();//
                                                                       //textBox1.Text= guzhang.guzhang_type[i].ToString();
                                                                       //guzhang_Type[i] =guzhangtype_comboBox1.Items[i].ToString();//此处不能赋值

                }
                guzhang.guzhang_type.CopyTo(guzhang_Type);
                for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                {
                    s += sum[i];
                }
                for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                {

                    pro[i] = sum[i] * (1 / s);
                   
                    yData[i] = double.Parse(pro[i].ToString("0.00"));

                }
                guzhang_pie_chart.Series[0].Points.DataBindXY(guzhang_Type, yData);
                guzhang_pie_chart.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                guzhang_pie_chart.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            }
            if (single_or_all)//绘制全部线路
            {
                guzhang_bar_chart.Visible = false;
                guzhang_pie_chart.Visible = true;
                double s = 0;//总
                             //List<String> xData = new List<String>();//故障类型就5种
                             //List<Double> yData = new List<Double>();
                double[] sum = new double[5];
                double[] pro = new double[5];
                string[] guzhang_Type = new string[5];
                double[] yData = new double[5];//故障类型的数量5中

                DBlink db = new DBlink();
                if (db.DBcon())
                {
                    db.Gettype_count(year);//输入线路名得到故障类型和数目
                }
                db.DBclose();

                for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                {
                    sum[i] = double.Parse(guzhang.count[i].ToString());//得到故障数量
                                                                       //  guzhang_Type[i] = guzhang.guzhang_type[i].ToString();//
                                                                       //textBox1.Text= guzhang.guzhang_type[i].ToString();
                                                                       //guzhang_Type[i] =guzhangtype_comboBox1.Items[i].ToString();//此处不能赋值

                }
                guzhang.guzhang_type.CopyTo(guzhang_Type);//将 ArrayList中guzhang.guzhang_type复制到数组guzhang_Type

                for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                {
                    s += sum[i];
                }
                for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                {
                    pro[i] = sum[i] * (1 / s);
                     
                    yData[i] = double.Parse(pro[i].ToString("0.00"));

                }
                guzhang_pie_chart.Series[0].Points.DataBindXY(guzhang_Type, yData);
            }



        }//用饼状图实现年份故障类型分析

        private void RbtnBar_CheckedChanged(object sender, EventArgs e)
        {
            guzhang_bar_chart.Visible = true;
            guzhang_pie_chart.Visible = false;
            

            //设置图表Y轴对应项
            guzhang_bar_chart.Series[0].YValueMembers = "断裂";//拉伸进水异常其他
            guzhang_bar_chart.Series[1].YValueMembers = "拉伸";
            guzhang_bar_chart.Series[2].YValueMembers = "进水";
            guzhang_bar_chart.Series[3].YValueMembers = "异常";
            guzhang_bar_chart.Series[4].YValueMembers = "其他";

            //设置图表X轴对应项
            guzhang_bar_chart.Series[1].XValueMember = "日期";

        }//用柱状图实现各个月份故障发生的频次

        private void 纤芯数录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Barchart_Click(object sender, EventArgs e)//选择全部线路
        {
            route_comboBox1.Enabled = false;//选择框不可选
            single_or_all = true;//当选择全部线路时，single_or_all为true
           
            //添加年份
            DBlink db1 = new DBlink();
            if (db1.DBcon())  //填充Pname 数组
            {
                db1.GetYear();//得到故障线路
            }
            db1.DBclose();
            year_comboBox1.Items.Clear();//

            for (int j = 0; j < guzhang.year.Count; j++)
                year_comboBox1.Items.Add(guzhang.year[j].ToString());

        }

        private void Piechart_Click(object sender, EventArgs e)//选择单条线路
        {
            route_comboBox1.Enabled = true;
            single_or_all = false;//默认选某条线路

        }
        private DataTable Create_DataTable(string rn)//输入某条线路得到柱状图
        {
            //创建一个dataTable chart 的数据源
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("日期");//添加5列数据
            dt1.Columns.Add("断裂");
            dt1.Columns.Add("拉伸");
            dt1.Columns.Add("进水");
            dt1.Columns.Add("异常");
            dt1.Columns.Add("其他");//添加5列
            double[] month = new double[12];//月份
            double[] date1 = new double[100];
            string[] type = new string[100];
 
            double resultmonth=0;
            DataRow dr;
            
            //添加行数据
            //连接数据库
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                db.Getmonth_guzhang(rn,year);//输入线路名得到故障日期和类型
            }
            db.DBclose();
            for (int i = 0; i < guzhang.date.Count; i++)
            {
                date1[i] = guzhang.date[i];//获得月份
                type[i] = guzhang.guzhang_type[i].ToString();//获得对应的故障类型
            }

            DBlink db1 = new DBlink();
            if (db1.DBcon())
            {
                db1.Getmonth(rn,year);//输入线路名得到故障日期月份 
            }
            db1.DBclose();
            for (int i = 0; i < guzhang.date.Count; i++)
                month[i] = guzhang.date[i];//8 9 10

            double[] num = new double[] { 0, 0, 0, 0, 0 };

            for (int k = 0; k < month.Length; k++)//已有月份month[0,1,2]=8,9,10
            {
                for (int i = 0; i < date1.Length; i++)//与实际对比
                {
                    if (month[k] == date1[i])//比如date1[0]==month[0]=8
                    {
                        resultmonth = month[k];
                        string type1 = type[i];//type[0]=断裂
                        switch (type1)//某一月份的故障类型统计
                        {
                                    case "断裂"://断裂
                                        num[0]++;
                                        break;
                                    case "拉伸":
                                        num[1]++;
                                        break;
                                    case "接头盒进水":
                                        num[2]++;
                                        break;
                                    case "异常":
                                        num[3]++;
                                        break;
                                    case "其他":
                                        num[4]++;
                                        break;
                                }

                    }

                        }

                    dr = dt1.NewRow();
                    dr["日期"] = month[k] + "月"; //8,9,10月}
                    dr["断裂"] = num[0];
                    dr["拉伸"] = num[1];
                    dr["进水"] = num[2];
                    dr["异常"] = num[3];
                    dr["其他"] = num[4];
                    dt1.Rows.Add(dr);
                    for (int i = 0; i < 5; i++)
                    {
                        num[i] = 0;
                    }

                }
   
            return dt1;
        }

        private DataTable Create_DataTable()//11-9-2125选择全部线路柱状图
        {
            //创建一个dataTable chart 的数据源
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("日期");//添加5列数据
            dt1.Columns.Add("断裂");
            dt1.Columns.Add("拉伸");
            dt1.Columns.Add("进水");
            dt1.Columns.Add("异常");
            dt1.Columns.Add("其他");//添加5列
            double[] month = new double[100];//月份
            double[] date1 = new double[100];
            string[] type = new string[100];

            double resultmonth = 0;
            DataRow dr;

            //添加行数据
            //连接数据库
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                db.Getmonth_guzhang(year);//输入线路名得到故障日期和类型
            }
            db.DBclose();
            for (int i = 0; i < guzhang.date.Count; i++)
            {
                date1[i] = guzhang.date[i];//获得月份
                type[i] = guzhang.guzhang_type[i].ToString();//获得对应的故障类型
            }

            DBlink db1 = new DBlink();
            if (db1.DBcon())
            {
                db1.Getmonth(year);//输入线路名得到故障日期月份 
            }
            db1.DBclose();
            for (int i = 0; i < guzhang.date.Count; i++)
                month[i] = guzhang.date[i];//7 8 9 10 11

            double[] num = new double[] { 0, 0, 0, 0, 0 };

            for (int k = 0; k < month.Length; k++)//已有月份month[0,1,2]=8,9,10
            {
                for (int i = 0; i < date1.Length; i++)//与实际对比
                {
                    if (month[k] == date1[i])//比如date1[0]==month[0]=8
                    {
                        resultmonth = month[k];
                        string type1 = type[i];//type[0]=断裂
                        switch (type1)//某一月份的故障类型统计
                        {
                            case "断裂"://断裂
                                num[0]++;
                                break;
                            case "拉伸":
                                num[1]++;
                                break;
                            case "接头盒进水":
                                num[2]++;
                                break;
                            case "异常":
                                num[3]++;
                                break;
                            case "其他":
                                num[4]++;
                                break;
                        }

                    }

                }

                dr = dt1.NewRow();
                dr["日期"] = month[k] + "月"; //8,9,10月}
                dr["断裂"] = num[0];
                dr["拉伸"] = num[1];
                dr["进水"] = num[2];
                dr["异常"] = num[3];
                dr["其他"] = num[4];
                dt1.Rows.Add(dr);
                for (int i = 0; i < 5; i++)
                {
                    num[i] = 0;
                }

            }

            return dt1;
        }

        private void Route_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            rn = route_comboBox1.Text;
            //添加年份
            DBlink db1 = new DBlink();
            if (db1.DBcon())  //填充Pname 数组
            {
                db1.GetYear(rn);//得到故障线路
            }
            db1.DBclose();
            year_comboBox1.Items.Clear();//

            for (int j = 0; j < guzhang.year.Count; j++)
                year_comboBox1.Items.Add(guzhang.year[j].ToString());
        }
        
        private void DataAnalysis_Click(object sender, EventArgs e)
        {
            if (!single_or_all)//如果选择某条线路
            {
                if (rbtnBar.Checked)//当选择柱状图时
                {
                    DataTable dt1 = default(DataTable);
                    dt1 = Create_DataTable(rn);//返回值给dt1//调用
                                               //设置图表数据源
                    guzhang_bar_chart.DataSource = dt1;
                    //绑定数据
                    guzhang_bar_chart.DataBind();
                }

                if (rbtnPie.Checked)
                {
                    double s = 0;//总
                                 //List<String> xData = new List<String>();//故障类型就5种
                                 //List<Double> yData = new List<Double>();
                    double[] sum = new double[5];
                    double[] pro = new double[5];
                    string[] guzhang_Type = new string[5];
                    double[] yData = new double[5];//故障类型的数量5中

                    DBlink db = new DBlink();
                    if (db.DBcon())
                    {
                        db.Gettype_count(rn, year);//输入线路名得到故障类型和数目
                    }
                    db.DBclose();

                    for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                    {
                        sum[i] = double.Parse(guzhang.count[i].ToString());//得到故障数量
                                                                           //  guzhang_Type[i] = guzhang.guzhang_type[i].ToString();//
                                                                           //textBox1.Text= guzhang.guzhang_type[i].ToString();
                                                                           //guzhang_Type[i] =guzhangtype_comboBox1.Items[i].ToString();//此处不能赋值

                    }
                    guzhang.guzhang_type.CopyTo(guzhang_Type);
                    for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                    {
                        s += sum[i];
                    }
                    for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                    {

                        pro[i] = sum[i] * (1/s);
                        yData[i] = double.Parse(pro[i].ToString("0.00"));

                    }
                    guzhang_pie_chart.Series[0].Points.DataBindXY(guzhang_Type, yData);


                }
            }

            if (single_or_all)//如果选择所有线路
            {
                if (rbtnBar.Checked)//当选择柱状图时
                {
                    DataTable dt1 = default(DataTable);
                    dt1 = Create_DataTable();//返回值给dt2//调用
                                               //设置图表数据源
                    guzhang_bar_chart.DataSource = dt1;
                    //绑定数据
                    guzhang_bar_chart.DataBind();
                }

                if (rbtnPie.Checked)
                {
                    double s = 0;//总
                                 //List<String> xData = new List<String>();//故障类型就5种
                                 //List<Double> yData = new List<Double>();
                    double[] sum = new double[5];
                    double[] pro = new double[5];
                    string[] guzhang_Type = new string[5];
                    double[] yData = new double[5];//故障类型的数量5中

                    DBlink db = new DBlink();
                    if (db.DBcon())
                    {
                        db.Gettype_count(year);//输入线路名得到故障类型和数目
                    }
                    db.DBclose();

                    for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                    {
                        sum[i] = double.Parse(guzhang.count[i].ToString());//得到故障数量
                                                                           //  guzhang_Type[i] = guzhang.guzhang_type[i].ToString();//
                                                                           //textBox1.Text= guzhang.guzhang_type[i].ToString();
                                                                           //guzhang_Type[i] =guzhangtype_comboBox1.Items[i].ToString();//此处不能赋值

                    }
                    guzhang.guzhang_type.CopyTo(guzhang_Type);//将 ArrayList中guzhang.guzhang_type复制到数组guzhang_Type

                    for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                    {
                        s += sum[i];
                    }
                    for (int i = 0; i < guzhang.guzhang_type.Count; i++)
                    {

                        double tmp = 1 / s;
                        pro[i] =sum[i]*tmp;
                        yData[i] = double.Parse(pro[i].ToString("0.00"));

                    }
                    guzhang_pie_chart.Series[0].Points.DataBindXY(guzhang_Type, yData);


                }
            }
            
        }

        private void RbtnAllRoute_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Year_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Double.TryParse(year_comboBox1.Text, out year))
                MessageBox.Show("日期不正确");//将年份以double形式存在year里
        }

        private void RbtnSelectedRoute_CheckedChanged(object sender, EventArgs e)
        {
            route_comboBox1.Enabled = true;
        }

        private void RbtnSelectedcheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            // 添加线路
            DBlink db = new DBlink();
            if (db.DBcon())  //填充Pname 数组
            {
                db.Get_guzhang_route();//得到故障线路
            }
            db.DBclose();
            route_comboBox1.Items.Clear();//需要修改
            int i;
            for (i = 0; i < guzhang.rn.Count; i++)
                route_comboBox1.Items.Add(guzhang.rn[i].ToString());//添加线路名

        }

        private void Frm_Child_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }

    
}

