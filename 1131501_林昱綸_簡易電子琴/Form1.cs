using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace _1131501_林昱綸_簡易電子琴
{
    public partial class frmBeepPlayer : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);
        int[] freq = { 523, 587, 659, 698, 784, 880, 988, 1046 };
        bool isLoaded = false; // ✅ 新增旗標，避免 SizeChanged 早於 Load 執行
        public frmBeepPlayer()
        {
            InitializeComponent();
            InitializeButton();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            Beep(freq[btn.TabIndex], 300);
            btn.Enabled = true;
        }
        private void InitializeButton()
        {
            // 讓btn1~btn8共用同一個事件處理函式
            btn2.Click += btn1_Click;
            btn3.Click += btn1_Click;
            btn4.Click += btn1_Click;
            btn5.Click += btn1_Click;
            btn6.Click += btn1_Click;
            btn7.Click += btn1_Click;
            btn8.Click += btn1_Click;
        }
        // =========================================
        // 視窗縮放功能
        // =========================================

        int initWidth = 0;
        int initHeight = 0;
        Dictionary<string, Rect> initControl = new Dictionary<string, Rect>();

        private void frmBeepPlayer_Load(object sender, EventArgs e)
        {
            this.initWidth = this.palMain.Width;
            this.initHeight = this.palMain.Height;

            foreach (Control ctl in this.palMain.Controls)
            {
                if (string.IsNullOrEmpty(ctl.Name)) continue; // ✅ 跳過無名控制項
                this.initControl.Add(ctl.Name, new Rect(ctl.Left, ctl.Top,
                    ctl.Width, ctl.Height));
            }

            isLoaded = true; // ✅ Load 完成才允許 SizeChanged 執行
        }

        private void frmBeepPlayer_SizeChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return; // ✅ 未完成 Load 就直接返回

            double width = this.palMain.Width;
            double height = this.palMain.Height;
            double iRatioWith = width / this.initWidth;
            double iRatioHeight = height / this.initHeight;

            foreach (Control ctl in this.palMain.Controls)
            {
                if (!initControl.ContainsKey(ctl.Name)) continue; // ✅ 安全檢查

                ctl.Left = (int)(initControl[ctl.Name].Left * iRatioWith);
                ctl.Top = (int)(initControl[ctl.Name].Top * iRatioHeight);
                ctl.Width = (int)(initControl[ctl.Name].Width * iRatioWith);
                ctl.Height = (int)(initControl[ctl.Name].Height * iRatioHeight);
            }
        }
        private void frmBeepPlayer_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "確定要關閉應用程式嗎？",
                "關閉確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
