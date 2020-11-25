using System;
using System.Linq;

namespace SnakeGame
{
   //класс поле - для отладки
           public class Field
           {
               public static int size = 11;
               private char[,] field;
   
               public Field()
               {
                   this.field = new char[size, size];
               }
   
               public void Render(Snake sn, Food food)
               {
                   for (int i = 0; i < size; i++)
                   {
                       for (int j = 0; j < size; j++)
                       {
                           field[i, j] = ' ';
                           for (int k = 0; k < sn.body.Count(); k++)
                           {
                               if (j == sn.body[k].x && sn.body[k].y == i)
                                   field[i, j] = '*';
                           }
   
                           for (int k = 0; k < food.food.Count(); k++)
                           {
                               if (j == food.food[k].x && food.food[k].y == i)
                                   field[i, j] = '@';
                           }
                       }
   
                   }
   
                   print_field();
   
               }
   
               public void print_field()
               {
                   for (int i = 0; i < size; i++)
                   {
                       for (int j = 0; j < size; j++)
                       {
                           Console.Write($"{field[i, j]} ");
                       }
   
                       Console.WriteLine();
                   }
               }
           }
}