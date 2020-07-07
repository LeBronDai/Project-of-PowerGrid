using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    public partial class Frm_Child_kongxin : Form
    {
        public int xd1 = 0;
        public int xd2 = 0;
        public int xd3 = 0;
        public int xd4 = 0;

        public List<Double> lt;
        public Frm_Child_kongxin()
        {
            lt = new List<double>();
            string connstr = "data source=localhost;database=mappoint;user id=root;password=root;pooling=false;charset=utf8";//pooling代表是否使用连接池
            MySqlConnection conn = new MySqlConnection(connstr);

            string sql = "select * from route1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> xData = new List<string>() { };
            List<int> yData = new List<int>() { };
            List<int> yData2 = new List<int>() { };


            double jg = 0;
            while (reader.Read())
            {
                int sy = reader.GetInt32("left_optical_cable");
                int zs = reader.GetInt32("total_num_optical_cable");
                xData.Add(reader.GetString("rn"));
                yData.Add(sy);
                yData2.Add(zs);
                jg = (Double)sy / (Double)zs;
                lt.Add(jg);
                if (jg < 0.25)
                {
                    xd1++;
                }
                else if (jg < 0.5)
                {
                    xd2++;
                }
                else if (jg < 0.75)
                {
                    xd3++;
                }
                else
                {
                    xd4++;
                }
            }
            conn.Close();
            InitializeComponent();

            chart1.Series[0]["Chart1LabelStyle"] = "Outside";//将文字移到外侧
            chart1.Series[0]["Chart1LineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[0].Points.DataBindXY(xData, yData);

            chart1.Series[1]["Chart1LabelStyle"] = "Outside";//将文字移到外侧
            chart1.Series[1]["Chart1LineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[1].Points.DataBindXY(xData, yData2);

            List<string> xData3 = new List<string>() { "负载率0-25%条数", "负载率25-50%条数", "负载率50-75%条数", "负载率75-100%条数" };
            List<int> yData3 = new List<int>() { xd1, xd2, xd3, xd4 };
            chart2.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            chart2.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            chart2.Series[0].Points.DataBindXY(xData3, yData3);

            chart1.Visible = false;
            chart2.Visible = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            btn_routenum.Checked = false;
            btn_count.Checked = false;
            radioButton3.Checked = true;
            radioButton3.Visible = false;
        }

        private void Btn_routenum_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Visible = true;
            chart2.Visible = false;
        }

        private void Btn_count_CheckedChanged(object sender, EventArgs e)
        {
            chart2.Visible = true;
            chart1.Visible = false;
        }

        private void Btn_fenxi_Click(object sender, EventArgs e)
        {
            string s1 = txt1_range.Text;
            string s2 = txt2_range.Text;
            int zs = 0;
            string msg = "";
            try
            {
                if (btn_count.Checked)//饼状图
                {
                    for (int i = 0; i < lt.ToArray().Length; i++)
                    {
                        if (lt[i] * 100 < Double.Parse(s2) && lt[i] * 100 >= Double.Parse(s1))
                        {
                            zs += 1;
                        }
                    }
                    msg = "在比例范围内的数量是" + zs.ToString();
                }
                if (btn_routenum.Checked)//柱状图
                {
                    for (int i = 0; i < lt.ToArray().Length; i++)
                    {
                        if (lt[i] * 100 < Double.Parse(s2) && lt[i] * 100 >= Double.Parse(s1))
                        {
                            zs += 1;
                        }
                    }
                    msg = "在比例范围内的数量是" + zs.ToString();
                }

                MessageBox.Show(msg);
            }
            catch (Exception ex)//捕获异常
            {
                MessageBox.Show(ex.Message);//输入异常信息
            }
        }
    }
}
