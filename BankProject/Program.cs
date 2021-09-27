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

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till banken!");
            Console.WriteLine(login());

        }

        static bool login()
        {
            Console.WriteLine("Vänligen mata in användarnamn.");
            string user = Console.ReadLine();

            Console.WriteLine("Vänligen mata in lösenord.");
            string password = Console.ReadLine();

            for (int i = 0; i < users.GetLength(0); i++ ){

                if (users[i, 0] == user)
                {
                    if (users[i, 1] == password) return true;
                }
            }
            return false;
        }
    }
}
