using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class CardHolder
    {
        //firstname,lastname,cardNumber,pin,balance
        private string _firstName;
        private string _lastName;
        private string _cardNumber;
        private int _pin;
        private double _balance;
        private long _phoneNumber;
        public CardHolder(string firstName, string lastName, string cardNum, int pin, double balance, long phoneNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _pin = pin;
            _balance = balance;
            _cardNumber = cardNum;
            _phoneNumber = phoneNumber;
        }
        public string GetFirstName()
        {
            return _firstName;
        }
        public void SetFirstName(string firstName)
        {
            _firstName = firstName;
        }
        public string GetLastName()
        {
            return _lastName;
        }
        public void setLastName(string lastName)
        {
            _lastName = lastName;
        }
        public string GetCardNumber()
        {
            return _cardNumber;
        }
        public void SetCardNumber(string CardNum)
        {
            _cardNumber = CardNum;
        }
        public double GetBalance()
        {
            return _balance;
        }
        public void SetBalance(double balance)
        {
            _balance = balance;
        }
        public int GetPin()
        {
            return _pin;
        }
        public void setPin(int pin)
        {
            _pin = pin;
        }
        public long GetNumber()
        {
            return _phoneNumber;
        }
        public void SetNumber(long phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

    }
    public class Atm
    {
        public bool resetFlag = false;
       
        public void printOption()
        {
            Console.WriteLine("|--> Please enter any of the option");
            Console.WriteLine("      |_ 1. Deposit");
            Console.WriteLine("      |_ 2. Withdrow");
            Console.WriteLine("      |_ 3. Check balance");
            Console.WriteLine("      |_ 4. Reset pin");
            Console.WriteLine("      |_ 5. exit");
        }
        public void Deposit(CardHolder cardHolder)
        {
            Console.WriteLine("|    Please deposit the desire amount");
            double amount = double.Parse(Console.ReadLine());
            if (amount > 0)
            {
                cardHolder.SetBalance(cardHolder.GetBalance() + amount);
                Console.WriteLine($"amount deposited successfully , your new balance is {cardHolder.GetBalance()}");
            }
            else
            {
                Console.WriteLine("|    please deposit the valid amount ");
            }
        }
        public void WithDraw(CardHolder cardholder)
        {
            Console.WriteLine("|    Please enter required amount to withdraw");
            double withDrawAmount = double.Parse(Console.ReadLine());
            if (cardholder.GetBalance() < withDrawAmount)
            {
                Console.WriteLine("|    Insufficient balance, please try again");
            }
            else
            {
                double newBalance = cardholder.GetBalance() - withDrawAmount;
                cardholder.SetBalance(newBalance);
                Console.WriteLine("|    Successfully withdraw , your new balance is : {0} ", cardholder.GetBalance());
            }
        }
        public void BalanceEnquiry(CardHolder cardHolder)
        {
            Console.WriteLine("|    Your Good balance is : {0}", cardHolder.GetBalance());
        }
        public void resetPin(CardHolder cardHolder)
        {
            if (resetFlag == false)
            {
            a:
                Console.WriteLine("|    enter your current pin number");
                int currentPin = int.Parse(Console.ReadLine());
                if (currentPin == cardHolder.GetPin())
                {
                    Console.WriteLine("|    please enter your new pin");
                    int newPin = int.Parse(Console.ReadLine());
                    Console.WriteLine("|    verify your new pin");
                    int verifyPin = int.Parse(Console.ReadLine());
                    if (newPin == verifyPin)
                    {
                        cardHolder.setPin(newPin);
                        Console.WriteLine($"|   pin reset successfully ,your new pin is {cardHolder.GetPin()}");
                    }
                    else
                    {
                        Console.WriteLine("|    ***pin dont match,try again***");
                    }
                }
                else
                {
                    Console.WriteLine("|    ***incorrect pin,try again***");
                    goto a;
                }
            }
            else
            {
                Console.WriteLine("|    please enter your new pin");
                int newPin = int.Parse(Console.ReadLine());
                Console.WriteLine("|    verify your new pin");
                int verifyPin = int.Parse(Console.ReadLine());
                if (newPin == verifyPin)
                {
                    cardHolder.setPin(newPin);
                    Console.WriteLine($"|   pin reset successfully ,your new pin is {cardHolder.GetPin()}");
                }
                else
                {
                    Console.WriteLine("|    ***pin dont match,try again***");
                }
            }
            resetFlag = false;
            
        }
       
        public void WelcomeMsg()
        {
            ////db fake
            List<CardHolder> cardHolders = new List<CardHolder>();
            cardHolders.Add(new CardHolder("Adarasha", "Gaihre", "123456789", 1069, 1000.00, 9746409101));
            cardHolders.Add(new CardHolder("Aakku", "Ccn", "23423498234", 1060, 1600.00,0106106106));
            cardHolders.Add(new CardHolder("CCN", "Samma", "987654321", 0106, 400.234, 1069106900));
            cardHolders.Add(new CardHolder("Toffu", "Shta", "123456780", 8453, 600.234, 9703069072));
            Console.WriteLine($@"
|                                                                                                                            |
|                                                                                                                            |
_______________________________________________________WELCOME TO CCN ATM SERVICE____________________________________________
                                        HOPE YOU HAVE A GOOD DAY !!
                                        DATTEBYO!!!");
            Console.WriteLine("|    Please enter the debit card number");
            CardHolder currentUser;
            string cardNumber = "";
            while (true)
            {
                try
                {
                    
                    cardNumber = Console.ReadLine();
                    //check current user in db
                    currentUser = cardHolders.FirstOrDefault(card => card.GetCardNumber() == cardNumber);
                    if (currentUser != null)
                    {
                        break;
                    }
                    else
                    {
                       int  repet = 5;
                        if (repet > 0)
                        {
                            repet--;
                            Console.WriteLine($@"|   * Invalid Card Number,{repet} times remains to try card number");
                        }
                       
                        
                    }
                }
                catch
                {
                    Console.WriteLine("|   + Invalid Card Number,Please Try again");
                }
            }
            int pin = 0;
            int pinCounter = 5;
            while (true)
            {
                try 
                {
                    Console.WriteLine("|    Please enter your pin");
                    pin = int.Parse(Console.ReadLine());
                    
                        {
                            if (currentUser.GetPin() == pin) //check pin
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"|   incorrect pin,  {pinCounter} attempt left  please try agian");
                                pinCounter--;
                                if (pinCounter < 0)
                                {
                                    Console.WriteLine($@"|
|
|    _*_*_*_*_*_*you have exceeded the maximum attempt, please try again later after reseting ur pin_*_*_*_*_*_*     ");
                                    limit(currentUser);
                                }
                            }
                        }
                    
                    
                      
                    
                   
                }
                catch
                {
                    Console.WriteLine("|    incorrect pin , please try agian");
                }

            }
           
            Console.WriteLine($"welcome {currentUser.GetFirstName()}");
            int option = 0;
            do
            {
                printOption();
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Deposit(currentUser);
                        break;
                    case 2:
                        WithDraw(currentUser); break;
                    case 3:
                        BalanceEnquiry(currentUser); break;
                    case 4:
                        resetPin(currentUser);
                        break;
                    case 5:
                        exit();
                        break;

                    default:
                        Console.WriteLine("|----> |PLEASE CHOOSE THE CORRECT NUMBERSS| ^_~  o(*￣▽￣*)ブ \n\n");
                        break;

                }
            }
            while (option != 5);
        }
            //CardHolder currentUser;
        public void limit(CardHolder cardHolder)
        {

            Console.WriteLine("|    ***you have exceeded the maximum attempt, please try again later after reseting ur pin***");
            Console.WriteLine("|    Please give your phone number for USER VERIFICATION....");
            Console.WriteLine($@"|
|                                           * * * _WARNING_ *  * * 
|----------------------------------------------------------------------------------------------------------------------------------
|   IF YOU ENTERED WRONG PHONE NUMBER THAN DUE TO SECUTITY REASONS WE HAVE TO SAY BYE X_X");

            
                long phoneNumber = long.Parse(Console.ReadLine());

                if (cardHolder.GetNumber()== phoneNumber)
                {
                resetFlag = true;
                    resetPin(cardHolder);

                }
                else
                {
                    exit();
                }


            
           



        }
        public void exit()
        {
            Console.WriteLine($@"
|                _*_*_*_*_*_*_*_*_*_*_*_*Thank you for using our service_*_*_*_*_*_*_*_*_*_*_*_*         
                                              !!!DATTEBTOOO!!!                                      
______________________________________________________________________________________________________________________
                                        ");
            Console.ReadKey();
            Environment.Exit(106);

        }
    }

}
