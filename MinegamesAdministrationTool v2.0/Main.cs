using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinegamesAdministrationTool_v2._0
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ProcessManager().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new DebuggingForm().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new EncryptionForm().ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new ThankYouAll().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new WindowsConfigurationForm().ShowDialog();
        }
    }
}
