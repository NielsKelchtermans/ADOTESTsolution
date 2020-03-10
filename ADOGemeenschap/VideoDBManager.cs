﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace ADOGemeenschap
{
    public class VideoDBManager
    {
        private static ConnectionStringSettings conVideoSetting = ConfigurationManager.ConnectionStrings["videoAdo"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conVideoSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conVideo = factory.CreateConnection();
            conVideo.ConnectionString = conVideoSetting.ConnectionString;
            return conVideo;
        }
    }
}
