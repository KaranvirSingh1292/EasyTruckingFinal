using CommonModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;

namespace DataAccessLayer
{
    public class DbLoadHelper
    {
        private IConfiguration _configuration;
        private readonly string DbConnectionString = "DefaultConnection";

        public DbLoadHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<LoadsModel> GetAvailableLoad()
        {
            List<LoadsModel> loads = null;
            string query = "SELECT Loads.*,Users.Username AS Dispatcher_Name FROM Users INNER JOIN Loads ON Loads.Dispatcher_Id=Users.Id WHERE Loads.Driver_Id IS NULL";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                loads = connection.Query<LoadsModel>(query).ToList();
            }
            return loads;
        }

        public void AcceptLoad(int id, int driver)
        {
            string query = "UPDATE Loads SET Driver_Id='"+driver+ "', Accepted_On=cast(GETDATE() as Date) WHERE Id ='" + id+"'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                connection.Query(query);
            }
        }

        public List<LoadsModel> GetMyLoads(int id)
        {
            List<LoadsModel> loads = null;
            string query = "SELECT Loads.*,Users.Username AS Dispatcher_Name FROM Users INNER JOIN Loads ON Loads.Dispatcher_Id=Users.Id WHERE Driver_Id='" + id+ "' ORDER BY Loads.Id DESC";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                loads = connection.Query<LoadsModel>(query).ToList();
            }
            return loads;
        }


        public void CreateLoad(LoadsModel model)
        {
            string query = "INSERT INTO Loads (Items,Pickup_Location,Drop_Location,Pay,Dispatcher_Id,Created_On) VALUES ('"+model.Items+"','"+model.Pickup_Location+"','"+model.Drop_Location+"','"+model.Pay+"','"+model.DispatcherID+"',cast(GETDATE() as Date))";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                connection.Query(query);
            }
        }


        public List<LoadsModel> GetMyLoadsDP(int id)
        {
            List<LoadsModel> loads = null;

            string query = "SELECT * FROM Loads WHERE Dispatcher_Id='" + id + "' ORDER BY Id DESC";
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                loads = connection.Query<LoadsModel>(query).ToList();
            }
            return loads;
        }

        public List<LoadsModel> GetAcceptedLoadsDP(int id)
        {
            List<LoadsModel> loads = null;
            
            string query = "SELECT Loads.*,Users.Username AS Driver_Name FROM Users RIGHT JOIN Loads ON Loads.Driver_Id=Users.Id WHERE Loads.Driver_Id IS NOT NULL AND Loads.Dispatcher_Id='" + id+"'";
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.BlogConnectionStringValue(_configuration, DbConnectionString)))
            {
                loads = connection.Query<LoadsModel>(query).ToList();
            }
            return loads;
        }
    }
}
