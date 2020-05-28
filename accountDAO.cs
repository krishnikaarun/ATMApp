using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using Database1.DAO;
using Database1.Model;
using Microsoft.Extensions.Configuration;

namespace Database1.DAO
{
    public class AccountDAO
    {
        public MySqlConnection conn;
        public string myConnectionString;

        public AccountDAO()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            var section = Configuration.GetSection("ConnectionString");
            myConnectionString = section.Value;
            MySqlConnection mySqlConnection = new MySqlConnection();
            conn = mySqlConnection;
            conn.ConnectionString = myConnectionString;
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

        public void BalanceCheck(int UserID)
        {
            conn.Open();
            string BalanceCheckQuery = " SELECT TotAmount FROM Bank where UserID = " + UserID;
            MySqlCommand view = new MySqlCommand(BalanceCheckQuery, conn);
            MySqlDataReader reader = view.ExecuteReader();
            int[] Balance = new int[1];
            while (reader.Read())
            {
                Balance[0] = reader.GetInt32(0);
            }
            conn.Close();
            Console.WriteLine("Your Balance is : " + Balance[0]);
        }

        public void Deposit(int UserID, int DepositAmount)
        {
            string selectBankQuery = "SELECT TotAmount FROM Bank WHERE UserID =" + UserID;
            MySqlCommand view = new MySqlCommand(selectBankQuery, conn);
            conn.Open();
            MySqlDataReader reader = view.ExecuteReader();
            if (reader.Read())
            {
                int[] TotAmount = new int[1];
                TotAmount[0] = reader.GetInt32(0);
                TotAmount[0] += DepositAmount;
                conn.Close();
                UpdateAmount(UserID, TotAmount[0]);
                InsertDepositTrans(UserID, DepositAmount);
            }
        }

        public void PINChange(int UserID, int NewPIN)
        {
            string NewPINChangeQuery = "UPDATE  Customers SET Pin=" + NewPIN + " where UserID = " + UserID;
            MySqlCommand updateCommand = new MySqlCommand(NewPINChangeQuery, conn);
            conn.Open();
            updateCommand.ExecuteReader();
            conn.Close();
        }

        public void Withdraw(int UserID, int WithdrawAmount)
        {
            string selectBankQuery = "SELECT TotAmount FROM Bank WHERE UserID =" + UserID;
            MySqlCommand view = new MySqlCommand(selectBankQuery, conn);
            conn.Open();
            MySqlDataReader reader = view.ExecuteReader();
            if (reader.Read())
            {
                int[] TotAmount = new int[1];
                TotAmount[0] = reader.GetInt32(0);
                conn.Close();
                if (WithdrawAmount <= TotAmount[0])
                {
                    TotAmount[0] -= WithdrawAmount;
                    Console.WriteLine(TotAmount[0]);
                    UpdateAmount(UserID, TotAmount[0]);
                    InsertWithdrawTrans(UserID, WithdrawAmount);
                }
            }
        }

        public void UpdateAmount(int UserID, int TotAmount)
        {
            string UpdateQuery = "UPDATE  Bank SET TotAmount =" + TotAmount + " where UserID = " + UserID;
            MySqlCommand updateCommand = new MySqlCommand(UpdateQuery, conn);
            conn.Open();
            updateCommand.ExecuteNonQuery();
            conn.Close();
        }

        public void InsertDepositTrans(int UserID, int TotAmount)
        {
            string selectTransQuery = "SELECT AccountNo from Trans Where UserID=" + UserID;
            MySqlCommand view = new MySqlCommand(selectTransQuery, conn);
            conn.Open();
            MySqlDataReader reader = view.ExecuteReader();
            int[] AccountNo = new int[1];
            if (reader.Read())
            {
                AccountNo[0] = reader.GetInt32(0);
            }
            conn.Close();
            string InsertTransQuery = "INSERT INTO Trans (CD,Amount,AccountNo,UserID,Dated) VALUES ('C'," + TotAmount + "," + AccountNo[0] + "," + UserID + ",@DATE)";
            MySqlCommand updateCommand = new MySqlCommand(InsertTransQuery, conn);
            updateCommand.Parameters.AddWithValue("@DATE", DateTime.Now);
            conn.Open();
            updateCommand.ExecuteNonQuery();
            conn.Close();
        }

        public void InsertWithdrawTrans(int UserID, int TotAmount)
        {
            string selectTransQuery = "SELECT AccountNo from Trans Where UserID=" + UserID;
            MySqlCommand view = new MySqlCommand(selectTransQuery, conn);
            conn.Open();
            MySqlDataReader reader = view.ExecuteReader();
            int[] AccountNo = new int[1];
            if (reader.Read())
            {
                AccountNo[0] = reader.GetInt32(0);
            }
            conn.Close();
            string InsertTransQuery = "INSERT INTO Trans (CD,Amount,AccountNo,UserID) VALUES ('D'," + TotAmount + "," + AccountNo[0] + "," + UserID + ",@DATE)";
            MySqlCommand updateCommand = new MySqlCommand(InsertTransQuery, conn);
            updateCommand.Parameters.AddWithValue("@DATE", DateTime.Now);
            conn.Open();
            updateCommand.ExecuteNonQuery();
            conn.Close();
        }

        public User[] Transact(int UserID)
        {
            int i = 0;
            string countTransQuery = "SELECT COUNT(*) FROM Trans WHERE UserID = " + UserID;
            MySqlCommand countCommand = new MySqlCommand(countTransQuery, conn);
            conn.Open();
            Int64 n = (Int64)countCommand.ExecuteScalar();
            User[] Tran1 = new User[n];
            string TransLogQuery = "SELECT TransID, CD, Amount, AccountNo, UserID, Dated FROM Trans WHERE UserID = " + UserID;
            MySqlCommand selectCommand = new MySqlCommand(TransLogQuery, conn);
            MySqlDataReader reader = selectCommand.ExecuteReader();
            Console.WriteLine("  TranID     CD     Amount      AccountNo      Date");
            while (reader.Read())
            {
                User Tran = new User
                {
                    TransID = reader.GetInt32(0),
                    CD = reader.GetString(1),
                    Amount = reader.GetInt32(2),
                    AccountNo = reader.GetInt32(3),
                    Dated = reader.GetString(5)
                };
                Tran1[i] = Tran;
                Console.WriteLine("  " + Tran.TransID + "      " + Tran.CD + "      " + Tran.Amount + "        " + Tran.AccountNo+"       "+Tran.Dated);
                i++;
            }
            conn.Close();
            return Tran1;
        }
    }
}