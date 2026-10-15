using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    //Member class
    internal class Member
    {
        //Set a lastId to give every member an unique id
        private static int lastId = 0;
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long PhoneNumber { get; set; }

        public int Password { get; set; }

        public string[] movieBorrowed { get; set; }

        //constructor
        public Member(string firstname, string lastname)
        {
            Id = ++lastId;
            FirstName = firstname;
            LastName = lastname;
            PhoneNumber = ValidatePhoneNumber();
            Password = SetPassword();
            movieBorrowed = new string[5];
        }


        public long ValidatePhoneNumber()
        {
             while (true)
            {
                if (FirstName != null && LastName != null) 
                {
                    Console.WriteLine("Member " + FirstName + " " + LastName);
                }
                Console.Write("Enter phone number (10 digits): ");
                string input = Console.ReadLine();

                if (long.TryParse(input, out long number) && input.Length == 10 && number >= 0)
                {
                    //PhoneNumber = number;
                    Console.WriteLine("Phone number is valid.");
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid phone number. Please try again.");
                }
            }
        }

        public int SetPassword()
        {
            while (true)
            {
                Console.Write("Set the password for member (4 digits): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number) && input.Length == 4 && number >= 0)
                {
                    //PhoneNumber = number;
                    Console.WriteLine("Password is Valid.");
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid password. Please try again.");
                }
            }
        }

        public void ListMember()
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("First name: " + FirstName);
            Console.WriteLine("Last name: " + LastName);
            Console.WriteLine("Phone number: " + PhoneNumber);
            Console.WriteLine("Password: " + Password);
        }

        public bool isHoldingDVDs()
        {
            for (int i = 0; i < movieBorrowed.Length; i++) 
            {
                if (movieBorrowed[i] != null) 
                {
                    return true; 
                }
            }

            return false;
        }
    }
}
