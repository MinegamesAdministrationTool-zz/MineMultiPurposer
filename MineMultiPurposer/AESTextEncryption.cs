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
    public partial class AESTextEncryption : Form
    {
        public AESTextEncryption()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("please enter a text to encrypt!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                if(string.IsNullOrEmpty(textBox5.Text) == true)
                {
                    MessageBox.Show("You have to enter a key to encrypt with!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if(string.IsNullOrEmpty(textBox6.Text) == true)
                    {
                        MessageBox.Show("You have to enter a key to encrypt with!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBox6.Text.Length == 16)
                        {
                            AesAlgorithms EncryptText = new AesAlgorithms();
                            textBox3.Text = EncryptText.AesTextEncryption(textBox1.Text, textBox5.Text, textBox6.Text);
                        }
                        else
                        {
                            MessageBox.Show("The IV Key should be excatly 16 character.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.textBox6.Text == "here you put the IV Key and this should be excatly 16 in length!") textBox6.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("please enter a text to encrypt!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(textBox5.Text) == true)
                {
                    MessageBox.Show("You have to enter a key to decrypt with!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (string.IsNullOrEmpty(textBox6.Text) == true)
                    {
                        MessageBox.Show("You have to enter a key to decrypt with!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBox6.Text.Length == 16)
                        {
                            AesAlgorithms EncryptText = new AesAlgorithms();
                            textBox3.Text = EncryptText.AesTextDecryption(textBox2.Text, textBox5.Text, textBox6.Text);
                        }
                        else
                        {
                            MessageBox.Show("The IV Key should be excatly 16 character.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
