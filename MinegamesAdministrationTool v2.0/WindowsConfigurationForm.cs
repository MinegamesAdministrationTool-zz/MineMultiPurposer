using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MinegamesAdministrationTool_v2._0
{
    public partial class WindowsConfigurationForm : Form
    {
        public WindowsConfigurationForm()
        {
            InitializeComponent();
        }

        private void TaskMgr_Click(object sender, EventArgs e)
        {
            RegistryKey DisableTaskManager = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            DisableTaskManager.SetValue("DisableTaskMgr", 0, RegistryValueKind.DWord);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey EnableRegedit = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            EnableRegedit.SetValue("DisableRegistryTools", 0, RegistryValueKind.DWord);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistryKey EnableCMD = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            EnableCMD.SetValue("DisableCMD", 0, RegistryValueKind.DWord);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegistryKey EnableWindowsUpdate = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AU");
            EnableWindowsUpdate.SetValue("NoAutoUpdate", 0, RegistryValueKind.DWord);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RegistryKey DisableUAC = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            DisableUAC.SetValue("EnableLUA", 1, RegistryValueKind.DWord);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RegistryKey EnableWSC = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\wscsvc");
            EnableWSC.SetValue("Start", 0, RegistryValueKind.DWord);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryKey DisableRun = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer");
            DisableRun.SetValue("NoRun", 0, RegistryValueKind.DWord);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RegistryKey DisableTaskManager = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            DisableTaskManager.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RegistryKey EnableRegedit = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            EnableRegedit.SetValue("DisableRegistryTools", 1, RegistryValueKind.DWord);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RegistryKey EnableCMD = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            EnableCMD.SetValue("DisableCMD", 1, RegistryValueKind.DWord);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RegistryKey EnableWindowsUpdate = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AU");
            EnableWindowsUpdate.SetValue("NoAutoUpdate", 1, RegistryValueKind.DWord);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RegistryKey EnableWSC = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\wscsvc");
            EnableWSC.SetValue("Start", 1, RegistryValueKind.DWord);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            RegistryKey DisableUAC = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            DisableUAC.SetValue("EnableLUA", 0, RegistryValueKind.DWord);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            RegistryKey DisableRun = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer");
            DisableRun.SetValue("NoRun", 1, RegistryValueKind.DWord);
        }
    }
}