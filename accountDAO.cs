using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using Database1.DAO;
using Database1.Model;

namespace Database1.DAO
{
    public class AccountDAO
    {
        MySqlConnection conn;
        string myConnectionString;
        public AccountDAO()
        {
            myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=MySQLRoot19@;database=BankAPP";
            conn = new MySqlConnection();
            conn.ConnectionString = myConnectionString;
        }
        public User Login(int UserID, int PIN)

        {
            conn.Open();
            string selectloginQuery = " SELECT UserID , PIN , UserName  FROM Customers where UserID = " + UserID + " AND PIN = " + PIN;
            MySqlCommand view = new MySqlCommand(selectloginQuery, conn);
            MySqlDataReader dr = view.ExecuteReader();
            User user1 = new User();
            while (dr.Read())
            {
                user1.UserID = dr.GetInt32(0);
                user1.UserName = dr.GetString(2);

            }
            conn.Close();
            return user1;
        }
    }
}