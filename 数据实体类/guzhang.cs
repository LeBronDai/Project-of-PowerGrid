using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WindowsFormsApp1.数据实体类
{
    class guzhang
    {
        public static List<Int32> rid = new List<Int32>();//线路编号
        public static ArrayList rn = new ArrayList();//线路名
        public static List<Double> date = new List<Double>();//故障日期
        public static ArrayList guzhang_type = new ArrayList();//故障类型
        public static ArrayList handler = new ArrayList();//处理人
        public static List<Int32> count = new List<Int32>();//故障数目
        public static List<Double> year = new List<Double>();//选择年份
    }
}
