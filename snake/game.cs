using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace snake
{
    internal class game
    {
        //private Varriables

        private Snake Player;     //The snake
        private sbyte direction = 1;  //The direction where the snake is looking
        private bool isRunning = false;  //So that we can't start 2 games at once
        private Grid Field;    //The Grid where we will draw
        private sbyte maxXsize;  //The max size on the x axis
        private sbyte maxYsize; //The max size on the y axis
        private sbyte[] Foodcord = new sbyte[2];  //The cord of the food
        private readonly Random rnd = new Random(); //Random
        private bool eaten = false;  //If we ate the food

        //Properties
        public int score { get; private set; }

        //------------------------------------------------------------------
        //Constructor
        public game(Grid Field)
        {
            this.Field = Field;
            maxXsize = (sbyte)Field.ColumnDefinitions.Count;
            maxYsize = (sbyte)Field.RowDefinitions.Count;

            SetupGrid();
        }

        //-----------------------------------------------------------------------
        //Spawns the food
        private void SpawnFood()
        {
            bool isFound = false;
            do
            {
                Foodcord[0] = (sbyte)rnd.Next(0, maxXsize);
                Foodcord[1] = (sbyte)rnd.Next(0, maxYsize);

                sbyte[,] cords = Player.Getcords();
                //So that the food can't spawn into the snake
                for (int i = 0; i < Player.size; i++)
                {
                    if (Foodcord[0] == cords[i, 0] && Foodcord[1] == cords[i, 1])
                    {
                        isFound = true;
                        break;
                    }//if
                }//for
            } while (isFound);
        }

        //-------------------------------------------------------------
        //Changes the direction
        public void ChangeDirection(sbyte direction)
        {
            if (!isRunning)
            {
                this.run();
            }
            else if (Math.Abs(this.direction - (2 * (direction % 2) + 2)) == direction && Player.size > 1)
            {
                return;
            }

            this.direction = direction;
        }

        //-----------------------------------------------------------------------
        //Updates the grid
        private void Update()
        {
            sbyte[,] cords = Player.Getcords();

            clear();

            for (int i = 0; i < Player.size; i++)
            {
                Rectangle temp = Field.Children[cords[i, 0] + cords[i, 1] * maxXsize] as Rectangle;
                temp.Fill = Brushes.Black;
            }//for
            Rectangle Foodsquare = Field.Children[Foodcord[0] + Foodcord[1] * maxXsize] as Rectangle;
            Foodsquare.Fill = Brushes.Red;
        }

        //----------------------------------------------------------------------
        //Sets up the Grid
        private void SetupGrid()
        {
            for (int i = 0; i < maxYsize; i++)
            {
                for (int j = 0; j < maxXsize; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = Brushes.White;
                    Grid.SetColumn(rect, j);
                    Grid.SetRow(rect, i);
                    Field.Children.Add(rect);
                }//for
            }//for
        }

        //-----------------------------------------------------------------
        //Paints all the Rectangles in the Field white
        private void clear()
        {
            foreach (var item in Field.Children)
            {
                Rectangle temp = item as Rectangle;
                temp.Fill = Brushes.White;
            }//forea
        }

        //-----------------------------------------------------------------------
        //Starts the game
        public async void run()
        {
            //So that we can't start multiple games
            if (isRunning)
            {
                return;
            }//if
            isRunning = true;

            Player = new Snake(maxXsize, maxYsize);
            //The first food
            SpawnFood();

            while (!Player.Dead)
            {
                //Updates the game
                Update();
                //Moves the snake
                Player.Move(direction, eaten);
                //Detects if it went to itself
                Player.DetectCollision();
                //if it eaten a food
                eaten = Player.head.xcord == Foodcord[0] && Player.head.ycord == Foodcord[1];
                if (eaten)
                {
                    //spawn new food
                    SpawnFood();
                }//if

                //A bit of delay
                await Task.Delay(150);
            }//while
            isRunning = false;
        }
    }
}