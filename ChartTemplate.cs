using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public enum ChartType
    {
        line,
        bar,
        pie,

    }

    public class ChartTemplate
    {
        public string strTemp;

        public ChartTemplate()
        {
            StringBuilder sbuild = new StringBuilder();
            sbuild.Append("<!DOCTYPE html>");
            sbuild.Append("<html>");
            sbuild.Append("<head>");
            sbuild.Append("<meta charset=\"utf-8\">");
            sbuild.Append("<title>ECharts</title>");
            sbuild.Append("<title>ECharts</title>");

            sbuild.Append("<script src=\"H:\\研究生学习\\电网项目\\8月31日1050备份\\bin\\Debug\\echarts.min.js\"></script>");  //如何更改
            sbuild.Append("</head>");
            sbuild.Append("<body>");
            sbuild.Append("<div id=\"main\" style=\"width: 1000px;height:600px;\"></div>");
            sbuild.Append("<script type=\"text/javascript\">");
            sbuild.Append("var myChart = echarts.init(document.getElementById('main'));");
            sbuild.Append("var option = { ");
            sbuild.Append("tooltip: {");
            sbuild.Append("trigger: 'item', ");
            sbuild.Append("formatter: \"{a} <br/>{b}: {c} ({d}%)\"");
            sbuild.Append("}, ");
            sbuild.Append("legend: { ");
            sbuild.Append("orient: 'horizontal', ");
            sbuild.Append("left: 'center', ");
            sbuild.Append("bottom: 0, ");
            sbuild.Append("data:['直达','其它外链','搜索引擎','直接输入网址或书签','cnblogs.com','微博','微信','百度','谷歌','360','必应','其他']  ");
            sbuild.Append("}, ");
            sbuild.Append("series: [ ");
            sbuild.Append("{ ");
            sbuild.Append(" name:'访问来源', ");
            sbuild.Append("type:'pie',");
            sbuild.Append("selectedMode: 'single',");
            sbuild.Append("radius: ['40%', '60%'],");
            sbuild.Append("label: { ");
            sbuild.Append("normal: { ");
            sbuild.Append(" position: 'inner'");
            sbuild.Append("} ");
            sbuild.Append(" },");
            sbuild.Append("labelLine: { ");
            sbuild.Append("normal: { ");
            sbuild.Append("show: false");
            sbuild.Append(" }");
            sbuild.Append(" },");
            sbuild.Append("data:[{value:335, name:'直达', selected:true}, {value:679, name:'其它外链'}, {value:1548, name:'搜索引擎'} ] ");
            sbuild.Append(" }");
            sbuild.Append("]");
            sbuild.Append("}; ");
            sbuild.Append("myChart.setOption(option);");
            sbuild.Append("</script>");
            sbuild.Append("</body>");
            sbuild.Append("</html>");
            strTemp = sbuild.ToString();
        }

        public ChartTemplate SetPath(string path)
        {
            string[] strsplit = { "<script src=" };
            string[] strArr = strTemp.Split(strsplit, StringSplitOptions.RemoveEmptyEntries);
            if (strArr.Length == 2)
            {
                StringBuilder build = new StringBuilder();
                build.Append(strArr[0]);
                build.Append(strsplit[0]);
                build.Append("\"");
                build.Append(path);
                build.Append("\"");
                int pos = strArr[1].IndexOf("></script>");
                build.Append(strArr[1].Substring(pos, strArr[1].Length - pos));
                strTemp = build.ToString();
            }
            return this;
        }

        public ChartTemplate Setlegend(string[] strLegend)
        {
            int posData = strTemp.IndexOf("data:[");
            StringBuilder build = new StringBuilder();
            build.Append(strTemp.Substring(0, posData + 6));
            for (int i = 0; i < strLegend.Length; i++)
            {
                build.Append("'");
                build.Append(strLegend[i]);
                build.Append("'");
                if (i != strLegend.Length - 1)
                {
                    build.Append(",");
                }

            }
            int posDataEnd = strTemp.IndexOf("]", posData);
            build.Append(strTemp.Substring(posDataEnd, strTemp.Length - posDataEnd));
            strTemp = build.ToString();
            return this;
        }

        public ChartTemplate SetSeriesData(string[] strNames, int[] Vals)
        {
            int startPos = strTemp.IndexOf("series: [");
            int posData = strTemp.IndexOf("data:[", startPos);
            StringBuilder build = new StringBuilder();
            build.Append(strTemp.Substring(0, posData + 6));
            for (int i = 0; i < strNames.Length; i++)
            {
                //{value:679, name:'其它外链'},
                build.Append("{value:" + Vals[i] + ",name:" + "'" + strNames[i] + "'}");
                if (i != strNames.Length - 1)
                {
                    build.Append(",");
                }

            }
            int posDataEnd = strTemp.IndexOf("]", posData);
            build.Append(strTemp.Substring(posDataEnd, strTemp.Length - posDataEnd));
            strTemp = build.ToString();
            return this;
        }


        public ChartTemplate SetSeriesName(string name)
        {
            //sbuild.Append(" name:'访问来源', ");
            int pos = strTemp.IndexOf("name:");
            StringBuilder build = new StringBuilder();
            build.Append(strTemp.Substring(0, pos + 5));
            build.Append("'" + name + "'");
            int posDataEnd = strTemp.IndexOf(",", pos);
            build.Append(strTemp.Substring(posDataEnd, strTemp.Length - posDataEnd));
            strTemp = build.ToString();
            return this;
        }

        public ChartTemplate SetType(ChartType type)
        {
            int pos = strTemp.IndexOf("type:");
            StringBuilder build = new StringBuilder();
            build.Append(strTemp.Substring(0, pos + 5));
            build.Append("'" + type.ToString() + "'");
            int posDataEnd = strTemp.IndexOf(",", pos);
            build.Append(strTemp.Substring(posDataEnd, strTemp.Length - posDataEnd));
            strTemp = build.ToString();
            return this;
        }
    }
}
