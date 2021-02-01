using System;
using MySqlConnector;

namespace Holamundo.Models{
    public class DBConexion : IDisposable{
        
        public MySqlConnection Connection {get;}

        public DBConexion(string connectionString){
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();

    }
}