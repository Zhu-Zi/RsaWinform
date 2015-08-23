using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace encrypt
{
    public partial class Form1 : Form
    {
        string PublicKey;
        string PrivateKey;
        byte[] EncryteData;
        byte[] DecryptData;
        string EncryteStr;
        string DecryptStr;

        /// <summary>
        /// 生成指定位数的公钥，私钥
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            PublicKey = rsa.ToXmlString(true);
            PrivateKey = rsa.ToXmlString(true);
        }
       
        /// <summary>
        /// 加密过程
        /// Winform程序注意加密过程中字符串转码的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(PublicKey);//拿到公钥加密

            byte[] data = System.Text.Encoding.Default.GetBytes(this.textBox1.Text); //Textbox数据转换Byte[] 数组  步骤A
            EncryteData = rsa.Encrypt(data, false);//加密

            EncryteStr = Convert.ToBase64String(EncryteData);//Byte[] 转Base64string以便数据库存储

            this.textBox3.Text = EncryteStr;

        }
        /// <summary>
        /// 解密过程
        /// Winform程序解密完后注意转码问题，以便利于显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)//解密
        {
            byte[] a;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(PrivateKey);//拿到私钥

            a = Convert.FromBase64String(EncryteStr);//Base64string转Byte[]数组
            
            DecryptData = rsa.Decrypt(a, false);//解密

           // DecryptStr = Convert.ToBase64String(DecryptData);
            DecryptStr = System.Text.Encoding.Default.GetString(DecryptData);//Byte[]数组转利于Textbox显示的string类型(和步骤A相反,用于显示)
            this.textBox2.Text = DecryptStr;
        }


    }
}
