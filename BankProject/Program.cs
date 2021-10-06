using System;
using System.Collections.Generic;

namespace BankProject
{
    class Program
    {
        //Namn, Efternamn, PIN, Kundnummer, AntalKonton
        static string[,] users = {
            {"isak.jensen","1","Isak","Jensen","1234","3"},
            {"tomas.andersson","2","Tomas","Andersson","1234","2"},
            {"susanna.svensson","3","Susanna","Svensson","1234","3"},
            {"vilma.carlsson","4","Vilma","Carlsson","1234","2"},
            {"bo.malmberg","5","Bo","Malmberg","1234","4"}
        };
        static List<string[]> bankAccounts = new List<string[]> {
            new string[]{"110", "1", "Sparkonto", "51000,00" },
            new string[]{"111", "1", "Transaktionskonto", "5000,90" },
            new string[]{"112", "1", "Lönekonto", "15000,00"},
            new string[]{"113", "2", "Transaktionskonto", "7090,50" },
            new string[]{"114", "2", "Lönekonto", "13000,00"},
            new string[]{"115", "3", "Transaktionskonto", "1300,00" },
            new string[]{"116", "3", "Sparkonto", "25100,00"},
            new string[]{"117", "3", "Lönekonto", "11200,00"},
            new string[]{"118", "5", "Transaktionskonto", "4020,70" },
            new string[]{"119", "4", "Lönekonto", "23000,00"},
            new string[]{"120", "5", "Transaktionskonto", "8700,50" },
            new string[]{"121", "5", "Sparkonto", "15000,00"},
            new string[]{"122", "6", "Transaktionskonto", "9100,90" },
            new string[]{"123", "6", "Lönekonto", "27000,00"}
        };
        static string loggedInUserNr;

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till banken!");

            bool continueRunning = true;
            while (continueRunning)
            {
                if (login())
                {
                    Console.Clear();
                    Console.WriteLine("Välkommen {0}!", getFirstName(loggedInUserNr));

                    bool continueRun = true;
                    while (continueRun)
                    {
                        bool stillLoggedIn = runMenu();
                        if (stillLoggedIn)
                        {
                            continueRun = false;
                        }
                        else
                        {

                            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn");
                            Console.ReadLine();
                            Console.Clear();
                        }

                    }

                }
            }
        }

        static bool login()
        {
            int försökKvar = 3;
            while (försökKvar != 0)
            {
                Console.WriteLine("\nVänligen mata in användarnamn för att logga in.");
                string user = Console.ReadLine();

                Console.WriteLine("\nVänligen mata in lösenord.");
                string password = Console.ReadLine();

                for (int i = 0; i < users.GetLength(0); i++)
                {
                    if (users[i, 0] == user)
                    {
                        if (users[i, 4] == password)
                        {
                            loggedInUserNr = users[i, 1];
                            return true;
                        }
                    }
                }
                försökKvar--;
                if (försökKvar > 0)
                {
                    Console.WriteLine("Fel användarnamn eller lösenord. Försök igen.");
                }
                else
                {
                    Console.WriteLine("Du har nu skrivit fel användarnamn eller lösenord tre gånger. Starta om programmet för att försöka igen.");
                }
            }
            return false;
        }

        //returns false if user should get logged out
        static bool runMenu()
        {
            bool noValidAnswer = true;
            while (noValidAnswer)
            {
                Console.WriteLine("\n1.Se dina konton och saldo");
                Console.WriteLine("2.Överföring mellan konton");
                Console.WriteLine("3.Ta ut pengar");
                Console.WriteLine("4.Logga ut\n");


                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        writeOutAccountAndBalance();
                        return false;
                    case "2":
                        //Run 2
                        return false;
                    case "3":
                        if (withdrawMoney())
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    case "4":
                        logOut();
                        return false;
                    default:
                        Console.Clear();
                        Console.WriteLine("Vänligen mata in en siffra som är med i menyn.");
                        break;
                }
            }
            return false;
        }

        static void logOut()
        {
            loggedInUserNr = null;
            Console.Clear();
            Console.WriteLine("Du är nu utloggad.");
        }

        static void writeOutAccountAndBalance()
        {
            Console.Clear();
            Console.WriteLine("Dina konton");
            foreach (var account in bankAccounts)
            {
                if (account[1] == loggedInUserNr)
                {
                    Console.WriteLine("{1} {0} - Saldo: {2}", account[2], account[0], account[3]);
                }
            }
        }

        //Returns false if the user entered wrong pin too many times
        static bool withdrawMoney()
        {
            writeOutAccountAndBalance();

            bool correctAccNr = false;
            Console.WriteLine("\nVilket konto vill du ta ut pengar ifrån? Skriv kontonumret.");
            string accountNr="0";

            while (!correctAccNr)
            {
                string input = Console.ReadLine();
                if (input.ToUpper() == "AVBRYT") break;
                foreach (var account in bankAccounts)
                {
                    if (account[1] == loggedInUserNr & account[0] == input)
                    {
                        correctAccNr = true;
                        accountNr = account[0];
                        break;
                    }
                }

                if (correctAccNr == false)
                {
                    Console.WriteLine("Du matade in ett felaktigt kontonummer. Vänligen mata in kontonumret igen. Vill du inte ta ut pengar längre skriv \"Avbryt\".");
                }
            }

            if(accountNr != "0")
            {
                int indexOfAccount = bankAccounts.IndexOf( bankAccounts.Find(x => x[0] == accountNr));

                Console.WriteLine("\nHur mycket pengar vill du ta ut?");
                bool correctAmount = false;
                while (!correctAmount)
                {
                    decimal balance = decimal.Parse(bankAccounts[indexOfAccount][3]);

                    try
                    {
                        string input = Console.ReadLine();

                        if (input.ToUpper() == "AVBRYT")
                        {
                            return true;
                        }
                        int amount = int.Parse(input);

                        if(amount>0 & amount <= balance)
                        {
                            correctAmount = true;

                            bool correctPin = false;
                            int triesLeft = 3;
                            while (!correctPin & triesLeft>0)
                            {
                                Console.WriteLine("\nVänligen mata in din pin.");
                                string input2 = Console.ReadLine();
                                string pin;

                                if (input.ToUpper() == "AVBRYT")
                                {
                                    return true;
                                }
                                else
                                {
                                    pin = input2;
                                }

                                if (pinAuth(loggedInUserNr, pin))
                                {
                                    correctPin = true;
                                    balance -= amount;
                                    bankAccounts[indexOfAccount][3]  = balance.ToString();

                                    Console.WriteLine("\nDu har tagit ut {0}kr från ditt {1} och din nya balans är {2}kr.", amount, bankAccounts[indexOfAccount][2] ,balance);
                                }
                                else if(triesLeft==1)
                                {
                                    triesLeft--;
                                    Console.WriteLine("Du har matat in fel pin för många gånger.");
                                    loggedInUserNr = null;
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Felaktig pin. Vill du inte ta ut pengar skriv skriv \"Avbryt\".");
                                    triesLeft--;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Felaktig summa för uttaget. Försök igen. Vill du inte ta ut pengar längre skriv \"Avbryt\".");
                        }
                    }
                    
                    catch(Exception e)
                    {
                        Console.WriteLine("Felaktig inmatning. Skriv ett heltal. Försök igen. Vill du inte ta ut pengar skriv skriv \"Avbryt\".");
                    }

                    
                }
            }

            return true;

        }

        static bool pinAuth(string customerNr, string pin)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (customerNr == users[i, 1] )
                {
                    if(pin == users[i, 4])
                    {
                        return true;
                    }                  
                }
            }
            return false;
        }
        static string getFirstName(string customerNr)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (users[i, 1] == customerNr)
                {
                    return users[i, 2];
                }
            }
            return null;
        }
    }
}
