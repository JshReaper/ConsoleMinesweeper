using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// this is a test
    /// </summary>
    class Program
    {
        /// <summary>
        /// this is main
        /// </summary>
        /// <param name="args"></param> 
        static void Main(string[] args)
        {
            MineSweeper();
        }
        /// <summary>
        /// her skabes alle de variabler og arrays der skal bruges i flere funktioner
        /// </summary>
        static string mPlayerInput;
        static int mSelectedBoard;
        /// <summary>
        /// der laves et synligt board (mBoard) og et skjult board (mHBoard) dette gøres sådan at jeg kan printe alle felter uden at spilleren ser hvor bomber er indtil at de faktisk afsløer hvert eneste felt som ikke er (eller rammer en bombe)
        /// </summary>
        static char[,] mBoard;
        static char[,] mHBoard;
        static bool mDifSelect = true;
        static bool mPlaying = true;
        static int clearedFields;
        static int maxFields;
        static int mMaxValueBoardX;
        static int mMaxValueBoardY;
        static int mBombs;
        static int mMaxRandomX;
        static int mMaxRandomY;
        static int flagPlaced;
        static int MaxFlags;
        /// <summary>
        /// her køres "main" til minesweeper spillet, inholder blandt andet sværhedsgrad vælger
        /// </summary>
        static void MineSweeper()
        {
            while (mDifSelect)
            {
                Console.WriteLine("Please select a difficulty or enter \"exit\" to exit the game\n 1: Beginner\n 2: Intermediate\n 3: Expert");
                mPlayerInput = Console.ReadLine();
                Console.Clear();
                switch (mPlayerInput)
                {
                    case "1":
                        mSelectedBoard = 1;
                        mPlaying = true;
                        MIniBoard();
                        MPlay();
                        Console.Clear();
                        break;
                    case "2":
                        mSelectedBoard = 2;
                        mPlaying = true;
                        MIniBoard();
                        MPlay();
                        Console.Clear();
                        break;
                    case "3":
                        mSelectedBoard = 3;
                        mPlaying = true;
                        MIniBoard();
                        MPlay();
                        Console.Clear();
                        break;
                    case "exit":
                        mDifSelect = false;
                        break;
                    default:
                        Console.Clear();
                        break;
                }

            }
        }
        /// <summary>
        /// her skabes minesweeper boardet baseret på valgt sværhedsgrad
        /// </summary>
        static void MIniBoard()
        {
            switch (mSelectedBoard)
            {
                case 1:
                    mHBoard = new Char[9, 9];
                    mBoard = new Char[9, 9];
                    mMaxValueBoardX = 8;
                    mMaxValueBoardY = 8;
                    mMaxRandomX = 9;
                    mMaxRandomY = 9;
                    mBombs = 10;
                    flagPlaced = 0;
                    maxFields = (mMaxRandomX * mMaxRandomY) - mBombs;
                    clearedFields = maxFields;
                    MIniBoard2();
                    break;
                case 2:
                    mHBoard = new Char[16, 16];
                    mBoard = new Char[16, 16];
                    mMaxValueBoardX = 15;
                    mMaxValueBoardY = 15;
                    mBombs = 40;
                    mMaxRandomX = 16;
                    mMaxRandomY = 16;
                    flagPlaced = 0;
                    maxFields = (mMaxRandomX * mMaxRandomY) - mBombs;
                    clearedFields = maxFields;
                    MIniBoard2();
                    break;
                case 3:
                    mBoard = new Char[16, 30];
                    mHBoard = new Char[16, 30];
                    mMaxValueBoardX = 15;
                    mMaxValueBoardY = 29;
                    mBombs = 99;
                    mMaxRandomX = 16;
                    mMaxRandomY = 30;
                    flagPlaced = 0;
                    maxFields = (mMaxRandomX * mMaxRandomY) - mBombs;
                    clearedFields = maxFields;
                    MIniBoard2();
                    break;
            }

        }
        /// <summary>
        /// her placeres alle felterne i boardet resten af det valgte board, da der er forskellige sværhedsgrader blev jeg nødt til at lave denne del en metode for sig selv
        /// </summary>
        static void MIniBoard2()
        {
            for (int x = 0; x < mBoard.GetLength(0); x++)
            {
                for (int y = 0; y < mBoard.GetLength(1); y++)
                {
                    mBoard[x, y] = '#';

                }
            }
            int generateBombs = mBombs;
            while (generateBombs >= 1)
            {
                Random r = new Random();

                int x = r.Next(0, mMaxRandomX);
                int y = r.Next(0, mMaxRandomY);
                if (mHBoard[x, y] != 'X')
                    generateBombs--;

                mHBoard[x, y] = 'X';
            }
            for (int x = 0; x < mHBoard.GetLength(0); x++)
            {
                for (int y = 0; y < mHBoard.GetLength(1); y++)
                {
                    if (mHBoard[x, y] != 'X')
                        mHBoard[x, y] = '0';

                }
            }
            for (int x = 0; x < mHBoard.GetLength(0); x++)
            {
                for (int y = 0; y < mHBoard.GetLength(1); y++)
                {
                    if (mHBoard[x, y] == 'X')
                    {

                        if (x != 0 && mHBoard[x - 1, y] != 'X')
                            mHBoard[x - 1, y] = '1';

                        if (x != mMaxValueBoardX && mHBoard[x + 1, y] != 'X')
                            mHBoard[x + 1, y] = '1';

                        if (x != 0 && y != 0 && mHBoard[x - 1, y - 1] != 'X')
                            mHBoard[x - 1, y - 1] = '1';

                        if (x != 0 && y != mMaxValueBoardY && mHBoard[x - 1, y + 1] != 'X')
                            mHBoard[x - 1, y + 1] = '1';

                        if (x != mMaxValueBoardX && y != mMaxValueBoardY && mHBoard[x + 1, y + 1] != 'X')
                            mHBoard[x + 1, y + 1] = '1';

                        if (x != mMaxValueBoardX && y != 0 && mHBoard[x + 1, y - 1] != 'X')
                            mHBoard[x + 1, y - 1] = '1';

                        if (y != mMaxValueBoardY && mHBoard[x, y + 1] != 'X')
                            mHBoard[x, y + 1] = '1';

                        if (y != 0 && mHBoard[x, y - 1] != 'X')
                            mHBoard[x, y - 1] = '1';

                    }


                }
            }
            for (int x = 0; x < mHBoard.GetLength(0); x++)
            {
                for (int y = 0; y < mHBoard.GetLength(1); y++)
                {
                    if (mHBoard[x, y] == '1')
                    {
                        int countX = 48;
                        if (x != 0 && mHBoard[x - 1, y] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (x != mMaxValueBoardX && mHBoard[x + 1, y] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (x != 0 && y != 0 && mHBoard[x - 1, y - 1] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (x != 0 && y != mMaxValueBoardY && mHBoard[x - 1, y + 1] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (x != mMaxValueBoardX && y != mMaxValueBoardY && mHBoard[x + 1, y + 1] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (x != mMaxValueBoardX && y != 0 && mHBoard[x + 1, y - 1] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (y != mMaxValueBoardY && mHBoard[x, y + 1] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;

                        if (y != 0 && mHBoard[x, y - 1] == 'X')
                            countX += 1;
                        mHBoard[x, y] = (char)countX;
                    }
                }
            }
        }
        /// <summary>
        /// her tegnes minesweeper boardet
        /// </summary>
        static void MDrawnBoard()
        {
            flagPlaced = 0;
            for (int x = 0; x < mBoard.GetLength(0); x++)
            {  
                for (int y = 0; y < mBoard.GetLength(1); y++)
                {
                    if (mBoard[x, y] == '?')
                        flagPlaced++;
                }
            }
            Console.WriteLine("flags placed {0} / {1}", flagPlaced, MaxFlags);
            Console.WriteLine("Fields left to reveal: " + clearedFields + "\n");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("   ");
            for (int x = 0; x < mBoard.GetLength(1); x++)
            {
                
                Console.Write(x + 1);
                if (x < 9)
                    Console.Write(" ");
                Console.Write(" ");
            }
            Console.WriteLine();
            for (int x = 0; x < mBoard.GetLength(0); x++)
            {
                if (x < 9)
                    Console.Write(" ");
                Console.Write(x + 1 + " ");
                for (int y = 0; y < mBoard.GetLength(1); y++)
                {
                    int boardL = mBoard.GetLength(1);
                    Console.Write(mBoard[x, y] + " ");
                    if (x < boardL )
                        Console.Write(" ");

                }
                Console.WriteLine(x + 1);
            }
            Console.Write("   ");
            for (int x = 0; x < mBoard.GetLength(1); x++)
            {

                Console.Write(x + 1);
                if (x < 9)
                    Console.Write(" ");
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// her spilles spillet, "playing" bliver true ligeså snart man har valgt sin sværhedsgrad og ens board er blevet dannet i MIniBoard()
        /// </summary>
        static void MPlay()
        {
            while (mPlaying)
            {
                MCheckZeros();
                MaxFlags = mBombs;
                MDrawnBoard();

                Console.WriteLine("to check if you won press any key(bug...)\n enter x cord you wish to reveal, restart or exit\n to place a flag type \"flag\"");
                mPlayerInput = Console.ReadLine();
                int intInput;
                bool inputIsInt = Int32.TryParse(mPlayerInput, out intInput);
                if (inputIsInt)
                {
                    int tempPlayerInput = Convert.ToInt32(mPlayerInput);
                    int y = tempPlayerInput - 1;
                    int x;
                    Console.WriteLine("enter y cord");
                    mPlayerInput = Console.ReadLine();
                    inputIsInt = Int32.TryParse(mPlayerInput, out intInput);
                    if (inputIsInt)
                    {

                        tempPlayerInput = Convert.ToInt32(mPlayerInput);
                        x = tempPlayerInput - 1;
                        if (mBoard[x, y] == '?')
                        {
                            flagPlaced--;
                            clearedFields--;

                        }

                        if (mBoard[x, y] == '#')
                            clearedFields--;
                        mBoard[x, y] = mHBoard[x, y];
                        MCheckZeros();

                        if (mBoard[x, y] == 'X')
                        {
                            Console.Clear();
                            MDrawnBoard();
                            Console.WriteLine("You hit a bomb, and lost the game\n press any key to start a new game");
                            Console.ReadKey();
                            mPlaying = false;
                        }
                        
                        
                    }
                    else
                    {
                        Console.WriteLine("you entered invaild input, please try again, press any key...");
                        Console.ReadKey();
                    }


                    Console.Clear();


                }
                else if (mPlayerInput == "flag")
                {
                    Console.WriteLine("Enter the x cord of the flag");
                    mPlayerInput = Console.ReadLine();
                    inputIsInt = Int32.TryParse(mPlayerInput, out intInput);
                    if (inputIsInt)
                    {
                        int tempPlayerInput = Convert.ToInt32(mPlayerInput);
                        int x = tempPlayerInput - 1;
                        int y;
                        Console.WriteLine("enter y cord of the flag");
                        mPlayerInput = Console.ReadLine();
                        inputIsInt = Int32.TryParse(mPlayerInput, out intInput);
                        if (inputIsInt)
                        {

                            tempPlayerInput = Convert.ToInt32(mPlayerInput);
                            y = tempPlayerInput - 1;
                            if (mBoard[x, y] == '#')
                            mBoard[x, y] = '?';
                            else if (mBoard[x,y] != '#')
                            {
                                flagPlaced--;
                                Console.WriteLine("You already revealed this field you can't place a flag here\n press any key to continue");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Flag already placed here !, if you wish to remove flag simple reveal the field\n press any key to continue...");
                                Console.ReadKey();
                            }
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("you entered invaild input, please try again, press any key...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("you entered invaild input, please try again, press any key...");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                else if (mPlayerInput == "exit")
                {
                    mPlaying = false;
                    mDifSelect = false;
                }
                else if (mPlayerInput == "restart")
                {
                    mPlaying = false;
                }
                else if (flagPlaced == mBombs && clearedFields == 0)
                {
                    Console.Clear();
                    Console.WriteLine("you won the game gz m8\n press any key to start a new game");
                    Console.ReadKey();
                    mPlaying = false;
                }
                else
                {
                    Console.WriteLine("invaild user input\n enter any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                }
            }
        /// <summary>
        /// denne funktion checker om der er blevet afsløret et "0" felt, og afslører derefter alle felter rundt om det
        /// </summary>
        static void MCheckZeros()
        {
            for (int i = 0; i < (mBoard.GetLength(0) * mBoard.GetLength(1)); i++)
            {
                for (int x = 0; x < mBoard.GetLength(0); x++)
                {
                    for (int y = 0; y < mBoard.GetLength(1); y++)
                    {
                        if (mBoard[x, y] == '0')
                        {

                            if (x != 0 && mBoard[x - 1, y] == '#')
                            {
                                mBoard[x - 1, y] = mHBoard[x - 1, y];
                                clearedFields--;
                            }
                            if (x != mMaxValueBoardX && mBoard[x + 1, y] == '#')
                            {
                                mBoard[x + 1, y] = mHBoard[x + 1, y];
                                clearedFields--;
                            }
                            if (x != 0 && y != 0 && mBoard[x - 1, y - 1] == '#')
                            {
                                mBoard[x - 1, y - 1] = mHBoard[x - 1, y - 1];
                                clearedFields--;
                            }
                            if (x != 0 && y != mMaxValueBoardY && mBoard[x - 1, y + 1] == '#')
                            {
                                mBoard[x - 1, y + 1] = mHBoard[x - 1, y + 1];
                                clearedFields--;
                            }
                            if (x != mMaxValueBoardX && y != mMaxValueBoardY && mBoard[x + 1, y + 1] == '#')
                            {
                                mBoard[x + 1, y + 1] = mHBoard[x + 1, y + 1];
                                clearedFields--;
                            }
                            if (x != mMaxValueBoardX && y != 0 && mBoard[x + 1, y - 1] == '#')
                            {
                                mBoard[x + 1, y - 1] = mHBoard[x + 1, y - 1];
                                clearedFields--;
                            }
                            if (y != mMaxValueBoardY && mHBoard[x, y + 1] == '#')
                            {
                                mBoard[x, y + 1] = mHBoard[x, y + 1];
                                clearedFields--;
                            }
                            if (y != 0 && mHBoard[x, y - 1] == '#')
                            {
                                mBoard[x, y - 1] = mHBoard[x, y - 1];
                                clearedFields--;
                            }
                        }
                    }



                }
            }
        }
        }
    }
