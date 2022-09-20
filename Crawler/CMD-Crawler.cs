using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crawler
{
    /**
     * The main class of the Dungeon Crawler Application
     * 
     * You may add to your project other classes which are referenced.
     * Complete the templated methods and fill in your code where it says "Your code here".
     * Do not rename methods or variables which already exist or change the method parameters.
     * You can do some checks if your project still aligns with the spec by running the tests in UnitTest1
     * 
     * For Questions do contact us!
     */
    public class CMDCrawler
    {
        /**
         * use the following to store and control the next movement of the yser
         */
        public enum PlayerActions {NOTHING, NORTH, EAST, SOUTH, WEST, PICKUP, ATTACK, QUIT };
        private PlayerActions action = PlayerActions.NOTHING;

        /**
         * tracks if the game is running
         */
        private bool active = true;

        //My stuff vvv

        bool playing = false;
        bool playerOnGold = false;
        int playerGoldAmount = 0;

        //WORK ON THIS STUFF
        Dictionary<char, int> monsterIDs = new Dictionary<char, int>();
        Dictionary<int, char> monsterPositions = new Dictionary<int, char>();
        Dictionary<int, bool> monstersOnGold = new Dictionary<int, bool>();

        //My stuff ^^^

        /**
         * Reads user input from the Console
         * 
         * Please use and implement this method to read the user input.
         * 
         * Return the input as string to be further processed
         * 
         */
        private string ReadUserInput()
        {
            string inputRead = string.Empty;

            // Your code here
            /*if (isMapLoaded == false)
            {
                inputRead = Console.ReadLine();
                Console.WriteLine("ReadUserInput() was used: " + inputRead);
            }*/

            if (playing == false)
            {
                inputRead = Console.ReadLine();
            }
            else if (playing == true)
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        inputRead = Console.ReadKey().KeyChar.ToString();
                        break;
                    }
                }
            }

            /*while (true)
            {
                inputRead = Console.ReadLine();
                if (inputRead != null)
                {
                    break;
                }
            }

            inputRead = Console.ReadLine();*/
            
            return inputRead;
        }

        /**
         * Processed the user input string
         * 
         * takes apart the user input and does control the information flow
         *  * initializes the map ( you must call InitializeMap)
         *  * starts the game when user types in Play
         *  * sets the correct playeraction which you will use in the GameLoop
         */

        public void ProcessUserInput(string input)
        {
            // Your Code here

            bool actionOccurred = false;

            //out of game inputs
            if (isMapLoaded == false)
            {
                if (input == "load Simple.Map")
                {
                    InitializeMap("Simple.map");
                }
            }
            else if (isMapLoaded == true)
            {
                //not a game mechanic action
                if (input == "play")
                {
                    Console.Clear();
                    UpdateMap();
                    playing = true;
                }
                else if (input == "NOTHING")
                {
                    action = PlayerActions.NOTHING;
                    GameLoop(active);
                    actionOccurred = true;
                }
                /*else
                {
                    ConsoleKeyInfo pressedKeyInfo;
                    while (true)
                    {
                        pressedKeyInfo = Console.ReadKey();
                        if (pressedKeyInfo.Key == ConsoleKey.W)
                        {
                            action = PlayerActions.NORTH;
                            GameLoop(active);
                            actionOccurred = true;
                            break;
                        }
                    }

                    while (!Console.KeyAvailable) { }
                    if (Console.ReadKey(true).Key == ConsoleKey.W)
                    {
                        action = PlayerActions.NORTH;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else
                }*/
                else if (playing == true)
                {
                    if (input == "w")
                    {
                        action = PlayerActions.NORTH;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else if (input == "d")
                    {
                        action = PlayerActions.EAST;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else if (input == "s")
                    {
                        action = PlayerActions.SOUTH;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else if (input == "a")
                    {
                        action = PlayerActions.WEST;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else if (input == "e")
                    {
                        action = PlayerActions.PICKUP;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else if (input == " ")
                    {
                        action = PlayerActions.ATTACK;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                    else if (input == "q")
                    {
                        action = PlayerActions.QUIT;
                        active = false;
                        GameLoop(active);
                        actionOccurred = true;
                    }
                }
            }

            //for Swen's tests probs
            if (actionOccurred == true)
            {
                GetPlayerAction();
                DoAction((int)action);
            }
        }

        /**
         * The Main Game Loop. 
         * It updates the game state.
         * 
         * This is the method where you implement your game logic and alter the state of the map/game
         * use playeraction to determine how the character should move/act
         * the input should tell the loop if the game is active and the state should advance
         */
        public void GameLoop(bool active)
        {
            // Your code here

            //WORK ON THIS 'SOME POINT
            /*if ((int)action == 1)
            {
                Console.WriteLine("swigswag");
            }*/

            /*while (active == true)
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey().Key == ConsoleKey.W)
                    {
                        action = PlayerActions.NORTH;
                    }
                }
            }*/
        }

        /**
        * Map and GameState get initialized
        * mapName references a file name 
        * 
        * Create a private object variable for storing the map in Crawler and using it in the game.
        */
        private char[,] globalMap = new char[10, 31];
        private bool isMapLoaded = false;
        public bool InitializeMap(String mapName)
        {
            bool initSuccess = false;

            // Your code here

            //char[,] map = new char[32, 10];
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //string filePath = @"\..\maps\Crawler\maps\Simple.map"; [Probably best to use this eventually]
            string filePathTempOld = @"\Simple.map";
            string filePathTemp = @"\" + mapName;

            try
            {
                using (StreamReader sr = new StreamReader(dir + filePathTemp))
                {
                    string[] allLines = System.IO.File.ReadAllLines(mapName);

                    /*DEBUG (shows loaded map)
                    foreach(string line in allLines)
                    {
                        Console.WriteLine(line);
                    }*/

                    for (int iY = 0; iY < 10; iY++)
                    {
                        for (int iX = 0; iX < allLines[iY].Length; iX++)
                        {
                            globalMap[iY, iX] = allLines[iY][iX];

                            //DEBUG
                            /*Console.WriteLine(globalMap[iY, iX]);
                            
                            string arrayPositions = "x: " + Convert.ToString(iX) + " | y: " + Convert.ToString(iY) + " | char: " + Convert.ToString(globalMap[iY, iX]);
                            Console.WriteLine(arrayPositions);*/
                        }
                    }
                    Console.WriteLine("Successfully loaded '" + mapName + ".'");
                    isMapLoaded = true;
                }

                initSuccess = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading map data");
                Console.WriteLine(e.Message);
            }

            return initSuccess;
        }

        //MY NEW METHODS VVV
        public void ActionListener()
        {

        }
        public void UpdateMap()
        {
            //DEBUG
            /*Console.WriteLine(Convert.ToString(globalMap.GetLength(0)));
            Console.WriteLine(Convert.ToString(globalMap.GetLength(1)));*/

            //prints out the map
            for (int iY = 0; iY < globalMap.GetLength(0); iY++)
            {
                for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                {
                    if (iX == globalMap.GetLength(1) - 1)
                    {
                        Console.WriteLine(globalMap[iY, iX]);
                    }
                    else
                    {
                        Console.Write(globalMap[iY, iX]);
                    }
                }
            }
        }
        public bool DoAction(int action)
        {
            if (action == 0 || action == 1 || action == 2 || action == 3 || action == 4 || action == 5 || action == 6)
            {
                Console.Clear();
            }

            bool foundPos = false;
            if (action == 0)
            {

            }
            //North
            else if (action == 1)
            {
                for (int iY = 0; iY < globalMap.GetLength(0); iY++)
                {
                    for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                    {
                        //Looks for 'S' (player) on the map
                        if (globalMap[iY, iX] == 'S')
                        {
                            //Checks for what should occur
                            if (globalMap[iY - 1, iX] == '#')
                            {
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY - 1, iX] == '.')
                            {
                                if (playerOnGold == true)
                                {
                                    globalMap[iY, iX] = 'G';
                                    playerOnGold = false;
                                }
                                else if (playerOnGold == false)
                                {
                                    globalMap[iY, iX] = '.';
                                }
                                globalMap[iY - 1, iX] = 'S';
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY - 1, iX] == 'G')
                            {
                                playerOnGold = true;
                                globalMap[iY, iX] = '.';
                                globalMap[iY - 1, iX] = 'S';
                                UpdateMap();
                                return true;
                            }
                        }
                    }
                }
            }
            //East
            else if (action == 2)
            {
                for (int iY = 0; iY < globalMap.GetLength(0); iY++)
                {
                    for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                    {
                        //Looks for 'S' (player) on the map
                        if (globalMap[iY, iX] == 'S')
                        {
                            //Checks for what should occur
                            if (globalMap[iY, iX + 1] == '#')
                            {
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY, iX + 1] == '.')
                            {
                                if (playerOnGold == true)
                                {
                                    globalMap[iY, iX] = 'G';
                                    playerOnGold = false;
                                }
                                else if (playerOnGold == false)
                                {
                                    globalMap[iY, iX] = '.';
                                }
                                globalMap[iY, iX + 1] = 'S';
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY, iX + 1] == 'G')
                            {
                                playerOnGold = true;
                                globalMap[iY, iX] = '.';
                                globalMap[iY, iX + 1] = 'S';
                                UpdateMap();
                                return true;
                            }
                        }
                    }
                }
            }
            //South
            else if (action == 3)
            {
                for (int iY = 0; iY < globalMap.GetLength(0); iY++)
                {
                    for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                    {
                        //Looks for 'S' (player) on the map
                        if (globalMap[iY, iX] == 'S')
                        {
                            //Checks for what should occur
                            if (globalMap[iY + 1, iX] == '#')
                            {
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY + 1, iX] == '.')
                            {
                                if (playerOnGold == true)
                                {
                                    globalMap[iY, iX] = 'G';
                                    playerOnGold = false;
                                }
                                else if (playerOnGold == false)
                                {
                                    globalMap[iY, iX] = '.';
                                }
                                globalMap[iY + 1, iX] = 'S';
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY + 1, iX ] == 'G')
                            {
                                playerOnGold = true;
                                globalMap[iY, iX] = '.';
                                globalMap[iY + 1, iX] = 'S';
                                UpdateMap();
                                return true;
                            }
                        }
                    }
                }
            }
            //West
            else if (action == 4)
            {
                for (int iY = 0; iY < globalMap.GetLength(0); iY++)
                {
                    for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                    {
                        //Looks for 'S' (player) on the map
                        if (globalMap[iY, iX] == 'S')
                        {
                            //Checks for what should occur
                            if (globalMap[iY, iX - 1] == '#')
                            {
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY, iX - 1] == '.')
                            {
                                if (playerOnGold == true)
                                {
                                    globalMap[iY, iX] = 'G';
                                    playerOnGold = false;
                                }
                                else if (playerOnGold == false)
                                {
                                    globalMap[iY, iX] = '.';
                                }
                                globalMap[iY, iX - 1] = 'S';
                                UpdateMap();
                                return true;
                            }
                            else if (globalMap[iY, iX - 1] == 'G')
                            {
                                playerOnGold = true;
                                globalMap[iY, iX] = '.';
                                globalMap[iY, iX - 1] = 'S';
                                UpdateMap();
                                return true;
                            }
                        }
                    }
                }
            }
            //player on gold
            else if (action == 5)
            {
                if (playerOnGold == true)
                {
                    playerOnGold = false;
                    playerGoldAmount++;
                    return true;
                }
                return false;
            }
            else if (action == 6)
            {

            }

            return false;
        }
        //WORK ON THIS STUFF vvv
        public void GetMonsters()
        {
            //CONTINUE WITH THIS AT SOME POINT
            int monsterIDAssigner = 0;
            for (int iY = 0; iY < globalMap.GetLength(0); iY++)
            {
                for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                {
                    //Looks for 'M' (monster) on the map
                    if (globalMap[iY, iX] == 'M')
                    {
                        monsterIDs.Add('M', monsterIDAssigner);
                    }
                }
            }
        }
        public void MonsterAI()
        {
            Random rnd = new Random();
            int direction;

            for (int iY = 0; iY < globalMap.GetLength(0); iY++)
            {
                for (int iX = 0; iX < globalMap.GetLength(1); iX++)
                {
                    //Looks for 'M' (monster) on the map
                    if (globalMap[iY, iX] == 'M')
                    {
                        direction = rnd.Next(1, 3);
                        //Checks for what should occur

                        if (direction == 0)
                        {

                        }
                        else if (direction == 1)
                        {

                        }
                        else if (direction == 2)
                        {

                        }
                        else if (direction == 3)
                        {

                        }

                        if (globalMap[iY - 1, iX] == '#')
                        {
                            UpdateMap();
                            break;
                        }
                        else if (globalMap[iY - 1, iX] == '.')
                        {
                            if (playerOnGold == true)
                            {
                                globalMap[iY, iX] = 'G';
                                playerOnGold = false;
                            }
                            else if (playerOnGold == false)
                            {
                                globalMap[iY, iX] = '.';
                            }
                            globalMap[iY - 1, iX] = 'S';
                            UpdateMap();
                            break;
                        }
                        else if (globalMap[iY - 1, iX] == 'G')
                        {
                            playerOnGold = true;
                            globalMap[iY, iX] = '.';
                            globalMap[iY - 1, iX] = 'S';
                            UpdateMap();
                            break;
                        }
                    }
                }
            }
        }

        //MY NEW METHODS ^^^

        /**
         * Returns a representation of the currently loaded map
         * before any move was made.
         */
        public char[][] GetOriginalMap()
        {
            char[][] map = new char[0][];

            // Your code here


            return map;
        }

        /*
         * Returns the current map state 
         * without altering it 
         */
        public char[][] GetCurrentMapState()
        {
            // the map should be map[y][x]
            char[][] map = new char[0][];

            // Your code here

            return map;
        }

        /**
         * Returns the current position of the player on the map
         * 
         * The first value is the x corrdinate and the second is the y coordinate on the map
         */
        public int[] GetPlayerPosition()
        {
            int[] position = { 0, 0 };

            // Your code here

            foreach (char potentialPos in globalMap)
            {
                if (potentialPos == 'S')
                {
                    //set pos & then return
                }
            }

            return position;
        }

        /**
        * Returns the next player action
        * 
        * This method does not alter any internal state
        */
        public int GetPlayerAction()
        {
            int action = 0;

            // Your code here
            action = (int)this.action;

            return action;
        }


        public bool GameIsRunning()
        {
            bool running = false;
            // Your code here 

            if (active == true)
            {
                running = true;
            }

            return running;
        }

        /**
         * Main method and Entry point to the program
         * ####
         * Do not change! 
        */
        static void Main(string[] args)
        {
            CMDCrawler crawler = new CMDCrawler();
            string input = string.Empty;
            Console.WriteLine("Welcome to the Commandline Dungeon!" +Environment.NewLine+ 
                "May your Quest be filled with riches!"+Environment.NewLine);
            
            // Loops through the input and determines when the game should quit
            while (crawler.active && crawler.action != PlayerActions.QUIT)
            {
                Console.WriteLine("Your Command: ");
                input = crawler.ReadUserInput();
                Console.WriteLine(Environment.NewLine);

                crawler.ProcessUserInput(input);
            
                crawler.GameLoop(crawler.active);
            }

            Console.WriteLine("See you again" +Environment.NewLine+ 
                "In the CMD Dungeon! ");


        }


    }
}
