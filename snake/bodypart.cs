namespace snake
{
    internal struct BodyPart
    {
        //Properties
        public sbyte XCoord { get; private set; }

        public sbyte YCoord { get; private set; }

        //--------------------------------------------------------
        //Constructor
        public BodyPart(sbyte XCoord, sbyte YCoord)
        {
            this.XCoord = XCoord;
            this.YCoord = YCoord;
        }
    }
}