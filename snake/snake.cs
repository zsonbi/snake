using System.Collections.Generic;
using System.Linq;

namespace snake
{
    internal class Snake
    {
        //Properties

        public int SnakeSize { get; private set; }
        public bool Dead { get; private set; }

        //----------------------------------------------------------------
        //Gets the head
        public BodyPart Head { get => bodyParts[0]; }

        public BodyPart Tail { get => bodyParts.Last(); }

        //Private Varriables

        private List<BodyPart> bodyParts = new List<BodyPart>();

        private sbyte maxXsize;
        private sbyte maxYsize;

        //----------------------------------------------------------------
        //Constructor
        public Snake(sbyte maxXsize, sbyte maxYsize, int initialsize = 1)
        {
            SnakeSize = initialsize;
            Dead = false;
            this.maxXsize = maxXsize;
            this.maxYsize = maxYsize;
            bodyParts.Add(new BodyPart((sbyte)(maxXsize >> 1), (sbyte)(maxYsize >> 1)));
        }

        //--------------------------------------------------------------
        //Movement of the snake
        public void Move(sbyte direction, bool eaten = false)
        {
            sbyte newXCoord;
            sbyte newYCoord;
            if ((direction % 2) == 0)
            {
                newXCoord = (sbyte)(Head.XCoord + direction - 1);
                newYCoord = Head.YCoord;
            }//if
            else
            {
                newXCoord = Head.XCoord;
                newYCoord = (sbyte)(Head.YCoord + direction - 2);
            }//else

            if (newXCoord == -1 || newXCoord == maxXsize || newYCoord == -1 || newYCoord == maxYsize)
            {
                Dead = true;
                return;
            }

            bodyParts.Insert(0, new BodyPart(newXCoord, newYCoord));
            if (!eaten)
            {
                bodyParts.Remove(bodyParts.Last());
            }//if
            else
            {
                SnakeSize++;
            }//else
        }

        //--------------------------------------------------------
        //Detects collision with itself
        public void DetectCollision()
        {
            sbyte[,] cords = Getcords();
            for (int i = 1; i < SnakeSize; i++)
            {
                if (Head.XCoord == cords[i, 0] && Head.YCoord == cords[i, 1])
                {
                    this.Dead = true;
                    return;
                }//if
            }//for
        }

        //------------------------------------------------------------------
        //Get the cords of all the bodyparts
        public sbyte[,] Getcords()
        {
            sbyte[,] output = new sbyte[SnakeSize, 2];
            for (int i = 0; i < SnakeSize; i++)
            {
                output[i, 0] = bodyParts[i].XCoord;
                output[i, 1] = bodyParts[i].YCoord;
            }//for
            return output;
        }

        //----------------------------------------------
        //Kill the snake
        public void Die()
        {
            this.Dead = true;
        }
    }
}