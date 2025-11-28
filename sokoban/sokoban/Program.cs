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
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Green;
            // 제목 수정
            Console.Title = "My Sokoban,";
            // 커서 숨김
            Console.CursorVisible = false;

            int playerX = 2;
            int playerY = 2;

            string player = "■";
            string wall = "□";
            while (true)
            {
                PlayerMove();
            }
            void makeWall()
            {
                for (int i = 0; i < 10; i++) 
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(wall);
                    }
                    Console.WriteLine();
                }
            }

            void PlayerMove()
            {
                Console.Clear();

                makeWall();

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);


                ConsoleKey input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        if (playerY < 9)
                            playerY++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (playerY != 0)
                            playerY--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerX < 18)
                            playerX +=2;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerX != 0)
                            playerX -=2;
                        break;
                }
            }


        }
    }
}
