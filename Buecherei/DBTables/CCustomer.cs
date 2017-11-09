using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTables
{
    public class CCustomer
    {
        #region Class-Atributes
        private int _iCustID, _iAddrID;
        private string _sForename, _sSurname, _sMailaddress, _sPassword;
        private DateTime _dtBirthday;
        #endregion
        public CCustomer()
        {

        }

        public int cust_id { get => _iCustID; set => _iCustID = value; }
        public int addr_id { get => _iAddrID; set => _iAddrID = value; }
        public string forename { get => _sForename; set => _sForename = value; }
        public string surname { get => _sSurname; set => _sSurname = value; }
        public string mailaddress { get => _sMailaddress; set => _sMailaddress = value; }
        public string password { get => _sPassword; set => _sPassword = value; }
        public DateTime birthday { get => _dtBirthday; set => _dtBirthday = value; }
    }
}
