using System.Numerics;

namespace sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 게임 초기화 
            Console.ResetColor();
            // 색깔 조정
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.Green;
            // 제목 수정
            Console.Title = "My Sokoban,";
            // 커서 숨김
            Console.CursorVisible = false;
            Console.Clear();
            int playerX = 0;
            int playerY = 0;
            string player = "A";
            Console.WriteLine(Console.ReadLine());
            Console.Clear();

            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                ConsoleKey input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        playerY++;
                        break;
                    case ConsoleKey.UpArrow:
                        playerY--;
                        break;
                    case ConsoleKey.RightArrow:
                        playerX++;
                        break;
                    case ConsoleKey.LeftArrow:
                        playerX++;
                        break;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                ConsoleKey input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        playerY++;
                        break;
                    case ConsoleKey.UpArrow:
                        playerY--;
                        break;
                    case ConsoleKey.RightArrow:
                        playerX++;
                        break;
                    case ConsoleKey.LeftArrow:
                        playerX++;
                        break;
                }
            }

        }
    }
}
