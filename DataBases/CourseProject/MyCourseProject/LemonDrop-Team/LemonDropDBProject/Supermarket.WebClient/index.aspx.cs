﻿using Supermarket.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Supermarket.WebClient
{
    public partial class index : System.Web.UI.Page
    {
        protected string dataDir = "data";
        protected string resultDir = "result";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath(resultDir)))
            {
                Directory.CreateDirectory(Server.MapPath(resultDir));
            }
        }

        protected void btnGenerateSQL_Click(object sender, EventArgs e)
        {
            Task1CopyToSQL.CopyMysqlToSql();

            string zipPath = Server.MapPath(dataDir + "/Sample-Sales-Reports.zip");
            string tempDir = Server.MapPath(@"tempDir");
            Task1CopyToSQL.ReadFromExcel(tempDir, zipPath);
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            string savePath = resultDir + "/Aggregated-Sales-Report.pdf";
            Task2GeneratePDF.PDFGenarator(Server.MapPath(savePath));
        }

        protected void btnGenerateXMLVendors_Click(object sender, EventArgs e)
        {
            string savePath = resultDir + "/Aggregated-Sales-Report.xml";
            Task3GenerateXML.GenerateXMLSalesRaport(Server.MapPath(savePath));
        }

        protected void btnProductReports_Click(object sender, EventArgs e)
        {
            string savePath = resultDir + "/Product-Reportst.json";
            Task4ProductReport.GeneraateMongoDRecords(Server.MapPath(savePath));
            grdResultFill();
        }

        private void grdResultFill()
        {
            grdResult.DataSource = MongoDbProvider.db.LoadData<ProductRaport>().ToList();
            grdResult.DataBind();
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            grdResult.DataSource = MongoDbProvider.db.LoadData<ProductRaport>().ToList();
            grdResult.DataBind();
        }

        protected void btnLoadVendorExpenses_Click(object sender, EventArgs e)
        {
            string readPath = dataDir + "/VendorExpenses.xml";
            Task5LoadVendorExpensesFromXML.LoadVendorExpensesFromXML(Server.MapPath(readPath));
        }

        protected void grdResult5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult5.PageIndex = e.NewPageIndex;
            grdResult5.DataSource = MongoDbProvider.db.LoadData<VendorExpenses>().ToList();
            grdResult5.DataBind();
        }

        private void grdResultFill5()
        {
            grdResult5.DataSource = MongoDbProvider.db.LoadData<VendorExpenses>().ToList();
            grdResult5.DataBind();
        }

        protected void btnShowVendorExpenses_Click(object sender, EventArgs e)
        {
            grdResultFill5();
        }

        protected void btnVendorsTotalReport_Click(object sender, EventArgs e)
        {
            string savePath = resultDir + "/Products-Total-Report.xlsx";
            Task6VendorsTotalReport.GenerateExcelVendorsTotalReport(Server.MapPath(savePath));
        }

        protected void btnOpenExcelFile_Click(object sender, EventArgs e)
        {
            Response.Redirect(resultDir + "/Products-Total-Report.xlsx");
        }

        protected void btnOpenPDF_Click(object sender, EventArgs e)
        {
            Response.Redirect(resultDir + "/Aggregated-Sales-Report.pdf");
        }

        protected void btnOpenXML_Click(object sender, EventArgs e)
        {
            Response.Redirect(resultDir + "/Aggregated-Sales-Report.xml");
        }

        
    }
}