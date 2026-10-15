using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    internal class MemberCollection
    {
        //Array with member class
        private Member[] members = new Member[1000];
        private int count;

        public MemberCollection()
        {
            count = 0;
        }

        public void AddMember()
        {
            string firstName;
            firstName = AskForFirstName();

            string lastName;
            lastName = AskForLastName();

            Member member = new Member(firstName, lastName);

            // Ensure members[i] is not null before accessing its properties
            if (GetMemberByName(firstName, lastName) == -1)
            {
                if (count < members.Length)
                {
                    members[count] = member;
                    Console.WriteLine("Member added: " + member.FirstName + " " + member.LastName);
                    members[count].ListMember();
                    count++;
                }
                else
                {
                    Console.WriteLine("Member collection is full. Cannot add more members.");
                }
            }
            else
            {
                Console.WriteLine("Member already exists.");
            }
        }

        //Remove member from array
        public void RemoveMember()
        {
            //Ask for member firstname and lastname
            string firstName;
            firstName= AskForFirstName();

            string lastName;
            lastName = AskForLastName();

            //try to find the user
            int i = GetMemberByName(firstName, lastName);
            if (i == -1)
            {
                Console.WriteLine("Member does not exist!");
            }
            else
            {
                if (members[i].isHoldingDVDs())
                {
                    Console.WriteLine("Member is holding some DVDs, please return all of it first!");
                    return;
                }
                else
                {
                    // Remove the member by setting it to null and shifting elements
                    for (int j = i; j < count - 1; j++)
                    {
                        members[i] = members[i + 1];
                    }
                    members[count - 1] = null;
                    count--;
                    Console.WriteLine("Member removed successfully!");
                }
            }
        }

        //Find phone number with particular member name
        public void FindPhoneNumber()
        {
            string firstname = AskForFirstName();
            string lastname = AskForLastName();

            int i = GetMemberByName(firstname, lastname);

            if(i == -1)
            {
                Console.WriteLine("Member does not exist!");
            }
            else
            {
                long phoneNumber = members[i].PhoneNumber;
                Console.WriteLine("Member name: " + firstname + " " + lastname);
                Console.WriteLine("Phone number: " +  phoneNumber);
            }
        }

        //Ask user to input valid firstname
        public string AskForFirstName()
        {
            string firstName;
            do
            {
                Console.WriteLine("Enter member firstname: ");
                firstName = Console.ReadLine();
                if (string.IsNullOrEmpty(firstName))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(firstName));

            return firstName;
        }

        //Ask user to input valid lastname
        public string AskForLastName()
        {
            string lastName;
            do
            {
                Console.WriteLine("Enter member lastname: ");
                lastName = Console.ReadLine();
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(lastName));

            return lastName;
        }
        public int GetMemberByName(string firstname, string lastname)
        {
            for (int i = 0; i < count; i++)
            {
                // Ensure members[i] is not null before accessing its properties
                if (members[i] != null && members[i].FirstName == firstname && members[i].LastName == lastname)
                {
                    return i;
                }
            }

            return -1;
        }

        public Member GetMemberByIndex(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Invalid index.");
                return null;
            }
            return members[index];
        }

        public void MemberRecord(string movieTitle, int memberIndex)
        {
            int count = 0;
            for (int i = 0; i < members[memberIndex].movieBorrowed.Length; i++)
            {
                if (members[memberIndex].movieBorrowed[i] != null)
                {
                    count ++;
                }
            }

            if (count >= 5)
            {
                Console.WriteLine("You have already borrowed five movies, borrow failed!");
            }
            else
            {
                for(int i = 0; i < members[memberIndex].movieBorrowed.Length; i++)
                {
                        if (members[memberIndex].movieBorrowed[i] == null)
                        {
                            members[memberIndex].movieBorrowed[i] = movieTitle;
                            Console.WriteLine("Movie Borrowed!");
                            break;
                        } 
                }
            }
        }

        public void MemberRecordDelete(string movieTitle, int memberIndex)
        {
            for (int i = 0; i < members[memberIndex].movieBorrowed.Length; i++)
            {
                if (movieTitle == members[memberIndex].movieBorrowed[i])
                {
                    members[memberIndex].movieBorrowed[i] = null;
                    Console.WriteLine("Movie return successful!");
                    break;
                }
            }
        }

        public void BorrowedMovieList(int memberIndex)
        {
            Console.WriteLine("Member " + members[memberIndex].FirstName + " " + members[memberIndex].LastName);
            Console.WriteLine("Here are the movies you have borrowed: ");
            Console.WriteLine("---------------------------------------");
            for (int i = 0; i < members[memberIndex].movieBorrowed.Length; i++)
            {
                Console.WriteLine((i+ 1) + ". " + members[memberIndex].movieBorrowed[i]);
            }
        }
    }
}
