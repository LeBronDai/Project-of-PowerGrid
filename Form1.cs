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

using WindowsFormsApp1.数据实体类;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static string ID;
        public double[] result=new double[100];
        private GMapOverlay gMapOverlay = new GMapOverlay("markers");//放置markers图层
        #region 路径加点
        private GMapOverlay routes = new GMapOverlay("routes");//放置路径图层
        private List<PointLatLng> RoutePoints = new List<PointLatLng>();//需要绘制的经纬度点集
        private List<Point> Point = new List<Point>();//用户绘制在视窗中的点，是将经纬度转换成GPoint再加上偏移处理后的点
        private bool HasNewPoint = false;// 是否有新的点加入
        private GMapMarkerRoute Route = null;
        private Point RightBDPoint;
        private Timer blinkTimer = new Timer();
        private Point BeforeZoomChangeMousePoint = new Point();
        private PointLatLng NewPointLatLng;//新加入点的经纬度
        #endregion
       // public delegate void RouteEnter(GMapRoute item);
      //  public delegate void RouteEnter(GMapMarkerRoute item);
        

        private GMapMarkerImage currentMarker; //建立GmapMakeImage 对象
        private GMapMarkerRoute currentRoute;  //建立currentRoute对象

        private Boolean addr = false;//控制手动填数还是点dataGridview填数
        private Boolean addover = true;//添加节点可添加
        private 绘制点和线 draw_Line = new 绘制点和线();
        private List<List<PointLatLng>> pointArray= new List<List<PointLatLng>>();//  动态创建一个二维的列表
        private PointLatLng[][] pointArray1 = new PointLatLng[100][];
        
        private int route_max;//路径的最大数目，初始化时使用


        private int DragOffsetX = 0, DragOffsetY = 0;
        private bool isLeftButtonDown = false; //鼠标左点积为空


       
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
            
            //地图由三层组成。最上层:GMapMarker，中间层:GMapOverlay，最底层:GMapControls　


            //起点水滴标记

            string s1 = start_lat.Text.Trim();//获取文本输入，出去文本框前后空白
            string s2 = start_lng.Text.Trim();//获取文本输入，出去文本框前后空白
            string s3 = end_lat.Text.Trim();//获取文本输入，出去文本框前后空白
            string s4 = end_lng.Text.Trim();//获取文本输入，出去文本框前后空白
            Double.TryParse(s1, out result[1]);
            Double.TryParse(s2, out result[2]);
            Double.TryParse(s3, out result[3]);
            Double.TryParse(s4, out result[4]);

            

            PointLatLng start = new PointLatLng(40.8456789180537, 111.618702219065);  //路径起点
            //PointLatLng start = new PointLatLng(result[1], result[2]);  //路径起点
            GMapMarker gMapMarker = new GMarkerGoogle(start, GMarkerGoogleType.green);
            gMapOverlay.Markers.Add(gMapMarker);　　                    //向图层中添加标记 
            gMap.Overlays.Add(gMapOverlay);　　                         //向控件中添加图层  
           
            //终点水滴标记
            // PointLatLng end = new PointLatLng(result[3], result[4]);   //路径终点
            PointLatLng end = new PointLatLng(40.8056789180537, 111.708702219065);   //路径终点
            gMapMarker = new GMarkerGoogle(end, GMarkerGoogleType.blue);
            gMapOverlay.Markers.Add(gMapMarker);                      //向图层中添加标记  


            gMap1.Overlays.Add(gMapOverlay);                          //向控件中添加图层 
            gMap1.Overlays.Add(routes);
           
            this.DrawLineBetweenTwoPoint(start, end);
            this.DrawrouteBetweenTowPoint(start, end);
            
            //鼠标事件-图层事件
            this.gMap1.MouseClick += new MouseEventHandler(mapControl_MouseClick);
            this.gMap1.MouseDoubleClick += new MouseEventHandler(mapControl_MouseDoubleClick);
            this.gMap1.MouseDown += new MouseEventHandler(mapControl_MouseDown); //鼠标下移
            this.gMap1.MouseUp += new MouseEventHandler(mapControl_MouseUp);
            this.gMap1.MouseMove += new MouseEventHandler(mapControl_MouseMove);                                                                    //this.gMap.MouseUp += new MouseEventHandler(mapControl_MouseUp);
            this.gMap1.OnMapZoomChanged += new MapZoomChanged(mapControl_OnMapZoomChanged);

            //图标事件
            this.gMap1.OnMarkerEnter += new MarkerEnter(mapControl_OnMarkerEnter);
            this.gMap1.OnMarkerLeave += new MarkerLeave(mapControl_OnMarkerLeave);
            for (int i = 0; i < 100; i++)
            {
                pointArray1[i] = new PointLatLng[100];//初始化了一个100*100的方阵6-21 14:00
            }

            #region 路径事件---未实现：mapControl_OnRouteEnter没有与委托RouteEnter重载 
          //  this.gMap.OnRouteEnter += new RouteEnter(mapControl_OnRouteEnter);
            #endregion
            void mapControl_MouseClick(object sender, MouseEventArgs e)//鼠标点击
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left) //鼠标右键.
                {
                    //gMapOverlay.Markers.Clear();//清理图层
                    PointLatLng point = gMap1.FromLocalToLatLng(e.X, e.Y);
                    Bitmap bitmap = Bitmap.FromFile("H:\\研究生学习\\电网项目资源\\图标1集合\\gur-project-13.png") as Bitmap;
                    //GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.arrow);//系统自带的绿色图标
                    GMapMarker marker = new GMapMarkerImage(point, bitmap);//自定义图标显示
                    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    string s = "address:";
                    marker.ToolTipText = string.Format("{0},{1},{2}", point.Lat, point.Lng,s);//生成坐标
                    gMapOverlay.Markers.Add(marker);
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right) //鼠标右键.
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }

            }
            //未实现出来

            /// <summary>
            /// 地图拖拽向量
            /// 在进行地图的缩放后需要将该偏移量清零
            /// </summary>
            
            void mapControl_MouseUp(object sender, MouseEventArgs e)     //鼠标上移
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

            }
            void mapControl_MouseMove(object sender, MouseEventArgs e)//鼠标移动，生成对应地坐标
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left && isLeftButtonDown)
                {
                    if (currentMarker != null)
                    {
                        PointLatLng point = gMap1.FromLocalToLatLng(e.X, e.Y);
                        currentMarker.Position = point;
                        currentMarker.ToolTipText = string.Format("{0},{1}", point.Lat, point.Lng);
                    }
                }
            }
            #region 左键画线需要修改
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

            }
            #endregion

            void mapControl_MouseDoubleClick(object sender, MouseEventArgs e)//双击生成圆圈
            {
                //var cc = new GMapMarkerCircle(gMap.FromLocalToLatLng(e.X, e.Y));
                //objects.Markers.Add(cc);
                //双击打开菜单栏选项
            }
            void mapControl_OnMarkerEnter(GMapMarker item)//选中边框变红
            {
                if (item is GMapMarkerImage)//自定义图标变红
                {
                    currentMarker = item as GMapMarkerImage;
                    currentMarker.Pen = new Pen(Brushes.Red, 2);
                }

                Debug.WriteLine("OnMarkerEnter: " + item.Position);
            }//OnMarkerEnter中设置选中的Marker,同时设置Pen的值,实现高亮
            void mapControl_OnRouteEnter(GMapMarkerRoute item)
            {
                string s = "route information:";
               
                item.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                item.ToolTipText = string.Format("{0}",s);
                currentRoute = item as GMapMarkerRoute;
                item.pen = new Pen(Brushes.Yellow, 2); ;
                Debug.WriteLine("OnRouteEnter: " + item.Position);
            }
            void mapControl_OnMarkerLeave(GMapMarker item)//OnMarkerLeave中取消选中的Marker，取消Pen的值，取消高亮
            {
                if (item is GMapMarkerImage)
                {
                    currentMarker = null;
                    GMapMarkerImage m = item as GMapMarkerImage;
                    m.Pen.Color = Color.Blue;
                    Debug.WriteLine("OnMarkerLeave: " + item.Position);
                }
            }
            void mapControl_OnMapZoomChanged()
            {
                //在进行地图的缩放后，视图的原点会重新回到MapControl控件的中心点
                DragOffsetX = 0;
                DragOffsetY = 0;

                if (Route != null)
                {
                    Route.IsZoomChanged = true;
                }

            }


        }




        private void GMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void 经度_Click(object sender, EventArgs e)
        {

        }

       
        //画出两点之间的线路
        private void DrawrouteBetweenTowPoint(PointLatLng pointLatLng_S, PointLatLng pointLatLng_E)
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

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #region 调用子窗体
        private void Button1_Click(object sender, EventArgs e)
        {
            Frm_Child frm = new Frm_Child();//创建子窗体对象

            //frm.TopLevel = false;
            //frm.TopMost = true;
            //frm.Dock = DockStyle.Fill;
            frm.ShowDialog();
            // frm.MdiParent = this;           //设置子窗体的父窗体为当前窗体
            //frm.WindowState = FormWindowState.Maximized;

            // frm.ShowDialog();//显示子窗体

        }
        #endregion

       

        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

      

        private void Btn_Add_Click_Click(object sender, EventArgs e)//SQL查询 展示出来
        {
            {
                MySqlConnection myconn = null;
                MySqlCommand mycom = null;
                //MySqlDataAdapter myrec = null;
                myconn = new MySqlConnection("Host =localhost;Database=MapPoint;Username=root;Password=123");//数据库连接
                myconn.Open();
                mycom = myconn.CreateCommand();
                mycom.CommandText = "select * from point,route1 where point.rid=route1.rid";//两个表连接操作
                MySqlDataAdapter adap = new MySqlDataAdapter(mycom);
                System.Data.DataSet ds = new System.Data.DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                string sql = string.Format("select * from point,route1 where point.rid=route1.rid");
                mycom.CommandText = sql;//获取或设置要对数据源执行的Transact-SQL语句
                mycom.CommandType = System.Data.CommandType.Text;//CommandType获取或设置一个值该值只是如何介数CommandText属性
                MySqlDataReader sdr = mycom.ExecuteReader();
                int i = 0;
                while (sdr.Read())
                {
                    listView1.Items.Add(sdr[0].ToString());
                    listView1.Items[i].SubItems.Add(sdr[1].ToString());
                    i++;
                }
                myconn.Close();
            }

       
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMap1.Visible = true;
            groupBox1.Visible = true;
           
          
            
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            tabControl1.Visible = false;
            bindListCiew();
            show_dataGridView();
           
        }
        //2019年5月28日
        private void show_dataGridView() //dataGridView 展示
        {
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                db.Get_dataGridView1_data("select * from route1");//route表中选择数据

            }
            db.DBclose();
            dataGridView1.Rows.Clear();
            for (int i = 0; i <route_data.id.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.dataGridView1.Rows.Add();//索引递加
                //if (uint_data.tf_audit[i].Equals("未审计"))
                //{
                //    this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                //}
                this.dataGridView1.Rows[index].Cells[0].Value = route_data.id[i];        //id
                this.dataGridView1.Rows[index].Cells[1].Value = route_data.rid[i];      //rid
                this.dataGridView1.Rows[index].Cells[2].Value = route_data.rn[i];      //填充路径名称
                this.dataGridView1.Rows[index].Cells[3].Value = route_data.rl[i];  //填充路径长度
            }
        }

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
                //if (uint_data.tf_audit[i].Equals("未审计"))
                //{
                //    this.show_now_point_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                //}
                this.show_now_point_dataGridView.Rows[index].Cells[0].Value = point_data.pid[i];        //节点编号
                this.show_now_point_dataGridView.Rows[index].Cells[1].Value = point_data.lat[i];      //维度
                this.show_now_point_dataGridView.Rows[index].Cells[2].Value = point_data.lng[i];      //经度
                this.show_now_point_dataGridView.Rows[index].Cells[3].Value = point_data.pname[i];  //节点名称
                this.show_now_point_dataGridView.Rows[index].Cells[4].Value = point_data.rid[i];    //线路编号
            }

        }

        private void show_now_route_dataGridView1()//展示现有节点6-19-17:05 
        {
            DBlink db = new DBlink();
            if (db.DBcon())
            {
                db.Get_dataGridView3_nowroute_data("select rid,rn,rl from route1");//route表中选择数据

            }
            db.DBclose();
           show_now_route_dataGridView.Rows.Clear();
            for (int i = 0; i < route_data.rn.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.show_now_route_dataGridView.Rows.Add();//索引递加
                //if (uint_data.tf_audit[i].Equals("未审计"))
                //{
                //    this.show_now_route_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                //}
                this.show_now_route_dataGridView.Rows[index].Cells[0].Value = route_data.rid[i];        //线路编号
                this.show_now_route_dataGridView.Rows[index].Cells[1].Value = route_data.rn[i];      //线路名
                this.show_now_route_dataGridView.Rows[index].Cells[2].Value = route_data.rl[i];      //线路长度
               
            }

        }

        //单击dataGridView1表中单击任意一行，显示某路径的节点信息
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//2019年6月9日22:11修改
        {
            try

            { 
                dataGridView2.Visible = true;
                ID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string route_name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                groupBox1.Text = route_name + "  路径情况";

                //label5.Text = route_name;

                DBlink db = new DBlink();
               
                    string sqlstr = "select route1.id,route1.rid,route1.rn,route1.rl,point.id,point.pid,point.lng,point.lat,point.pname from point,route1 where point.rid=route1.rid and route1.id = " + ID;
                    //string sqlstr = "select * from point,route1 where point.rid=route1.rid;";
                    if (db.DBcon())
                    {
                        db.Get_dataGridView2_data(sqlstr);
                    }
                    db.DBclose();
               
                dataGridView2.Rows.Clear();
                for (int i = 0; i < point_data.id.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
                {
                    int index = this.dataGridView2.Rows.Add();

                    this.dataGridView2.Rows[index].Cells[0].Value = route_data.id[i];
                    this.dataGridView2.Rows[index].Cells[1].Value = route_data.rid[i];
                    this.dataGridView2.Rows[index].Cells[2].Value = route_data.rn[i];
                    this.dataGridView2.Rows[index].Cells[3].Value = route_data.rl[i];
                    this.dataGridView2.Rows[index].Cells[4].Value = point_data.id[i];
                    this.dataGridView2.Rows[index].Cells[5].Value = point_data.pid[i];
                    this.dataGridView2.Rows[index].Cells[6].Value = point_data.lng[i];
                    this.dataGridView2.Rows[index].Cells[7].Value = point_data.lat[i];
                    this.dataGridView2.Rows[index].Cells[8].Value = point_data.pname[i];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
        //#region 添加数据 2019年5月28日
        //private void 数据录入ToolStripMenuItem_Click(object sender, EventArgs e)
        //{


        //  //  unit_name.Text = "";
        //}
        //#endregion

        //当在dataGridView改变数据时，保存到数据库中2019年6月9日22:20修改
        private void dataView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string rid, urid, rn, rl, pid, puid, pname,lng, lat;
                
                DBlink db = new DBlink();
                Boolean after = true, prior = true;
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
                    string mysqlstr = "update route1 r,point p set r.rid='" + urid + "',r.rn ='" + rn + "',r.rl ='" + rl + "',p.id ='" + pid + "',p.lng ='" + lng + "',p.lat ='" + lat + "',p.pname ='" + pname + "' where r.id='" + rid + "' and p.id='" + pid+"'";//6.10-16:15-更新
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


        private void bindListCiew() //添加两列
        {
            this.listView1.Columns.Add("编号");
            this.listView1.Columns.Add("地址");
            this.listView1.Columns.Add("经度");
            this.listView1.Columns.Add("纬度");
            this.listView1.View = System.Windows.Forms.View.Details;


        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region 向数据库中存数据，添加路径
        private void 添加路径_Click(object sender, EventArgs e)//手动添加路径
        {
            try
            {
                addr = true;
                string constr = "Host = localhost; Database = MapPoint; Username = root; Password = 123";
                //连接数据库
                MySqlConnection mycon = new MySqlConnection(constr);
                mycon.Open();//打开数据库
                string sql = " insert into start_end(s_lat,s_lng,e_lat,e_lng) values('" + start_lat.Text + ',' + start_lng.Text + ',' + end_lat.Text + ',' + start_lng.Text + ")";
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);
                MySqlCommand myCmd = new MySqlCommand(sql, mycon);


                myCmd.ExecuteNonQuery();
                mycon.Close();
                MessageBox.Show("已存入数据库！");
            }
            catch(MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("输入异常请重新输入！");
               
                
            }
        }
        #endregion
        #region 将输入文本转为double类型
        private void StringToDouble_Click(object sender, EventArgs e)
        {

            string s = start_lat.Text.Trim();//获取文本输入，出去文本框前后空白
            double result;
            if (Double.TryParse(s, out result))
            {
                //转换成功，显示结果
                MessageBox.Show("输出的数为：" + result.ToString());
            }
            else
            {
                //转换失败，提示错误
                MessageBox.Show("输出错误", "错误");
                start_lat.Text = string.Empty;
            }
            start_lat.Focus();
        }
        #endregion
        #region 输入坐标划线
        private void ADDR_Click(object sender, EventArgs e)
        {
            if (addr) {
                string s1 = start_lat.Text.Trim();//获取文本输入，出去文本框前后空白
                string s2 = start_lng.Text.Trim();//获取文本输入，出去文本框前后空白
                string s3 = end_lat.Text.Trim();//获取文本输入，出去文本框前后空白
                string s4 = end_lng.Text.Trim();//获取文本输入，出去文本框前后空白
                Double.TryParse(s1, out result[1]);
                Double.TryParse(s2, out result[2]);
                Double.TryParse(s3, out result[3]);
                Double.TryParse(s4, out result[4]);
                PointLatLng start = new PointLatLng(result[1], result[2]);  //路径起点
                GMapMarker gMapMarker = new GMarkerGoogle(start, GMarkerGoogleType.green);
                gMapOverlay.Markers.Add(gMapMarker);                      //向图层中添加标记 
                gMap1.Overlays.Add(gMapOverlay);                           //向控件中添加图层  

                //终点水滴标记
                PointLatLng end = new PointLatLng(result[3], result[4]);   //路径终点
                                                                           //PointLatLng end = new PointLatLng(40.8056789180537, 111.708702219065);   //路径终点
                gMapMarker = new GMarkerGoogle(end, GMarkerGoogleType.blue);
                gMapOverlay.Markers.Add(gMapMarker);                      //向图层中添加标记  
                gMap1.Overlays.Add(gMapOverlay);                          //向控件中添加图层 
                this.DrawLineBetweenTwoPoint(start, end); }
            #region
            //else
            //{
            //    for (int k = 1; k < 3; k++)
            //    {
            //        if (k == 1)
            //        {
            //            string s1 = lng.ToString();//获取文本输入，出去文本框前后空白
            //            string s2 = lat.ToString();//获取文本输入，出去文本框前后空白


            //            Double.TryParse(s1, out result[1]);
            //            Double.TryParse(s2, out result[2]);

            //            PointLatLng start = new PointLatLng(result[1], result[2]);  //路径起点
            //            GMapMarker gMapMarker = new GMarkerGoogle(start, GMarkerGoogleType.green);
            //            gMapOverlay.Markers.Add(gMapMarker);                      //向图层中添加标记 
            //            gMap1.Overlays.Add(gMapOverlay);                           //向控件中添加图层  
            //        }
            //        else
            //        {
            //            string s3 = lng.ToString();//获取文本输入，出去文本框前后空白
            //            string s4 = lat.ToString();//获取文本输入，出去文本框前后空白
            //            Double.TryParse(s3, out result[3]);
            //            Double.TryParse(s4, out result[4]);
            //            //终点水滴标记
            //            PointLatLng end = new PointLatLng(result[3], result[4]);   //路径终点
            //                                                                       //PointLatLng end = new PointLatLng(40.8056789180537, 111.708702219065);   //路径终点
            //            GMapMarker gMapMarker = new GMarkerGoogle(end, GMarkerGoogleType.blue);
            //            gMapOverlay.Markers.Add(gMapMarker);                      //向图层中添加标记  
            //            gMap1.Overlays.Add(gMapOverlay);                          //向控件中添加图层 

            //        }
            //        this.DrawLineBetweenTwoPoint(start, end);
            //    }
            //}
            #endregion
        }


        private void 添加线路_Click(object sender, EventArgs e)
        {
            try
            {
                string constr1 = "Host = localhost; Database = MapPoint; Username = root; Password = 123";
                //连接数据库
                MySqlConnection mycon1 = new MySqlConnection(constr1);
                mycon1.Open();//打开数据库
                string R = R_name.Text;
                string S = S_name.Text;
                string E = E_name.Text;
                string sql = " insert into route(id,routename,s_name,e_name,length,xianxinshu) values(" + RouteId.Text + ',' + R + ',' + S + ',' + E + ',' + length.Text+','+ xianxinshu.Text+")";
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon1);
                MySqlCommand myCmd = new MySqlCommand(sql, mycon1);


                myCmd.ExecuteNonQuery();
                mycon1.Close();
                MessageBox.Show("已存入数据库！");
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("输入异常请重新输入！");


            }
        }
        #endregion

        #region 画出两点直接的直线
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
       

        private void Delete_route_Enter(object sender, EventArgs e)
        {

        }

        #region 添加路径注释
        //        private void Button2_Click(object sender, EventArgs e)//增加路径确认
        //        {
        //            if (routeinf.Text == "")
        //                return;
        //            DBlink db = new DBlink();
        //            Boolean tag = true;
        //#region 录入路径信息 可修改5月30日
        //            string str = "select name from unit_name where name ='" + routeinf.Text + "'";

        //            string id = ID;
        //            if (db.DBcon())   //连接数据库成功
        //            {
        //                if (db.search(str))
        //                {
        //                    label2.Text = "该路径已存在";
        //                    tag = false;
        //                }
        //            }
        //            db.DBclose();//避免多线程操作数据库，影响系统报错，先关闭数据连接

        //            if (tag)
        //            {
        //                str = "insert into unit_name(name,TF_audit) values('" + routeinf.Text + "','未审计');";
        //                if (db.DBcon())   //连接数据库成功
        //                {
        //                    if (!db.UpdataDeletAdd(str))
        //                    {
        //                        DialogResult dr = MessageBox.Show("路径添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
        //                    }
        //                }
        //                db.DBclose();


        //                str = "select ID from unit_name where name ='" + routeinf.Text + "'";
        //                if (db.DBcon())   //连接数据库成功
        //                {
        //                    if (!db.UpdataDeletAdd(str))
        //                    {
        //                        id = db.search_id(str);
        //                    }
        //                }
        //                db.DBclose();

        //                for (int i = 0; i < route_data.id.Count; i++)
        //                {
        //                    str = "insert into audit_data(ID,time,financial_audit,er_audit,investment_audit,sor_audit,major_audit,audit_investigation) values('" + id + "','" + "year.year_list[i].ToString()" + "','0','0','0','0','0','0');";
        //                    if (db.DBcon())   //连接数据库成功
        //                    {
        //                        if (!db.UpdataDeletAdd(str))
        //                        {
        //                            DialogResult dr = MessageBox.Show("数据添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
        //                        }
        //                    }
        //                    db.DBclose();
        //                }
        //                routeinf.Text = "";
        //                show_dataGridView();
        //            }
        //        }

        //private void Routeinf_TextChanged(object sender, EventArgs e)
        //{
        //    // 修改属性框时发生
        //    label2.Text = "";
        //}
        //private void Button1_Click_1(object sender, EventArgs e)//增加路径取消
        //{
        //    routeinf.Text = "";
        //    dataGridView2.Visible = true;
        //    Form1_Load(null, null);//重新加载数据
        //}
        #endregion

        #region 添加节点信息 可修改5月30日
        //private void 增加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
             
        //    //if (db.DBcon())  //填充id 数组
        //    //{
        //    //    //db.Get_id();
        //    //}
        //    //db.DBclose();


        //    //int i;

        //    //for (i = 0; i < point_data.pid.Count; i++)
        //    //    已有节点展示.Text += point_data.pid.ToString() + " ";

        //    //label10.Text = "";
        //    groupBox1.Visible = false;
        //    add_route.Visible = false;
        //    delete_route.Visible = false;
        //    add_p.Visible = true;
        //    delete_point.Visible = false;

            
        //}

       

        
        #endregion
        private void TextBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

 

       

        private void GMap1_Load(object sender, EventArgs e)
        {
           
        }

   
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
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

            tabControl1.Visible = true;
            dataGridView2.Visible = false;
            groupBox1.Visible = false;
            tabControl1.SelectTab(增加节点);//跳转到tabControl指定的页面
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
            tabControl1.Visible = true;
            dataGridView2.Visible = false;
            groupBox1.Visible = false;
          
            tabControl1.SelectTab(增加线路);//跳转到tabControl指定的页面
            show_now_route_dataGridView1();


        }

        private void 删除线路ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
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
            tabControl1.SelectTab(删除线路);//跳转到tabControl指定的页面
        }

        private void 删除节点ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            groupBox1.Visible = false;
            tabControl1.Visible = true;
            
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
            tabControl1.SelectTab(删除节点);//跳转到tabControl指定的页

        }
        private void 路径信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            tabControl1.Visible = false;
        }
        private void 节点信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            groupBox1.Visible = true;
            tabControl1.Visible = false;
        }

        #endregion
        #region TabControl增删路径节点6-16---6-18
        private void 增加线路确认_Click(object sender, EventArgs e)//TabControl1 
        {
            if (rn1.Text == "")
                return;
            DBlink db = new DBlink();
            Boolean tag = true;
            #region 录入路径信息 可修改5月30日
            string str = "select rn from route1 where rn ='" + rn1.Text + "'";
            #endregion

            string id = ID;
            if (db.DBcon())   //连接数据库成功
            {
                if (db.search(str))
                {
                    label135.Text = "该路径已存在";
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
                str = "insert into route1(rid,rn,rl) values('" + rid1.Text + "','" + rn1.Text + "','" + rl1.Text + "');";
                if (db.DBcon())   //连接数据库成功
                {
                    if (!db.UpdataDeletAdd(str))
                    {
                        DialogResult dr = MessageBox.Show("线路添加失败！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("线路添加成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                db.DBclose();


                str = "select id from route1 where rn ='" + rid1.Text + "'";
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

        
        private void Button27_Click(object sender, EventArgs e)
        {
            addover = true;
        }
        int k = 0;
        int m = 0;
        PointLatLng[] a = new PointLatLng[100];//22:24  6-19
        PointLatLng[] b = new PointLatLng[100];//6月29日
        string[] p_name = new string[100];
        private void 节点添加确定_Click(object sender, EventArgs e)//TabControl1 6-16-11;25
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
                       
                        a[k]= draw_Line.S_Turn_P(lng1, lat1);//将输入的节点放入a[k]中
                        string str1=null;

                        str1= "insert into route_point1(线路编号,节点维度,节点经度,节点名称) values('" + add_rid1.Text + "','" + add_lat1维度.Text + "','" + add_lng1经度.Text + "','" + add_pname1.Text +"');";
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
        
        private void Add_over_Click(object sender, EventArgs e)//每次添加完一个线路上数据点击一次并将a[i]记录到
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
                k = 0; //添加完成后将计数重置为零,此时k=0;就没法画线了
            }//
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void DrawLine_Click(object sender, EventArgs e)
        {
            //drawLine.DrawLine(a, k, gMapOverlay, gMap1);
            draw_Line.DrawLine(a, k, p_name,gMapOverlay, gMap1);
        }
        private void Route_init_Click(object sender, EventArgs e)//6月20日22:09
        {
            //6-28 15 20-修改
            int[] rid = new int[20];//定义一个整型数组存线路编号
            string[] pname = new string[20];//定义字符型的存名字 
            int j;
            int n;
            int temp=0;
            int count=0;
            int[] count1 = new int[20]; 
            DBlink db = new DBlink();
            string lng1, lat1;
            if (db.DBcon())  //得到id序列
            {
                db.Get_rid();
            }
            db.DBclose();
            for (int i = 0; i < r_p.rid.Count; i++)//遍历画线
            {   try
                {
                    rid[i] = Convert.ToInt32(r_p.rid[i]);
                    if (db.DBcon())  //填充id 数组
                    {
                        db.Get_lng_lat_pname(rid[i]);//得到经度维度，节点名称
                    }
                    db.DBclose();
                  
                     for (  j = temp, n=0; j < r_p.pname.Count && rid[i] == Convert.ToInt32(r_p.rid[i]); j++,n++)
                         {
                            lng1 = Convert.ToString(r_p.lng[j]);
                            lat1 = Convert.ToString(r_p.lat[j]);
                        //for（k = 0; k <3;k++ ）{
                        //    a[k] = draw_Line.S_Turn_P(lng1, lat1);
                        //}
                            b[j] = draw_Line.S_Turn_P(lng1, lat1);//a[j]存放某一条线路上坐标
                            a[n] = b[j];
                            pname[n] = Convert.ToString(r_p.pname[j]);
                            
                        }
                  
                        temp = j;//2019年6月28日20:53
                        count1[i] = r_p.pname.Count;//取中间值
                    if (i > 0) { count = count1[i] -count1[i - 1]; }//6.29--17:05 修改，使count值为该行的数
                    else if (i == 0) { count = count1[0]; }
                        draw_Line.DrawLine(a, count, pname, gMapOverlay, gMap1);//画a[j]中的坐标
                        Array.Clear(a, 0, a.Length);//把a清空，等待画另一条线
                    
                    
                    // r_p.pname.Count = 0;
                }
                catch
                {

                }
             }
            //route_max = m;//将添加线路最大赋给route_max
            //draw_Line.route_init(4, a, pointArray1, route_max, gMapOverlay ,gMap1);//4为节点上、
        }

         
        private void 增加节点取消1_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);     //重新加载数据
            dataGridView2.Visible = true;

        }

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
        private void 删除节点确定_Click_1(object sender, EventArgs e)//TabControl1 6.16-11:20
        {
            try
            {
                DBlink db = new DBlink();
                string str = null;
                string str1 = null;
                str = "delete from point where pname ='" + 输入待删除节点.Text + "'";
                str1 = "delete from route_point1 where 节点名称 ='" + 输入待删除节点.Text + "'";
                if (db.DBcon())   //连接数据库成功
                {
                    if (db.UpdataDeletAdd(str)&& db.UpdataDeletAdd(str1))
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

        private void 已有节点显示下拉列表3_SelectedIndexChanged(object sender, EventArgs e)
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

        #region 键盘操作
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
                //if (currentPolygon != null)
                //{
                //    polygons.Polygons.Remove(currentPolygon);
                //    currentPolygon = null;
                //}

                //if (currentRoute != null)
                //{
                //    routes.Routes.Remove(currentRoute);
                //    currentRoute = null;
                //}

                //if (CurentRectMarker != null)
                //{
                //    objects.Markers.Remove(CurentRectMarker);

                //    if (CurentRectMarker.InnerMarker != null)
                //    {
                //        objects.Markers.Remove(CurentRectMarker.InnerMarker);
                //    }
                //    CurentRectMarker = null;

                //    RegeneratePolygon();
                }
            }
        #endregion
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

        private void Delete_Route_Click(object sender, EventArgs e)
        {
            routes.Routes.Clear();
        }

        private void Quit_Tabcontrol_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == 增加线路)
            {
                tabControl1.Visible = true;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;

                //tabControl1.SelectTab(增加线路);//跳转到tabControl指定的页面
                show_now_route_dataGridView1();
            }
            if (e.TabPage == 删除线路)
            {
                tabControl1.Visible = true;
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

                tabControl1.Visible = true;
                dataGridView2.Visible = false;
                groupBox1.Visible = false;
                //tabControl1.SelectTab(增加节点);//跳转到tabControl指定的页面
                show_now_pdataGridView1();
            }
            if (e.TabPage == 删除节点)
            {
                dataGridView2.Visible = false;
                groupBox1.Visible = false;
                tabControl1.Visible = true;

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
        }

        private void Show_now_route_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Delete_Marker_Click(object sender, EventArgs e)
        {
            gMapOverlay.Markers.Clear();//清理图层
            gMapOverlay.Routes.Clear();//清理路径
            
        }
        #endregion






    }
}

