namespace SnakeGame
{
    public class GameBoardSize
    { 
        private int width;
        private int height;
        public int Width
        {
            get => width;
            set
            {
                if (value > 5)
                    width = value;
                else width = 5;
            }
            
        }

        public int Height
        {
            get => height;
            set
            {
                if (value > 5)
                    height = value;
                else height = 5;
            }
        }

    }
}