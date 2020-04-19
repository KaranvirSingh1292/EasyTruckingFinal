using CommonModels;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.BusinessLogic
{
    public class LoadsHandler
    {
        private IConfiguration _configuration;
        private DbLoadHelper data;

        public LoadsHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            data = new DbLoadHelper(_configuration);
        }

        public List<LoadsModel> GetAvailableLoads()
        {
            var loads = data.GetAvailableLoad();
            return loads;
        }

        public void AcceptLoad(int load_id, int driver_id)
        {
            data.AcceptLoad(load_id, driver_id);
        }

        public List<LoadsModel> GetMyLoads(int id)
        {
            var loads = data.GetMyLoads(id);
            return loads;
        }

        public void CreateLoad(LoadsModel model)
        {
            data.CreateLoad(model);
        }

        public List<LoadsModel> GetMyLoadsDP(int id)
        {
            var loads = data.GetMyLoadsDP(id);
            return loads;
        }

        public List<LoadsModel> GetAcceptedLoadsDP(int id)
        {
            var loads = data.GetAcceptedLoadsDP(id);
            return loads;
        }
    }
}
