﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace VatTu.Report
{
    public partial class XtraReport_DonDatHangChuaPhieuNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_DonDatHangChuaPhieuNhap()
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Fill();
        }

    }
}
