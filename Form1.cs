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
using Demo.WindowsForms.CustomMarkers;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

using WindowsFormsApp1.工具类;
//using WindowsFormsApp2;
using WindowsFormsApp1.数据实体类;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        #region 全局变量定义
        private static string ID;//线路编号
        private double[] result = new double[100];//节点转换结果集
        private GMapOverlay gMapOverlay = new GMapOverlay("markers");//放置markers图层

        private GMapOverlay routes = new GMapOverlay("routes");//放置路径图层
        private List<PointLatLng> RoutePoints = new List<PointLatLng>();//需要绘制的经纬度点集
        private List<Point> Point = new List<Point>();//用户绘制在视窗中的点，是将经纬度转换成GPoint再加上偏移处理后的点
        private GMapMarkerRoute Route = null;
        private Point RightBDPoint;
        private Timer blinkTimer = new Timer();

        private GMapMarkerImage currentMarker; //建立GmapMakeImage 对象
        private GMapMarkerRoute currentRoute;  //建立currentRoute对象

        private Boolean addr = false;//控制手动填数还是点dataGridview填数
        private Boolean addover = true;//添加节点可添加
        private 绘制点和线 draw_Line = new 绘制点和线();
        private List<List<PointLatLng>> pointArray = new List<List<PointLatLng>>();//  动态创建一个二维的列表
        private PointLatLng[][] pointArray1 = new PointLatLng[100][];


        private int DragOffsetX = 0, DragOffsetY = 0;//初始化X和Y 坐标
        private bool isLeftButtonDown = false; //鼠标左点积为空

        int k = 0;//绘制线路
        int m = 0;

        PointLatLng[] a = new PointLatLng[100];//a[i]存放一条线上的坐标信息，用于线路初始化画线
        PointLatLng[] b = new PointLatLng[100];//b[j]存放每两个节点之间的线，用于线路初始化画线
        string[] p_name = new string[100];  //

        string[] dengji = new string[] { "220kV", "110kV", "66kV", "35kV", "10kV", "6kV", "380V", "220V" };
        string[] leixing = new string[] { "ADSS", "OPGW" };
        int[] zongxin = new int[] { 12, 24, 36, 48 };
        string g_dengji;//电压等级全局
        string g_leixing;//光缆类型全局
        int g_zongxin;//总芯数
        string[] guzhang_type = new string[] { "断裂", "拉伸", "接头盒进水", "异常", "其他" };
        string guzhang_date;
        string guzhang_type1;
       
        #endregion

        /// <summary>
        /// 主界面初始化显示
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            try
            {
                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("ditu.google.cn");//首先打开网站地图
            }
            catch
            {
                gMap1.Manager.Mode = AccessMode.CacheOnly; //若打不开则使用离线地图  
                MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET Demo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #region 地图初始化
            String mapPath = Application.StartupPath + "H:\\研究生学习\\电网项目\\离线地图\\googlechina_serverandcache_huhehaote.gmdb";
            gMap1.CacheLocation = System.Windows.Forms.Application.StartupPath;//指定地图缓存存放路径
            gMap1.MapProvider = GMapProviders.GoogleChinaMap;//指定地图类型
            this.gMap1.MinZoom = 1;   //最小比例
            this.gMap1.MaxZoom = 23; //最大比例
            this.gMap1.Zoom = 12; //当前比例
            this.gMap1.ShowCenter = false; //不显示中心十字点
            this.gMap1.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            this.gMap1.Position = new PointLatLng(40.8456789180537, 111.618702219065); //地图中心位置
            this.gMap1.Show();                                                                         //form.AddLocation("huhehaote");
            #endregion

            #region 初始化时地图上显示的两个点。考虑删除添加节点示例
        
            #endregion

            #region Gmap1鼠标事件-图层事件
            //注释掉鼠标点击事件this.gMap1.MouseClick += new MouseEventHandler(mapControl_MouseClick);
            this.gMap1.MouseDown += new MouseEventHandler(mapControl_MouseDown); //鼠标下移
            this.gMap1.MouseUp += new MouseEventHandler(mapControl_MouseUp);
          //  this.gMap1.MouseMove += new MouseEventHandler(mapControl_MouseMove);                                                                    //this.gMap.MouseUp += new MouseEventHandler(mapControl_MouseUp);
            this.gMap1.OnMapZoomChanged += new MapZoomChanged(mapControl_OnMapZoomChanged);

            //图标事件
            this.gMap1.OnMarkerEnter += new MarkerEnter(mapControl_OnMarkerEnter);
            this.gMap1.OnMarkerLeave += new MarkerLeave(mapControl_OnMarkerLeave);
            for (int i = 0; i < 100; i++)
            {
                pointArray1[i] = new PointLatLng[100];//初始化了一个100*100的方阵6-21 14:00
            }

            ///<summary>
           ///鼠标左键地图时，标记所在的点的位置信息,生成小锤头的注释掉了
           /// </summary>
            //void mapControl_MouseClick(object sender, MouseEventArgs e)//鼠标点击
            //{
            //    if (e.Button == System.Windows.Forms.MouseButtons.Left) //当点击鼠标左键时，标记节点经纬度
            //    {
              
            //        PointLatLng point = gMap1.FromLocalToLatLng(e.X, e.Y);
            //        Bitmap bitmap = Bitmap.FromFile("H:\\研究生学习\\电网项目资源\\图标1集合\\gur-project-13.png") as Bitmap;
            //        //GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.arrow);//系统自带的绿色图标
            //        GMapMarker marker = new GMapMarkerImage(point, bitmap);//自定义图标显示
            //        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //        string s = "address:";
            //        marker.ToolTipText = string.Format("{0},{1},{2}", point.Lat, point.Lng, s);//生成坐标
            //        gMapOverlay.Markers.Add(marker);
            //    }
            //    if (e.Button == System.Windows.Forms.MouseButtons.Right) //鼠标右键.
            //    {
            //        contextMenuStrip1.Show(Cursor.Position);
            //    }

            //}
           
            /// <summary>
            /// 地图拖拽向量
            /// 在进行地图的缩放后需要将该偏移量清零
            /// </summary>
            void mapControl_MouseUp(object sender, MouseEventArgs e)     
            {

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                    //在拖拽地图后地图原点和视窗原点的偏移量
                    DragOffsetX = DragOffsetX + e.X - RightBDPoint.X;
                    DragOffsetY = DragOffsetY + e.Y - RightBDPoint.Y;
                    if (Route != null)
                    {
                        //设置Route的中心偏移
                        Route.OriginOffset.X = DragOffsetX;
                        Route.OriginOffset.Y = DragOffsetY;
                    }
                }

            }//鼠标拖拽地图实现上下移动
            void mapControl_MouseDown(object sender, MouseEventArgs e)  //画线
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    PointLatLng point = gMap1.FromLocalToLatLng(e.X, e.Y);
                    if (Route == null)
                    {
                        Route = new GMapMarkerRoute(point);
                        Route.Origin.X = gMap1.Size.Width / 2;
                        Route.Origin.Y = gMap1.Size.Height / 2;
                        Route.OriginOffset.X = DragOffsetX;
                        Route.OriginOffset.Y = DragOffsetY;
                        routes.Markers.Add(Route as GMapMarker);//划线

                        //Route.AddPoint();
                    }
                    Route.AddPoint(point);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //记录鼠标按下位置
                    RightBDPoint.X = e.X;
                    RightBDPoint.Y = e.Y;
                }

            }//鼠标按下左键，进行画线
            void mapControl_OnMarkerEnter(GMapMarker item)//选中边框变红
            {
                if (item is GMapMarkerImage)//自定义图标变红
                {
                    currentMarker = item as GMapMarkerImage;
                    currentMarker.Pen = new Pen(Brushes.Red, 2);
                }

                Debug.WriteLine("OnMarkerEnter: " + item.Position);
            }//OnMarkerEnter中设置选中的Marker,同时设置Pen的值,实现高亮
            void mapControl_OnMarkerLeave(GMapMarker item
            {
                if (item is GMapMarkerImage)
                {
                    currentMarker = null;
                    GMapMarkerImage m = item as GMapMarkerImage;
                    m.Pen.Color = Color.Blue;
                    Debug.WriteLine("OnMarkerLeave: " + item.Position);
                }
            })//OnMarkerLeave中取消选中的Marker，取消Pen的值，取消高亮      
            void mapControl_OnMapZoomChanged()
            {
                //在进行地图的缩放后，视图的原点会重新回到MapControl控件的中心点
                DragOffsetX = 0;
                DragOffsetY = 0;

                if (Route != null)
                {
                    Route.IsZoomChanged = true;
                }

            }//地图缩放

            #endregion
        }

        /// <summary>
        /// 实现在两点之间画线功能，输入节点的起始和终止坐标
        /// </summary>
        /// <param name="pointLatLng_S"> 节点起始坐标</param>
        /// <param name="pointLatLng_E">节点终止坐标</param>
        private void DrawrouteBetweenTowPoint(PointLatLng pointLatLng_S, PointLatLng pointLatLng_E)//输入两个坐标在地图上画线
        {
            RoutingProvider rp = gMap1.MapProvider as RoutingProvider;
            if (rp != null)
            {
                rp = GMapProviders.GoogleChinaMap;
            }
            MapRoute route = rp.GetRoute(pointLatLng_S, pointLatLng_E, false, false, (int)gMap1.Zoom);
            if (route != null)
            {
                GMapRoute r = new GMapRoute(route.Points, route.Name);
                gMapOverlay.Routes.Add(r);
                gMap1.ZoomAndCenterRoute(r);
            }
        }

        public Frm_Child_guzhang frm1;
        public BeiXianShuaiHao frm3;
        public Analysis_Pre frm;
        public Frm_Child_kongxin frm2;
        /// <summary>
        /// 主界面加载，显示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            gMap1.Visible = true;
            groupBox1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            TabControl_1.Visible = false;
            show_dataGridView();
            this.WindowState = FormWindowState.Maximized;
            frm1 = new Frm_Child_guzhang();
            frm = new Analysis_Pre();
            frm2 = new Frm_Child_kongxin();

        }


        /// <summary>
        /// 菜单“线路信息”显示
        /// </summary>
        private void show_dataGridView() //dataGridView 展示//2019年5月28日---2019年10月28日更改
        {
            dataGridView1.Rows.Clear();
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                db.Get_dataGridView1_data("select rid,rn,rl,voltage_level,cable_type,total_num_optical_cable,left_optical_cable from route1");//route表中选择数据

            }
            db.DBclose();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < route_data.rid.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.dataGridView1.Rows.Add();//索引递加
              
                this.dataGridView1.Rows[index].Cells[0].Value = route_data.rid[i];        //rid
                this.dataGridView1.Rows[index].Cells[1].Value = route_data.rn[i];      //填充路径名称
                this.dataGridView1.Rows[index].Cells[2].Value = route_data.rl[i];  //填充路径长度
                this.dataGridView1.Rows[index].Cells[3].Value = route_data.volatge_level[i];  //电压等级2
                this.dataGridView1.Rows[index].Cells[4].Value = route_data.cable_type[i];  //光缆类型
                this.dataGridView1.Rows[index].Cells[5].Value = route_data.total_num_optical_cable[i];  //光缆总芯数
                this.dataGridView1.Rows[index].Cells[6].Value = route_data.left_optical_cable[i];  //剩余纤芯数
            }
            dataGridView1.AllowUserToAddRows = false;
        }


        /// <summary>
        /// “增加节点”选项卡的已有节点显示信息
        /// </summary>
        private void show_now_pdataGridView1()//展示现有节点6-19-16:25
        {
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                db.Get_dataGridView3_nowpoint_data("select pid,lat,lng,pname,rid from point");//route表中选择数据

            }
            db.DBclose();
            show_now_point_dataGridView.Rows.Clear();
            for (int i = 0; i < point_data.pname.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.show_now_point_dataGridView.Rows.Add();//索引递加
               
                this.show_now_point_dataGridView.Rows[index].Cells[0].Value = point_data.pid[i];        //节点编号
                this.show_now_point_dataGridView.Rows[index].Cells[1].Value = point_data.lat[i];      //维度
                this.show_now_point_dataGridView.Rows[index].Cells[2].Value = point_data.lng[i];      //经度
                this.show_now_point_dataGridView.Rows[index].Cells[3].Value = point_data.pname[i];  //节点名称
                this.show_now_point_dataGridView.Rows[index].Cells[4].Value = point_data.rid[i];    //线路编号
            }
            show_now_point_dataGridView.AllowUserToAddRows = false;//不显示最后一行留白
        }


        /// <summary>
        /// “增加线路”选项卡已有线路显示信息
        /// </summary>
        private void show_now_route_dataGridView1()//展示现有节点 
        {
           
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                /* db.Get_dataGridView3_nowroute_data("select rid,rn,rl from route1");*///route表中选择数据
                //db.Get_dataGridView3_nowroute_data("select rid, rn, rl, voltage_level, cable_type, total_num_optical_cable, left_optical_cable from route1");//route1选择数据
                
                db.Get_dataGridView1_data("select rid, rn, rl, voltage_level, cable_type, total_num_optical_cable, left_optical_cable from route1");
            }
            db.DBclose();
            show_now_route_dataGridView.Rows.Clear();
            for (int i = 0; i < route_data.rn.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.show_now_route_dataGridView.Rows.Add();//索引递加
                
                this.show_now_route_dataGridView.Rows[index].Cells[0].Value = route_data.rid[i];        //线路编号
                this.show_now_route_dataGridView.Rows[index].Cells[1].Value = route_data.rn[i];      //线路名
                this.show_now_route_dataGridView.Rows[index].Cells[2].Value = route_data.rl[i];      //线路长度
                this.show_now_route_dataGridView.Rows[index].Cells[3].Value = route_data.volatge_level[i];        //线路编号
                this.show_now_route_dataGridView.Rows[index].Cells[4].Value = route_data.cable_type[i];      //线路名
                this.show_now_route_dataGridView.Rows[index].Cells[5].Value = route_data.total_num_optical_cable[i];      //线路长度
                this.show_now_route_dataGridView.Rows[index].Cells[6].Value = route_data.left_optical_cable[i];      //线路长度



            }
            show_now_route_dataGridView.AllowUserToAddRows = false;//不显示最后一行留白
        }


       /// <summary>
       /// 单击已有线路表的某行，显示该条线路上节点基本信息
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//2019年6月9日22:11修改 单击dataGridView1表中单击任意一行，显示某路径的节点信息
        {
            try

            {
                dataGridView2.Visible = true;
                ID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string route_name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                groupBox1.Text = route_name + "  线路节点详情";
                DBlink db = new DBlink();
                string sqlstr = "select route1.rid,route1.rn,point.pid,point.lng,point.lat,point.pname from point,route1 where point.rid=route1.rid and route1.id = " + ID;
                if (db.DBcon())
                {
                    db.Get_dataGridView2_data(sqlstr);
                }
                db.DBclose();

                dataGridView2.Rows.Clear();
                for (int i = 0; i < point_data.pid.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
                {
                    int index = this.dataGridView2.Rows.Add();

                    
                    this.dataGridView2.Rows[index].Cells[0].Value = route_data.rid[i];
                    this.dataGridView2.Rows[index].Cells[1].Value = route_data.rn[i];
                    //this.dataGridView2.Rows[index].Cells[2].Value = route_data.rl[i];
                
                    this.dataGridView2.Rows[index].Cells[2].Value = point_data.pid[i];
                    this.dataGridView2.Rows[index].Cells[3].Value = point_data.lng[i];
                    this.dataGridView2.Rows[index].Cells[4].Value = point_data.lat[i];
                    this.dataGridView2.Rows[index].Cells[5].Value = point_data.pname[i];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }


        /// <summary>
        /// 实现对选定的线路列表产生的节点信息，进行更新操作，可考虑删除或者保留
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string rid, urid, rn, rl, pid, puid, pname, lng, lat;

                DBlink db = new DBlink();
             
                if (dataGridView2.Rows.Count > 0)
                {
                    rid = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    urid = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    rn = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                    rl = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    pid = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                    puid = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                    lng = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                    lat = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                    pname = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();

                    //string mysqlstr = "update r_p set id= '" + rid + "',rid= '" + urid + "',rn ='" + rn + "',rl ='" + rl + "',ppid ='" + pid + "',pid = '" + puid + "',lng = '" + lng + "',lat = '" + lat + "',pname = '" + pname +"';";
                    string mysqlstr = "update route1 r,point p set r.rid='" + urid + "',r.rn ='" + rn + "',r.rl ='" + rl + "',p.id ='" + pid + "',p.lng ='" + lng + "',p.lat ='" + lat + "',p.pname ='" + pname + "' where r.id='" + rid + "' and p.id='" + pid + "'";//6.10-16:15-更新
                    if (db.DBcon())
                    {
                        if (!db.UpdataDeletAdd(mysqlstr))//修改当前用户的tag标志
                        {
                            MessageBox.Show("修改失败！");
                        }
                    }
                    db.DBclose();
                }


                show_dataGridView();


            }

            catch  //点到其他的不进行操作
            { }
        }

       
      

        #region 初始化地图时显示两个节点的画线调用函数，考虑删除
        private void DrawLineBetweenTwoPoint(PointLatLng pointLatLng_S, PointLatLng pointLatLng_E)
        {
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(pointLatLng_S);
            points.Add(pointLatLng_E);
            GMapRoute r = new GMapRoute(points, "");
            r.Stroke = new Pen(Color.Green, 1);
            gMapOverlay.Routes.Add(r);
        }
        #endregion
                
        #region 菜单栏跳转页面
        private void 增加节点ToolStripMenuItem1_Click(object sender, EventArgs e)//TabControl_增加节点
        {
            DBlink db = new DBlink();
            if (db.DBcon())  //填充Pname 数组
            {
                db.Get_route();
            }
            db.DBclose();

            已有线路显示下拉列表.Items.Clear();//需要修改
            int i;
            for (i = 0; i < route.route_list.Count; i++)
                已有线路显示下拉列表.Items.Add(route.route_list[i].ToString());
            //for (i = 0; i < route.id.Count; i++)
            //    已有线路显示下拉列表.Items.Add(route.id.ToString());

            //label10.Text = "";

            TabControl_1.Visible = true;
            dataGridView2.Visible = false;
            groupBox1.Visible = false;
            TabControl_1.SelectTab(增加节点);//跳转到tabControl指定的页面
            show_now_pdataGridView1();
        }
        private void 增加线路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DBlink db = new DBlink();
            //if (db.DBcon())   //连接数据库成功
            //{
            //    db.Get_route();
            //}
            //db.DBclose();     //避免多线程操作数据库，影响系统报错，先关闭数据连接
            TabControl_1.Visible = true;
            dataGridView2.Visible = false;
            groupBox1.Visible = false;

            TabControl_1.SelectTab(增加线路);//跳转到tabControl指定的页面
            show_now_route_dataGridView1();//显示线路信息
            cbo_dengji.Items.Clear();
            for (int i = 0; i < dengji.Length; i++)
                cbo_dengji.Items.Add(dengji[i].ToString());

            cbo_leixing.Items.Clear();
            for (int i = 0; i < leixing.Length; i++)
                cbo_leixing.Items.Add(leixing[i].ToString());
            cbo_zongxin.Items.Clear();
            for (int i = 0; i < zongxin.Length; i++)
                cbo_zongxin.Items.Add(zongxin[i]);


        }

        private void 删除线路ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TabControl_1.Visible = true;
            dataGridView2.Visible = false;
            groupBox1.Visible = false;


            DBlink db = new DBlink();//2019年6月6日22:15
            if (db.DBcon())//填充route_list数组
            {
                db.Get_route();
            }
            db.DBclose();
            已有线路显示下拉列表2.Items.Clear();
            delete_r1.Text = "已有线路：";
            for (int i = 0; i < route.route_list.Count; i++)
                已有线路显示下拉列表2.Items.Add(route.route_list[i].ToString());
            TabControl_1.SelectTab(删除线路);//跳转到tabControl指定的页面
        }

        private void 删除节点ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            groupBox1.Visible = false;
            TabControl_1.Visible = true;

            DBlink db = new DBlink();
            if (db.DBcon())  //填充Pname 数组
            {
                db.Get_point();
            }
            db.DBclose();
            已有节点显示下拉列表3.Items.Clear();
            int i;
            for (i = 0; i < point_data.pname.Count; i++)
                已有节点显示下拉列表3.Items.Add(point_data.pname[i].ToString());
            if (db.DBcon())   //连接数据库成功
            {
                #region 连接数据库Get_point应是，可修改
                db.Get_route();
                #endregion
            }
            db.DBclose();
            TabControl_1.SelectTab(删除节点);//跳转到tabControl指定的页

        }
        private void 路径信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            TabControl_1.Visible = false;
        }
        private void 节点信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            groupBox1.Visible = true;
            TabControl_1.Visible = false;
        }

        #endregion
        #region TabControl增删路径节点6-16---6-18
       

        /// <summary>
        /// “增加线路”选项卡，向route1库中增加线路信息,确认与取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 增加线路确认_Click(object sender, EventArgs e)//TabControl1 
        {
           
            if (rn1.Text == "")
                return;
            DBlink db = new DBlink();//建立新的数据连接
            Boolean tag = true;
            string str = "select rn from route1 where rn ='" + rn1.Text + "'";//sql查询语句
            string id = ID;
            if (db.DBcon())   //连接数据库成功
            {
                if (db.search(str))
                {
                    tishi.Text = "该路径已存在";
                    tag = false;
                }
            }
            db.DBclose();//避免多线程操作数据库，影响系统报错，先关闭数据连接

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
            if (tag)
            {
                str = "insert into route1(rid,rn,rl,voltage_level,cable_type,total_num_optical_cable,left_optical_cable) values ('" + rid1.Text + "','" + rn1.Text + "','" + rl1.Text + "','" + g_dengji + "','" +g_leixing + "','" +g_zongxin + "','" + txt_left_optical_cable.Text + "');";
                if (db.DBcon())   //连接数据库成功
                {
                    if (!db.UpdataDeletAdd(str))//检查能否添加数据
                    {
                        DialogResult dr = MessageBox.Show("线路添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("线路添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                db.DBclose();


                str = "select id from route1 where rn ='" + rn1.Text + "'";
                if (db.DBcon())   //连接数据库成功
                {
                    if (!db.UpdataDeletAdd(str))
                    {
                        id = db.search_id(str);
                    }
                }
                db.DBclose();
                rid1.Text = ""; //输入复位
                rn1.Text = "";
                rl1.Text = "";
                //txt_voltage_level.Text = "";
                //txt_cable_type.Text = "";
                //txt_total_num_optical_cable.Text="";
                txt_left_optical_cable.Text="";
                cbo_dengji.Text = "";
                cbo_leixing.Text = "";
                cbo_zongxin.Text = "";

                show_dataGridView();
                show_now_route_dataGridView1();
            }
        }
        private void 增加线路取消_Click(object sender, EventArgs e)
        {
            rid1.Text = "";
            dataGridView2.Visible = true;
            Form1_Load(null, null);//重新加载数据
        }


        /// <summary>
        /// “添加节点”选项卡信息，“添加”，“添加完成”，“继续添加”，“绘制”，“取消”功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 节点添加确定_Click(object sender, EventArgs e)//"添加"，添加一个节点的基本信息
        {
            try
            {
                DBlink db = new DBlink();
                string str = null;

                if (addover)
                {
                    if (point_data.pname.Contains(add_pname1.Text))
                    {
                        增加节点提示信息2.Text = "该节点已存在";

                    }
                    else
                    {

                        #region 向数据库添加点

                        str = "insert into point(pid,lat,lng,pname,rid) values('" + add_pid1.Text + "','" + add_lat1维度.Text + "','" + add_lng1经度.Text + "','" + add_pname1.Text + "','" + add_rid1.Text + "');";



                        if (db.DBcon())   //连接数据库成功
                        {
                            if (!db.UpdataDeletAdd(str))
                            {
                                DialogResult dr = MessageBox.Show("数据添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                return;
                            }
                            else
                            {
                                DialogResult dr2 = MessageBox.Show("数据添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                        }
                        db.DBclose();


                        if (db.DBcon())  //填充Pname 数组后显示
                        {
                            db.Get_point();
                        }
                        db.DBclose();

                        已有线路显示下拉列表.Items.Clear();//需要修改
                        int i;
                        for (i = 0; i < route.route_list.Count; i++)
                            已有线路显示下拉列表.Items.Add(route.route_list[i].ToString());
                        //for (i = 0; i < route.id.Count; i++)
                        //      已有线路显示下拉列表.Items.Add(route.id.ToString());
                        #endregion


                        string lng1 = add_lat1维度.Text.ToString();//6-17-22:04  添加节点//起点经度
                        string lat1 = add_lng1经度.Text.ToString();//起点维度
                                                                 //string lng2 = add_lng2经度.Text.ToString();//
                                                                 //string lng2 = add_lat2维度.Text.ToString();  已删除
                                                                 //string lat2 = add_lng2经度.Text.ToString();

                        string sname = add_pname1.Text.ToString();//6-18-22:13得到起点的名称
                                                                  //string ename = add_pname2.Text.ToString();   已删除

                        //string lat2 = add_lat2维度.Text.ToString();
                        //string lng2 = end_lng.Text.ToString();//终点经度
                        //string lat2 = end_lat.Text.ToString();//终点维度
                        #region 三个点划线 6-17 16:18---22:00修改成功   6-19-22:22分先注释掉，要恢复
                        //for (int j = 0; j < 3; j++)// 先存到
                        //{
                        //    PointLatLng[] a = new PointLatLng[10];
                        //    //PointLatLng[] b = new PointLatLng[10];
                        //    a[j] = drawLine.S_Turn_P(lng1, lat1);
                        //    //b[j] = drawLine.E_Turn_P(lng2, lat2);

                        //    //drawLine.DrawLine(a, b, gMapOverlay, gMap1);
                        //   // drawLine.DrawLine(a,b,sname,ename, gMapOverlay, gMap1);
                        //}
                        #endregion

                        a[k] = draw_Line.S_Turn_P(lng1, lat1);//将输入的节点放入a[k]中
                        string str1 = null;

                        str1 = "insert into route_point1(rid,lat,lng,pname) values('" + add_rid1.Text + "','" + add_lat1维度.Text + "','" + add_lng1经度.Text + "','" + add_pname1.Text + "');";
                        if (db.DBcon())   //连接数据库成功
                        {
                            if (!db.UpdataDeletAdd(str1))
                            {
                                DialogResult dr1 = MessageBox.Show("数据添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                return;
                            }
                            else
                            {
                                DialogResult dr3 = MessageBox.Show("数据添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                        }
                        db.DBclose();

                        p_name[k] = add_pname1.Text;
                        k++;

                        //6-17-17:30
                        //drawLine.DrawLine(lng1, lat1, lng2, lat2, gMapOverlay, gMap1);

                        add_pid1.Text = "";
                        add_lat1维度.Text = "";
                        add_lng1经度.Text = "";
                        add_pname1.Text = "";
                        add_rid1.Text = "";

                        show_now_pdataGridView1();

                    }
                }
                else { MessageBox.Show("本次添加完成！"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            //向地图图画点连线


        }

        private void Button27_Click(object sender, EventArgs e)//继续添加，添加完一条线路上节点信息
        {
            addover = true;
        }

        private void Add_over_Click(object sender, EventArgs e)////增加节点“添加完成”功能
        {

            addover = false;
            add_pid1.Text = "";
            add_lat1维度.Text = "";
            add_lng1经度.Text = "";
            add_pname1.Text = "";
            add_rid1.Text = "";
            DBlink db = new DBlink();
            try
            {
                for (int i = 0; i < k; i++)//6-20日22:00添加用于页面初始化
                {

                    pointArray1[m][i] = a[i];// 将a[i]中的坐标存入poinyArray[0][j]中//21 -11:00 
                                             //string str = "insert into values pointArray[m][i]";
                                             //if (db.DBcon())   //连接数据库成功
                                             //{
                                             //    if (!db.UpdataDeletAdd(str))
                                             //    {
                                             //        DialogResult dr = MessageBox.Show("数据添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                             //        return;
                                             //    }
                                             //    else
                                             //    {
                                             //        DialogResult dr2 = MessageBox.Show("数据添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                             //    }
                                             //}
                                             //db.DBclose();

                }
                m++;
                Array.Clear(a, 0, a.Length);
               // k = 0; //添加完成后将计数重置为零,此时k=0;就没法画线了
            }//
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        bool drawLined = false;
        private void DrawLine_Click(object sender, EventArgs e)//添加完一个线路节点添加节点“绘制”功能
        {

            //drawLine.DrawLine(a, k, gMapOverlay, gMap1);
            draw_Line.DrawLine(a, k, p_name, gMapOverlay, gMap1);
            drawLined = true;
            k = 0;
        }

        private void 增加节点取消1_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);     //重新加载数据
            dataGridView2.Visible = true;

        }


        /// <summary>
        /// “删除线路”选项卡，向route1库中删除线路信息,确认与取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除线路确认_Click_1(object sender, EventArgs e)//TabControl1 6.16-10:48
        {
            if (待删除线路.Text == "路径名")
                return;
            DBlink db = new DBlink();
            if (db.DBcon())//填充route_list数组
            {
                db.Get_route();
            }
            db.DBclose();
            delete_r1.Text = "已有线路：";
            已有线路显示下拉列表2.Items.Clear();//
            for (int i = 0; i < route.route_list.Count; i++)
                已有线路显示下拉列表2.Items.Add(route.route_list[i].ToString()); ;

            string str = "delete from route1 where rn='" + 待删除线路.Text + "'";//从数据库中删除节点的名称

            if (db.DBcon())//连接数据库成功
            {
                if (!db.UpdataDeletAdd(str))
                {
                    DialogResult dr = MessageBox.Show("删除失败", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    DialogResult dr = MessageBox.Show("删除成功", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            db.DBclose();//避免多线程操作数据库，影响系统报错，先关闭数据库连接
            待删除线路.Text = "";

        }

        private void 删除线路取消_Click_1(object sender, EventArgs e)
        {
            待删除线路.Text = "路径名";
            Form1_Load(null, null);//重新加载数据
            dataGridView2.Visible = true;
        }

        private void 已有线路显示下拉列表2_SelectedIndexChanged(object sender, EventArgs e)//6-16-10；58
        {
            try
            {
                待删除线路.Text = 已有线路显示下拉列表2.Text;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        
        /// <summary>
        /// “删除节点”选项卡，“确定”删除和“取消”删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除节点确定_Click_1(object sender, EventArgs e)//TabControl1 6.16-11:20
        {
            try
            {
                DBlink db = new DBlink();
                string str = null;
                string str1 = null;
                str = "delete from point where pname ='" + 输入待删除节点.Text + "'";
                str1 = "delete from route_point1 where pname ='" + 输入待删除节点.Text + "'";
                if (db.DBcon())   //连接数据库成功
                {
                    if (db.UpdataDeletAdd(str))
                    {
                        DialogResult dr = MessageBox.Show("数据删除成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("数据删除失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                db.DBclose();
                if (db.DBcon())   //连接数据库成功
                {
                    if (db.UpdataDeletAdd(str1))
                    {
                        DialogResult dr = MessageBox.Show("数据删除成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("数据删除失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                db.DBclose();

                if (db.DBcon())  //填充Pname 数组 删除后显示combox1.text
                {
                    db.Get_point();
                }
                db.DBclose();

                已有节点显示下拉列表3.Items.Clear();//清空再刷新
                int i;
                for (i = 0; i < point_data.pname.Count; i++)
                    已有节点显示下拉列表3.Items.Add(point_data.pname[i].ToString());



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private void 已有节点显示下拉列表3_SelectedIndexChanged(object sender, EventArgs e)//待删除已有节点显示
        {
            try
            {
                输入待删除节点.Text = 已有节点显示下拉列表3.Text;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 删除节点取消_Click(object sender, EventArgs e)
        {
            输入待删除节点.Text = "";
            Form1_Load(null, null);     //重新加载数据
        }
        bool chushi = false;
        /// <summary>
        /// 实现“线路初始化”，把数据库中的线路绘制出来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Route_init_Click(object sender, EventArgs e)//线路初始化
        {
            if(!gMapOverlay.IsVisibile)
            {
                gMapOverlay.IsVisibile = true;
            }

            if (!chushi|| drawLined)
            {
                //6-28 15 20-修改
                int[] rid = new int[50];//定义一个整型数组存线路编号
                string[] pname = new string[50];//定义字符型的存名字 

                int j;
                int n;
                int temp = 0;
                int count = 0;
                int[] count1 = new int[20];
                DBlink db = new DBlink();
                string lng1, lat1;
                if (db.DBcon())  //得到id序列
                {
                    db.Get_rid();
                }
                db.DBclose();
                for (int i = 0; i < r_p.rid.Count; i++)//遍历画线
                {
                    try
                    {
                        rid[i] = Convert.ToInt32(r_p.rid[i]);
                        if (db.DBcon())  //填充id 数组
                        {
                            db.Get_lng_lat_pname(rid[i]);//得到经度维度，节点名称
                        }
                        db.DBclose();

                        for (j = temp, n = 0; j < r_p.pname.Count && rid[i] == Convert.ToInt32(r_p.rid[i]); j++, n++)
                        {
                            lng1 = Convert.ToString(r_p.lng[j]);
                            lat1 = Convert.ToString(r_p.lat[j]);
                            b[j] = draw_Line.S_Turn_P(lng1, lat1);//a[j]存放某一条线路上坐标
                            a[n] = b[j];
                            pname[n] = Convert.ToString(r_p.pname[j]);

                        }

                        temp = j;//2019年6月28日20:53
                        count1[i] = r_p.pname.Count;//取中间值
                        if (i > 0) { count = count1[i] - count1[i - 1]; }//6.29--17:05 修改，使count值为该行的数
                        else if (i == 0) { count = count1[0]; }
                        draw_Line.DrawLine(a, count, pname, gMapOverlay, gMap1);//画a[j]中的坐标
                        Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
                    }

                    catch
                    {

                    }
                }

                Array.Clear(a, 0, a.Length);
                Array.Clear(count1, 0, count1.Length);
                Array.Clear(b, 0, b.Length);
                Array.Clear(pname, 0, pname.Length);
                Array.Clear(rid, 0, rid.Length);
                chushi = true;
            }

        }



        /// <summary>
        /// 已有线路显示下拉列表，选项，未找到，建议删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 已有线路显示下拉列表_SelectedIndexChanged(object sender, EventArgs e)//6-19-15:55. 
        {

            try
            {
                string id;
                DBlink db = new DBlink();
                string str = "select id from route1 where rn ='" + 已有线路显示下拉列表.Text + "'";
                if (db.DBcon())   //连接数据库成功
                {
                    if (!db.UpdataDeletAdd(str))
                    {
                        id = db.search_id(str);
                        add_rid1.Text = id;
                        //add_rid2.Text = id;
                    }
                }
                db.DBclose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        
        #region 键盘操作地图，放大缩小功能+ -
        private void ButtonZoomUp_Click_Click(object sender, EventArgs e)
        {
            gMap1.Zoom = ((int)gMap1.Zoom) + 1;
        }
        private void ButtonZoomDown_Click(object sender, EventArgs e)
        {
            gMap1.Zoom = ((int)(gMap1.Zoom + 0.99)) - 1;
        }

        // key-press events
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gMap1.Focused)
            {
                if (e.KeyChar == '+')
                {
                    gMap1.Zoom = ((int)gMap1.Zoom) + 1;
                }
                else if (e.KeyChar == '-')
                {
                    gMap1.Zoom = ((int)(gMap1.Zoom + 0.99)) - 1;
                }
                else if (e.KeyChar == 'a')
                {
                    gMap1.Bearing--;
                }
                else if (e.KeyChar == 'z')
                {
                    gMap1.Bearing++;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            int offset = -22;

            if (e.KeyCode == Keys.Left)
            {
                gMap1.Offset(-offset, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                gMap1.Offset(offset, 0);
            }
            else if (e.KeyCode == Keys.Up)
            {
                gMap1.Offset(0, -offset);
            }
            else if (e.KeyCode == Keys.Down)
            {
                gMap1.Offset(0, offset);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                
            }
        }
        
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gMap1.Focused)
            {
                if (e.KeyChar == '+')
                {
                    gMap1.Zoom = ((int)gMap1.Zoom) + 1;
                }
                else if (e.KeyChar == '-')
                {
                    gMap1.Zoom = ((int)(gMap1.Zoom + 0.99)) - 1;
                }
                else if (e.KeyChar == 'a')
                {
                    gMap1.Bearing--;
                }
                else if (e.KeyChar == 'z')
                {
                    gMap1.Bearing++;
                }
            }
        }
        #endregion
        #region 关闭TabControl1选项卡
        private void Quit_Tabcontrol_Click(object sender, EventArgs e)
        {
            TabControl_1.Visible = false;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            TabControl_1.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TabControl_1.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            TabControl_1.Visible = false;
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            TabControl_1.Visible = false;
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            TabControl_1.Visible = false;
        }

        #endregion
        # region 选项卡选择功能
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == 增加线路)
            {
                TabControl_1.Visible = true;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;

                //tabControl1.SelectTab(增加线路);//跳转到tabControl指定的页面
                show_now_route_dataGridView1();
                cbo_dengji.Items.Clear();
                for (int i = 0; i < dengji.Length; i++)
                    cbo_dengji.Items.Add(dengji[i].ToString());

                cbo_leixing.Items.Clear();
                for (int i = 0; i < leixing.Length; i++)
                    cbo_leixing.Items.Add(leixing[i].ToString());
                cbo_zongxin.Items.Clear();
                for (int i = 0; i < zongxin.Length; i++)
                    cbo_zongxin.Items.Add(zongxin[i]);

            }
            if (e.TabPage == 删除线路)
            {
                TabControl_1.Visible = true;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;


                DBlink db = new DBlink();//2019年6月6日22:15
                if (db.DBcon())//填充route_list数组
                {
                    db.Get_route();
                }
                db.DBclose();
                已有线路显示下拉列表2.Items.Clear();
                delete_r1.Text = "已有线路：";
                for (int i = 0; i < route.route_list.Count; i++)
                    已有线路显示下拉列表2.Items.Add(route.route_list[i].ToString());
                //tabControl1.SelectTab(删除线路);//跳转到tabControl指定的页面//方法1
            }
            if (e.TabPage == 增加节点)
            {
                DBlink db = new DBlink();
                if (db.DBcon())  //填充Pname 数组
                {
                    db.Get_route();
                }
                db.DBclose();

                已有线路显示下拉列表.Items.Clear();//需要修改
                int i;
                for (i = 0; i < route.route_list.Count; i++)
                    已有线路显示下拉列表.Items.Add(route.route_list[i].ToString());

                TabControl_1.Visible = true;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;
                //tabControl1.SelectTab(增加节点);//跳转到tabControl指定的页面
                show_now_pdataGridView1();
            }
            if (e.TabPage == 删除节点)
            {
                dataGridView2.Visible = false;
                groupBox1.Visible = false;
                TabControl_1.Visible = true;

                DBlink db = new DBlink();
                if (db.DBcon())  //填充Pname 数组
                {
                    db.Get_point();
                }
                db.DBclose();
                已有节点显示下拉列表3.Items.Clear();
                int i;
                for (i = 0; i < point_data.pname.Count; i++)
                    已有节点显示下拉列表3.Items.Add(point_data.pname[i].ToString());
                if (db.DBcon())   //连接数据库成功
                {
                    #region 连接数据库Get_point应是，可修改
                    db.Get_route();
                    #endregion
                }
                db.DBclose();
                //tabControl1.SelectTab(删除节点);//跳转到tabControl指定的页
            }
            if (e.TabPage == 录入光功率)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;
                TabControl_1.Visible = true;

                DBlink db = new DBlink();
                if (db.DBcon())  //填充Pname 数组
                {
                    db.Get_route();
                }
                db.DBclose();

                已有光功率线路下拉列表.Items.Clear();//需要修改
                int i;
                for (i = 0; i < route.route_list.Count; i++)
                    已有光功率线路下拉列表.Items.Add(route.route_list[i].ToString());
                //tabControl1.SelectTab(删除节点);//跳转到tabControl指定的页
            }

            if (e.TabPage == 查看线路和节点)
            {
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;
                TabControl_1.Visible = true;
                
                groupBox1.Visible = true;
            }
            if (e.TabPage == 录入故障信息)
            {
                TabControl_1.Visible = true;
                DBlink db = new DBlink();
                if (db.DBcon())  //填充Pname 数组
                {
                    db.Get_route();
                }
                db.DBclose();

                cbo_guzhangroute.Items.Clear();
                
                for (int i = 0; i < route.route_list.Count; i++)
                    cbo_guzhangroute.Items.Add(route.route_list[i].ToString());

                cbo_guzhangleixing.Items.Clear();
                for (int i = 0; i < guzhang_type.Length; i++)
                    cbo_guzhangleixing.Items.Add(guzhang_type[i].ToString());


                TabControl_1.Visible = true;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;
                TabControl_1.SelectTab(录入故障信息);//跳转到tabControl指定的页面

            }
        }
        #endregion

        /// <summary>
        /// 清理图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Marker_Click(object sender, EventArgs e)//清理图标
        {

            //gMap1.Overlays.Add(gMapOverlay);

            //gMap1.Overlays.Clear();

            gMapOverlay.IsVisibile = false;

            //gMapOverlay.Routes.Clear();//清理路径
            // routes.Routes.Clear();

        }

        private void Delete_Route_Click(object sender, EventArgs e)//清理线路
        {
            gMapOverlay.IsVisibile = false;
            //foreach (var c in CountryStatus)
            //    if (tcpRoutes.TryGetValue(tcp.Key, out route))
            //{
            //    routes.Routes.Remove(route);
            //}
            //gMapOverlay.Routes.Remove();
            //sessions.Clear();
            //sessions = null;
            //gMap1.Overlays.Add(gMapOverlay);
            //gMap1.Overlays.Clear();
        }


        #region 添加光功率2019年10月24日
        private void 光功率录入确认_Click(object sender, EventArgs e)
        {
            try{
                DBlink db = new DBlink();
                string str = null;
                str = "insert into optical_power values('" + add_rid2.Text + "','" + add_rn2.Text + "','" + add_dB.Text + "','" + add_day.Text + "');";//向光功率表中插入数据
               
                if (db.DBcon())   //连接数据库成功
                {
                    if (!db.UpdataDeletAdd(str))
                    {
                        DialogResult dr = MessageBox.Show("数据添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    else
                    {
                        DialogResult dr2 = MessageBox.Show("数据添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                db.DBclose();

                if (db.DBcon())//填充route_list数组
                {
                    db.Get_route();
                }
                db.DBclose();
                已有光功率线路下拉列表.Items.Clear();//清空
                int j;
                for (j = 0;j< route.route_list.Count;j++)
                    已有光功率线路下拉列表.Items.Add(route.route_list[j].ToString());
                
                add_rid2.Text = "";//输入复位
                add_rn2.Text = "";
                add_dB.Text = "";
                add_day.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            add_day.Text = dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Day.ToString();
        }
        private void 已有光功率线路下拉列表_SelectedIndexChanged(object sender, EventArgs e)
        {
            add_rn2.Text=已有光功率线路下拉列表.Text;
            string id;
            DBlink db=new DBlink();
            string str = "select id from route1 where rn ='" + 已有光功率线路下拉列表.Text + "'";
            if (db.DBcon())   //连接数据库成功
            {
                if (!db.UpdataDeletAdd(str))
                {
                    id = db.search_id(str);
                    //add_rid1.Text = id;
                    add_rid2.Text = id;
                }
            }
            db.DBclose();
        }
        private void 光功率录入ToolStripMenuItem_Click(object sender, EventArgs e)//2019年10月24日
        {
            DBlink db = new DBlink();
            if (db.DBcon())  //填充Pname 数组
            {
                db.Get_route();
            }
            db.DBclose();

            已有光功率线路下拉列表.Items.Clear();//需要修改
            int i;
            for (i = 0; i < route.route_list.Count; i++)
                已有光功率线路下拉列表.Items.Add(route.route_list[i].ToString());
            //for (i = 0; i < route.id.Count; i++)
            //    已有线路显示下拉列表.Items.Add(route.id.ToString());

            //label10.Text = "";

            TabControl_1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView1.Visible = false;
            groupBox1.Visible = false;
            TabControl_1.SelectTab(录入光功率);//跳转到tabControl指定的页面
        }
        #endregion


        private void 查看线路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            TabControl_1.Visible = true;
            TabControl_1.SelectTab(查看线路和节点);//跳转到tabControl指定的页面
        }

        private void 查看节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            TabControl_1.Visible = true;
            TabControl_1.SelectTab(查看线路和节点);
        }

        private void 统计分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void GMap1_Load(object sender, EventArgs e)
        {
            //gMapOverlay.Markers.Clear();//清除标记
            //gMapOverlay.Routes.Clear();//清除线路
            ////6-28 15 20-修改
            //int[] rid = new int[20];//定义一个整型数组存线路编号
            //string[] pname = new string[20];//定义字符型的存名字 
            //int j;
            //int n;
            //int temp = 0;
            //int count = 0;
            //int[] count1 = new int[20];
            //DBlink db = new DBlink();
            //string lng1, lat1;
            //if (db.DBcon())  //得到id序列
            //{
            //    db.Get_rid();
            //}
            //db.DBclose();
            //for (int i = 0; i < r_p.rid.Count; i++)//遍历画线
            //{
            //    try
            //    {
            //        rid[i] = Convert.ToInt32(r_p.rid[i]);
            //        if (db.DBcon())  //填充id 数组
            //        {
            //            db.Get_lng_lat_pname(rid[i]);//得到经度维度，节点名称
            //        }
            //        db.DBclose();

            //        for (j = temp, n = 0; j < r_p.pname.Count && rid[i] == Convert.ToInt32(r_p.rid[i]); j++, n++)
            //        {
            //            lng1 = Convert.ToString(r_p.lng[j]);
            //            lat1 = Convert.ToString(r_p.lat[j]);
            //            b[j] = draw_Line.S_Turn_P(lng1, lat1);//a[j]存放某一条线路上坐标
            //            a[n] = b[j];
            //            pname[n] = Convert.ToString(r_p.pname[j]);

            //        }

            //        temp = j;//2019年6月28日20:53
            //        count1[i] = r_p.pname.Count;//取中间值
            //        if (i > 0) { count = count1[i] - count1[i - 1]; }//6.29--17:05 修改，使count值为该行的数
            //        else if (i == 0) { count = count1[0]; }
            //        draw_Line.DrawLine(a, count, pname, gMapOverlay, gMap1);//画a[j]中的坐标
            //        Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
            //    }
            //    catch
            //    {

            //    }
            //}
            
        }

        private void SplitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {

        }

        private void 查看线路和节点_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 空芯率统计分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (frm1 != null)
            {
                if (frm1.IsDisposed)
                    frm1 = new Frm_Child_guzhang();//如果已经销毁，则重新创建子窗口对象
                frm1.Show();
                
            }
            else
            {
                frm1 = new Frm_Child_guzhang();
                frm1.Show();
                
            }
         //   frm1.Show();
           
        }

        private void 故障统计分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm1 != null)
            {
                if (frm1.IsDisposed)
                    frm1 = new Frm_Child_guzhang();//如果已经销毁，则重新创建子窗口对象
                frm1.Show();

            }
            else
            {
                frm1 = new Frm_Child_guzhang();
                frm1.Show();

            }
            //frm1.Show();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm1.Close();
            frm.Close();
        }

        private void Cbo_dengji_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               g_dengji = cbo_dengji.Text;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Cbo_leixing_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                g_leixing = cbo_leixing.Text;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Cbo_zongxin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                g_zongxin = Convert.ToInt32(cbo_zongxin.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 录入故障_Click(object sender, EventArgs e)
        {
            try
            {
                DBlink db = new DBlink();
                string str ;
                try
                {
                    str = "insert into guzhang values('" + txt_addrid.Text + "','" + txt_addrn.Text + "','" + txt_addday.Text + "','" + guzhang_type1 + "')";//向故障表添加数据

                    if (db.DBcon())   //连接数据库成功
                    {
                        if (!db.UpdataDeletAdd(str))
                        {
                            DialogResult dr = MessageBox.Show("数据添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        else
                        {
                            DialogResult dr2 = MessageBox.Show("数据添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                    }
                    db.DBclose();
                }
                catch (System.Exception ex) {
                    MessageBox.Show("录入信息有误请检查输入","错误提示！");
                }

                if (db.DBcon())//填充route_list数组
                {
                    db.Get_route();
                }
                db.DBclose();
                cbo_guzhangroute.Items.Clear();//清空
                int j;
                for (j = 0; j < route.route_list.Count; j++)
                    cbo_guzhangroute.Items.Add(route.route_list[j].ToString());

                txt_addrid.Text = "";//输入复位
                txt_addrn.Text = "";
                txt_addday.Text = "";
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private void Cbo_guzhangleixing_SelectedIndexChanged(object sender, EventArgs e)
        {

            guzhang_type1 = cbo_guzhangleixing.Text;

        }

        private void 故障信息录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {

          
            DBlink db = new DBlink();
            if (db.DBcon())  //填充Pname 数组
            {
                db.Get_route();
            }
            db.DBclose();

            cbo_guzhangroute.Items.Clear();
            for (int i = 0; i < route.route_list.Count; i++)
                cbo_guzhangroute.Items.Add(route.route_list[i].ToString());

            cbo_guzhangleixing.Items.Clear();
            for (int i = 0; i < guzhang_type.Length; i++)
                cbo_guzhangleixing.Items.Add(guzhang_type[i].ToString());


            TabControl_1.Visible = true;
            dataGridView2.Visible = false;
            groupBox1.Visible = false;
            TabControl_1.SelectTab(录入故障信息);//跳转到tabControl指定的页面


        }

        

        private void 光功率录入取消_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }

        private void 录入故障取消_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }

        private void Cbo_guzhangroute_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_addrn.Text = cbo_guzhangroute.Text;
            string id;
            DBlink db = new DBlink();
            string str = "select id from route1 where rn ='" + cbo_guzhangroute.Text + "'";
            if (db.DBcon())   //连接数据库成功
            {
                if (!db.UpdataDeletAdd(str))
                {
                    id = db.search_id(str);
                    //add_rid1.Text = id;
                    txt_addrid.Text = id;
                }
            }
            db.DBclose();
        }
      
        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //txt_addday.Text = dateTimePicker2.Value.Year.ToString() + dateTimePicker2.Value.Month.ToString() + dateTimePicker2.Value.Day.ToString();
       
            txt_addday.Text =dateTimePicker2.Value.Date.ToString();

        }

        private void GroupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void 备纤衰耗分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm3 != null)
            {
                if (frm3.IsDisposed)
                    frm3 = new BeiXianShuaiHao();//如果已经销毁，则重新创建子窗口对象
                frm3.Show();

            }
            else
            {
                frm3 = new BeiXianShuaiHao();
                frm3.Show();

            }
        }

        private void 光功率预测toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (frm != null)
            {
                if (frm.IsDisposed)
                    frm = new Analysis_Pre();//如果已经销毁，则重新创建子窗口对象
                frm.Show();

            }
            else
            {
                frm = new Analysis_Pre();
                frm.Show();

            }
            //frm.Show();

        }

        

       

        #endregion






    }
}

