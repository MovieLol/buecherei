using MySql.Data.MySqlClient;
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
        private static DBConnection dbcInstance;

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

        #region Database Methods

        protected T GetObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public List<T> ReadDB<T>(string sSQL)
        {
            try
            {
                //Verbindung öffnen
                OpenDBConnection();

                //Ausführen des SQL-Statements
                MySqlCommand msCommand = new MySqlCommand(sSQL);
                msCommand.Connection = dbconnect;

                //Ergebnis in eine Liste Speichern
                //Liste hat den Typ T
                //T ist der Typ des Zielobjekts
                //Zielobjekte sind Objekte der Klassen der DB-Tabellen
                //Ermöglicht eine universelle Methode, welche Datenbankabfragen durchführen kann und zwar für verschiedene Typen
                //Typen müssen Objekte der Klassen sein
                //Gewünschte DB-Spalten müssen in den Klassen eine Property des selben Namens haben!!
                //Rückgabe ist eine Liste von Objekten der gesuchten Klasse
                MySqlDataReader msDataReader = msCommand.ExecuteReader();
                List<T> lst = new List<T>();
                while (msDataReader.Read())     //Solange im SQL Ergebnis etwas drin steht
                {
                    T temp = GetObject<T>();
                    for (int t = 0; t < msDataReader.FieldCount; t++)   //Für jede "Spalte" die aus der Datenbank geholt wird
                    {
                        typeof(T).GetProperty(msDataReader.GetName(t)).SetValue(temp, msDataReader.GetValue(t));  //Speichere Spaltenwert in ein Objekt  
                    }
                    lst.Add(temp);  //Füge Wert einer Liste von Objekten hinzu               
                }
                return lst; //Zurückgeben der Liste mit Objekten
            }
            catch (Exception e)
            {
                //Abfangen der Fehlermeldung
                //MessageBox.Show("Fehler bei der Datenbankabfrage!\n\n" + e.ToString(), "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;

            }
            finally
            {
                //Verbindung schliessen
                CloseDBConnection();
            }

        }

        #endregion
    }
}
