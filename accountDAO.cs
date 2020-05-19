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
        ~AccountDAO()
        {
            Console.WriteLine("End");
        }
        public User Login(int UserID, int PIN)

        {
            conn.Open();
            string selectloginQuery = " SELECT  UserID, UserName  FROM Customers where UserID = " + UserID + " AND PIN = " + PIN;
            MySqlCommand view = new MySqlCommand(selectloginQuery, conn);
            MySqlDataReader dr = view.ExecuteReader();
            User user1 = new User();
            while (dr.Read())
            {
                user1.UserID = dr.GetInt32(0);
                user1.UserName = dr.GetString(1);
            }
            conn.Close();
            return user1;
        }
        public User BalanceCheck(int UserID)
        {
            conn.Open();
            string BalanceCheckQuery = " SELECT TotAmount FROM Bank where UserID = " + UserID;
            MySqlCommand view = new MySqlCommand(BalanceCheckQuery, conn);
            MySqlDataReader reader = view.ExecuteReader();
            int[] Balance = new int[1];
            User user2 = new User();
            while (reader.Read())
            {

                Balance[0] = reader.GetInt32(0);
            }

            conn.Close();
            Console.WriteLine("Your Balance is : " + Balance[0]);
            return user2;
        }
        public User Deposit(int UserID, int DepositAmount)
        {
            string BalanceCheckSelectQuery = "SELECT TotAmount FROM Bank WHERE UserID =" + UserID;
            MySqlCommand view = new MySqlCommand(BalanceCheckSelectQuery, conn);
            conn.Open();
            MySqlDataReader reader = view.ExecuteReader();
            User user3 = new User();
            while (reader.Read())
            {
                int[] TotAmount = new int[1];
                TotAmount[0] = reader.GetInt32(0);
                TotAmount[0] = TotAmount[0] + DepositAmount;
                Console.WriteLine(TotAmount[0]);
                string BalanceCheckQuery = "UPDATE Bank SET TotAmount ="+TotAmount[0]+"WHERE UserID =" + UserID;
                MySqlCommand views = new MySqlCommand(BalanceCheckQuery, conn);
                if (reader.Read())
                {
                    Console.WriteLine("Updated Amount"+TotAmount[0]);
                }
            }
            conn.Close();
            Console.WriteLine("Deposited Successfully !");
            return user3;
        }
        public User PINChange(int UserID, int NewPIN)
        {
            conn.Open();
            string selectNewPINChagneQuery = " UPDATE Customers SET PIN = " + NewPIN + "where UserID = " + UserID; ;
            MySqlCommand Upadtecommand = new MySqlCommand(selectNewPINChagneQuery, conn);
            User user4 = new User();
            conn.Close();
            return user4;
        }
    }
}