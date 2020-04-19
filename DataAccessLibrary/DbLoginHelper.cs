using CommonModels;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace DataAccessLibrary
{
    public class DbLoginHelper
    {
        private IConfiguration _configuration;
        private readonly string DbConnectionString = "DefaultConnection";

        public DbLoginHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LoginModel verifyUser(LoginModel user)
        {

             string query = "SELECT * FROM Users WHERE Username='" + user.Username + "'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                 return connection.QuerySingleOrDefault<LoginModel>(query);
            }

        }


    }
}
