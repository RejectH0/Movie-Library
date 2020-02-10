using System;
using System.Linq;
using System.Text;

namespace MovieLibrary
{
    // Initial Program Creation: 09FEB2020 / 1729
    // Code Update: 09FEB2020 / 1803
    // Code Commit Update: 09FEB2020 / 1830 - Header, Menu, ExitGracefully all working

    class Program
    {
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
                "Create Movie Library Entry",
                "List All Movies",
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

                Console.WriteLine("\n[=+> # <+=] Choose ({0}-{1})",menuMin,menuMax);

                Console.SetCursorPosition(5, Console.CursorTop - 1);
                StringBuilder sb = new StringBuilder();
                ConsoleKeyInfo cki;
                cki = Console.ReadKey();
                sb.Append(cki.KeyChar);

                int userChoice = 0;
                try
                {
                    userChoice = Int32.Parse(sb.ToString());
                } catch
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
            Console.WriteLine("CreateMovie()");
        }

        public void ListAllMovies()
        {
            Console.WriteLine("ListAllMovies()");
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
            Console.WriteLine("{0,15}",invalidChoice);
            PressEnterToContinue();
        }

        public void PressEnterToContinue()
        {
            Console.Write("Press Enter to Continue: ");
            Console.ReadKey(false);
            Console.WriteLine();
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
                } else if (str.Equals(""))
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                } else
                {
                    return str;
                }
            }
        }
        public void ReadFile()
        {

        }

        public void WriteFile()
        {

        }



    }
}
