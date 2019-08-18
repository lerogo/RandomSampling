using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//------------------------------------------------------------------------------
//博客www.lerogo.top
//------------------------------------------------------------------------------
namespace 随机选择
{
    public partial class Form1 : Form
    {
        List<string> users = new List<string>();
        List<Label> labels1 = new List<Label>();
        //------------------图片存放位置
        String strDirImg = Application.StartupPath+@"\人员照片";
        //------------------开始
        Point point1 = new Point(210,60);
        int x = 300;
        int y = 100;
        public Form1()
        {
                InitializeComponent();
                string content = "";
            //------------------人员名单存放位置
            if (File.Exists(@"人员名单.txt")==false)
            {
                MessageBox.Show("说明：\n"+"请在软件目录下添加:" + "\n" + "文件“人员名单.txt”"+"\n你暂未添加\n请添加后使用!\n" + "个人博客: www.lerogo.top");
                System.Environment.Exit(0);
            }
                using (StreamReader sr = new StreamReader(Application.StartupPath + @"\人员名单.txt"))
            {
                   content = sr.ReadToEnd();
                sr.Close();
                }
            
                string[] list = content.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string name in list)
                {
                    if (string.IsNullOrEmpty(name.Trim()) == false)
                        users.Add(name.Trim());
                }
                listBox1.Items.Clear();
                foreach (string name in users)
                    listBox1.Items.Add(name);
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
                button1.Enabled = false;
                button3.Enabled = false;
                //预加载图片
                pictureBox2.Image = pictureBox3.Image;
                //以上
                int count = 1;
                if (count == 0 || count > users.Count)
                {
                    MessageBox.Show("人数为0，不足选择");
                    string txt = "";
                    foreach (Label label in labels1)
                    {
                    txt += label.Text + "\r\n";
                }
                txt = txt.Trim();
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\随机选择名单.txt"))
                {
                    sw.Write(txt);
                    sw.Close();
                }
                MessageBox.Show("保存随机选择顺序成功，自动退出软件");
                System.Environment.Exit(0);
                return;
                }

                Thread thread = new Thread(Run1);
                thread.Start();
            
        }

        private void Run1() {
            int count = 1;
            for (int i = 0; i < count; i++)
            {
                string name = GetRandName(0);
                if (string.IsNullOrEmpty(name) == false)
                {
                    users.Remove(name);
                    this.Invoke(new EventHandler(delegate { listBox1.Items.Remove(name); }));
                    SetLabelPosition1(name);
                }
                Thread.Sleep(5000);
            }
        }

        private string GetRandName(int key) {
            string name = "";
            if (users.Count > 1)
            {
                int randcount = 0;
                while (randcount < 240)
                {
                    int index = new Random().Next(1, users.Count);
                    name = users[index - 1];
                    randcount = randcount + 1;
                    if(key ==0)
                        this.Invoke(new EventHandler(delegate { label3.Text = name; }));
                    Thread.Sleep(10);
                }
            }
            else {
                name = users[0];
            }
            
            //--------------图片最后位置
            if (File.Exists( strDirImg + "/"+name + ".jpg"))
            {
                String strPath = strDirImg + "/" + name + ".jpg";
                pictureBox2.Image = new Bitmap(strPath);
                //---------------------
            }
            else
            {
               pictureBox2.Image = pictureBox4.Image;
            }
            button1.Enabled = true;
            button3.Enabled = true;
            return name;

        }

        private void SetLabelPosition1(string name)
        {
            Label label = new Label();
            label.Text = name;
           
            labels1.Add(label);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string txt = "";
            foreach (Label label in labels1)
            {
                txt += label.Text + "\r\n";
            }
            txt = txt.Trim();
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\随机选择名单.txt"))
            {
                sw.Write(txt);
                sw.Close();
            }
            MessageBox.Show("保存成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请在软件目录下添加:"+"\n"+"文件“人员名单.txt”"+"\n"+"目录“人员照片”"+"\n"
                +"其中照片需要和人员名单同名，后缀均为.jpg"
                +"\n"+"如:人员名单.txt含有“小明”;那么人员照片目录就需要有“小明.jpg”\n"+"当然，没有照片也能运行哦！\n"+"个人博客: www.lerogo.top");
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
