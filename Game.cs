using System;
using System.Collections.Generic;
using System.Threading;
using System.Media;

namespace snake_game
{
    class Game
    {
        static ConsoleKey key;
        static bool right = true; static bool left = false; static bool up = false; static bool down = false;
        public static List<Snake> snakes = new List<Snake>();
        static Random random = new Random();
        public static uint score = 0;

        //Made after the game - Code isn't optimal.
        public static char[,] skin_parts = new char[5, 3]
        {
                {'O', '╬', '╬'},
                {'■', '■', '■'},
                {'░', '▓', '▓'},
                {'¤', '▄', '▀'},
                {'O', 'X', 'Y'}
        };
        public static ConsoleColor[,] skin_colors = new ConsoleColor[5, 2]
        {
                { ConsoleColor.Red, ConsoleColor.White },
                {ConsoleColor.Blue, ConsoleColor.Yellow },
                {ConsoleColor.DarkRed, ConsoleColor.DarkBlue},
                {ConsoleColor.Green, ConsoleColor.Red},
                {ConsoleColor.Magenta, ConsoleColor.Cyan}
        };
        public static ushort skin = 0;


        


        static void Main(string[] args)
        {
            Console.Title = "Snake Game By Dr. G";

            SoundPlayer bg_music = new SoundPlayer("snake_bg_music.wav");
            SoundPlayer game_over_music = new SoundPlayer("game_over_music.wav");
            bg_music.PlayLooping();

            Snake snake = new Snake(Screen.width / 2, Screen.height / 2, "RIGHT");
            Snake snake2 = new Snake(Screen.width / 2 - 1, Screen.height / 2, "RIGHT");
            Snake snake3 = new Snake(Screen.width / 2 - 2, Screen.height / 2, "RIGHT");
            snakes.Add(snake);
            snakes.Add(snake2);
            snakes.Add(snake3);

            Food food = new Food('■');
            food.x = random.Next(5, Screen.width - 6);
            food.y = random.Next(4, Screen.height - 4);
            food.spawn();

            Thread keyInput = new Thread(Input);
            keyInput.Start();

            

            Screen.UpdateScreen();

            uint check = 0;
            
            do
            {
                Thread.Sleep(100);
                Console.SetWindowSize(Screen.width, Screen.height);

                for (int row = 4; row < Screen.height - 3; row++)
                {
                    Console.SetCursorPosition(4, row);
                    Console.Write(new string(' ', Screen.width - 7));
                }

                
                skin_changer();
                snake_drawer();
                food.spawn();

                if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && !left)
                {
                    right = true;
                    left = false;
                    up = false;
                    down = false;
                }
                else if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && !right)
                {
                    right = false;
                    left = true;
                    up = false;
                    down = false;
                }
                else if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W) && !down)
                {
                    right = false;
                    left = false;
                    up = true;
                    down = false;
                }
                else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && !up)
                {
                    right = false;
                    left = false;
                    up = false;
                    down = true;
                }
                else if (key == ConsoleKey.E)
                {
                    if (skin >= skin_parts.GetLength(0) - 1)
                    {
                        skin = Convert.ToUInt16(skin_parts.GetLength(0) - 1);
                    }
                    else
                    {
                        skin++;
                    }
                    key = ConsoleKey.N;

                }
                else if (key == ConsoleKey.Q)
                {
                    if (skin <= 0)
                    {
                        skin = 0;
                    }
                    else
                    {
                        skin--;
                    }
                    key = ConsoleKey.N;
                }

                if (right)
                {
                    snakes.Add(new Snake(snakes[snakes.Count - 1].x + 1, snakes[snakes.Count - 1].y, "RIGHT"));
                    snakes.RemoveAt(0);

                }
                else if (left)
                {
                    snakes.Add(new Snake(snakes[snakes.Count - 1].x - 1, snakes[snakes.Count - 1].y, "LEFT"));
                    snakes.RemoveAt(0);
                }
                else if (up)
                {
                    snakes.Add(new Snake(snakes[snakes.Count - 1].x, snakes[snakes.Count - 1].y - 1, "UP"));
                    snakes.RemoveAt(0);
                }
                else if (down)
                {
                    snakes.Add(new Snake(snakes[snakes.Count - 1].x, snakes[snakes.Count - 1].y + 1, "DOWN"));
                    snakes.RemoveAt(0);
                }


                if (snakes[snakes.Count - 1].x == food.x && snakes[snakes.Count - 1].y == food.y)
                {
                    snakes.Add(new Snake(snakes[snakes.Count - 1].x, snakes[snakes.Count - 1].y, "NULL"));
                    food.x = random.Next(5, Screen.width - 6);
                    food.y = random.Next(4, Screen.height - 4);
                    
                    score++;
                    Console.SetCursorPosition(7, 0);
                    Console.Write(score);
                    Console.SetCursorPosition(8, 1);
                    Console.Write(snakes.Count);

                    while (check != snakes.Count - 1) //fix
                    {
                        check = 0;
                        for (int i = 0; i < snakes.Count - 1; i++)
                        {
                            if (food.x != snakes[i].x && food.y != snakes[i].y)
                            {
                                check++;
                            }
                        }
                        food.x = random.Next(5, Screen.width - 6);
                        food.y = random.Next(4, Screen.height - 4);
                    }
                }
                for (int index = 0; index < snakes.Count - 3; index++)
                {
                    if (snakes[snakes.Count - 1].x == snakes[index].x && snakes[snakes.Count - 1].y == snakes[index].y)
                    {
                        key = ConsoleKey.Escape;
                    }
                }

                if (snakes[snakes.Count - 1].x <= 4 || snakes[snakes.Count - 1].x >= Screen.width - 4 || snakes[snakes.Count - 1].y >= Screen.height - 3 || snakes[snakes.Count - 1].y <= 3)
                {
                    key = ConsoleKey.Escape;
                }
                Console.SetCursorPosition(0, Screen.height - 2);

            } while (key != ConsoleKey.Escape);
            game_over_music.PlaySync();
            keyInput.Abort();
            Console.Clear();
            Console.SetCursorPosition(Screen.width / 2 - string.Format($"YOUR SCORE IS: {score}").Length / 2, Screen.height / 2);
            Console.Write($"YOUR SCORE IS: {score}");
            Console.SetCursorPosition(0, 0);
            Console.Write("Made by Itay Shinderman.\nPress any key to exit. . .");
        }

        static void Input()
        {
            do
            {
                key = Console.ReadKey(true).Key;
                
            } while (key != ConsoleKey.Escape);
        }

        static void snake_drawer()
        {
            


            for (int i = 0; i < snakes.Count - 1; i++)
            {
                Console.SetCursorPosition(snakes[i].x, snakes[i].y);
                Console.Write(snakes[i].char_display);
            }
        }

        static void skin_changer()
        {
            snakes[snakes.Count - 1].char_display = skin_parts[skin, 0];
            for (int i = 0; i < snakes.Count - 1; i++)
            {
                switch (snakes[i].position)
                {
                    case "RIGHT":
                    case "LEFT":
                        snakes[i].char_display = skin_parts[skin, 1];
                        break;
                    case "UP":
                    case "DOWN":
                        snakes[i].char_display = skin_parts[skin, 2];
                        break;
                    case "NULL":
                        snakes[i].char_display = '¤';
                        break;
                }
                
            }

            Console.SetCursorPosition(snakes[snakes.Count - 1].x, snakes[snakes.Count - 1].y);
            Console.ForegroundColor = skin_colors[skin, 0];
            Console.Write(snakes[snakes.Count - 1].char_display);
            Console.ForegroundColor = skin_colors[skin, 1];
        }

    }
}
