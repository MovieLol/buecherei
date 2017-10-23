using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Buecherei.classes
{
    class ServerManager
    {
        private string DBIP;
        private int DBPort;
        private string DBUser;
        private string DBPass;
        private string DBName;
        MySqlConnection dbconnect;

        public ServerManager()
        {
            StreamReader reader = new StreamReader("C:" + Environment.ExpandEnvironmentVariables("%HOMEPATH%") + "\\.connectionDB.cfg");
            string connect = reader.ReadLine();
            string[] allDataForDB = connect.Split(';');
            this.DBIP   = allDataForDB[0];
            this.DBPort = Convert.ToInt32(allDataForDB[1]);
            this.DBUser = allDataForDB[2];
            this.DBPass = allDataForDB[3];
            this.DBName = allDataForDB[4];

        }

        public MySqlConnection CreateConnectionString()
        {
            MySqlConnectionStringBuilder con_string = new MySqlConnectionStringBuilder()
            {
                Server = DBIP,
                Port = Convert.ToUInt32(DBPort),
                UserID = DBUser,
                Password = DBPass,
                Database = DBName
            };
            dbconnect = new MySqlConnection(con_string.ToString());
            return dbconnect;
        }
    }
}
