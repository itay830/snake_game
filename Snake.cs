using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game
{
    class Snake
    {
        public int[] parts = new int[10];
        public int x;
        public int y;
        public char char_display;
        public string position;

        public Snake(int _x, int _y, string _position)
        {
            x = _x;
            y = _y;
            position = _position;
        }

        public void Spawn()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(char_display);
        }


    }
}
