using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using Database1.DAO;
using Database1.Model;

namespace Database1.Banks
{
    public class Bank
    {
        public int UserID = 0, PIN = 0, UserName = 0;
        AccountDAO accountdao;
        public Bank()
        {
            this.accountdao = new AccountDAO();
        }
        public static void HomePage()
        {
            Console.WriteLine("\t\t--------------------------------------------");
            Console.WriteLine("\t\t|          C ATM BANK LIMITED              |");
            Console.WriteLine("\t\t|           Customer Banking               |");
            Console.WriteLine("\t\t|    1. Deposit Funds                      |");
            Console.WriteLine("\t\t|    2. Withdraw Funds                     |");
            Console.WriteLine("\t\t|    3. Transfer Funds                     |");
            Console.WriteLine("\t\t|    4. Check Account balance              |");
            Console.WriteLine("\t\t|    5. Display Account Transaction Log    |");
            Console.WriteLine("\t\t|    6. Change Pin                         |");
            Console.WriteLine("\t\t|                                          |");
            Console.WriteLine("\t\t|    7.Save & Exit                         |");
            Console.WriteLine("\t\t--------------------------------------------");
            Console.Write("Enter your option: ");
        }

        //main operation funtion
        public void MainAtm()
        {
            Console.Write("UserID: ");
            UserID = Convert.ToInt32(Console.ReadLine());
            Console.Write("PIN: ");
            PIN = Convert.ToInt32(Console.ReadLine());
            User User1 = this.accountdao.Login(UserID, PIN);
            if (User1 != null)
            {
                Console.Clear();
                Console.WriteLine("Welcome {0}!", User1.UserName);
                HomePage();
                int op;
                op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Deposit(User1);
                        break;
                    case 2:
                        Withdraw();
                        break;
                    case 3:
                        Transfer();
                        break;
                    case 4:
                        CheckBalance(User1);
                        break;
                    case 5:
                        Transaction();
                        break;
                    case 6:
                        ChangePin();
                        break;

                    case 7:
                        Console.WriteLine("LoggedOut");
                        break;
                    default:
                        Console.WriteLine("Enter a Vaild Input!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect UserID or Password!!!");
            }


        }
        public static void Deposit(User currentuser)
        {
            try
            {
                int DepositAmount = 0;
                Console.Write("Enter the amount to Deposit: ");
                DepositAmount = Convert.ToInt32(Console.ReadLine());
                MySqlConnection conn;
                string myConnectionString;
                myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=MySQLRoot19@;database=BankAPP";
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                string BalanceCheckSelectQuery = "SELECT TotAmount FROM Bank WHERE UserID =" + currentuser.UserID;
                MySqlCommand view = new MySqlCommand(BalanceCheckSelectQuery, conn);
                conn.Open();
                MySqlDataReader reader = view.ExecuteReader();
                while (reader.Read())
                {
                    int[] TotAmount = new int[1];
                    TotAmount[0] = reader.GetInt32(0);
                    TotAmount[0] = TotAmount[0] + DepositAmount;
                    Console.WriteLine(TotAmount[0]);
                    string BalanceCheckQuery = "UPDATE Bank SET TotAmount = " + TotAmount[0] + "WHERE UserID =" + currentuser.UserID;
                    MySqlCommand view1 = new MySqlCommand(BalanceCheckQuery, conn);
                    while (reader.Read())
                    {
                        Console.WriteLine("Inside while 2");
                    }
                }
                conn.Close();
                Console.WriteLine("Deposited Successfully !");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void Withdraw()
        {

        }

        public static void Transfer()
        {

        }

        public void CheckBalance(User currentuser)
        {
            try
            {
                MySqlConnection conn;
                string myConnectionString;
                myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=MySQLRoot19@;database=BankAPP";
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                string BalanceCheckQuery = " SELECT TotAmount FROM Bank where UserID = " + currentuser.UserID;
                MySqlCommand view = new MySqlCommand(BalanceCheckQuery, conn);
                conn.Open();
                MySqlDataReader reader = view.ExecuteReader();
                int[] Balance = new int[1];
                while (reader.Read())
                {

                    Balance[0] = reader.GetInt32(0);
                }

                conn.Close();
                Console.WriteLine("Your Balance is : " + Balance[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void Transaction()
        {

        }

        public static void ChangePin()
        {

        }

    }





}