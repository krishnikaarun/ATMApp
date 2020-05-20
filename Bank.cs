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
        public int UserID = 0, PIN = 0, UserName = 0, UserID2 = 0, ToAmount = 0;
        AccountDAO accountdao;
        public Bank()
        {
            this.accountdao = new AccountDAO();
        }
        public static void HomePage()
        {
            DateTime now = DateTime.Now;
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
            Console.WriteLine("\t\t|    Login Time:  {0}   |", now.ToLocalTime());
            Console.WriteLine("\t\t--------------------------------------------");
            Console.Write("Enter your option: ");
        }

        //main operation funtion
        public void MainAtm()
        {
            try
            {
                Console.Write("UserID: ");
                UserID = Convert.ToInt32(Console.ReadLine());
                Console.Write("PIN: ");
                PIN = Convert.ToInt32(Console.ReadLine());
                User User1 = this.accountdao.Login(UserID, PIN);
                if (User1.UserID != 0)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome {0}!", User1.UserName);
                    HomePage();
                    int op;
                    op = Convert.ToInt32(Console.ReadLine());

                    switch (op)
                    {
                        case 1:
                            try
                            {
                                int DepositAmount = 0;
                                Console.Write("Enter the amount to Deposit: ");
                                DepositAmount = Convert.ToInt32(Console.ReadLine());
                                this.accountdao.Deposit(UserID, DepositAmount);
                                Console.WriteLine("Deposit is Successful !");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error in Deposit !: " + e.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                int WithdrawAmount = 0;
                                Console.Write("Enter the amount to withdraw: ");
                                WithdrawAmount = Convert.ToInt32(Console.ReadLine());
                                if (WithdrawAmount > 0)
                                {
                                    this.accountdao.Withdraw(UserID, WithdrawAmount);
                                    Console.WriteLine("Withdraw is Successful !");

                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error in Withdraw !: " + e.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                Console.Write("Enter the UserID to which Amount must be transferd :");
                                UserID2 = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter the Amount to transfer :");
                                ToAmount = Convert.ToInt32(Console.ReadLine());
                                //this.accountdao.Transfer(UserID,UserID2,ToAmount);
                                this.accountdao.Withdraw(UserID, ToAmount);
                                this.accountdao.Deposit(UserID2, ToAmount);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error in Transfer!: " + e.Message);
                            }
                            break;
                        case 4:
                            try
                            {
                                User User2 = this.accountdao.BalanceCheck(UserID);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error in BalanceCheck!: " + e.Message);
                            }
                            break;
                        case 5:
                            //User User5 = this.accountdao.Transation[] Transations (UserID);
                            Transaction();
                            break;
                        case 6:
                            try
                            {
                                int NewPIN = 0;
                                Console.Write("Enter the NewPIN: ");
                                NewPIN = Convert.ToInt32(Console.Read());
                                this.accountdao.PINChange(UserID, NewPIN);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error in PINChange: " + e.Message);
                            }
                            break;

                        case 7:
                            Console.WriteLine("You are Logged out Successfully!");
                            break;
                        default:
                            Console.WriteLine("Enter a Vaild Input!");
                            break;

                    }
                    MainAtm();
                }
                else
                {
                    Console.WriteLine("Incorrect UserID or Password!!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void Deposit(User currentuser)
        {
           
        }

        public static void Withdraw()
        {

        }

        public static void Transfer()
        {

        }

        public void CheckBalance(User currentuser)
        {
           
        }

        public static void Transaction()
        {

        }

        public static void ChangePin()
        {

        }

    }
}