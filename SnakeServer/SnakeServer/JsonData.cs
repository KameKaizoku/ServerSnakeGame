using System.Collections.Generic;

namespace SnakeGame
{
    public class JsonData
    {
        public int turnNumber => Linker.Snake.turn;
        public int timeUntilNextTurnMilliseconds => Linker.gameConfig.timeUntilNextTurnMilliseconds;
        public GameBoardSize gameBoardSize{get; set;}
        public List<Position> snake{get; set;}
        public List<Position> food{get; set;}

    }
}