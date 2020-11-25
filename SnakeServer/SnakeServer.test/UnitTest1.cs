using System.Collections.Generic;
using System.Linq;
using SnakeGame;
using NUnit.Framework;

namespace SnakeServer.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGameSize()
        {
            var wrong_test = $"Wrong game size";
            GameBoardSize gameBoardSize = new GameBoardSize();
            
            //проверяем возможные значения поля
            gameBoardSize.Height = 11;
            gameBoardSize.Width = 12;
            Assert.AreEqual(11, gameBoardSize.Height, wrong_test);
            Assert.AreEqual(12, gameBoardSize.Width, wrong_test);
            
            //проверяем недопустимые значения поля
            gameBoardSize.Height = 0;
            gameBoardSize.Width = 0;
            Assert.AreEqual(5, gameBoardSize.Height, wrong_test); 
            //при вводе недопустимого значения поля,
            //будет установлено 5
            Assert.AreEqual(5, gameBoardSize.Width, wrong_test);
        }
        
        
        [Test]
        public void TestPosibleRotation()
        {
            SnakeGame.Snake snake_test = new SnakeGame.Snake(5, 5);//создаем экземпляр класса змеи
            var wrong_test = $"Wrong rotation ";

            //проверка на разрешенные повороты(90')
            snake_test.Rotate("Left");
            snake_test.Move(); // вызов функции движения необходим для того чтобы можно было поворачиваться в новвую сторрну
            Assert.AreEqual(Direction.Left, snake_test.direction, wrong_test);
            snake_test.Rotate("Bottom");
            snake_test.Move();
            Assert.AreEqual(SnakeGame.Direction.Down, snake_test.direction,  wrong_test);
            snake_test.Rotate("Right");
            snake_test.Move();
            Assert.AreEqual(SnakeGame.Direction.Right, snake_test.direction,  wrong_test);
            snake_test.Rotate("Top");
            snake_test.Move();
            Assert.AreEqual(SnakeGame.Direction.Up, snake_test.direction,  wrong_test);
            
        }

        [Test]
        public void TestForbidenRotation() // проверка "неразрешенных поворотов"
        {
            SnakeGame.Snake snake_test = new SnakeGame.Snake(5, 5);//создаем экземпляр класса змеи
            
            var wrong_test = $"Wrong rotation ";
            //проверка не изменяет ли змея положение при получении того же напрявление, что уже есть
            snake_test.Rotate("Top");
            
            Assert.AreEqual(SnakeGame.Direction.Up, snake_test.direction, wrong_test);
            snake_test.Rotate("Bottom");
            snake_test.Move();
            Assert.AreEqual(SnakeGame.Direction.Up, snake_test.direction,  wrong_test);
            snake_test.direction = SnakeGame.Direction.Left;
            snake_test.Rotate("Right");
            snake_test.Move();
            Assert.AreEqual(SnakeGame.Direction.Left, snake_test.direction,  wrong_test);
        }

        [Test]
        public void TestBump() // проверка врезалась ли змейка в стену
        {
            SnakeGame.Snake snake_test = new SnakeGame.Snake(5, -1); // вышел по y вверх
            bool in_field =  snake_test.body.Last().Bump();
            var wrong_test = "wrong in field";
            Assert.IsFalse(in_field,  wrong_test);
            
            snake_test = new SnakeGame.Snake(5, 12); // вышел по y вниз
            in_field =  snake_test.body.Last().Bump();
            Assert.IsFalse(in_field,  wrong_test);
            
            snake_test = new SnakeGame.Snake(-1, 3); // вышел по x влево
            in_field =  snake_test.body.Last().Bump();
            Assert.IsFalse( in_field,  wrong_test);
            
            snake_test = new SnakeGame.Snake(12, 3); // вышел по x вправо
            in_field =  snake_test.body.Last().Bump();
            Assert.IsFalse( in_field,  wrong_test);
        }

        [Test]
        public void TestEat() // проверка ест ли змея еду
        {
            SnakeGame.Snake snake_test = new SnakeGame.Snake(5, 5);
            var wrong_test = "wrong eating method";
                
            Food food = new Food(); // сами задаем координаты еды
            food.food.Add(new Position(5, 4));
            snake_test.Eat(food.food);
            Assert.AreEqual(3, snake_test.body.Count, wrong_test);
        }

        [Test]
        public void TestBite() // проверка функции кусает ли змея себя
        {
            SnakeGame.Snake snake_test = new SnakeGame.Snake(5, 5);
            snake_test.body = new List<Position> //задаем массив элементов змейки
            {
                new Position(5, 7),
                new Position(5, 6),
                new Position(5, 5),
                new Position(6, 5),
                new Position(6, 6)
            };
            snake_test.direction = SnakeGame.Direction.Left; 
            snake_test.Move();
           bool was_bitten  = snake_test.Bite(snake_test.body.Last());
           Assert.IsTrue(was_bitten, "method Bite() works not correctly");
        }
        
    }
}