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
            string wallSpace = " ";
            string WallString = "□";

            string movingBox = "☆";
            int maxWallX = 18;
            int maxWallY = 10;
            int[][] wallBase = { 
            [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,2,0,1,0,1,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
            };

       

            while (true)
            {
                PlayerMove();
            }

            void makeWall()
            {
                for (int x = 0; x < maxWallX; x++)
                {
                    for (int y = 0; y < maxWallY; y++)
                    {
                        Console.SetCursorPosition(x, y);

                        if (wallBase[y][x] == 0)
                            Console.Write(wallSpace);
                        else if(wallBase[y][x] == 2)
                            Console.Write(movingBox);

                            Console.Write(WallString);
                    }
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
                        playerY = Math.Max(playerY++, -1);
                        break;
                    case ConsoleKey.UpArrow:
                        if (wallBase[playerY-1][playerX] != 1)
                            playerY--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (wallBase[playerY][playerX+2] != 1)
                            playerX +=2;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (wallBase[playerY][playerX-2] != 1)
                            playerX -=2;
                        break;
                }
            }


        }
    }
}
