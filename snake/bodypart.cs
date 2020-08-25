using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class bodypart
    {
        //Properties
        public sbyte xcord { get; private set; }

        public sbyte ycord { get; private set; }

        //--------------------------------------------------------
        //Constructor
        public bodypart(sbyte xcord, sbyte ycord)
        {
            this.xcord = xcord;
            this.ycord = ycord;
        }
    }
}