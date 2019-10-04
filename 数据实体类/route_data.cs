using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace WindowsFormsApp1.数据实体类
{
    class route_data
    {
        public static ArrayList id = new ArrayList();//路径Id，系统随机生成
        public static ArrayList rid = new ArrayList();//路径Id,用户给出可为空
        public static ArrayList rn = new ArrayList();//路径名
        public static ArrayList rl = new ArrayList();//路径长度
    }
}
