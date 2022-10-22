using snake_game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



internal class Food
{
    public int x;
    public int y;
    char char_display;

    public Food(char _char_display)
    {
        char_display = _char_display;
    }

    public void spawn()
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(char_display);
        Console.ForegroundColor = ConsoleColor.White;

    }


}

