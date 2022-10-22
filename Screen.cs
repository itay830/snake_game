using System;
using System.Threading;
using System.Threading.Tasks;

namespace snake_game
{
    static class Screen
    {
        public static int width = 80;
        public static int height = 40;


        public static void UpdateScreen()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"SCORE: {Game.score}");
            Console.SetCursorPosition(0, 1);
            Console.Write($"LENGTH: {Game.snakes.Count}");
            Console.SetCursorPosition(width / 2 - "USE THE ARROW KEYS TO MOVE!".Length / 2, 2);
            Console.WriteLine("USE THE ARROW KEYS TO MOVE!");

            for (int x = 4; x < width - 3; x++)
            {
                Console.SetCursorPosition(x, height - 3);
                Console.Write("═");
            }

            for (int x = 4; x < width - 3; x++)
            {
                Console.SetCursorPosition(x, 3);
                Console.Write("═");
            }

            for (int y = 4; y < height - 3; y++)
            {
                Console.SetCursorPosition(width - 3, y);
                Console.Write("║");
            }

            for (int y = 4; y < height - 3; y++)
            {
                Console.SetCursorPosition(3, y);
                Console.Write("║");
            }
            Console.SetCursorPosition(3, 3);
            Console.Write("╔");
            Console.SetCursorPosition(3, Screen.height - 3);
            Console.Write("╚");
            Console.SetCursorPosition(Screen.width - 3, 3);
            Console.Write("╗");
            Console.SetCursorPosition(Screen.width - 3, Screen.height - 3);
            Console.Write("╝");
            Console.SetCursorPosition(0, 0);
            Console.Write("");
        }



    }
}
