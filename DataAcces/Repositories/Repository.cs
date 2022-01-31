using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositorios
{
    public abstract class Repository
    {
        private readonly string connectionString;

        public Repository()
        {
            //Referenciamos a System.Configuration

           connectionString = ConfigurationManager.ConnectionStrings["EasyExchange"].ToString();
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
