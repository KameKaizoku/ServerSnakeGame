using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SnakeGame
{
    public class Snake
        {
            public List<Position> body { get; set; }
            public Direction direction = Direction.Up;
            public int turn { get; set; }
            private bool can_move = true;

            public Snake(int x, int y) //задаем начальные параметры
            {
                body = new List<Position>();
                Position tail = new Position(x, y +1);
                
                body.Add(tail);
                Position head = new Position(x, y);
                body.Add(head);
                turn = 1;
            }

            public bool Move()
            {
                Position NewHead = this.NextPosition();
                if (NewHead.Bump())
                {
                    body.Add(this.NextPosition());
                    body.Remove(body.First()); // удаление хвоста
                    can_move = true;
                    return true;
                }

                return false;
                
            }

            public Position NextPosition()
            {
                Position head = new Position(body.Last().x, body.Last().y);
                switch (direction) // высчитывание сл позиции головы исходя из направления
                {
                    case Direction.Down:
                        head.y += 1;
                        break;
                    case Direction.Up:
                        head.y -= 1;
                        break;
                    case Direction.Left:
                        head.x -= 1;
                        break;
                    case Direction.Right:
                        head.x += 1;
                        break;
                }
          
                    return head;
            }

            public void Rotate(string toDo)
            {
                if (can_move)
                {
                    switch (direction) // изменение напраления
                    {
                        case Direction.Up:
                        case Direction.Down:
                            if (toDo == "Left")
                                direction = Direction.Left;
                            else if (toDo == "Right")
                                direction = Direction.Right;
                            else return;
                            break;
                        case Direction.Left:
                        case Direction.Right:
                            if (toDo == "Top")
                                direction = Direction.Up;
                            else if (toDo == "Bottom")
                                direction = Direction.Down;
                            else return;
                            break;
                    }

                    can_move = false;
                }
            }

            public bool Bite(Position head) // проверка не укусила ли змея саму себя
            {
                for (int i = 0; i < body.Count - 1; i++)
                {
                    if (body[i].Collide(head))
                        return true;
                }

                return false;
            }

            public bool Eat(List<Position> food) // съела ли змея еду
            {
                Position head = NextPosition();
                foreach (var el in food)
                {
                    if (head.Collide(el)) // если съела
                    {
                        food.Remove(el); // удаляем еду из списка
                        body.Add(head); // добавдяем змее элемент
                        return true;
                    }
                }

                return false;
            }


        }

        public enum Direction //перечисление направлений
        {
            Left,
            Right,
            Up,
            Down
        }

       
    }
