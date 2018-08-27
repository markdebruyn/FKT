using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Finance_Keeper_Tool
{
    class DBConnection
    {
        // sorce: https://stackoverflow.com/questions/21618015/how-to-connect-to-mysql-database

        
        private string server = "localhost";
        private string database = "financialdb";
        private string username = "root";
        private string password = "R00t";


        public DBConnection(string _server, string _database, string _username, string _password)
        {
            if (!String.IsNullOrEmpty(_server))
            {
                server = _server;
            }
            if (!String.IsNullOrEmpty(_database))
            {
                database = _database;
            }
            if (!String.IsNullOrEmpty(_username))
            {
                username = _username;
            }
            if (!String.IsNullOrEmpty(_password))
            {
                password = _password;
            }
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get
            {
                return databaseName;
            }
            set
            {
                databaseName = value;
            }
        }

        public string Password
        {
            get; set;
        }

        private MySqlConnection sqlConnection = null;
        public MySqlConnection SqlConnection
        {
            get
            {
                return sqlConnection;
            }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection(null, null, null, null);
            return _instance;
        }

        public bool Login(string username, string password)
        {
            string query = "SELECT * FROM financialdb.users WHERE username ='" + username + "';";
            query = "SELECT * FROM financialdb.users WHERE username = 'Mark'";
            MySqlCommand loginQuery = new MySqlCommand(query, sqlConnection);
            try
            {
                MySqlDataReader reader = loginQuery.ExecuteReader();
                if (reader != null)
                {
                    string test = string.Empty;
                    while(reader.Read())
                    {
                        
                        string tefde = reader["Username"].ToString();
                        if (tefde == username)
                        {
                            if (reader["Password"].ToString() == password)
                            {
                                Close();
                                return true;
                            }
                        }
                    }
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    Close();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public void ReciveData(string query, List<string> parameterList)
        {

          //  MySqlCommandBuilder mySqlCommand = new MySqlCommandBuilder();
          //  MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlConnection);
            MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);

            //  private String readCommand = "SELECT LEVEL FROM USERS WHERE VAL_1 = @param_val_1 AND VAL_2 = @param_val_2;";


            sqlCommand.Parameters.AddRange(parameterList.ToArray()); 

            sqlCommand.Parameters.AddWithValue("@param_val_1", parameterList[0]);
            sqlCommand.Parameters.AddWithValue("@param_val_2", parameterList[1]);
          //  level = Convert.ToInt32(m.ExecuteScalar());
         
       

    }

 


        public bool Connect()
        {
            if (sqlConnection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                {
                    return false;
                }
                MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder();
                connectionString.Server = server;
                connectionString.Database = database;
                connectionString.UserID = username;
                connectionString.Password = password;
                sqlConnection = new MySqlConnection(connectionString.ToString());
                sqlConnection.Open();
            }

            return true;
        }

        public void Close()
        {
            sqlConnection.Close();
        }
    }

}
