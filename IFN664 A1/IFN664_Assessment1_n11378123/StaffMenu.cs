using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    internal class StaffMenu
    {
        private static bool login = false;

        public static void Menu(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            login = StaffLogin();
            if (login) 
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Staff Menu");
                functionSelect(movieCollection, memberCollection);
            }
            else
            {
                MainMenu.Menu();
            }
        }

        public static bool StaffLogin()
        {
               string staffUsername = "staff";
               string staffPassword = "today123";
               bool loginCheck = false;
            //function to recognize staff account
                Console.WriteLine("Please enter staff username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter staff password: ");
                string password = Console.ReadLine();
                
                //login check
                if(username != null && password != null)
                {
                    if(username == staffUsername && password == staffPassword)
                    {
                        Console.WriteLine("Welcome Staff!");
                        loginCheck = true;
                        return loginCheck;
                    }
                    else
                    {
                        Console.WriteLine("username or password not match, login failed!");
                        return loginCheck;
                    }
                }
                else
                {
                    Console.WriteLine("Login username or password can not be null!");
                    return loginCheck;
                }
            
        }

        public static void functionSelect(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            //input for function selection
            int select;
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("1. Add DVDs to system");
                Console.WriteLine("2. Remove DVDs from system");
                Console.WriteLine("3. Register a new member to system");
                Console.WriteLine("4. Remove a registered member from system");
                Console.WriteLine("5. Find a member contact phone number, given the member's name");
                Console.WriteLine("6. Find members who are currently renting a particular movie");
                Console.WriteLine("0. Return to main menu");
                Console.WriteLine();
                Console.WriteLine("Enter your choice ==>");

                string input = Console.ReadLine();
                if (int.TryParse(input, out select) && (select >= 0 && select <= 6))
                {
                    switch (select)
                    {
                        //back to main menu
                        case 0:
                            Console.WriteLine("Back to main menu");
                            MainMenu.Menu();
                            isValid = true;
                            break;
                        //add movie
                        case 1:
                            movieCollection.AddMovieByStaff();
                            break;
                        //remove movie
                        case 2:
                            movieCollection.RemoveMovie();
                            break;
                        //add member
                        case 3:
                            memberCollection.AddMember();
                            break;
                        //remove member
                        case 4:
                            memberCollection.RemoveMember();
                            break;
                        //find member contact phone number
                        case 5:
                            memberCollection.FindPhoneNumber();
                            break;
                        case 6:
                            movieCollection.MemberBorrowedCurrently();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }
        }
    }
}
