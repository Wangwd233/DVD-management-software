using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    //the main menu function
    internal class MainMenu
    {
        public static MovieCollection movieCollection = new MovieCollection();
        public static MemberCollection memberCollection = new MemberCollection();
        public static void Menu()
        {
            //The function to print out the main menu
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Main Menu");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Select from the following: ");
            Console.WriteLine("");
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Member");
            Console.WriteLine("0. End the program");
            Console.WriteLine();
            Console.WriteLine("Enter your choice ==>");
            LoginSelect();
        }

        private static void LoginSelect() 
        {
            //User input for user selection
            int select;
            bool isValid = false;

            while (!isValid) 
            {
                string input = Console.ReadLine();
                if(int.TryParse(input, out select) && select ==0 || select == 1 || select == 2)
                {
                    isValid = true;
                    switch (select)
                    {
                        case 0:
                            Console.WriteLine("Exiting the program...");
                            return; // Exit the method, which will end the program
                        case 1:
                            //Choose staff menu
                            StaffMenu.Menu(movieCollection, memberCollection);
                            //Console.WriteLine("staff");
                            break;
                        case 2:
                            //Choose member menu
                            MemberMenu.Menu(movieCollection, memberCollection);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input!");
                    Console.WriteLine("Enter your choice ==>");
                }
            }
        }
    }
}
