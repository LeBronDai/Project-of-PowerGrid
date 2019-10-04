using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GMap.NET;
namespace WindowsFormsApp1.数据实体类
{
    class r_p
    {
        public static ArrayList rid = new ArrayList();//路径Id
        public static ArrayList lng=new ArrayList(); //节点维度 
        public static ArrayList lat = new ArrayList(); //节点经度
        public static ArrayList pname = new ArrayList(); //节点名称
        public static List<PointLatLng> pointLatLng = new List<PointLatLng>();//存坐标
       
    }
}
