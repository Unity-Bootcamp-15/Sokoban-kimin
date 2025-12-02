using System;
using System.Runtime.Intrinsics.X86;



public class Program
{
    static public int BoxCnt = 0;
    static public int GameCnt = 0;
    static public int mapMaxX = 18;
    static public int mapMaxY = 10;

    // 맵 
    static public int[][] wallBase = {
            [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1],
            [1,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,2,0,1,0,1,0,0,0,0,0,0,1],
            [1,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,3,0,0,0,0,0,0,0,0,6,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1],
            [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
            };
    static int BoxCount()
    {
       

        BoxCnt = 0;
        for (int x = 0; x < mapMaxX; x++)
        {
            for (int y = 0; y < mapMaxY; y++)
            {
                if (wallBase[y][x] == 2)
                {
                    BoxCnt++;
                }
            }
        }

        return BoxCnt;
    }
    public static void main()
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

      
        int mapMinX = 1;
        int mapMinY = 1;

        string player = "P";
        string wallSpace = " ";
        string WallString = "W";
        string movingBox = "b";
        string BoxOnGoal = "B";
        string finishBox = "F";
        string potal1 = "O";
        string potal2 = "0";


        

        int playerX = 5;
        int playerY = 5;

        GameCnt = BoxCount();

        while (true)
        {
            Console.SetCursorPosition(20, 1);
            Console.WriteLine($"{GameCnt}개 남음");

            int newPlayerX = playerX;
            int newPlayerY = playerY;


            makeWall();
            // 키보드 입력
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(player);

            
            ConsoleKey keyInfo = Console.ReadKey().Key;

            

            switch (keyInfo) 
            {
                case ConsoleKey.UpArrow:
                    newPlayerY = Math.Max(mapMinY, newPlayerY -1);
                    break;
                case ConsoleKey.DownArrow:
                    newPlayerY = Math.Min(mapMaxY -2, newPlayerY + 1);
                    break;
                case ConsoleKey.RightArrow:
                    newPlayerX = Math.Min(mapMaxX -2, newPlayerX + 1);
                    break;
                case ConsoleKey.LeftArrow:
                    newPlayerX = Math.Max(mapMinX, newPlayerX - 1);
                    break;
            }
            

            // 박스와 닿았나  // 어디 방향으로 밀지 
            bool isBoxOn = wallBase[newPlayerY][newPlayerX] == 2;
            int newBoxX = newPlayerX;
            int newBoxY = newPlayerY;

            

            if (isBoxOn)
            {
                switch (keyInfo)
                { 
                    case ConsoleKey.UpArrow:
                        newBoxY = Math.Max(mapMinY, newBoxY - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        newBoxY = Math.Min(mapMaxY, newBoxY + 1);
                        break;
                    case ConsoleKey.RightArrow:
                        newBoxX = Math.Max(mapMinX, newBoxX + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        newBoxX = Math.Min(mapMaxX, newBoxX - 1);
                        break;
                }

                bool isWallBoxOn = wallBase[newBoxY][newBoxX] == 1;
                bool isBoxBoxOn = wallBase[newBoxY][newBoxX] == 2;
                bool isBoxFinishOn = wallBase[newBoxY][newBoxX] == 3;


                if (isWallBoxOn|| isBoxBoxOn)
                    continue;

               

                wallBase[newPlayerY][newPlayerX] = 0;
                wallBase[newBoxY][newBoxX] = 2;

                if (isBoxFinishOn)
                {
                    wallBase[newBoxY][newBoxX] = 4;

                    GameCnt--;
                    
                    if (GameCnt == 0)
                        break;
                }

            }

            

            // 벽과 닿았나
            bool isWallOn = wallBase[newPlayerY][newPlayerX] == 1;
            if (isWallOn)
                continue;

            // 포탈 5번과 닿았나
            bool isPotal = wallBase[newPlayerY][newPlayerX] == 5;
            bool isPotal2 = wallBase[newPlayerY][newPlayerX] == 6;

            if (isPotal)
            {
                newPlayerX = 15;
                newPlayerY = 7;
            }else if (isPotal2)
            {
                newPlayerX = 2;
                newPlayerY = 2;
            }
            playerX = newPlayerX;
            playerY = newPlayerY;

          
        }

        // Box 수세기
        
        void makeWall()
        {
            for (int x = 0; x < mapMaxX; x++)
            {
                for (int y = 0; y < mapMaxY; y++)
                {
                    Console.SetCursorPosition(x, y);

                    if (wallBase[y][x] == 0)
                        Console.Write(wallSpace);
                    else if (wallBase[y][x] == 3)
                        Console.Write(finishBox);
                    else if(wallBase[y][x] == 1)
                        Console.Write(WallString);
                    else if (wallBase[y][x] == 2)
                        Console.Write(movingBox);
                    else if (wallBase[y][x] == 4)
                        Console.Write(BoxOnGoal);
                    else if (wallBase[y][x] == 5)
                        Console.Write(potal1);
                    else if (wallBase[y][x] == 6)
                        Console.Write(potal2);
                }
            }
        }




    }
}