using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _WriteDic = new Dictionary<string, object>();
            _WriteDic.Add("LoginName", txtname.Text.Trim());
            _WriteDic.Add("Password", txtpwd.Text.Trim());
            string str = JsonConvert.SerializeObject(_WriteDic);
            string requestUrl = "http://127.0.0.1/DLT/DLT.svc/LogIn";
            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            req.Method = "POST";
            byte[] barr = Encoding.UTF8.GetBytes(str.Trim());//将串转换为字节数组
            req.ContentLength = barr.Length;//length总数
            using (Stream myStream = req.GetRequestStream())
            {
                myStream.Write(barr, 0, barr.Length);
                myStream.Close();
            }
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                lst.Items.Add(string.Format("返回状态码:{0}", (int)resp.StatusCode));
                lst.Items.Add(string.Format("返回状态描述:{0}", resp.StatusDescription));
                Stream stream1 = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream1, Encoding.UTF8);
                string respBody = sr.ReadToEnd();
               
                lst.Items.Add(respBody);
            }
            lblid.Visible = true;
            lblname.Visible = true;
            txtjgid.Visible = true;
            txtjgname.Visible = true;
            button3.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            label3.Visible= true;
            txtnewpwd.Visible = true;
            txtname.Enabled = false;
            txtpwd.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = txtname.Text.Trim()+"|"+txtnewpwd.Text.Trim();
            string requestUrl = "http://127.0.0.1/DLT/DLT.svc/SET";
            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            req.Method = "POST";
            byte[] barr = Encoding.UTF8.GetBytes(str.Trim());//将串转换为字节数组
            req.ContentLength = barr.Length;//length总数
            using (Stream myStream = req.GetRequestStream())
            {
                myStream.Write(barr, 0, barr.Length);
                myStream.Close();
            }
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                lst.Items.Add(string.Format("返回状态码:{0}", (int)resp.StatusCode));
                lst.Items.Add(string.Format("返回状态描述:{0}", resp.StatusDescription));
                Stream stream1 = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream1);
                string respBody = sr.ReadToEnd();
                lst.Items.Add(respBody);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = txtjgid.Text.Trim() + "|" + txtjgname.Text.Trim();
            string requestUrl = "http://127.0.0.1/DLT/DLT.svc/SELECT";
            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            req.Method = "POST";
            byte[] barr = Encoding.UTF8.GetBytes(str.Trim());//将串转换为字节数组
            req.ContentLength = barr.Length;//length总数
            using (Stream myStream = req.GetRequestStream())
            {
                myStream.Write(barr, 0, barr.Length);
                myStream.Close();
            }
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                lst.Items.Add(string.Format("返回状态码:{0}", (int)resp.StatusCode));
                lst.Items.Add(string.Format("返回状态描述:{0}", resp.StatusDescription));
                Stream stream1 = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream1);
                string respBody = sr.ReadToEnd();
                lst.Items.Add(respBody);

            }
        }
    }
    }

