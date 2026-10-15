using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    //Movie class
    internal class Movie
    {
        public string Title { get; set; }
        public GenreType Genre { get; set; }
        public ClassificationType Classification { get; set; }

        public long Copies { get; set; }

        public long BorrowTimes { get; set; }

        public List<Record> records = new List<Record>();

        //Constructor
        public Movie(string title, GenreType genre, ClassificationType classification, long copies)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            Copies = copies;
            BorrowTimes = 0;
        }

        //Add some copies to movie object
        public void AddNewCopies(long copies)
        {
            Copies += copies;
        }

        //Remove some copies from the object
        public void RemoveCopies(long copies)
        {
            Copies -= copies;
        }

        public void AddBorrowTime()
        {
            BorrowTimes += 1;
        }

        public void AddRecord(string firstname, string lastname)
        {
            Record record = new Record(firstname, lastname, true);
            records.Add(record);
        }

        public void MovieDetail()
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Movie title: " +  Title);
            Console.WriteLine("Movie genre: " + Genre);
            Console.WriteLine("Movie classification: " + Classification);
            Console.WriteLine("Movie copies: " +  Copies);
            Console.WriteLine("Movie borrow times: " + BorrowTimes);
            //foreach(Record record in records)
            //{
            //    Console.WriteLine("Member: " + record.Firstname + " " + record.Lastname);
            //    Console.WriteLine("Status: " + record.Status);
            //}
        }

    }

    //Movie genre type preset
    public enum GenreType
    {
        Drama,
        Adventure,
        Family,
        Action,
        SciFi,
        Comedy,
        Animated,
        Thriller,
        Other
    }

    //Movie classification preset
    public enum ClassificationType
    {
        G,          //General
        PG,         //Parental Guidance
        M15Plus,    //Mature (15+)
        MA15Plus    //Mature Accompanied (15+)
    }
}
