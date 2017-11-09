﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO delete unnecessary using directives 

namespace DBAccess
{
    public class DBConnection
    {
        #region Class-Attributes

        private string sDBIP;
        private int iDBPort;
        private string sDBUser;
        private string sDBPass;
        private string sDBName;
        private MySqlConnection dbconnect;

        #endregion 

        #region Konstruktor

        public DBConnection()
        {
            StreamReader reader = new StreamReader("C:" + Environment.ExpandEnvironmentVariables("%HOMEPATH%") + "\\.connectionDB.cfg");
            string connect = reader.ReadLine();
            string[] allDataForDB = connect.Split(';');
            this.sDBIP = allDataForDB[0];
            this.iDBPort = Convert.ToInt32(allDataForDB[1]);
            this.sDBUser = allDataForDB[2];
            this.sDBPass = allDataForDB[3];
            this.sDBName = allDataForDB[4];

        }

        #endregion

        #region Database Connection

        public void OpenDBConnection()
        {
            MySqlConnectionStringBuilder con_string = new MySqlConnectionStringBuilder()
            {
                Server = sDBIP,
                Port = Convert.ToUInt32(iDBPort),
                UserID = sDBUser,
                Password = sDBPass,
                Database = sDBName
            };
            dbconnect = new MySqlConnection(con_string.ToString());

            try
            {
                dbconnect.Open();
            }
            catch (MySqlException)
            {

                throw;
            }
        }

        public void CloseDBConnection()
        {
            dbconnect.Close();
        }
        #endregion 
    }
}
