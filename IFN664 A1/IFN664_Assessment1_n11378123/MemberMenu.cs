using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    internal class MemberMenu
    {
        private static bool login = false;

        private static int memberIndex = -1;

        public static void Menu(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            (login, memberIndex) = MemberLogin(memberCollection);
            if (login)
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Member Menu");
                functionSelect(movieCollection, memberCollection, memberIndex);
            }
            else
            {
                MainMenu.Menu();
            }
        }

        public static (bool loginCheck, int memberIndex) MemberLogin(MemberCollection memberCollection)
        {
            int pwd;
            bool loginCheck = false;
            string firstname = memberCollection.AskForFirstName();
            string lastname = memberCollection.AskForLastName();

            int i = memberCollection.GetMemberByName(firstname, lastname);
            Member member = memberCollection.GetMemberByIndex(i);


            //login check
            if(i == -1) 
            {
                Console.WriteLine("Member does not exist!");
                return (loginCheck, i);
            }
            else
            {   
                if(member != null)
                {
                    Console.WriteLine("Member " + firstname + " " + lastname + "Please enter your password(4 digits): ");
                    string password = Console.ReadLine();
                    if (int.TryParse(password, out pwd) && password.Length == 4)
                    {
                        if (pwd == member.Password) 
                        {
                            Console.WriteLine("Password matches, welcome, member " + member.FirstName + " " + member.LastName);
                            loginCheck = true;
                            return (loginCheck, i);
                        }
                        else
                        {
                            Console.WriteLine("Password doesn't match, login failed.");
                            return (loginCheck, i);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid password!");
                        return (loginCheck, i);
                    }
                }
                else
                {
                    Console.WriteLine("Can not find member, something wrong.");
                    return (loginCheck, i);
                }
                
            }
        }

        public static void functionSelect(MovieCollection movieCollection, MemberCollection memberCollection, int memberIndex)
        {
            //input for function selection
            int select;
            bool isValid = false;
            Member member = memberCollection.GetMemberByIndex(memberIndex);
            string movietitle;
            bool isBorrowed;

            while (!isValid)
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("1. Browse all the movies");
                Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
                Console.WriteLine("3. Borrow a movie DVD");
                Console.WriteLine("4. Return a movie DVD");
                Console.WriteLine("5. List current borrowing movies");
                Console.WriteLine("6. Display the top 3 movies rented by the members");
                Console.WriteLine("0. Return to main menu");
                Console.WriteLine();
                Console.WriteLine("Enter your choice ==>");

                string input = Console.ReadLine();
                if (int.TryParse(input, out select) && (select >= 0 && select <= 6))
                {
                    //switch to the function user choosed
                    switch (select)
                    {
                        //back to main menu
                        case 0:
                            Console.WriteLine("Back to main menu");
                            MainMenu.Menu();
                            isValid = true;
                            break;
                        //Display all movies information
                        case 1:
                            movieCollection.MovieList();
                            break;
                        //Display information about a movie
                        case 2:
                            movieCollection.GetMovieInformation();
                            break;
                        //Borrow a movie with title
                        case 3:
                            (movietitle, isBorrowed) = movieCollection.MovieBorrowRecord(member.FirstName, member.LastName);
                            if (!isBorrowed)
                            {
                                memberCollection.MemberRecord(movietitle, memberIndex);
                            }
                            break;
                        //Return movie
                        case 4:
                            (movietitle, isBorrowed) = movieCollection.MovieReturn(member.FirstName, member.LastName);
                            if (isBorrowed)
                            {
                                memberCollection.MemberRecordDelete(movietitle, memberIndex);
                            }
                            break;
                        case 5:
                            memberCollection.BorrowedMovieList(memberIndex);
                            break;
                        case 6:
                            movieCollection.MostPopularThree();
                            break;
                    }
                }
            }
        }
    }
}
