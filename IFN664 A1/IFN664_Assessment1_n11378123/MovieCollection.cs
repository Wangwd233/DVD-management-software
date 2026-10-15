using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace IFN664_Assessment1_n11378123
{
    internal class MovieCollection
    {
        private List<(string? Title, Movie? Movie)> movies;

        //Constructor
        public MovieCollection()
        {
            movies = new List<(string?, Movie?)>(1000);
            for (int i = 0; i < 1000; i++)
            {
                movies.Add((null, null));
            }
        }

        //public void AddMovie(string title, Movie movie)
        //{
        //    string hashedTitle = HashTitle(title);
        //    movies.Add((hashedTitle, movie));
        //}

        public void AddMovieByStaff()
        {
            // check if the collection is full before add
            if (IsFull())
            {
                Console.WriteLine("Movie collection is full. Cannot add more movies.");
                return;
            }

            string title;
            int select;

            do
            {
                Console.WriteLine("Enter movie title: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(title));

            int i = GetMovieByTitle(title);

            if (i != -1)
            {
                long copies;
                Console.WriteLine("Add new copies into movie: " + movies[i].Movie.Title + ", please enter a number: ");
                string input = Console.ReadLine();
                if (long.TryParse(input, out copies))
                {
                    movies[i].Movie.AddNewCopies(copies);
                    Console.WriteLine("Copies added successfully");
                }
                else
                {
                    Console.WriteLine("Invalid copy input, added failed");
                }
            }
            else
            {
                bool isValid = false;
                GenreType genre = GenreType.Drama;
                Console.WriteLine("Enter movie genre (1 to 9): ");
                while (!isValid)
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("1. Drama");
                    Console.WriteLine("2. Adventure");
                    Console.WriteLine("3. Family");
                    Console.WriteLine("4. Action");
                    Console.WriteLine("5. SciFi");
                    Console.WriteLine("6. Comedy");
                    Console.WriteLine("7. Animated");
                    Console.WriteLine("8. Thriller");
                    Console.WriteLine("9. Other");
                    Console.WriteLine("Enter your choice ==>");
                    string genreInput = Console.ReadLine();

                    if (int.TryParse(genreInput, out select) && (select > 0 && select <= 9))
                    {
                        //switch to the function user choosed
                        switch (select)
                        {
                            case 1:
                                genre = GenreType.Drama;
                                break;
                            case 2:
                                genre = GenreType.Adventure; 
                                break;
                            case 3:
                                genre = GenreType.Family; 
                                break;
                            case 4:
                                genre = GenreType.Action;
                                break;
                            case 5:
                                genre = GenreType.SciFi;
                                break;
                            case 6:
                                genre = GenreType.Comedy;
                                break;
                            case 7:
                                genre = GenreType.Animated;
                                break;
                            case 8:
                                genre = GenreType.Thriller;
                                break;
                            case 9:
                                genre = GenreType.Other;
                                break;
                        }
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter again");
                    }
                }



                //while (!Enum.TryParse(genreInput, true, out genre))
                //{
                //    Console.WriteLine("Invalid genre. Please enter a valid genre: ");
                //    genreInput = Console.ReadLine();
                //}
                bool isValid1 = false;
                ClassificationType classification = ClassificationType.G;
                Console.WriteLine("Enter movie classification （1 to 4）: ");
                while (!isValid1)
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("1. General");
                    Console.WriteLine("2. Parental Guidance");
                    Console.WriteLine("3. Mature (15+)");
                    Console.WriteLine("4. Mature Accompanied (15+)");
                    Console.WriteLine("Enter your choice ==>");
                    string classificationInput = Console.ReadLine();

                    if (int.TryParse(classificationInput, out select) && (select > 0 && select <= 4))
                    {
                        //switch to the function user choosed
                        switch (select)
                        {
                            case 1:
                                classification = ClassificationType.G;
                                break;
                            case 2:
                                classification = ClassificationType.PG;
                                break;
                            case 3:
                                classification = ClassificationType.M15Plus;
                                break;
                            case 4:
                                classification = ClassificationType.MA15Plus;
                                break;
                        }
                        isValid1 = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter again");
                    }
                }

                //while (!Enum.TryParse(classificationInput, true, out classification))
                //{
                //    Console.WriteLine("Invalid classification. Please enter a valid classification: ");
                //    classificationInput = Console.ReadLine();
                //}

                Console.WriteLine("Enter number of copies: ");
                long copies;
                while (!long.TryParse(Console.ReadLine(), out copies) || copies < 0)
                {
                    Console.WriteLine("Invalid number of copies. Please enter a valid number: ");
                }

                Movie movie = new Movie(title, genre, classification, copies);

                string hashedTitle = HashTitle(title);

                //convert hashed title into big integer
                BigInteger hased = BigInteger.Parse(hashedTitle);

                //Convert and mod the result to get the address for hash table
                int position = (int)(hased % 1000);
                if(position >= 1000) 
                {
                    Console.WriteLine("Index out of range");
                }
                else
                {
                    //The opening address strategie Linear probing to handle the collisions and also check if postion is out of boundary
                    while (movies[position].Title != null)
                    {
                        position = (position + 1) % 1000;
                    }
                }
                

                movies.Insert(position, (hashedTitle, movie));

                Console.WriteLine("Movie added successfully!");
            }

            
        }

        public void RemoveMovie()
        {
            string title;
            //let user input a title which is not empty 
            do
            {
                Console.WriteLine("Enter movie title: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(title));

            //try to find the movie with the title
            int i = GetMovieByTitle(title);

            if (i != -1)
            {
                //let user insert remove quantities
                long copies;
                Console.WriteLine("Remove some copies from movie: " + movies[i].Movie.Title);
                Console.WriteLine("Current copies remaining: " + movies[i].Movie.Copies);
                Console.WriteLine("Please enter a number(less or equal to the copies): ");
                string input = Console.ReadLine();
                
                if (long.TryParse(input, out copies) && copies >= 0)
                {
                    //With the copies quantity decide whether remove copies or movie
                    if (copies < movies[i].Movie.Copies)
                    {
                        movies[i].Movie.RemoveCopies(copies);
                        Console.WriteLine(copies + "copies has been removed!");
                    }
                    else if(copies == movies[i].Movie.Copies)
                    {
                        movies[i] = (null, null);
                        Console.WriteLine("Movie has been removed.");
                    }
                    else
                    {
                        Console.WriteLine("Error: The number of DVDs to move is exceed the maximum number of DVDs in the storage, remove failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid copy input, remove failed");
                }
            }
            else
            {
                Console.WriteLine("Movie doesn't exist!");
            }
        }

        public void GetMovieInformation()
        {
            string title;
            do
            {
                Console.WriteLine("Enter movie title: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(title));

            int i = GetMovieByTitle(title);

            if(i != -1) 
            {
                movies[i].Movie.MovieDetail();
            }
            else
            {
                Console.WriteLine("Movie doesn't exist!");
            }
        }

        // Method to get a movie from the collection by its title
        public int GetMovieByTitle(string title)
        {
            string hashedTitle = HashTitle(title);
            for(var i = 0; i < movies.Count; i++)
            {
                if (movies[i].Title == hashedTitle)
                {
                    //Console.WriteLine(i);
                    return i;
                }
            }

            //Console.WriteLine("Movie with the given title does not exist in the collection.");
            return -1;

        }

        // Method to access a movie by its index
        public (string Title, Movie Movie) this[int index]
        {
            get
            {
                if (index >= 0 && index < movies.Count)
                {
                    return movies[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
        }


        //Hashed function used to convert title into decimal numbers
        private string HashTitle(string title)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(title));

                // Convert the byte array to a hexadecimal string
                string hexHash = BitConverter.ToString(bytes).Replace("-", "");

                // Convert hexadecimal string to BigInteger
                BigInteger bigIntegerHash = BigInteger.Parse(hexHash, System.Globalization.NumberStyles.HexNumber);

                // Ensure the hash is positive
                if (bigIntegerHash < 0)
                {
                    bigIntegerHash = BigInteger.Abs(bigIntegerHash);
                }

                // Convert BigInteger to decimal string
                string decimalHash = bigIntegerHash.ToString();

                //Console.WriteLine(decimalHash);

                return decimalHash;
            }
        }

        //Handling if the movie collection is full of 1000
        private bool IsFull()
        {
            int count = 0;
            foreach (var item in movies)
            {
                if (item.Movie != null)
                {
                    count++;
                }
            }
            
            if(count >= 1000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //function to display all movies information
        public void MovieList()
        {
            //create a new list to hold all the movies from the hash table and then sort it, which will not alter the order in hashed ordered list
            List<(string Title, Movie Movie)> tempMovies = new List<(string Title, Movie Movie)>();

            foreach (var movie in movies)
            {
                if(movie.Title != null)
                {
                    tempMovies.Add(movie);
                }
            }

            if(tempMovies.Count > 0)
            {
                //Sort movies with alphabetical order
                tempMovies.Sort((x, y) => x.Movie.Title.CompareTo(y.Movie.Title));

                //Display information about all movies
                foreach(var movie in tempMovies)
                {
                    movie.Movie.MovieDetail();
                }
            }
            else
            {
                Console.WriteLine("There is currently no movies in storage, contact staff for help.");
            }
        }

        public void MostPopularThree()
        {
            List<(string Title, Movie Movie)> tempMovies = new List<(string Title, Movie Movie)>();
            string tempTitle;
            Movie tempMovie;
            //filter the list with not null object
            foreach (var movie in movies)
            {
                if (movie.Title != null)
                {
                    tempMovies.Add(movie);
                }
            }

            if (tempMovies.Count > 0)
            {
                //Sort movie with borrow times in desending order
                //tempMovies.Sort((x, y) => y.Movie.BorrowTimes.CompareTo(x.Movie.BorrowTimes));
                if (tempMovies.Count >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        (tempTitle, tempMovie) = BorrowTimeCompare(tempMovies);
                        tempMovies.Remove((tempTitle, tempMovie));
                        tempMovie.MovieDetail();
                    }
                }
                else if(tempMovies.Count == 2 || tempMovies.Count == 1)
                {
                    for(int i = tempMovies.Count - 1; i >= 0 ; i--)
                    {
                        tempMovies[i].Movie.MovieDetail();
                    }
                }
                
            }
            else
            {
                Console.WriteLine("There is currently no movies in storage, contact staff for help.");
            }
        }

        public void MemberBorrowedCurrently()
        {
            string title;
            do
            {
                Console.WriteLine("Enter movie title: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(title));

            int i = GetMovieByTitle(title);

            if (i != -1) 
            {
                Console.WriteLine("Members currently renting " + movies[i].Movie.Title + "are: ");
                foreach (Record record in movies[i].Movie.records)
                {
                    if(record.Status == true)
                    {
                        Console.WriteLine(record.Firstname + " " + record.Lastname);
                    }
                }
            }
            else
            {
                Console.WriteLine("Movie doesn't exist!");
            }
        }

        public (string title, bool isBorrowed) MovieBorrowRecord(string firstname, string lastname)
        {
            string title;
            bool isBorrowed = false;
            do
            {
                Console.WriteLine("Enter movie title: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(title));

            int i = GetMovieByTitle(title);

            if (i == -1)
            {
                Console.WriteLine("Movie does not exist!");
                isBorrowed = true;
                return (null, isBorrowed);
            }
            else
            {
                foreach(Record record in movies[i].Movie.records)
                {
                    if (record.Firstname == firstname && record.Lastname == lastname && record.Status == true) 
                    {
                        isBorrowed = true;
                        break;
                    }
                }

                if (isBorrowed)
                {
                    Console.WriteLine("Movie has been borrowed, can not borrow the same movie twice!");
                    return (title, isBorrowed);
                }
                else
                {
                    if (movies[i].Movie.Copies <= 0)
                    {
                        Console.WriteLine("The movie is currently out of stock!");
                        isBorrowed = true;
                        return (title, isBorrowed);
                    }
                    else
                    {
                        movies[i].Movie.RemoveCopies(1);
                        movies[i].Movie.AddRecord(firstname, lastname);
                        movies[i].Movie.AddBorrowTime();

                        Console.WriteLine("Borrow movie: " + movies[i].Movie.Title);
                        title = movies[i].Movie.Title;
                        return (title, isBorrowed);
                    }  
                }
            }
        }

        public (string title, bool isBorrowed) MovieReturn(string firstname, string lastname)
        {
            string title;
            bool isBorrowed = false;
            do
            {
                Console.WriteLine("Enter movie title: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrEmpty(title));

            int i = GetMovieByTitle(title);

            if (i == -1)
            {
                Console.WriteLine("Movie does not exist!");
                isBorrowed = false;
                return (null, isBorrowed);
            }
            else
            {
                foreach (Record record in movies[i].Movie.records)
                {
                    if (record.Firstname == firstname && record.Lastname == lastname)
                    {
                        isBorrowed = true;
                        break;
                    }
                }

                if (isBorrowed)
                {
                    for(int j = 0; j < movies[i].Movie.records.Count; j++)
                    {
                        if (movies[i].Movie.records[j].Firstname == firstname  &&  movies[i].Movie.records[j].Lastname == lastname && movies[i].Movie.records[j].Status == true)
                        {
                            movies[i].Movie.records[j].Status = false;
                            movies[i].Movie.AddNewCopies(1);
                            Console.WriteLine("Return your DVD!");
                            break;
                        }
                    }

                    return(title, isBorrowed);
                }
                else
                {
                    Console.WriteLine("Your have no borrowed record on this movie!");
                    return(null, isBorrowed);
                }
            }
        }

        public (string Title, Movie Movie) BorrowTimeCompare(List<(string Title, Movie Movie)> Movies)
        {
            List<(string Title, Movie Movie)> tempMovies = new List<(string Title, Movie Movie)>();
            
            
            if(Movies.Count % 2 == 0)
            {
                for (int i = 0; i < Movies.Count; i = i + 2)
                {
                    if (Movies[i].Movie.BorrowTimes < Movies[i+1].Movie.BorrowTimes)
                    {
                        tempMovies.Add(Movies[i + 1]);
                    }
                    else
                    {
                        tempMovies.Add(Movies[i]);
                    }
                }
            }
            else
            {
                (string tempTitle, Movie tempMovie) tempItem = (Movies[0].Title, Movies[0].Movie);
                for (int i = 1; i < Movies.Count; i = i + 2)
                {
                    if (Movies[i].Movie.BorrowTimes < Movies[i + 1].Movie.BorrowTimes)
                    {
                        tempMovies.Add(Movies[i + 1]);
                    }
                    else
                    {
                        tempMovies.Add(Movies[i]);
                    }
                }

                if (tempMovies.Count > 0 && tempItem.tempMovie.BorrowTimes > tempMovies[0].Movie.BorrowTimes)
                {
                    tempMovies[0] = tempItem;
                }
                else if (tempMovies.Count == 0)
                {
                    // Add the first movie if the temp list is empty
                    tempMovies.Add(tempItem);
                }
            }

            if(tempMovies.Count == 1) 
            {
                return tempMovies[0];
            }
            else
            {
                return BorrowTimeCompare(tempMovies);
            }
            
        }
    }
}
