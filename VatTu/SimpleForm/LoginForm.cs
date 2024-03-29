﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VatTu
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.v_DS_PHANMANHTableAdapter.Fill(this.qLVT_DATHANGDataSet.V_DS_PHANMANH);
            //comboBox_ChiNhanh.SelectedIndex = 1;
            //comboBox_ChiNhanh.SelectedIndex = 0;
        }

        private void Login() {
            if (textBox_Username.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền login name!", "Error message", MessageBoxButtons.OK);
                textBox_Username.Focus();
                return;
            }
            if (textBox_Password.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền password!", "Error message", MessageBoxButtons.OK);
                textBox_Username.Focus();
                return;
            }
            Program.mlogin = textBox_Username.Text;
            Program.password = textBox_Password.Text;
            Program.serverName = this.comboBox_ChiNhanh.SelectedValue.ToString();
            if (Program.KetNoi() == 0) return; // Bắt đầu kết nối tới database

            //Program.tenChiNhanh = comboBox_ChiNhanh.Text; // Lay ten chi nhanh da dang nhap
            Program.mChinhanh = comboBox_ChiNhanh.SelectedIndex;
            Program.bds_dspm = vDSPHANMANHBindingSource;
            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;
            Program.serverNameHT = Program.serverName;
            Program.conn = new SqlConnection(Program.connstr);
            Program.conn.Open();

            SqlDataReader myReader;

            String strleng = "EXEC SP_DANGNHAP '" + Program.mloginDN + "'";
            myReader = Program.ExecSqlDataReader(strleng);
            if (myReader == null) return;
            myReader.Read();

            Program.username = myReader.GetString(0);
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login của bạn không có quyền truy cập dữ liệu", "", MessageBoxButtons.OK);
                return;
            }

            Program.maNV = int.Parse(Program.username);
            Program.mHoten = myReader.GetString(1);
            Program.mGroup = myReader.GetString(2);
            myReader.Close();
           
            Program.formMain = new FormChinh(); 
            Program.formMain.tslMaNV.Text = "Mã nhân viên: " + Program.username;
            Program.formMain.tslTen.Text = "Họ tên: " + Program.mHoten;
            Program.formMain.tslNhom.Text = "Nhóm: " + Program.mGroup;
            Program.formMain.Activate();
            Program.formMain.Show();
            this.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát hoàn toàn chương trình không?", "Thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_ChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
