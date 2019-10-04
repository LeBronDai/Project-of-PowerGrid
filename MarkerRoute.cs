using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;


namespace WindowsFormsApp1
{
    class GMapMarkerRoute : GMapMarker
    {
        //用户绘制在视窗中的点，是将经纬度转换成GPoint再加上偏移处理后的点
        private List<Point> Point = new List<Point>();
        //需要绘制的经纬度点集
        private List<PointLatLng> PointLL = new List<PointLatLng>();


        // 是否有新的点加入
        private bool HasNewPoint = false;
        //新加入点的经纬度
        private PointLatLng NewPointLatLng;
        /// <summary>
        /// 图层缩放比例是否变化
        /// </summary>
        public bool IsZoomChanged = false;
        /// <summary>
        /// 拖拽地图后图层原点与视窗原点的偏差向量
        /// </summary>
        public Point OriginOffset = new Point();
        /// <summary>
        /// 视窗原点相对于图层原点的像素偏差向量
        /// 视窗原点默认是视窗中心点
        /// 图层原点默认是视窗左上角的点
        /// </summary>
        public Point Origin = new Point();
        /// <summary>
        /// 绘制点集的pen
        /// </summary>
        public Pen pen = new Pen(Color.Red, 1);


        public GMapMarkerRoute(GMap.NET.PointLatLng p) : base(p)
        {

        }


        public override void OnRender(Graphics g)
        {

            GPoint gp = new GPoint();

            //地图拖拽
            if (this.Overlay.Control.IsDragging)
            {
                pen.Color = Color.Green;
            }
            //地图缩放比例改变后需要重新计算Point    
            else if (IsZoomChanged)
            {
                pen.Color = Color.Black;
                OriginOffset.X = 0;
                OriginOffset.Y = 0;
                if (PointLL.Count > 1)
                {
                    Point.Clear();
                    {
                        foreach (PointLatLng p in PointLL)
                        {
                            gp = this.Overlay.Control.FromLatLngToLocal(p);
                            Point.Add(new Point((int)(gp.X - Origin.X), (int)(gp.Y - Origin.Y)));
                        }
                    }
                }
                IsZoomChanged = false;
            }
            //其他事件
            else
            {
                pen.Color = Color.Red;
            }
            //判断是否有新的点加入,如果有将其添加进Point点集
            //同时也添加相应的经纬度到相关点集
            if (HasNewPoint)
            {
                gp = this.Overlay.Control.FromLatLngToLocal(NewPointLatLng);
                Point.Add(new Point((int)gp.X - Origin.X - OriginOffset.X, (int)gp.Y - Origin.Y - OriginOffset.Y));
                PointLL.Add(NewPointLatLng);
                HasNewPoint = false;
            }
            if (Point.Count > 1)
            {
                g.DrawLines(pen, Point.ToArray());
            }
        }

        public void AddPoint(PointLatLng p)
        {
            NewPointLatLng = p;
            HasNewPoint = true;
        }
    }
}


