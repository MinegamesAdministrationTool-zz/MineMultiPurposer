using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MinegamesAdministrationTool_v2._0
{
    public partial class DebuggingForm : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int DebugActiveProcess(int PID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int DebugActiveProcessStop(int PID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool IsDebuggerPresent();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr ProcessHandle, ref bool BoolForChecking);
        public DebuggingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please Specifiy a Process Name to Debug.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    var ProcessPID = Process.GetProcessesByName(textBox1.Text);
                    foreach (Process GetPID in ProcessPID)
                    {
                        int PIDOfProcess = GetPID.Id;
                        DebugActiveProcess(PIDOfProcess);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bool CheckDebugger = false;
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref CheckDebugger);
                if (IsDebuggerPresent() == true || CheckDebugger == true || Debugger.IsAttached == true)
                {
                    MessageBox.Show("Yes, this program are being debugged.", "Being Debugged", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("this program are not detecting any debuggers that debugs this process (notice that the anti-debugging here are basic cause of the limited things you can do in C# and because i'm lazy to import from dll's and this other stuff).", "Nope", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please Specifiy a Process Name to UnDebug.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    var ProcessPID = Process.GetProcessesByName(textBox1.Text);
                    foreach (Process GetPID in ProcessPID)
                    {
                        DebugActiveProcessStop(GetPID.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("you have to enter process name to check.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    var ProcessPID = Process.GetProcessesByName(textBox1.Text);
                    foreach (Process GetPID in ProcessPID)
                    {
                        bool CheckDebugger = false;
                        CheckRemoteDebuggerPresent(GetPID.Handle, ref CheckDebugger);
                        if (CheckDebugger == true)
                        {
                            MessageBox.Show("Yes the process are being debugged.", "Yes", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No The Process are not being debugged.", "Nope", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}