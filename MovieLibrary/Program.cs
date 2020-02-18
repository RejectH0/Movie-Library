using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieLibrary
{
    // Initial Program Creation: 09FEB2020 / 1729
    // Code Update: 09FEB2020 / 1803
    // Code Commit Update: 09FEB2020 / 1830 - Header, Menu, ExitGracefully all working
    // Version 1.0a Released: 10FEB2020 / 0115
    // Version 1.1a Released: 10FEB2020 / 0130 
    //// Version 1.2a Released: 10FEB2020 / 0140
    // Version 1.3 Released: 17FEB2020 / 1835

    class Movie : IDisposable
    {
        private static int idNext = 1;
        private static int idDestroy = -1;
        public int MovieID { get; private set; }
        public String MovieTitle { get; set; }
        public int MovieYear { get; set; }
        public GenreList<Genre> Genres { get; set; }

        public Movie(String MovieTitle, int MovieYear, String Genres)
        {
            if (idDestroy == -1)
            {
                this.MovieID = Movie.idNext;
                Movie.idNext++;
            }
            else
            {
                this.MovieID = Movie.idDestroy;
            }

            this.MovieTitle = MovieTitle;

            this.MovieYear = MovieYear;

            this.Genres = new GenreList<Genre>();
            if (!(Genres == null || Genres.Equals("none")))
            {
                var genreList = Genres.Split('|');

                for (int g = 0; g < genreList.Length; g++)
                {
                    if (Genre.AllGenres.Any(item => item.GenreName.Equals(genreList[g])))
                    {
                        this.Genres.Add(Genre.AllGenres.Find(item => item.GenreName.Equals(genreList[g])));
                    }
                    else
                    {
                        this.Genres.Add(new Genre(genreList[g]));
                    }
                }
            }
        }
        public Movie(String MovieTitle, int MovieYear, List<Genre> Genres)
        {
            if (idDestroy == -1)
            {
                this.MovieID = Movie.idNext;
                Movie.idNext++;
            }
            else
            {
                this.MovieID = Movie.idDestroy;
            }
            this.MovieTitle = MovieTitle;
            this.MovieYear = MovieYear;
            this.Genres = new GenreList<Genre>();
            foreach (var item in Genres)
            {
                this.Genres.Add(item);
            }
        }
        public Movie(String MovieTitle, String MovieYear, String Genres)
        {
            if (idDestroy == -1)
            {
                this.MovieID = Movie.idNext;
                Movie.idNext++;
            }
            else
            {
                this.MovieID = Movie.idDestroy;
            }

            this.MovieTitle = MovieTitle;

            bool success = Int32.TryParse(MovieYear, out int movieYearInterim);
            if (success)
            {
                this.MovieYear = movieYearInterim;
            }
            else
            {
                this.MovieYear = 9999;
            }

            this.Genres = new GenreList<Genre>();
            if (!(Genres == null || Genres.Equals("none")))
            {
                var genreList = Genres.Split('|');

                for (int g = 0; g < genreList.Length; g++)
                {
                    if (Genre.AllGenres.Any(item => item.GenreName.Equals(genreList[g])))
                    {
                        this.Genres.Add(Genre.AllGenres.Find(item => item.GenreName.Equals(genreList[g])));
                    }
                    else
                    {
                        this.Genres.Add(new Genre(genreList[g]));
                    }
                }
            }
        }

        public Movie(String MovieTitle, String MovieYear, List<Genre> Genres)
        {
            if (idDestroy == -1)
            {
                this.MovieID = Movie.idNext;
                Movie.idNext++;
            }
            else
            {
                this.MovieID = Movie.idDestroy;
            }

            this.MovieTitle = MovieTitle;
            this.MovieYear = Int32.Parse(MovieYear);
            this.Genres = new GenreList<Genre>();
            foreach (var item in Genres)
            {
                this.Genres.Add(item);
            }
        }
        public class GenreList<Genre> : List<Genre>
        {
            public override string ToString()
            {
                var str = "";
                int thisCount = this.Count();
                if (thisCount > 1)
                {
                    for (int g = 0; g < thisCount; g++)
                    {
                        str += this.ElementAt(g) + "|";
                    }
                    return str.Substring(0, (str.Length) - 1);
                }
                else
                {
                    str += this.ElementAt(0);
                    return str;
                }
            }
        }
        public override string ToString()
        {
            var str = "";
            int comma = MovieTitle.IndexOf(',');
            if (comma > 0)
            {
                str += MovieID + "," + "\"" + MovieTitle + "\"" + "," + MovieYear;
            }
            else
            {
                str += MovieID + "," + MovieTitle + "," + MovieYear;
            }

            int numGenres = Genres.Count();

            if (numGenres > 0)
            {
                str += ",";
                str += Genres.ToString();
            }

            return str;
        }
        public void Dispose()
        {
            Movie.idDestroy = this.MovieID;
        }
    }

    class Genre : IDisposable
    {
        private static int idNext = 1;
        private static int idDestroy = -1;
        public static List<Genre> AllGenres = new List<Genre>();
        public int idNumber { get; private set; }
        public String GenreName { get; set; }

        public Genre(String GenreName)
        {
            if (idDestroy == -1)
            {
                this.idNumber = Genre.idNext;
                Genre.idNext++;
            }
            else
            {
                this.idNumber = Genre.idDestroy;
            }

            this.GenreName = GenreName;
            Genre.AllGenres.Add(this);
        }
        public override string ToString()
        {
            return GenreName;
        }
        public void Dispose()
        {
            Genre.idDestroy = this.idNumber;
        }
    }

    class Program
    {
        public bool DisplayDebugInformation = false;
        public static List<Movie> movies = new List<Movie>();
        private static int index = 0;

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            RunMainMenu();
        }

        public void DisplayHeader()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            string menuText = "Welcome to Gregg's Movie Library System!";
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("*");
            }

            int winWidth = Console.WindowWidth - 1;
            Console.SetCursorPosition(winWidth, 1);
            Console.Write("*");
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("*");
            }

            Console.SetCursorPosition(0, 1);
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (menuText.Length / 2)) + "}", menuText));
            Console.SetCursorPosition(0, 1);
            Console.Write("*");
            Console.SetCursorPosition(0, 5);
        }
        private int PrintMainMenu()
        {
            string menuName = "Main";
            string[] menuChoices = new string[]
            {
                "Read Movie Library File",
                "Enter Movie into Database",
                "List All Movies",
                "List All Genres",
                "Exit Program"
            };

            Console.WriteLine(menuName + " Menu");
            for (int i = 0; i < menuChoices.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + menuChoices[i]);
            }
            return menuChoices.Count();
        }
        private void RunMainMenu()
        {
            bool userContinue = true;
            while (userContinue)
            {
                DisplayHeader();
                int menuMin = 1;
                int menuMax = PrintMainMenu();

                Console.WriteLine("\n[=+> # <+=] Choose ({0}-{1})", menuMin, menuMax);

                Console.SetCursorPosition(5, Console.CursorTop - 1);
                StringBuilder sb = new StringBuilder();
                ConsoleKeyInfo cki;
                cki = Console.ReadKey();
                sb.Append(cki.KeyChar);

                int userChoice = 0;
                try
                {
                    userChoice = Int32.Parse(sb.ToString());
                }
                catch
                {
                    userChoice = 0;
                }

                switch (userChoice)
                {
                    case 1:
                        ReadFile();
                        break;
                    case 2:
                        CreateMovie();
                        break;
                    case 3:
                        ListAllMovies();
                        break;
                    case 4:
                        ListAllGenres();
                        break;
                    case 5:
                        ExitGracefully();
                        break;
                    default:
                        InvalidMenuChoice();
                        break;
                }
            }
        }

        public void CreateMovie()
        {
            DisplayHeader();
            string menuText = "Enter a Movie into the Library";
            Console.SetCursorPosition(0, 6);
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (menuText.Length / 2)) + "}", menuText));

            Console.SetCursorPosition(0, 8);
            string[] enterMovie = new string[]
            {
                getStringValue("Enter Movie Name"),
                getIntValueFixedLength("Enter Movie's Year of Release",4,1970,2068).ToString(),
            };

            Console.SetCursorPosition(0, 8);
            ConsoleSpaces(90, 20);

            Console.SetCursorPosition(75, 9);
            Console.Write("Movie   : {0}", enterMovie[0]);

            Console.SetCursorPosition(75, 10);
            Console.Write("Year    : {0}", enterMovie[1]);

            List<Genre> genres = new List<Genre>();
            ConsoleKey userResponse;

            do
            {
                Console.SetCursorPosition(0, 8);
                ConsoleSpaces(60, 10);
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("Please choose a Genre for this Movie:");
                Genre genre = MenuItemGenreSelection();
                genres.Add(genre);
                Console.SetCursorPosition(75, (11 + (genres.Count())));
                Console.WriteLine("Genre #{0}: {1}", genres.Count(), genre);
                Console.SetCursorPosition(0, 8);
                ConsoleSpaces(60, 10);
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("Would you like to add another Genre? (Y/N): ");
                userResponse = Console.ReadKey(true).Key;
            } while (userResponse == ConsoleKey.Y);

            movies.Add(new Movie(enterMovie[0], enterMovie[1], genres));
            Console.SetCursorPosition(10, 5);
            PressEnterToContinue();

        }

        private void ConsoleSpaces(int spaces, int lines)
        {
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            for (int i = 0; i < lines; i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop + i);
                for (int g = 0; g < spaces; g++)
                {
                    Console.Write(" ");
                }
            }
        }

        public Genre MenuItemGenreSelection()
        {
            index = 0;
            string str = "";
            Console.CursorVisible = false;
            List<string> vs = new List<string>();
            vs.Add("[New]");

            foreach (var item in Genre.AllGenres)
            {
                vs.Add(item.GenreName);
            }

            while (str.Length == 0)
            {
                str = drawMenu(vs, Console.CursorLeft, Console.CursorTop);
            }
            Console.CursorVisible = true;

            if (str.Equals("[New]"))
            {

                Console.SetCursorPosition(10, 5);
                string newGenre = getStringValue("Please enter the new Genre");
                Console.SetCursorPosition(10, 5);
                ConsoleSpaces(70, 2);
                new Genre(newGenre);
                return Genre.AllGenres.Find(item => item.GenreName.Equals(newGenre));
            }
            else
            {
                return Genre.AllGenres.Find(item => item.GenreName.Equals(str));
            }

        }

        private string drawMenu(List<string> items, int cursorLeft, int cursorTop)
        {

            Console.SetCursorPosition(cursorLeft, cursorTop);
            for (int g = 0; g < items.Count; g++)
            {
                if (g == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(items[g]);
                }
                else
                {
                    Console.WriteLine(items[g]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    index = 0;
                }
                else
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    index++;
                }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    index = items.Count - 1;
                }
                else
                {

                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    index--;
                }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }
            return "";
        }

        public void PressEnterToContinue()
        {

            Console.Write("Press Enter to Continue: ");
            ConsoleKeyInfo ckey = Console.ReadKey(false);
            if (ckey.Key == ConsoleKey.Q)
            {
                return;
            }

        }
        public void ListAllMovies()
        {
            DisplayHeader();
            var moviesCount = movies.Count();

            Console.WriteLine("There {0} currently {1:N0} Movie{2} in the Library.", (moviesCount == 0 || moviesCount > 1) ? "are" : "is", moviesCount, (moviesCount == 0 || moviesCount > 1) ? "s" : "");

            PressEnterToContinue();

            Console.Write("{0,-10}{1,-60}{2,-6}{3,-20}\n", "MovieID", "Movie Title", "Year", "Genre(s)");

            for (int g = 0; g < moviesCount; g++)
            {
                bool isEmpty = !movies[g].Genres.Any();
                if (isEmpty)
                {
                    Console.Write("{0,-10}{1,-60}{2,-6}{3,-20}\n", movies[g].MovieID, movies[g].MovieTitle, movies[g].MovieYear, "none");
                }
                else
                {
                    int maxLength = (movies[g].MovieTitle.Length < 60) ? movies[g].MovieTitle.Length : 60;
                    Console.Write("{0,-10}{1,-60}{2,-6}{3,-20}\n", movies[g].MovieID, movies[g].MovieTitle.Substring(0, maxLength), movies[g].MovieYear, movies[g].Genres);
                }

                if (g % 28 == 0 && g != 0)
                {
                    PressEnterToContinue();
                }
            }

            PressEnterToContinue();
        }

        public void ListAllGenres()
        {
            DisplayHeader();
            var genresCount = Genre.AllGenres.Count();
            Console.WriteLine("There {0} currently {1:N0} Genre{2} in the Library.", (genresCount == 0 || genresCount > 1) ? "are" : "is", genresCount, (genresCount == 0 || genresCount > 1) ? "s" : "");

            PressEnterToContinue();

            Console.Write("{0,-10}{1,-20}\n", "idNumber", "GenreName");

            foreach (var item in Genre.AllGenres)
            {
                Console.Write("{0,-10}{1,-20}\n", item.idNumber, item.GenreName);
            }
            PressEnterToContinue();
        }
        public void ExitGracefully()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("Now exiting this application...");
            WriteFile();
            PressEnterToContinue();
            System.Environment.Exit(0);
        }

        public void InvalidMenuChoice()
        {
            Console.Clear();
            DisplayHeader();
            string invalidChoice = "You have made an invalid selection. Please try again.";
            Console.WriteLine("{0,15}", invalidChoice);
            PressEnterToContinue();
        }

        private string getStringValue(String prompt)
        {
            var str = "";
            while (true)
            {
                Console.Write((prompt != null) ? prompt : "Please enter your response");
                Console.Write(": ");
                str = Console.ReadLine();

                if (str.Equals("-99"))
                {
                    ExitGracefully();
                }
                else if (str.Equals(""))
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                }
                else
                {
                    return str;
                }
            }
        }

        private int getIntValueFixedLength(string prompt, int fixedLength, int min, int max)
        {
            while (true)
            {
                Console.Write((prompt != null) ? prompt : "Please enter your response");
                Console.Write(": ");

                var str = Console.ReadLine();

                bool success = Int32.TryParse(str, out int value);

                if (str.Equals("-99"))
                {
                    ExitGracefully();
                }
                else if (str.Equals(""))
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                }
                else if (str.Length < fixedLength || str.Length > fixedLength)
                {
                    Console.WriteLine("Your entry must be exactly {0} digits.", fixedLength);
                }
                else if (!success)
                {
                    Console.WriteLine("Your entry must be a number.");
                }
                else if (value < min || value > max)
                {
                    Console.WriteLine("Your entry must be between {0} and {1}. Your entry ({2}) is not between those values.", min, max, value);
                }
                else
                {
                    return value;
                }
            }
        }

        public int countChar(string str, char contains)
        {
            int count = 0;
            while (count < str.Length && str[count] == contains) { count++; }
            return count;
        }
        public void ReadFile()
        {
            DisplayHeader();
            var pathWithEnv = @"%USERPROFILE%\Documents\GS Movie Library\GS Movie Library Movies.txt";
            var fileData = Environment.ExpandEnvironmentVariables(pathWithEnv);
            Console.WriteLine("File Location: {0}", fileData);

            if (!File.Exists(fileData))
            {
                Console.WriteLine("The file does not exist, so I will create test data for you.");
                movies.Add(new Movie("The Matrix", 1999, "Action|Sci-Fi"));
                movies.Add(new Movie("The Matrix Reloaded", 2003, "Action|Sci-Fi"));
                movies.Add(new Movie("The Matrix Revolutions", 2003, "Action|Sci-Fi"));
                WriteFile();
                Console.WriteLine("Test data has been created and written out to the file.");
                PressEnterToContinue();
            }

            using (CsvReader csv = new CsvReader(new StreamReader(fileData), true))
            {
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                int recordCount = 0;

                if (fieldCount == 3)
                {
                    if (headers[0].Equals("movieId"))
                    {
                        if (DisplayDebugInformation)
                        {
                            Console.Write("{0,-60}{1,-10}{2,-30}\n", headers);
                        }

                        while (csv.ReadNextRecord())
                        {
                            string[] movieImport = new string[]
                            {
                            csv[csv.GetFieldIndex("title")],
                            csv[csv.GetFieldIndex("title")].Substring(csv[csv.GetFieldIndex("title")].Length - 5, 4),
                            csv[csv.GetFieldIndex("genres")]
                            };

                            if (movieImport[1].Substring(movieImport[1].Length - 1, 1).Equals(" "))
                            {
                                movieImport[1] = csv[csv.GetFieldIndex("title")].Substring(csv[csv.GetFieldIndex("title")].Length - 6, 4);
                                movieImport[0] = csv[csv.GetFieldIndex("title")].Substring(0, csv[csv.GetFieldIndex("title")].Length - 8);
                            }
                            else
                            {
                                movieImport[0] = csv[csv.GetFieldIndex("title")].Substring(0, csv[csv.GetFieldIndex("title")].Length - 7);
                            }

                            if (DisplayDebugInformation)
                            {
                                Console.Write("{0,-60}{1,-10}{2,-30}\n", movieImport);
                            }

                            movies.Add(new Movie(movieImport[0], movieImport[1], movieImport[2]));
                            recordCount++;
                        }
                    }
                }

                if (fieldCount == 4)
                {
                    if (headers[0].Equals("MovieID"))
                    {
                        if (DisplayDebugInformation)
                        {
                            Console.Write("{0,-10}{1,-60}{2,-6}{3,-20}\n", headers);
                        }

                        while (csv.ReadNextRecord())
                        {
                            string[] movieImport = new string[4];
                            try
                            {
                                movieImport = new string[] {
                                csv[csv.GetFieldIndex("MovieID")],
                                csv[csv.GetFieldIndex("MovieTitle")],
                                csv[csv.GetFieldIndex("MovieYear")],
                                csv[csv.GetFieldIndex("MovieGenres")]
                                };
                            }
                            catch (MalformedCsvException e)
                            {
                                Console.WriteLine(" [=+-> Found a problem with import record #{0} <-+=]", recordCount+1);
                            }

                            if (DisplayDebugInformation)
                            {
                                Console.Write("{0,-10}{1,-60}{2,-6}{3,-20}\n", movieImport);
                            }

                            movies.Add(new Movie(movieImport[1], movieImport[2], movieImport[3]));
                            recordCount++;
                        }
                    }
                }

                Console.WriteLine("A total of {0:N0} records were added.", recordCount);
                Console.WriteLine("A total of {0:N0} Movies now exist in the Library.", movies.Count());
            }

            PressEnterToContinue();
        }
        private static void Dump(object o)
        {
            string json = JsonConvert.SerializeObject(o, Formatting.Indented);
            Console.WriteLine(json);
        }

        public void WriteFile()
        {
            DisplayHeader();
            var pathWithEnv = @"%USERPROFILE%\Documents\GS Movie Library\GS Movie Library Movies.txt";
            var fileData = Environment.ExpandEnvironmentVariables(pathWithEnv);
            Console.WriteLine("File Location: {0}", fileData);

            try
            {
                using (StreamWriter sw = new StreamWriter(fileData, false))
                {
                    sw.Write("MovieID,MovieTitle,MovieYear,MovieGenres\n");

                    foreach (var movie in movies)
                    {
                        String str = movie.ToString();
                        sw.Write(str + "\n");
                    }
                    sw.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be written: {0}", e.Message);
            }
        }

    }
}
