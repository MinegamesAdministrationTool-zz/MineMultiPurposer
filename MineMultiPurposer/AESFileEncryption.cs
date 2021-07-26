using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using CryptoPrivacy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineMultiPurposer_2
{
    public partial class AESFileEncryption : Form
    {
        public AESFileEncryption()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog GetFile = new OpenFileDialog();
            if(radioButton1.Checked == true)
            {
                GetFile.Title = "Select File To Encrypt";
            }
            else if(radioButton2.Checked == true)
            {
                GetFile.Title = "Select File To Decrypt";
            }

            if(GetFile.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = GetFile.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("You have to enter a file to encrypt or you haven't enter a password to encrypt with!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    try
                    {
                        AesAlgorithms EncryptFile = new AesAlgorithms();
                        EncryptFile.EncryptFile(textBox1.Text, textBox2.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message + ", if you want any fixes or there's a bug or you recommend something then contact in my github page!");
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    try
                    {
                        AesAlgorithms DecryptFile = new AesAlgorithms();
                        DecryptFile.DecryptFile(textBox1.Text, textBox2.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message + ", if you want any fixes or there's a bug or you recommend something then contact in my github page!");
                    }
                }
            }
        }
    }
}