using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MinegamesAdministrationTool_v2._0
{
    public partial class ProcessManager : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr ProcessHandle, ref bool Checker);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool IsProcessCritical(IntPtr Handle, ref bool BoolToCheck);
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int InformationClass, ref int Information, int InformationLength);
        public ProcessManager()
        {
            InitializeComponent();
        }

        private void ProcessManager_Load(object sender, EventArgs e)
        {
            var GetAllProcesses = Process.GetProcesses();
            foreach (Process GetProceses in GetAllProcesses)
            {
                try
                {
                    ListViewItem ToColumns = new ListViewItem(GetProceses.ProcessName);
                    ToColumns.SubItems.Add(GetProceses.Id.ToString());
                    bool CheckBeingDebugged = false;
                    CheckRemoteDebuggerPresent(GetProceses.Handle, ref CheckBeingDebugged);
                    if (CheckBeingDebugged == true)
                    {
                        ToColumns.SubItems.Add("True");
                    }
                    else
                    {
                        ToColumns.SubItems.Add("False");
                    }
                    bool IsCritical = false;
                    IsProcessCritical(GetProceses.Handle, ref IsCritical);
                    if (IsCritical == true)
                    {
                        ToColumns.SubItems.Add("True");
                    }
                    else if (IsCritical == false)
                    {
                        ToColumns.SubItems.Add("False");
                    }
                    listView1.Items.Add(ToColumns);
                }
                catch
                {
                    continue;
                }
            }
        }

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        private static void SuspendProcess(int pid)
        {
            var process = Process.GetProcessById(pid);

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem GetNameFromList in listView1.SelectedItems)
                {
                    string ProcessName = GetNameFromList.Text;
                    var GetProcessByName = Process.GetProcessesByName(ProcessName);
                    foreach (Process GetID in GetProcessByName)
                    {
                        SuspendProcess(GetID.Id);
                        MessageBox.Show("Successfully Suspended The Process.", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public static void ResumeProcess(int pid)
        {
            var process = Process.GetProcessById(pid);

            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem GetNameFromList in listView1.SelectedItems)
                {
                    string ProcessName = GetNameFromList.Text;
                    var GetProcessByName = Process.GetProcessesByName(ProcessName);
                    foreach (Process GetID in GetProcessByName)
                    {
                        ResumeProcess(GetID.Id);
                        MessageBox.Show("Successfully Resumed The Process.", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ProcessName = "";
            bool IsExecptionOccured = false;
            try
            {
                foreach (ListViewItem GetNameFromList in listView1.SelectedItems)
                {
                    var GetProcessByName = Process.GetProcessesByName(GetNameFromList.Text);
                    foreach (Process GetID in GetProcessByName)
                    {
                        ProcessName = GetNameFromList.Text;
                        bool IsCritical = false;
                        IsProcessCritical(GetID.Handle, ref IsCritical);
                        if (IsCritical)
                        {
                            DialogResult GetResult = MessageBox.Show("This Process are critical that means that it can cause a BSOD if we try to kill it.... do you want to try to make it Un-Critical?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (GetResult == DialogResult.Yes)
                            {
                                int Critical = 0;
                                NtSetInformationProcess(GetID.Handle, 0x1D, ref Critical, sizeof(int));
                                IsCritical = false;
                                IsProcessCritical(GetID.Handle, ref IsCritical);
                                if (IsCritical == false)
                                {
                                    DialogResult GetResultTerminate = MessageBox.Show("Success! do you want to terminate it now?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (GetResultTerminate == DialogResult.Yes)
                                    {
                                        listView1.Items.Remove(GetNameFromList);
                                        GetID.Kill();
                                        if (IsExecptionOccured == false)
                                        {
                                            MessageBox.Show("The Process " + GetID.ProcessName + " Have been terminated.", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            listView1.Items.Remove(GetNameFromList);
                            GetID.Kill();
                            if (IsExecptionOccured == false)
                            {
                                MessageBox.Show("The Process " + GetID.ProcessName + " Have been terminated.", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsExecptionOccured = true;
                listView1.Items.Add(ProcessName);
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem GetNameFromList in listView1.SelectedItems)
                {
                    var GetProcessByName = Process.GetProcessesByName(GetNameFromList.Text);
                    foreach (Process GetID in GetProcessByName)
                    {
                        bool IsCritical = false;
                        IsProcessCritical(GetID.Handle, ref IsCritical);
                        if (IsCritical)
                        {
                            int Critical = 0;
                            NtSetInformationProcess(GetID.Handle, 0x1D, ref Critical, sizeof(int));
                            IsCritical = false;
                            IsProcessCritical(GetID.Handle, ref IsCritical);
                            if (IsCritical == false)
                            {
                                DialogResult GetResultTerminate = MessageBox.Show("Success! The Process are normal now!", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            if (GetID.ProcessName.Length == 0)
                            {
                                MessageBox.Show("The Process are not alive anymore.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Couldn't Make The Process Un-Critical for some reason.....", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var GetAllProcesses = Process.GetProcesses();
            foreach (Process GetProceses in GetAllProcesses)
            {
                try
                {
                    listView1.FullRowSelect = true;
                    ListViewItem ToColumns = new ListViewItem(GetProceses.ProcessName);
                    ToColumns.SubItems.Add(GetProceses.Id.ToString());
                    bool CheckBeingDebugged = false;
                    CheckRemoteDebuggerPresent(GetProceses.Handle, ref CheckBeingDebugged);
                    if (CheckBeingDebugged == true)
                    {
                        ToColumns.SubItems.Add("True");
                    }
                    else
                    {
                        ToColumns.SubItems.Add("False");
                    }
                    bool IsCritical = false;
                    IsProcessCritical(GetProceses.Handle, ref IsCritical);
                    if (IsCritical == true)
                    {
                        ToColumns.SubItems.Add("True");
                    }
                    else if (IsCritical == false)
                    {
                        ToColumns.SubItems.Add("False");
                    }
                    listView1.Items.Add(ToColumns);
                }
                catch
                {
                    continue;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem GetNameFromList in listView1.SelectedItems)
                {
                    var GetProcessByName = Process.GetProcessesByName(GetNameFromList.Text);
                    foreach (Process GetID in GetProcessByName)
                    {
                        bool IsCritical = false;
                        IsProcessCritical(GetID.Handle, ref IsCritical);
                        if (IsCritical == false)
                        {
                            int Critical = 1;
                            NtSetInformationProcess(GetID.Handle, 0x1D, ref Critical, sizeof(int));
                            IsCritical = false;
                            IsProcessCritical(GetID.Handle, ref IsCritical);
                            if (IsCritical == true)
                            {
                                DialogResult GetResultTerminate = MessageBox.Show("Success! The Process are critical now!", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                            else if (IsCritical == false)
                            {
                                MessageBox.Show("Couldn't Make The Process Un-Critical for some reason.....", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            if (GetID.ProcessName.Length == 0)
                            {
                                MessageBox.Show("The Process are not alive anymore.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Couldn't Make The Process Un-Critical for some reason.....", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
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