using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class Snake
    {
        //Properties

        public int size { get; private set; }
        public bool Dead { get; private set; }

        //Private Varriables

        private List<bodypart> bodyparts = new List<bodypart>();
        private sbyte maxXsize;
        private sbyte maxYsize;

        //----------------------------------------------------------------
        //Constructor
        public Snake(sbyte maxXsize, sbyte maxYsize, int initialsize = 1)
        {
            size = initialsize;
            Dead = false;
            this.maxXsize = maxXsize;
            this.maxYsize = maxYsize;
            bodyparts.Add(new bodypart((sbyte)(maxXsize / 2), (sbyte)(maxYsize / 2)));
        }

        //----------------------------------------------------------------
        //Gets the head
        public bodypart head { get => bodyparts[0]; }

        //--------------------------------------------------------------
        //Movement of the snake
        public void Move(sbyte direction, bool eaten = false)
        {
            sbyte newxcord;
            sbyte newycord;
            if (direction % 2 == 0)
            {
                newxcord = (sbyte)(head.xcord + direction - 1);
                newycord = head.ycord;
            }//if
            else
            {
                newxcord = head.xcord;
                newycord = (sbyte)(head.ycord + direction - 2);
            }//else

            if (newxcord == -1 || newxcord == maxXsize || newycord == -1 || newycord == maxYsize)
            {
                Dead = true;
                return;
            }

            bodypart temp = new bodypart(newxcord, newycord);

            bodyparts.Insert(0, temp);

            if (!eaten)
            {
                bodyparts.Remove(bodyparts.Last());
            }//if
            else
            {
                size++;
            }//else
        }

        //--------------------------------------------------------
        //Detects collision with itself
        public void DetectCollision()
        {
            sbyte[,] cords = Getcords();
            for (int i = 1; i < size; i++)
            {
                if (head.xcord == cords[i, 0] && head.ycord == cords[i, 1])
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
            sbyte[,] output = new sbyte[size, 2];
            for (int i = 0; i < size; i++)
            {
                output[i, 0] = bodyparts[i].xcord;
                output[i, 1] = bodyparts[i].ycord;
            }//for
            return output;
        }
    }
}