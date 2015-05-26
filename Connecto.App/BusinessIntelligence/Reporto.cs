using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Connecto.App.Models;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.BusinessIntelligence
{
    public static class Reporto
    {
        public static LocalReport Execute(ReportCriteriaViewModel vm)
        {
            if (!File.Exists(vm.ReportPath)) return null;

            var connectionString = ConfigurationManager.ConnectionStrings["ConnectoDb"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand
            {
                CommandText = vm.CommandText,
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            foreach (var parameter in GetParameters(vm)) cmd.Parameters.Add(parameter);

            var adapter = new SqlDataAdapter(cmd);
            var dataset = new DataSet();

            adapter.Fill(dataset);

            var rd = new ReportDataSource("Dataset", dataset.Tables[0]);
            var lr = new LocalReport { ReportPath = vm.ReportPath };
            lr.DataSources.Add(rd);
            return lr;
        }

        private static IEnumerable<SqlParameter> GetParameters(ReportCriteriaViewModel vm)
        {
            var items = new List<SqlParameter>();
            if (vm.Id > 0) items.Add(new SqlParameter(string.Format("@Id"), vm.Id));
            if (vm.ProductId > 0) items.Add(new SqlParameter(string.Format("@ProductId"), vm.ProductId));
            if (!string.IsNullOrEmpty(vm.Date)) items.Add(new SqlParameter(string.Format("@Date"), vm.Date));

            if (string.IsNullOrEmpty(vm.DateRange)) return items;
            var dates = vm.DateRange.Split('-');
            if (dates[0] != null) items.Add(new SqlParameter(string.Format("@DateFrom"), dates[0]));
            if (dates[1] != null) items.Add(new SqlParameter(string.Format("@DateTo"), dates[1]));
            return items;
        }

        public static string[] GetRenderControls(string parameters)
        {
            if (string.IsNullOrEmpty(parameters)) return new string[] {};
            return parameters.Split(',');
        }
    }

}