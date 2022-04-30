using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace snake
{
    internal class Game
    {
        //private Varriables

        private Snake player;     //The snake
        private sbyte direction = 1;  //The direction where the snake is looking
        private bool isRunning = false;  //So that we can't start 2 games at once
        private Grid field;    //The Grid where we will draw
        private sbyte maxXsize;  //The max size on the x axis
        private sbyte maxYsize; //The max size on the y axis
        private sbyte[] foodCoord = new sbyte[2];  //The cord of the food
        private readonly Random rnd = new Random(); //Random
        private bool eaten = false;  //If we ate the food
        private Label scoreLabel; //The Label for the score
        private readonly Brush backgroundColor = Brushes.Gray; //The default background

        //Properties
        public int Score { get; private set; }

        //------------------------------------------------------------------
        //Constructor
        public Game(Grid field, Label scoreLabel)
        {
            this.field = field;
            maxXsize = (sbyte)field.ColumnDefinitions.Count;
            maxYsize = (sbyte)field.RowDefinitions.Count;
            this.Score = 0;
            SetupGrid();
            this.scoreLabel = scoreLabel;
        }

        //-----------------------------------------------------------------------
        //Spawns the food
        private void SpawnFood()
        {
            if (maxXsize * maxYsize == player.SnakeSize)
            {
                System.Windows.MessageBox.Show("Win!");
                return;
            }

            bool isFound;
            do
            {
                isFound = false;
                foodCoord[0] = (sbyte)rnd.Next(0, maxXsize);
                foodCoord[1] = (sbyte)rnd.Next(0, maxYsize);

                sbyte[,] cords = player.Getcords();
                //So that the food can't spawn into the snake
                for (int i = 0; i < player.SnakeSize; i++)
                {
                    if (foodCoord[0] == cords[i, 0] && foodCoord[1] == cords[i, 1])
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
            else if (Math.Abs(this.direction - (2 * (direction % 2) + 2)) == direction && player.SnakeSize > 1)
            {
                return;
            }

            this.direction = direction;
        }

        //-----------------------------------------------------------------------
        //Updates the grid
        private void Update(sbyte[] previousTailCoord)
        {
            (field.Children[player.Head.XCoord + player.Head.YCoord * maxXsize] as Rectangle).Fill = Brushes.Black;

            if (!eaten)
                (field.Children[previousTailCoord[0] + previousTailCoord[1] * maxXsize] as Rectangle).Fill = backgroundColor;

            Rectangle Foodsquare = field.Children[foodCoord[0] + foodCoord[1] * maxXsize] as Rectangle;
            Foodsquare.Fill = Brushes.Red; //Paints the place for the food
            scoreLabel.Content = Score; //Updates score
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
                    rect.Fill = backgroundColor;
                    Grid.SetColumn(rect, j);
                    Grid.SetRow(rect, i);
                    field.Children.Add(rect);
                }//for
            }//for
        }

        //-----------------------------------------------------------------
        //Paints all the Rectangles in the Field white
        //No longer used
        private void clear()
        {
            foreach (var item in field.Children)
            {
                Rectangle temp = item as Rectangle;
                temp.Fill = backgroundColor;
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

            player = new Snake(maxXsize, maxYsize);
            //The first food
            SpawnFood();

            while (!player.Dead)
            {
                sbyte[] previousTail = new sbyte[2] { player.Tail.XCoord, player.Tail.YCoord };

                //Moves the snake
                player.Move(direction, eaten);

                //Updates the game
                Update(previousTail);

                //Detects if it went to itself
                player.DetectCollision();
                //if it eaten a food
                eaten = player.Head.XCoord == foodCoord[0] && player.Head.YCoord == foodCoord[1];
                if (eaten)
                {
                    (field.Children[foodCoord[0] + foodCoord[1] * maxXsize] as Rectangle).Fill = Brushes.Black;

                    Score++;
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