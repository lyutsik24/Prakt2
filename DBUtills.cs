using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Prakt2
{
    class DBUtills
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "37.77.105.162";
            string db = "Kostya";
            int port = 3306;
            string user = "Lyuts";
            string pass = "meox9AKR@";

            return DB.GetConnection(host, db, port, user, pass);
        }
    }
}
