using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Forms;  
using WindowsFormsApp1.数据实体类;
namespace WindowsFormsApp1.工具类
{
    //将数据库连接功能，单独定义为一个类来执行
    class DBlink
    {
        MySqlConnection sqlCnn = new MySqlConnection();//创建数据库连接对象
        MySqlCommand sqlCmd = new MySqlCommand();//创建执行sql对象

        //创建数据库连接
        public Boolean DBcon()
        {
            Boolean tag = false;
            sqlCnn.ConnectionString = "server='127.0.0.1';uid='root';pwd='root';database='mappoint';";//连接字符串
            sqlCmd.Connection = sqlCnn;
            try
            {
                sqlCnn.Open();
                tag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            return tag;
        }

        //关闭数据库连接
        public void DBclose()
        {
            sqlCnn.Close();
        }

        //获取数据库中的Id,存入动态数据route.id中
        public void Get_Id()
        {
            sqlCmd.CommandText = "select id from route1";
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            route.id.Clear();
            while (rec.Read())
            {
                route.id.Add(rec.GetString(0));
            }

        }

        public void Get_dataGridView1_data(string str)
        {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            //初始化数据;
           
            route_data.rid.Clear();
            route_data.rn.Clear();
            route_data.rl.Clear();
            route_data.volatge_level.Clear();//2019.10.28
            route_data.cable_type.Clear();
            route_data.total_num_optical_cable.Clear();
            route_data.left_optical_cable.Clear();

            while (rec.Read())
            {
                route_data.rid.Add(rec.GetString(0));
                route_data.rn.Add(rec.GetString(1));
                route_data.rl.Add(rec.GetString(2));
                route_data.volatge_level.Add(rec.GetString(3));
                route_data.cable_type.Add(rec.GetString(4));
                route_data.total_num_optical_cable.Add(rec.GetInt32(5));
                route_data.left_optical_cable.Add(rec.GetInt32(6));
            }
        }
        public void Get_dataGridView2_data(string str)
        {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            //初始化数据;
            
            route_data.rid.Clear();
            route_data.rn.Clear();
            

           
           
            point_data.pid.Clear();
            point_data.lng.Clear();
            point_data.lat.Clear();
            point_data.pname.Clear();
            
            while (rec.Read())
            {
                
                route_data.rid.Add(rec.GetString(0));
                route_data.rn.Add(rec.GetString(1));
                
                
                point_data.pid.Add(rec.GetString(2));
                point_data.lng.Add(rec.GetString(3));
                point_data.lat.Add(rec.GetString(4));
                point_data.pname.Add(rec.GetString(5));
                //point_data.s_name.Add(rec.GetString(5));
                //point_data.e_name.Add(rec.GetString(6));
                
            }
        }
        public void Get_dataGridView3_nowpoint_data(string str)//展示现有节点
        {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            //初始化数据;
            point_data.pid.Clear();
            point_data.lat.Clear();
            point_data.lng.Clear();
            point_data.pname.Clear();
            point_data.rid.Clear();
            while (rec.Read())
            {
                point_data.pid.Add(rec.GetString(0));
                point_data.lat.Add(rec.GetString(1));
                point_data.lng.Add(rec.GetString(2));
                point_data.pname.Add(rec.GetString(3));
                point_data.rid.Add(rec.GetString(4));
            }
        }

        public void Get_dataGridView3_nowroute_data(string str)//展示现有节点6-19-16:25
        {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            //初始化数据;
            route_data.rid.Clear();
            route_data.rn.Clear();
            route_data.rl.Clear();
            
            while (rec.Read())
            {
                route_data.rid.Add(rec.GetString(0));
                route_data.rn.Add(rec.GetString(1));
                route_data.rl.Add(rec.GetString(2));
               
            }
        }

        public void Get_dataGridView3_gonglv_data(string str)//展示电功率信息10月25日
        {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            //初始化数据;
            date.rn.Clear();
            date.dateset.Clear();
            date.gglv.Clear();

            while (rec.Read())
            {
                date.rn.Add(rec.GetString(0));
                date.dateset.Add(rec.GetDateTime(1));
                date.gglv.Add(rec.GetDouble(2));

            }
        }
        //执行查询的sql
        public Boolean search(String str)
        {
            Boolean tag = false;
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();//创建结果集
            if (rec.HasRows)
            {
                tag = true;     // 查询到数据
            }
            return tag;
        }

        //查询刚添加路径的序号，好进行添加
        public string search_id(String str)
        {
            string id = null;
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();//创建结果集
            while (rec.Read())
            {
                id = rec.GetString(0);     // 查询到数据
            }
            return id;
        }
        
        //执行增删改的sql
        public Boolean UpdataDeletAdd(string str)
        {
            sqlCmd.CommandText = str;
            return sqlCmd.ExecuteNonQuery() > 0;
        }

        ///获取数据库中的路径信息，存入动态数组route中
        public void Get_route()
        {
            sqlCmd.CommandText = "select rn from route1";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            route.route_list.Clear();
            while (rec.Read())
            {
                route.route_list.Add(rec.GetString(0));//添加路径名
            }
        }
        public void Get_guzhang_route()//得到故障线路
        {
            sqlCmd.CommandText = "select distinct rn from guzhang";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.rn.Clear();
            while (rec.Read())
            {
                guzhang.rn.Add(rec.GetString(0));//添加路径名
            }
        }
        public void Getmonth_guzhang(string rn,double year)//输入线路名得到故障的月份和故障类型
        {
            sqlCmd.CommandText = "select month(date),guzhang_type from guzhang where rn='"+rn+ "'and year(date)="+year+" order by date";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.date.Clear();
            guzhang.guzhang_type.Clear();
            while (rec.Read())
            {
                guzhang.date.Add(rec.GetDouble(0));//
                guzhang.guzhang_type.Add(rec.GetString(1));//添加故障类型
            }
        }

        public void Getmonth_guzhang(double year)//输入年份得到故障的月份和故障类型
        {
            sqlCmd.CommandText = "select month(date),guzhang_type from guzhang where year(date)=" + year + " order by date";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.date.Clear();
            guzhang.guzhang_type.Clear();
            while (rec.Read())
            {
                guzhang.date.Add(rec.GetDouble(0));//添加月份
                guzhang.guzhang_type.Add(rec.GetString(1));//添加故障类型
            }
        }
        public void Getmonth(string rn,double year)//输入线路名和年份得到故障的月份
        {
            sqlCmd.CommandText = "select distinct month(date)from guzhang where rn='" + rn + "'and year(date)='" + year + "' order by date";  //输入线路，年份查找
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.date.Clear();
           
            while (rec.Read())
            {
                guzhang.date.Add(rec.GetDouble(0));//只要日期
                
            }
        }
        public void Getmonth(double year)//输入线路名和年份得到故障的月份//用作横坐标
        {
            sqlCmd.CommandText = "select distinct month(date) from guzhang where year(date)='" + year + "' order by date";  //输入线路，年份查找
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.date.Clear();

            while (rec.Read())
            {
                guzhang.date.Add(rec.GetDouble(0));//只要日期

            }
        }
        public void GetYear()//选择年份
        {
            sqlCmd.CommandText = "select distinct year(date)from guzhang  order by date";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.year.Clear();

            while (rec.Read())
            {
                guzhang.year.Add(rec.GetDouble(0));//只要日期

            }
        }

        public void GetYear(string rn)//选择特定线路，得到年份
        {
            sqlCmd.CommandText = "select distinct year(date)from guzhang where rn='" + rn +"' order by date";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.year.Clear();

            while (rec.Read())
            {
                guzhang.year.Add(rec.GetDouble(0));//只要日期

            }
        }
        public void Gettype_count(string rn,double year)//输入线路名得到故障的类型和数目
        {
            sqlCmd.CommandText = "select guzhang_type,count(guzhang_type)from guzhang where rn='" + rn + "'and year(date)=" + year + " group by guzhang_type";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.guzhang_type.Clear();
            guzhang.count.Clear();

            while (rec.Read())
            {
                guzhang.guzhang_type.Add(rec.GetString(0));//得到故障类型
                guzhang.count.Add(rec.GetInt32(1));//得到故障数量

            }
        }

        public void Gettype_count(double year)//输入年份得到所有故障的类型和数目
        {
            sqlCmd.CommandText = "select guzhang_type,count(guzhang_type)from guzhang where  year(date)=" + year + " group by guzhang_type";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            guzhang.guzhang_type.Clear();
            guzhang.count.Clear();

            while (rec.Read())
            {
                guzhang.guzhang_type.Add(rec.GetString(0));//得到故障类型
                guzhang.count.Add(rec.GetInt32(1));//得到故障数量

            }
        }
        public void Get_point()  //得到point 的名字
        {
            sqlCmd.CommandText = "select pname from point";  //查找point表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            point_data.pname.Clear();
            while (rec.Read())
            {
                point_data.pname.Add(rec.GetString(0));//添加point名字到pname的list
            }
        }
        public void Get_route1()  //得到光功率表线路信息
        {
            sqlCmd.CommandText = "select distinct rn from optical_power";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            route.route_list.Clear();
            while (rec.Read())
            {
                route.route_list.Add(rec.GetString(0));//添加路径名
            }
        }
        public void Get_date(string rn)  //得到光功率表日期信息
        {
            sqlCmd.CommandText = "select date from optical_power where rn='"+rn+"'";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            date.dateset.Clear();
            while (rec.Read())
            {
                date.dateset.Add(rec.GetDateTime(0));//添加日期信息
            }
        }
        public void Get_date1(string rn)  //得到光功率表日期信息
        {
            sqlCmd.CommandText = "select date from beixianshuaihao where rn='" + rn + "'";  //查找route表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            date.dateset.Clear();
            while (rec.Read())
            {
                date.dateset.Add(rec.GetDateTime(0));//添加日期信息
            }
        }
        public void Get_gonglv(string rn)  //得到光功率信息日期信息
        {
            sqlCmd.CommandText = "select optical_power,date from optical_power where rn='" + rn + "'"+"order by date asc";  //查找光功率日期
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            date.gglv.Clear();//光功率清空
            date.dateset.Clear(); //日期清空

            while (rec.Read())
            {
                date.gglv.Add(rec.GetDouble(0));//添加光功率信息
                date.dateset.Add(rec.GetDateTime(1));//添加日期信息
            }
        }
        public void Get_gonglv1(string rn, DateTime time1, DateTime time2)  //得到光功率信息日期信息
        {
            sqlCmd.CommandText = "select optical_power,date from optical_power where date>='" + time1+ "'"+"and date<='"+time2+ "'and rn ='" + rn + "'" ;  //查找特定线路时间段光功率
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            date.gglv.Clear();//光功率清空
            date.dateset.Clear(); //日期清空

            while (rec.Read())
            {
                date.gglv.Add(rec.GetDouble(0));//添加光功率信息
                date.dateset.Add(rec.GetDateTime(1));//添加日期信息
            }
        }
        public void Get_Beixian(string rn, DateTime time1, DateTime time2)  //得到光功率信息日期信息
        {
            sqlCmd.CommandText = "select shuaihao,date from beixianshuaihao where date>='" + time1 + "'" + "and date<='" + time2 + "'and rn ='" + rn + "'";  //查找特定线路时间段光功率
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            date.beixian.Clear();//备纤清空
            date.dateset.Clear(); //日期清空

            while (rec.Read())
            {
                date.beixian.Add(rec.GetDouble(0));//添加备纤
                date.dateset.Add(rec.GetDateTime(1));//添加日期信息
            }
        }
        public void Get_num_optical(string rn)  //输入线路名得到纤芯数11月3日
        {
            sqlCmd.CommandText = "select total_num_optical_cable,left_optical_cable from route1 where rn='" + rn + "'";  //查找特定线路的纤芯数
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            route_data.total_num_optical_cable.Clear();//总体光缆数清空
            route_data.left_optical_cable.Clear();//剩余光缆数清空
            while (rec.Read())
            {
                route_data.total_num_optical_cable.Add(rec.GetInt32(0));//添加光功率信息
                route_data.left_optical_cable.Add(rec.GetInt32(1));//添加日期信息
            }
        }

        public void Get_datediff(DateTime time2, DateTime time1) {
            
            sqlCmd.CommandText = "select DateDiff('"+time2+"','"+time1+"');";
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            date.rqcz.Clear();
            while (rec.Read())
            {
                date.rqcz.Add(rec.GetInt32(0));
            }

        }

        public void Get_rid()//将线路编号传到rid中
        {
            sqlCmd.CommandText = "select distinct rid from route_point1 ;";  //查找point表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            while (rec.Read())
            {
                r_p.rid.Add(rec.GetInt32(0));
              
            }

        }
        public void Get_lng_lat_pname(int rid)//传进来得到某线路上的维度，经度，节点名称6-28 15:16
        {
            sqlCmd.CommandText = "select distinct lat,lng,pname from route_point1 where rid ="+rid;  //查找point表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            while (rec.Read())
            {
                r_p.lng.Add(rec.GetDouble(0));
                r_p.lat.Add(rec.GetDouble(1));
                r_p.pname.Add(rec.GetString(2));
            }

        }
    }
}
