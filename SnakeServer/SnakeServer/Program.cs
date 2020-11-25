using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SnakeGame.Models;
using Timer = System.Threading.Timer;

namespace SnakeGame
{
    /// <summary>
    /// Этот класс необходим для связки контроллера, json и игры
    /// </summary>
    public static class Linker
    {
        public static JsonData jsonData;
        public static Snake Snake;
        public static GameConfig gameConfig;

    }
    
    public class Program
    {
        private static Timer _time;
        public static async void Game()
        {
            //объявление переменных 
            var jsonDate = File.ReadAllText("GameState.json");
            var config = JsonConvert.DeserializeObject<GameConfig>(jsonDate);
            Field game = new Field();
            Snake snake = new Snake(config.Height/2, config.Width/2);
            Food food = new Food();
            food.GenerateFood(snake, config.Height, config.Width);
            game.Render(snake, food);
            string enter = Console.ReadLine();
            
            Linker.jsonData = new JsonData();
            Linker.Snake = snake;
            Linker.jsonData.food = food.food;
            Linker.jsonData.snake = snake.body;
            Linker.jsonData.gameBoardSize = new GameBoardSize();
            Linker.jsonData.gameBoardSize.Height = config.Height;
            Linker.jsonData.gameBoardSize.Width = config.Width;
            Linker.gameConfig = config;
            
            await Task.Run(() =>
            {
                _time = new Timer (Loop, null, 0, config.timeUntilNextTurnMilliseconds);

            });
            
            void Loop(object obj)
            {
                //проверка на конец игры
                if (snake.Bite(snake.body.Last()))
                {
                    Console.WriteLine("U lose");
                    snake= new Snake(config.Height/2, config.Width/2);
                    Linker.Snake = snake;
                    Linker.jsonData.snake = snake.body;
                }
                else if (snake.Eat(food.food)) // съела ли змея еду
                {
                    snake.turn++;
                    food.GenerateFood(snake, config.Height, config.Width);
                }
                else // автоматическое перемещение
                {
                    if (snake.Move())
                        snake.turn++;
                    else
                    {
                        Console.WriteLine("U lose");
                        snake= new Snake(config.Height/2, config.Width/2);
                        Linker.Snake = snake;
                        Linker.jsonData.snake = snake.body;
                    }
                }
                    
                game.Render(snake, food);
            }
        }
        public static void Main(string[] args)
        {
            Game();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}