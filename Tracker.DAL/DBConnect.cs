using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace Tracker.DAL
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            /*server = "localhost";
            database = "tracker";
            uid = "root";
            password = "password01";
            
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password ;*/
            connectionString = ConfigurationManager.ConnectionStrings["TrackerWebContext"].ConnectionString;
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        break;

                    case 1045:
                        break;
                }
                throw ex;
                //return false;
            }

        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
        //Insert statement
        public int Insert(string query)
        {
            try
            {
                int ret = -1;
                ///string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    ret = cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                }
                return ret;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string query)
        {
            //string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";
            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                }
            }
            finally
            {
                this.CloseConnection();
            }
        }

        //Select statement
        public DataSet Select(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataAdapter dadp = new MySqlDataAdapter(cmd);
                    dadp.Fill(ds);
                    return ds;
                    //close connection
                    this.CloseConnection();
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
