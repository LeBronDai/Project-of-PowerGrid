using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using WindowsFormsApp1.数据实体类;
namespace WindowsFormsApp1.工具类
{
    class 绘制点和线
    {
        public PointLatLng S_Turn_P(string s1,string s2)//起点转换
        {
            double[] result = new double[100];
            Double.TryParse(s1, out result[1]);
            Double.TryParse(s2, out result[2]);
           
            PointLatLng start = new PointLatLng(result[1], result[2]);
            
            return start;
    
        }
        public PointLatLng E_Turn_P(string s3, string s4)//终点将两个字符型的转换成坐标形式
        {
            double[] result = new double[100];
            Double.TryParse(s3, out result[3]);
            Double.TryParse(s4, out result[4]);
            PointLatLng end = new PointLatLng(result[3], result[4]);   //路径终点
            return end;
        }
        public void DrawLine(string s1, string s2, string s3, string s4, GMapOverlay gMapOverlay,GMapControl gMap1)
        {
            double[] result = new double[100];
            Double.TryParse(s1, out result[1]);
            Double.TryParse(s2, out result[2]);
            Double.TryParse(s3, out result[3]);
            Double.TryParse(s4, out result[4]);
           
            PointLatLng start = new PointLatLng(result[1], result[2]);
            PointLatLng end = new PointLatLng(result[3], result[4]);
            GMapMarker gMapMarker = new GMarkerGoogle(start,GMarkerGoogleType.green);//；类型为绿色
            //6-18
            gMapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            gMapMarker.ToolTipText = string.Format("{0},{1}", start.Lat, start.Lng);
            
            #region 这里定义的图层能够传递下去吗，gMap1与原函数相同吗？
            //  GMapOverlay gMapOverlay = new GMapOverlay("markers");   //这种随用随定义能够传递吗
            gMapOverlay.Markers.Add(gMapMarker);                      //向图层中添加标记 
            gMap1.Overlays.Add(gMapOverlay);                           //向控件中添加图层  
            #endregion
            GMapMarker gMapMarker1 = new GMarkerGoogle(end,GMarkerGoogleType.blue);//；类型为蓝色
            gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            gMapMarker1.ToolTipText = string.Format("{0},{1}", end.Lat, end.Lng);

            gMapOverlay.Markers.Add(gMapMarker1);                      //向图层中添加标记  
            gMap1.Overlays.Add(gMapOverlay);                          //向控件中添加图层 
            DrawLineBetweenTwoPoint(start,end,gMapOverlay);
        }
        //6-17:16:30
        public void DrawLine(PointLatLng [] a, PointLatLng [] b, string sname,string ename,GMapOverlay gMapOverlay, GMapControl gMap1)
        {
            GMapMarker gMapMarker = new GMarkerGoogle(a[1], GMarkerGoogleType.green);//起点

            gMapMarker.ToolTipText = "Start:"+sname;   //标记设置6-18-22:00
            gMapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式鼠标放上面显示

            gMapOverlay.Markers.Add(gMapMarker);
            gMap1.Overlays.Add(gMapOverlay);

            GMapMarker gMapMarker1 = new GMarkerGoogle(b[1], GMarkerGoogleType.blue);//；终点类型为蓝色
            gMapMarker1.ToolTipText = "End:"+ename;   //标记设置
            gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式

            gMapOverlay.Markers.Add(gMapMarker1);

            gMap1.Overlays.Add(gMapOverlay);
            DrawLineBetweenTwoPoint(a[1], b[1], gMapOverlay);
        }

        public void DrawLine(PointLatLng a, PointLatLng b, string sname, string ename, GMapOverlay gMapOverlay, GMapControl gMap1)
        {
            GMapMarker gMapMarker = new GMarkerGoogle(a, GMarkerGoogleType.green);//起点

            gMapMarker.ToolTipText = "Start:" + sname;   //标记设置6-18-22:00
            gMapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式鼠标放上面显示

            gMapOverlay.Markers.Add(gMapMarker);
            gMap1.Overlays.Add(gMapOverlay);

            GMapMarker gMapMarker1 = new GMarkerGoogle(b, GMarkerGoogleType.blue);//；终点类型为蓝色
            gMapMarker1.ToolTipText = "End:" + ename;   //标记设置
            gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式

            gMapOverlay.Markers.Add(gMapMarker1);

            gMap1.Overlays.Add(gMapOverlay);
            DrawLineBetweenTwoPoint(a, b, gMapOverlay);
        }

        //public void DrawLine(PointLatLng [] a, int k, string [] p_name,GMapOverlay gMapOverlay, GMapControl gMap1)//6月20日10:30
        //{
        //    for (int i = 0; i < k; i++)                      //输入k次值，从k 次值中取出前后a[i],a[i+1]连线
        //    {
        //        GMapMarker gMapMarker = new GMarkerGoogle(a[i], GMarkerGoogleType.green);//起点

        //        gMapMarker.ToolTipText = "Start:"+p_name[i];   //标记设置6-18-22:00
        //        gMapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式鼠标放上面显示

        //        gMapOverlay.Markers.Add(gMapMarker);
        //        gMap1.Overlays.Add(gMapOverlay);
        //        if (i + 1 < k)
        //        {
        //            GMapMarker gMapMarker1 = new GMarkerGoogle(a[i + 1], GMarkerGoogleType.blue);//；终点类型为蓝色
        //            gMapMarker1.ToolTipText = "End:"+p_name[i+1];   //标记设置
        //            gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式

        //            gMapOverlay.Markers.Add(gMapMarker1);

        //            gMap1.Overlays.Add(gMapOverlay);
        //            DrawLineBetweenTwoPoint(a[i], a[i + 1], gMapOverlay);
        //        }
        //        else if(i+1==k) 
        //            {
        //            GMapMarker gMapMarker1 = new GMarkerGoogle(a[i + 1], GMarkerGoogleType.blue);//；终点类型为蓝色
        //            gMapMarker1.ToolTipText = "End:" + p_name[i + 1];   //标记设置
        //            gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式

        //            gMapOverlay.Markers.Add(gMapMarker1);

        //            gMap1.Overlays.Add(gMapOverlay);
        //        }
        //    }
        //}
        public void DrawLine(PointLatLng[] a, int k, string[] p_name, GMapOverlay gMapOverlay, GMapControl gMap1)//6月20日10:30
        {
            for (int i = 0; i < k - 1; i++)                      //输入k次值，从k 次值中取出前后a[i],a[i+1]连线
            {
                GMapMarker gMapMarker = new GMarkerGoogle(a[i], GMarkerGoogleType.green);//起点

                gMapMarker.ToolTipText = "Start:" + p_name[i];   //标记设置6-18-22:00
                gMapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式鼠标放上面显示

                gMapOverlay.Markers.Add(gMapMarker);
                gMap1.Overlays.Add(gMapOverlay);
                if (i + 1 < k)
                {
                    GMapMarker gMapMarker1 = new GMarkerGoogle(a[i + 1], GMarkerGoogleType.blue);//；终点类型为蓝色
                    gMapMarker1.ToolTipText = "End:" + p_name[i + 1];   //标记设置
                    gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式

                    gMapOverlay.Markers.Add(gMapMarker1);

                    gMap1.Overlays.Add(gMapOverlay);
                    DrawLineBetweenTwoPoint(a[i], a[i + 1], gMapOverlay);
                }
            }
        }  //修正版备份16：42

        public void DrawLine(PointLatLng[] a, int k, string p_name, GMapOverlay gMapOverlay, GMapControl gMap1)//6月20日10:30
        {
            for (int i = 0; i < k - 1; i++)                      //输入k次值，从k 次值中取出前后a[i],a[i+1]连线
            {
                GMapMarker gMapMarker = new GMarkerGoogle(a[i], GMarkerGoogleType.green);//起点

                gMapMarker.ToolTipText = "Start:" + p_name;   //标记设置6-18-22:00
                gMapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式鼠标放上面显示

                gMapOverlay.Markers.Add(gMapMarker);
                gMap1.Overlays.Add(gMapOverlay);
                if (i + 1 < k)
                {
                    GMapMarker gMapMarker1 = new GMarkerGoogle(a[i + 1], GMarkerGoogleType.blue);//；终点类型为蓝色
                    gMapMarker1.ToolTipText = "End:" + p_name;   //标记设置
                    gMapMarker1.ToolTipMode = MarkerTooltipMode.OnMouseOver; //标记模式

                    gMapOverlay.Markers.Add(gMapMarker1);

                    gMap1.Overlays.Add(gMapOverlay);
                    DrawLineBetweenTwoPoint(a[i], a[i + 1], gMapOverlay);
                }
            }
        }

        private void DrawLineBetweenTwoPoint(PointLatLng pointLatLng_S, PointLatLng pointLatLng_E,GMapOverlay gMapOverlay)
        {
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(pointLatLng_S);
            points.Add(pointLatLng_E);
            GMapRoute r = new GMapRoute(points, "");
            
            
            r.Stroke = new Pen(Color.Green, 1);
            
            gMapOverlay.Routes.Add(r);
        }
        public void DrawRoute(GMapMarkerRoute Route, GMapControl gMap1, GMapOverlay routes,MouseEventArgs e)//画线6-18-16:11
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PointLatLng point = gMap1.FromLocalToLatLng(e.X, e.Y);
                if (Route == null)
                {
                    Route = new GMapMarkerRoute(point);
                    Route.Origin.X = gMap1.Size.Width / 2;
                    Route.Origin.Y = gMap1.Size.Height / 2;
                    Route.OriginOffset.X = 0;
                    Route.OriginOffset.Y = 0;
                    routes.Markers.Add(Route as GMapMarker);//划线

                    
                }
                Route.AddPoint(point);//向路径上添加点
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //记录鼠标按下位
            }
        }

        public void DrawRoute(PointLatLng s,int pid)//6-18 21:00,实现功能将画线将路径名显示出来
        {
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(s); //将s点坐标加入points点集，不断

            DBlink db = new DBlink();  //连接数据库，得到路径名和id的关联关系
            if (db.DBcon())//填充route_list数组
            {
                db.Get_route();
            }
            db.DBclose();
            if (db.DBcon())  //填充id 数组
            {
                db.Get_Id();
            }
            db.DBclose();
            string[] routes_name = new string[20];
            string route_name;
            int[] routes_id = new int[20];
            int i;
            GMapRoute r;
            for (i = 0; i < route.route_list.Count; i++)
               routes_name[i] =route.route_list[i].ToString();
               int.TryParse(route.id.ToString(), out routes_id[i]);
                if (routes_id[i] == pid)
                {
                    route_name = routes_name[i];
                     r = new GMapRoute(points, route_name);
                 }
             
            

        }

        public void route_init(int k, PointLatLng[] a,string [] p_name, List<List<PointLatLng>> pointArray,int route_max, GMapOverlay gMapOverlay, GMapControl gMap1)//6月20日22:25
        {
            int m = 0;
            while (m < route_max)//max为路径的最大条数
            {
                for (int i = 0; i < k; i++)//k为一条线路上节点个数
                {
                    a[i] = pointArray[m][i];//m为存的路径条数

                }
                DrawLine(a, k, p_name, gMapOverlay, gMap1);//画出一条线k为这条线路上节点个数
                Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
                m++;
            }
        }
        public void route_init(int k, PointLatLng[] a, List<List<PointLatLng>> pointArray, int route_max, GMapOverlay gMapOverlay, GMapControl gMap1)//6月20日22:25
        {
            string pname = "pname";
            int m = 0;
            while (m < route_max)//max为路径的最大条数
            {
                for (int i = 0; i < k; i++)//k为一条线路上节点个数
                {
                    a[i] = pointArray[m][i];//m为存的路径条数

                }
                DrawLine(a, k,pname,gMapOverlay, gMap1);//画出一条线k为这条线路上节点个数
                Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
                m++;
            }
        }
        public void route_init(int k, PointLatLng[] a, PointLatLng[] [] pointArray, int route_max, GMapOverlay gMapOverlay, GMapControl gMap1)//6月20日22:25
        {
            string pname = "pname";
            int m = 0;
            while (m < route_max)//max为路径的最大条数
            {
                for (int i = 0; i < k; i++)//k为一条线路上节点个数
                {
                    a[i] = pointArray[m][i];//m为存的路径条数

                }
                DrawLine(a, k, pname, gMapOverlay, gMap1);//画出一条线k为这条线路上节点个数
                Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
                m++;
            }
        }
        //public void route_init(int k, PointLatLng[] a, PointLatLng[][] pointArray, int route_max, GMapOverlay gMapOverlay, GMapControl gMap1)//6月28日17:05
        //{
        //    string pname = "pname";
        //    int m = 0;
        //    while (m < route_max)//max为路径的最大条数
        //    {
        //        for (int i = 0; i < k; i++)//k为一条线路上节点个数
        //        {
        //            a[i] = pointArray[m][i];//m为存的路径条数

        //        }
        //        DrawLine(a, k, pname, gMapOverlay, gMap1);//画出一条线k为这条线路上节点个数
        //        Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
        //        m++;
        //    }
        //}

        //存数据6月23日12：00
        //public void Put_SQL_Stor()
        //{
        //    for (int rid = 0; rid < m; rid++)    //rid为路径数
        //    {
        //        for (int i=0;i<k[rid];i++)       //k[rid]为该路径上的节点个数
        //        {
        //           string str1 = "insert into route_point1(rid) values(" + rid + "');";//向数据库中插入第几个行列
        //           string str2 = "insert into route_point1(point) values('" + PointArray1[rid][i] + "');";//向第几行添加节点
        //           string str3 = "insert into route_point1(pname) values('"+ +"');";

        //        }
        //    }
        //}
        //public void Get_SQL()
        //{


        //        for(int i = 0; i < k[rid]; i++)
        //        {
        //            pointArray1[rid][i] = r_p.;

        //        }
        //    }
        //}

    }
}
