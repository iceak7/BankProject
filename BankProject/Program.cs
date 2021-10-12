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

        static int firstFreeAccountNr = 124;

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till banken!");

            bool continueRunning = true;
            while (continueRunning)
            {
                if (Login())
                {
                    Console.Clear();
                    Console.WriteLine("Välkommen {0}!", GetFirstName(loggedInUserNr));

                    bool continueRun = true;
                    while (continueRun)
                    {
                        bool stillLoggedIn = RunMenu();
                        if (stillLoggedIn)
                        {
                            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            continueRun = false;
                        }

                    }

                }
            }
        }

        static bool Login()
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
        static bool RunMenu()
        {
            bool noValidAnswer = true;
            while (noValidAnswer)
            {
                Console.WriteLine("\n1.Se dina konton och saldo");
                Console.WriteLine("2.Överföring mellan konton");
                Console.WriteLine("3.Ta ut pengar");
                Console.WriteLine("4.Sätta in pengar");
                Console.WriteLine("5.Öppna nytt konto");
                Console.WriteLine("6.Logga ut\n");


                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        WriteOutAccountAndBalance();
                        return true;
                    case "2":
                        return TransferMoney();
                    case "3":
                        return WithdrawMoney();
                    case "4":
                        return DepositMoney();
                    case "5":
                        OpenNewAccount();
                        return true;
                    case "6":
                        LogOut();
                        return false;
                    default:
                        Console.Clear();
                        Console.WriteLine("Vänligen mata in en siffra som är med i menyn.");
                        break;
                }
            }
            return false;
        }

        static void LogOut()
        {
            loggedInUserNr = null;
            Console.Clear();
            Console.WriteLine("Du är nu utloggad.");
        }

        static void WriteOutAccountAndBalance()
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
        static bool WithdrawMoney()
        {
            WriteOutAccountAndBalance();

            bool correctAccNr = false;
            Console.WriteLine("\nVilket konto vill du ta ut pengar ifrån? Skriv kontonumret.");
            string accountNr = "0";

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

            if (correctAccNr)
            {
                int indexOfAccount = bankAccounts.IndexOf(bankAccounts.Find(x => x[0] == accountNr));

                Console.WriteLine("\nHur mycket pengar vill du ta ut? Du måste ta ut minst 10kr och endast hela kronor.");
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

                        decimal amount = decimal.Parse(input);

                        if (amount >= 10 & amount % 1 == 0 & amount <= balance)
                        {
                            correctAmount = true;

                            bool correctPin = false;
                            int triesLeft = 3;
                            Console.WriteLine("\nVänligen mata in din pin.");
                            while (!correctPin & triesLeft > 0)
                            {
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

                                if (PinAuth(loggedInUserNr, pin))
                                {
                                    correctPin = true;
                                    balance -= amount;
                                    bankAccounts[indexOfAccount][3] = balance.ToString();

                                    Console.WriteLine("\nDu har tagit ut {0}kr från ditt {1} och din nya balans är {2}kr.", amount, bankAccounts[indexOfAccount][2], balance);
                                }
                                else if (triesLeft == 1)
                                {
                                    triesLeft--;
                                    Console.WriteLine("Du har matat in fel pin för många gånger.");
                                    loggedInUserNr = null;
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Felaktig pin. Mata in den igen. Vill du inte ta ut pengar skriv skriv \"Avbryt\".");
                                    triesLeft--;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Felaktig summa för uttaget. Du måste ta ut minst 10kr och endast hela kronor. Försök igen. Vill du inte ta ut pengar längre skriv \"Avbryt\".");
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Felaktig inmatning.  Försök igen. Vill du inte ta ut pengar skriv \"Avbryt\".");
                    }
                }
            }
            return true;
        }

        static bool DepositMoney()
        {
            WriteOutAccountAndBalance();
            bool correctAccNr = false;
            Console.WriteLine("\nVilket konto vill du sätta in pengar på? Skriv kontonumret.");
            string accountNr = "0";

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

            if (correctAccNr)
            {
                int indexOfAccount = bankAccounts.IndexOf(bankAccounts.Find(x => x[0] == accountNr));

                Console.WriteLine("\nHur mycket pengar vill du sätta in? Du måsta sätta in minst 10kr och endast hela kronor.");
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

                        decimal amount = decimal.Parse(input);

                        if (amount >= 10 & amount%1==0 )
                        {
                            correctAmount = true;

                            bool correctPin = false;
                            int triesLeft = 3;
                            Console.WriteLine("\nVänligen mata in din pin.");
                            while (!correctPin & triesLeft > 0)
                            {
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

                                if (PinAuth(loggedInUserNr, pin))
                                {
                                    correctPin = true;
                                    balance += amount;
                                    bankAccounts[indexOfAccount][3] = balance.ToString();

                                    Console.WriteLine("\nDu har satt in {0}kr på ditt {1} och din nya balans är {2}kr.", amount, bankAccounts[indexOfAccount][2], balance);
                                }
                                else if (triesLeft == 1)
                                {
                                    triesLeft--;
                                    Console.WriteLine("Du har matat in fel pin för många gånger.");
                                    loggedInUserNr = null;
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Felaktig pin. Mata in den igen. Vill du inte ta ut pengar skriv skriv \"Avbryt\".");
                                    triesLeft--;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Felaktig summa för insättningen. Du måste ta ut minst 10kr och endast hela kronor. Försök igen. Vill du inte ta ut pengar längre skriv \"Avbryt\".");
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Felaktig inmatning. Försök igen. Vill du inte ta ut pengar skriv \"Avbryt\".");
                    }
                }
            }

            return true;
        }

        static bool TransferMoney()
        {
            WriteOutAccountAndBalance();

            bool correctAccNr = false;
            Console.WriteLine("\nVilket konto vill du överföra ifrån? Skriv kontonumret.");
            string transferFromAccountNr = "0";
            string transferToAccountNr = "0";

            while (!correctAccNr)
            {
                bool correctAccButNoFunds = false;
                string input = Console.ReadLine();

                if (input.ToUpper() == "AVBRYT") break;

                foreach (var account in bankAccounts)
                {
                    if (account[1] == loggedInUserNr & account[0] == input)
                    {
                        if (decimal.Parse(account[3]) >= 1)
                        {
                            correctAccNr = true;
                            transferFromAccountNr = account[0];
                            break;
                        }
                        else
                        {
                            correctAccButNoFunds = true;
                        }

                    }
                }
                if (correctAccButNoFunds)
                {
                    Console.WriteLine("Kontot du valde har inte tilräckligt med pengar för att föra över till ett annat konto. Vänligen välj ett annat konto. Vill du inte överföra pengar längre skriv \"Avbryt\".");
                }
                else if (correctAccNr == false)
                {
                    Console.WriteLine("Du matade in ett felaktigt kontonummer. Vänligen mata in kontonumret igen. Vill du inte överföra pengar längre skriv \"Avbryt\".");
                }
            }

            if (correctAccNr)
            {
                correctAccNr = false;
                Console.WriteLine("\nVilket konto vill du överföra pengar till? Skriv kontonumret.");

                while (!correctAccNr)
                {
                    string input = Console.ReadLine();
                    bool sameAccount = false;

                    if (input.ToUpper() == "AVBRYT") break;

                    if (transferFromAccountNr == input)
                    {
                        sameAccount = true;
                    }
                    else
                    {
                        foreach (var account in bankAccounts)
                        {
                            if (account[1] == loggedInUserNr & account[0] == input)
                            {
                                correctAccNr = true;
                                transferToAccountNr = account[0];
                                break;
                            }
                        }
                    }

                    if (sameAccount)
                    {
                        Console.WriteLine("Du måste välja ett annat konto. Vänligen mata in kontonumret igen. Vill du inte överföra pengar längre skriv \"Avbryt\".");
                    }
                    else if (correctAccNr == false)
                    {
                        Console.WriteLine("Du matade in ett felaktigt kontonummer. Vänligen mata in kontonumret igen. Vill du inte överföra pengar längre skriv \"Avbryt\".");
                    }
                }
            }

            if (correctAccNr)
            {

                Console.WriteLine("\nHur mycket pengar vill du överföra?");
                bool correctAmount = false;

                int indexOfFromAccount = bankAccounts.IndexOf(bankAccounts.Find(x => x[0] == transferFromAccountNr));
                int indexOfToAccount = bankAccounts.IndexOf(bankAccounts.Find(x => x[0] == transferToAccountNr));


                while (!correctAmount)
                {
                    decimal balance = decimal.Parse(bankAccounts[indexOfFromAccount][3]);

                    try
                    {
                        string input = Console.ReadLine();

                        if (input.ToUpper() == "AVBRYT")
                        {
                            return true;
                        }
                        int amount = int.Parse(input);

                        if (amount > 0 & amount <= balance)
                        {
                            correctAmount = true;

                            bool correctPin = false;
                            int triesLeft = 3;
                            Console.WriteLine("\nVänligen mata in din pin.");
                            while (!correctPin & triesLeft > 0)
                            {                               
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

                                if (PinAuth(loggedInUserNr, pin))
                                {
                                    correctPin = true;
                                    balance -= amount;
                                    bankAccounts[indexOfFromAccount][3] = balance.ToString();

                                    decimal balance2 = decimal.Parse(bankAccounts[indexOfToAccount][3]) + amount;
                                    bankAccounts[indexOfToAccount][3] = balance2.ToString();


                                    Console.WriteLine("\nDu har överfört {0}kr från ditt {1} till ditt {2}. Din nya balans på ditt {1} är och {3} och din nya balans på ditt {2} är {4}kr.", amount, bankAccounts[indexOfFromAccount][2], bankAccounts[indexOfToAccount][2] ,balance, balance2);
                                }
                                else if (triesLeft == 1)
                                {
                                    triesLeft--;
                                    Console.WriteLine("Du har matat in fel pin för många gånger.");
                                    loggedInUserNr = null;
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Felaktig pin. Mata in den igen. Vill du inte överföra pengar skriv \"Avbryt\".");
                                    triesLeft--;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Felaktig summa för uttaget. Försök igen. Vill du inte överföra pengar längre skriv \"Avbryt\".");
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Felaktig inmatning. Skriv ett heltal. Försök igen. Vill du inte ta överföra pengar skriv \"Avbryt\".");
                    }

                }

            }
            return true;
        }

        static void OpenNewAccount()
        {
            Console.Clear();

            Console.WriteLine("Vad ska ditt konto heta? Det måste sluta på \"konto\".");

            bool correctAccountName = false;

            while (!correctAccountName)
            {
                try
                {
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "AVBRYT")
                    {
                        break;
                    }

                    string lastFiveLetters = input.Substring(input.Length - 5);

                    if (lastFiveLetters.ToUpper() == "KONTO")
                    {
                        bankAccounts.Add(new string[] { firstFreeAccountNr.ToString(), loggedInUserNr.ToString(), input, "0,00" });
                        firstFreeAccountNr++;
                        correctAccountName = true;
                        Console.WriteLine("\nDu har nu öppnat ett konto med namnet \"{0}\".", input);
                    }
                    else
                    {
                        Console.WriteLine("\nNamnet på ditt konto måste sluta på \"konto\". Skriv in kontonamnet igen. Vill du avbryta öppnandet av konto skriv \"avbryt\".");
                    }

                }
                catch
                {
                    Console.WriteLine("\nNamnet på ditt konto måste sluta på \"konto\". Skriv in kontonamnet igen. Vill du avbryta öppnandet av konto skriv \"avbryt\".");
                }

            }
        }

        static bool PinAuth(string customerNr, string pin)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (customerNr == users[i, 1])
                {
                    if (pin == users[i, 4])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static string GetFirstName(string customerNr)
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
