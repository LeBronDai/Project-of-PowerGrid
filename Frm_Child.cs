using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSScriptControl;
namespace WindowsFormsApp1
{
    public partial class Frm_Child : Form
    {
        string strHtml = "";
        ChartTemplate charttemp = new ChartTemplate();
        public Frm_Child()
        {
            InitializeComponent();
        }

      


        private void Frm_Child_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
          
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "";
        }

        private void 备纤衰耗分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            // 输入JS方法参数
            object[] para = new object[] { this.textBox1.Text.Trim() };

            string str = GetJsMethd("test", para);

            MessageBox.Show(str);
        }
        /// <summary>
        /// 执行JS方法
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="para">参数</param>
        /// <returns></returns>
        /// 
        private static string GetJsMethd(string methodName, object[] para)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("package aa{");
            sb.Append(" public class JScript {");
            sb.Append("     public static function test(str) {");
            sb.Append("         return 'Hello,'+str;");
            sb.Append("     }");
            sb.Append(" }");
            sb.Append("}");

            CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateInMemory = true;

            CodeDomProvider _provider = new Microsoft.JScript.JScriptCodeProvider();

            CompilerResults results = _provider.CompileAssemblyFromSource(parameters, sb.ToString());

            Assembly assembly = results.CompiledAssembly;

            Type _evaluateType = assembly.GetType("aa.JScript");

            object obj = _evaluateType.InvokeMember("test", BindingFlags.InvokeMethod,
            null, null, para);

            return obj.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "test.js";
            string str2 = File.ReadAllText(path);
            string path1 = AppDomain.CurrentDomain.BaseDirectory + "echartstest3.html";
            strHtml = File.ReadAllText(path1);

            webBrowser1.DocumentText = strHtml;
            webBrowser1.Document.InvokeScript("SetSize", new object[] { 1100, 700 });
        }
        /// <summary>
        /// 执行JS
        /// </summary>
        /// <param name="sExpression">参数体</param>
        /// <param name="sCode">JavaScript代码的字符串</param>
        /// <returns></returns>

        private string ExecuteScript(string sExpression, string sCode)
        {
            MSScriptControl.ScriptControl scriptControl = new MSScriptControl.ScriptControl();
            scriptControl.UseSafeSubset = true;
            scriptControl.Language = "JScript";
            scriptControl.AddCode(sCode);
            try
            {
                string str = scriptControl.Eval(sExpression).ToString();
                return str;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return null;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "echartstest3.html";

            //this.webBrowser1.Url = new Uri(path);
            //webBrowser1.Navigate(path);
            webBrowser1.ObjectForScripting = this;    //这句是必不可少的，是调用JS的前提  
            strHtml = File.ReadAllText(path);

            webBrowser1.DocumentText = strHtml;
            //webBrowser1.Document.InvokeScript("SetSize", new object[] { 500, 300 });

            timer1.Enabled = false;
        }

        private void Button5_Click(object sender, EventArgs e)//份额
        {
            string[] strLegend = {
                                     "ali","tencent","baidu"
                                  };
            int[] values = {
                                    100,100,89
                                  };
            charttemp.Setlegend(strLegend).SetSeriesData(strLegend, values).SetSeriesName("市值统计");
            strHtml = charttemp.strTemp;
            webBrowser1.DocumentText = strHtml;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string[] strLegend = {
                                     "Man","Woman"
                                  };
            int[] values = {
                                    55,45
                                  };
            charttemp.Setlegend(strLegend).SetSeriesData(strLegend, values).SetSeriesName("性别统计");
            strHtml = charttemp.strTemp;
            webBrowser1.DocumentText = strHtml;
        }
    }
    
}
