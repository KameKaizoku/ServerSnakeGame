using System.IO;
using Newtonsoft.Json;

namespace SnakeGame
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Collide(Position b) // метод для сравнения элементов класса
        {
            return (this.x == b.x && this.y == b.y) ? true : false;
        }

        public bool Bump() // метод для проверкии, что змея вышла за пределы поля
        {
            var jsonDate = File.ReadAllText("GameState.json");
            var gameBoard = JsonConvert.DeserializeObject<GameBoardSize>(jsonDate);
            return 0 <= x && x < gameBoard.Width && 0 <= y && y < gameBoard.Height;
        }
    }
}