using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FastReport;
using FastReport.Data;
using FastReport.Utils;
using FastReport.Web;
using Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc;

namespace Merkato.Controllers
{
     
    public class ReportsController : Controller
    {
        private  MerkatoDbContext ctx;

        public IActionResult Index()
        {
            using (ctx = new MerkatoDbContext(null))
            {
               WebReport report = new WebReport();
               
                //Report.Report.Load($@"Reports/Simple List.frx");
                RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                report.Report.Load($@"Reports/LocationForm.frx");
                var data = (from location in ctx.Location
                         join outlet in ctx.Outlet on location.Id equals outlet.LocationId
                         
                       select new { Location = location, Outlet = outlet })
                       .Select(a => new Location { Code = a.Outlet.Name, Name= a.Location.Name }).ToList();
                //var data = ctx.Outlet.ToList().AsEnumerable();
                //var databand = (DataBand)report.Report.AllObjects[6];
                //databand.Report.RegisterData(data, "LocationDS");
                //var source = new FastReport.Data.
                //databand.DataSource = 
                //   .LoadData(new System.Collections.ArrayList(data));
                //databand.InitDataSource();
                //var dataSet = new DataSet();
                //dataSet.ReadXml(@"Reports/nwind.xml");

                                
                report.Report.RegisterData(data, "LocationDS");
                report.Report.GetDataSource("LocationDS").Enabled = true;

                var databand = (DataBand)report.Report.FindObject("Data1");
                databand.DataSource = report.Report.GetDataSource("LocationDS");
                ViewBag.WebReport = report;
                return View();
            }
           

           
        }
    }

    internal class MsSqlDataConnection : XmlDataConnection
    {
    }
}