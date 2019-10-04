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
            route_data.id.Clear();
            route_data.rid.Clear();
            route_data.rn.Clear();
            route_data.rl.Clear();

            while (rec.Read())
            {
                route_data.id.Add(rec.GetString(0));
                route_data.rid.Add(rec.GetString(1));
                route_data.rn.Add(rec.GetString(2));
                route_data.rl.Add(rec.GetString(3));
            }
        }
        public void Get_dataGridView2_data(string str)
        {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            //初始化数据;
            route_data.id.Clear();
            route_data.rid.Clear();
            route_data.rn.Clear();
            route_data.rl.Clear();

           
            point_data.id.Clear();
            point_data.pid.Clear();
            point_data.lng.Clear();
            point_data.lat.Clear();
            point_data.pname.Clear();
            
            while (rec.Read())
            {
                route_data.id.Add(rec.GetString(0));
                route_data.rid.Add(rec.GetString(1));
                route_data.rn.Add(rec.GetString(2));
                route_data.rl.Add(rec.GetString(3));
                point_data.id.Add(rec.GetString(4));
                point_data.pid.Add(rec.GetString(5));
                point_data.lng.Add(rec.GetString(6));
                point_data.lat.Add(rec.GetString(7));
                point_data.pname.Add(rec.GetString(8));
                //point_data.s_name.Add(rec.GetString(5));
                //point_data.e_name.Add(rec.GetString(6));
                
            }
        }
        public void Get_dataGridView3_nowpoint_data(string str)//展示现有节点6-19-16:25
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
        public void Get_rid()//将线路编号传到rid中
        {
            sqlCmd.CommandText = "select distinct 线路编号 from route_point1 ;";  //查找point表
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            while (rec.Read())
            {
                r_p.rid.Add(rec.GetInt32(0));
              
            }

        }
        public void Get_lng_lat_pname(int rid)//传进来得到某线路上的维度，经度，节点名称6-28 15:16
        {
            sqlCmd.CommandText = "select distinct 节点维度,节点经度,节点名称 from route_point1 where 线路编号 ="+rid;  //查找point表
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
