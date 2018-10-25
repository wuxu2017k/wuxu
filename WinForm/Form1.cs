using DLTLib.Classes;
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


            //Dictionary<string, object> _WriteDic = new Dictionary<string, object>();
            //_WriteDic.Add("LoginName", txtname.Text.Trim());
            //_WriteDic.Add("Password", txtpwd.Text.Trim());
            //string str = JsonConvert.SerializeObject(_WriteDic);
            //string requestUrl = "http://127.0.0.1/DLT/DLT.svc/LogIn";
            //HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            //req.Method = "POST";
            //byte[] barr = Encoding.UTF8.GetBytes(str.Trim());//将串转换为字节数组
            //req.ContentLength = barr.Length;//length总数
            //using (Stream myStream = req.GetRequestStream())
            //{
            //    myStream.Write(barr, 0, barr.Length);
            //    myStream.Close();
            //}
            //using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            //{

            //    Stream stream1 = resp.GetResponseStream();
            //    StreamReader sr = new StreamReader(stream1, Encoding.UTF8);
            //    string respBody = sr.ReadToEnd();


            string respBody = ServiceCreat.Creat(txtname.Text.Trim(), txtpwd.Text.Trim());


            Dictionary<string, object> _RepDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(respBody);

            //_RepDic["res"] = JsonConvert.SerializeObject(data);
            //respBody = JsonConvert.SerializeObject(_RepDic,Formatting.Indented);
            textBox.Text = respBody.Replace("\\r\\n","\r\n").Replace("\\\"", "\"") + "\r\n";
            //textBox.Text += _RepDic["res"];

            if (Convert.ToBoolean(_RepDic["status"]))
            {
                button1.Visible = false;
                lblid.Visible = true;
                lblname.Visible = true;
                txtjgid.Visible = true;
                txtjgname.Visible = true;
                button3.Visible = true;
                button2.Visible = true;
                button4.Visible = true;
                label3.Visible = true;
                txtnewpwd.Visible = true;
                txtname.Enabled = false;
                txtpwd.Enabled = false;
                //List<Control> DisplayControlsSet = new List<Control>();
                //ControlCollection FormControlList = (ControlCollection)this.FindForm().Controls;
                //for (int i = 0; i < FormControlList.Count; i++)
                //{
                //    if (FormControlList[i].Tag.ToString() == "215")
                //    {
                //        DisplayControlsSet.Add(FormControlList[i]);
                //    }
                //}

                //for (int i = 0; i < DisplayControlsSet.Count; i++)
                //{
                //    DisplayControlsSet[i].Visible = false;
                //}

            }
            //   }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> _WriteDic = new Dictionary<string, object>();
            _WriteDic.Add("loginname", txtname.Text.Trim());
            _WriteDic.Add("newpassword", txtnewpwd.Text.Trim());
            string str = JsonConvert.SerializeObject(_WriteDic);
            string requestUrl = "http://127.0.0.1/DLT/DLT.svc/SET";//http://localhost:34253/DLT.svc/SET
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
                Stream stream1 = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream1);
                string respBody = sr.ReadToEnd();
                textBox.Text = respBody;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _WriteDic = new Dictionary<string, object>();
            _WriteDic.Add("ygid", txtjgid.Text.Trim());
            _WriteDic.Add("role", txtjgname.Text.Trim());
            string str = JsonConvert.SerializeObject(_WriteDic);
            string requestUrl = "http://127.0.0.1/DLT/DLT.svc/SELECT";//http://localhost:34253/DLT.svc/SELECT
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

                Stream stream1 = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream1);
                string respBody = sr.ReadToEnd();
                Dictionary<string, object> _RepDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(respBody);

                textBox.Text = respBody.Replace("\\r\\n", "\r\n").Replace("\\\"", "\"") + "\r\n";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            txtjgid.Clear();
            txtjgname.Clear();
            txtnewpwd.Clear();
            txtname.Clear();
            txtpwd.Clear();
            button1.Visible = true;
            lblid.Visible = false;
            lblname.Visible = false;
            txtjgid.Visible = false;
            txtjgname.Visible = false;
            button3.Visible = false;
            button2.Visible = false;

            label3.Visible = false;
            txtnewpwd.Visible = false;
            txtname.Enabled = true;
            txtpwd.Enabled = true;
            button1.Visible = true;
            textBox.Clear();
        }
    }
}

