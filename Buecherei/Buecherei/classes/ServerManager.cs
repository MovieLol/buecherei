using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//TODO: delete unnecessary using directives 

namespace Buecherei.classes
{
    class ServerManager
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

        public ServerManager()
        {
            StreamReader reader = new StreamReader("C:" + Environment.ExpandEnvironmentVariables("%HOMEPATH%") + "\\.connectionDB.cfg");
            string connect = reader.ReadLine();
            string[] allDataForDB = connect.Split(';');
            this.sDBIP   = allDataForDB[0];
            this.iDBPort = Convert.ToInt32(allDataForDB[1]);
            this.sDBUser = allDataForDB[2];
            this.sDBPass = allDataForDB[3];
            this.sDBName = allDataForDB[4];

        }

        #endregion

        #region Create Database Connection

        public MySqlConnection CreateConnectionString()
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
            return dbconnect;
        }
        #endregion 
    }
}
