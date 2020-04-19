using CommonModels;
using DataAccessLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.BusinessLogic
{
    public class LoginHandler
    {
        private IConfiguration _configuration;


        public LoginHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<string> VerifyUser(LoginModel login)
        {
            DbLoginHelper data = new DbLoginHelper(_configuration);
            LoginModel rlogin = data.verifyUser(login);
            
            List<string> response = new List<string>();
            if (rlogin != null)
            {
                //username found, now verify password
                if (CommonModels.Hashing.VerifyHash(login.Password, "SHA512", rlogin.Password))
                {
                    response.Add("1");
                    response.Add(rlogin.Id.ToString());
                    response.Add(rlogin.Role);
                }
                else
                {
                    response.Add("2");
                }
            }

            //username doesnot  found
            else
            {
                response.Add("0");
            }
            
            return response;
        }
    }
}
