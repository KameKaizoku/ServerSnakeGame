using System;
using System.Collections.Generic;

namespace SnakeGame
{
    // класс для генерации еды
    public class Food
    {

        public List<Position> food;

        public Food()
        {
            food = new List<Position>();
        }

        public void GenerateFood(Snake sn, int sizey, int sizex)
        {
            Random rnd = new Random();
            List<Position> positions = new List<Position>();
            
            for(int i =0;  i<sizey; i++)
                for (int j = 0; j < sizex; j++)
                {
                    positions.Add(new Position(i, j));
                }
            
            foreach (var el in food) //удляем из массива координаты уже занятые едой
            {
                positions.Remove(el);
            }
            
            foreach (var el in sn.body) //удляем из массива координаты уже занятые змеей
            {
                positions.Remove(el);
            }
            
            food.Add(positions[rnd.Next(0, positions.Count)]);
        }
    }
}