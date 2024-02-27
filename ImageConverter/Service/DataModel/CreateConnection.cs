using System;
using System.Collections;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ImageConverter.DataModel
{
    public class CreateConnection
    {
        string strConData;
        public SqlConnection obdConData;
        SqlDataAdapter objdDa;
        SqlCommand objComData;
        private DataSet mSQLDataSet;

        public CreateConnection()
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            strConData = myConfig.GetValue<string>("ConnectionStrings:DefaultConnection");
            obdConData = new SqlConnection();
            objComData = new SqlCommand();
        }
    }
}
