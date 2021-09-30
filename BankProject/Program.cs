using System;

namespace BankProject
{
    class Program
    {
        //Namn, Efternamn, PIN, Kundnummer, AntalKonton
        static string[,] users = {
            {"isak.jensen","1","Isak","Jensen","1234","3"},
            {"tomas.andersson","2","Tomas","Andersson","1234","2"},
            {"susanna.svensson","3","Susanna","Svensson","1234","3"},
            {"vilma.carlsson","4","Vilma","carlsson","1234","2"},
            {"bo.malmberg","5","Bo","Malmberg","1234","4"}
        };
        static string loggedInUserNr;

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till banken!");
            if (login())
            {
                runMenu();
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
                            loggedInUserNr =users[i, 1];
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

        static void runMenu()
        {
            Console.Clear();
            Console.WriteLine("Välkommen {0}!", getFirstName(loggedInUserNr));
            bool noValidAnswer = true;
            while(noValidAnswer){
                Console.WriteLine("\n1.Se dina konton och saldo");
                Console.WriteLine("2.Överföring mellan konton");
                Console.WriteLine("3.Ta ut pengar");
                Console.WriteLine("4.Logga ut\n");


                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //Run 1
                        noValidAnswer = false;
                        break;
                    case "2":
                        //Run 2
                        noValidAnswer = false;
                        break;
                    case "3":
                        //Run 3
                        noValidAnswer = false;
                        break;
                    case "4":
                        logOut();
                        noValidAnswer = false;
                        break;
                    default:
                        Console.WriteLine("Vänligen mata in en siffra som är med i menyn.");
                        break;
                }
            }
        }

        static void logOut()
        {
            loggedInUserNr = null;
            Console.Clear();
            Console.WriteLine("Du är nu utloggad.");
            login();
        }

        static string getFirstName(string customerNr)
        {
            for(int i = 0; i < users.GetLength(0); i++)
            {
                if (users[i,1] == customerNr)
                {
                    return users[i, 2];
                }
            }
            return null;
        }
    }
}
