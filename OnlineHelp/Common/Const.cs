﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
namespace OnlineHelp.Common
{
    public static class Const
    {
        public static string Connectring=  ConfigurationManager.ConnectionStrings["ConString"].ConnectionString; 
     
    }
    public class CommonFunction
    {
        public static bool GetLoginStatus(string UserId, string Password)
        {
            string sqlcommand = string.Format("select count(1) from Users where UserId = '{0}' and Password = '{1}'", UserId, Password);
            return (int)SqlHelper.ExecuteScalar(Const.Connectring, CommandType.Text, sqlcommand) > 0;
        }
    }
}