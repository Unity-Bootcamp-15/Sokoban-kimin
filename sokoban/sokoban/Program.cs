using System.Numerics;

namespace sokoban
{

    class SokoBanClass
    {
        bool[] isBool = { true, false, true };

        bool isFinish = true;

       public static  int playerX = 2;
       public static  int playerY = 2;

        string player = "P";
        string wallSpace = " ";
        string WallString = "W";
        string movingBox = "b";
        string BoxOnGoal = "B";
        string finishBox = "F";


        int maxWallX = 18;
        int maxWallY = 10;
        int BoxCnt = 0;
        int GameCnt;

        int newPlayerX = playerX;
        int newPlayerY = playerY;

        // 맵 
        int[][] wallBase = {
            [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1],
            [1,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,2,0,1,0,1,0,0,0,0,0,0,1],
            [1,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,1],
            [1,0,0,3,0,0,3,0,0,0,0,0,0,0,0,0,0,1],
            [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1],
            [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
            };
        
        
        public enum ObjectType
        {
            SPACE = 0,
            WALL = 1,
            STAR = 2,
            FINISH = 3,
            LOOKSTAR = 4,
        }


        public enum PlayerMoveNum
        {
            UP = 0,
            DOWN = 1,
            RIGHT = 2,
            LEFT = 3,
            STOP = 4
        }
        // 게임 플레이  모든 기능을 사용 
        public void PlaySokoBan()
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



            while (isFinish)
            {
                GameCnt = BoxCount();
                PlayerMove();

                if (BoxCount() == 0)
                {
                    isFinish = false;
                }
            }


            while (true)
            {
                Console.Clear();
                Console.WriteLine("클리어");
                ConsoleKey input = Console.ReadKey().Key;
            }

        }


        // 박스 골 박스 정적으로 만들기 
        bool Finish(int x, int y)
        {

            if (wallBase[y][x] == (int)ObjectType.WALL)
            {
                return true;
            }

            return false;
        }
        // 벽만들기 
        void makeWall()
        {
            for (int x = 0; x < maxWallX; x++)
            {
                for (int y = 0; y < maxWallY; y++)
                {
                    Console.SetCursorPosition(x, y);

                    if (wallBase[y][x] == 0)
                        Console.Write(wallSpace);
                    else if (wallBase[y][x] == 2)
                        Console.Write(movingBox);
                    else if (wallBase[y][x] == 3)
                    {
                        Console.Write(finishBox);
                    }
                    else if (wallBase[y][x] == 4)
                    {
                        Console.Write(BoxOnGoal);
                    }
                    Console.Write(WallString);

                    Console.SetCursorPosition(20, 1);
                    Console.Write($"{BoxCnt}");
                }
            }
        }
        // Box 수세기 
        int BoxCount()
        {
            BoxCnt = 0;
            for (int x = 0; x < maxWallX; x++)
            {
                for (int y = 0; y < maxWallY; y++)
                {
                    if (wallBase[y][x] == 2)
                    {
                        BoxCnt++;
                    }
                }
            }

            return BoxCnt;
        }
        // 이동방향키 누를시 이동 
        void PlayerMoveCheck(int X, int Y, PlayerMoveNum pmc)
        {
            switch (pmc)
            {
                case PlayerMoveNum.UP:
                    playerY = wallBase[playerY - 1][playerX] != (int)ObjectType.WALL ? playerY - 1 : playerY;
                    break;
                case PlayerMoveNum.DOWN:
                    playerY = wallBase[playerY + 1][playerX] != (int)ObjectType.WALL ? playerY + 1 : playerY;
                    break;
                case PlayerMoveNum.RIGHT:
                    playerX = wallBase[playerY][playerX + 1] != (int)ObjectType.WALL ? playerX + 1 : playerX;
                    break;
                case PlayerMoveNum.LEFT:
                    playerX = wallBase[playerY][playerX - 1] != (int)ObjectType.WALL ? playerX - 1 : playerX;
                    break;
                case PlayerMoveNum.STOP:
                    break;
            }
        }

        // 박스가 골에 들어갔을시 // 박스가 박스와 닿았는지// 박스가 벽과 닿았는지
        void BoxMoveCheck(PlayerMoveNum pmc)
        {
            switch (pmc)
            {
                case PlayerMoveNum.UP:

                    // 골대에 들어갔다면 
                    if (wallBase[playerY - 1][playerX] == (int)ObjectType.STAR &&
                       wallBase[playerY - 2][playerX] == (int)ObjectType.FINISH)
                    {
                        wallBase[playerY - 1][playerX] = (int)ObjectType.SPACE;
                        wallBase[playerY - 2][playerX] = (int)ObjectType.LOOKSTAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.UP);
                    }
                    // 골대에서 나온다면 
                    if (wallBase[playerY - 1][playerX] == (int)ObjectType.LOOKSTAR &&
                        wallBase[playerY-2][playerX] != (int)ObjectType.WALL)
                    {
                        wallBase[playerY - 1][playerX] = (int)ObjectType.FINISH;
                        wallBase[playerY - 2][playerX] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.UP);
                    }

                    if (wallBase[playerY - 1][playerX] == (int)ObjectType.STAR &&
                       (wallBase[playerY - 2][playerX] == (int)ObjectType.STAR ||
                        wallBase[playerY - 2][playerX] == (int)ObjectType.WALL)
                        )
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.STOP);

                    }
                    else if (wallBase[playerY - 1][playerX] == (int)ObjectType.STAR)
                    {
                        wallBase[playerY - 1][playerX] = (int)ObjectType.SPACE;
                        wallBase[playerY - 2][playerX] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.UP);
                    }
                    else
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.UP);
                    }



                    break;
                case PlayerMoveNum.DOWN:
                    if (wallBase[playerY + 1][playerX] == (int)ObjectType.STAR &&
                       wallBase[playerY + 2][playerX] == (int)ObjectType.FINISH)
                    {

                        wallBase[playerY + 1][playerX] = (int)ObjectType.SPACE;
                        wallBase[playerY + 2][playerX] = (int)ObjectType.LOOKSTAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.DOWN);

                        break;
                    }

                    // 골대에서 나온다면 
                    if (wallBase[playerY + 1][playerX] == (int)ObjectType.LOOKSTAR &&
                        wallBase[playerY+2][playerX] != (int)ObjectType.WALL)
                    {
                        wallBase[playerY + 1][playerX] = (int)ObjectType.FINISH;
                        wallBase[playerY + 2][playerX] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.DOWN);
                    }
                    if (wallBase[playerY + 1][playerX] == (int)ObjectType.STAR &&
                       wallBase[playerY + 2][playerX] == (int)ObjectType.FINISH)
                    {
                        wallBase[playerY + 1][playerX] = (int)ObjectType.SPACE;
                        wallBase[playerY + 2][playerX] = (int)ObjectType.LOOKSTAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.UP);

                        break;
                    }
                    if (wallBase[playerY + 1][playerX] == (int)ObjectType.STAR &&
                       (wallBase[playerY + 2][playerX] == (int)ObjectType.STAR ||
                        wallBase[playerY + 2][playerX] == (int)ObjectType.WALL)
                        )
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.STOP);

                    }
                    else if (wallBase[playerY + 1][playerX] == (int)ObjectType.STAR)
                    {
                        wallBase[playerY + 1][playerX] = (int)ObjectType.SPACE;
                        wallBase[playerY + 2][playerX] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.DOWN);
                    }
                    else
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.DOWN);
                    }
                    break;

                case PlayerMoveNum.RIGHT:

                    if (wallBase[playerY][playerX + 1] == (int)ObjectType.STAR &&
                       wallBase[playerY][playerX + 2] == (int)ObjectType.FINISH)
                    {

                        wallBase[playerY][playerX + 1] = (int)ObjectType.SPACE;
                        wallBase[playerY][playerX + 2] = (int)ObjectType.LOOKSTAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.RIGHT);

                        break;
                    }

                    // 골대에서 나온다면 
                    if (wallBase[playerY][playerX+1] == (int)ObjectType.LOOKSTAR &&
                        wallBase[playerY][playerX + 2] != (int)ObjectType.WALL)
                    {
                        wallBase[playerY][playerX+1] = (int)ObjectType.FINISH;
                        wallBase[playerY][playerX+2] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.RIGHT);
                    }
                    if (wallBase[playerY][playerX + 1] == (int)ObjectType.STAR &&
                       (wallBase[playerY][playerX + 2] == (int)ObjectType.STAR ||
                        wallBase[playerY][playerX + 2] == (int)ObjectType.WALL)
                        )
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.STOP);

                    }
                    else if (wallBase[playerY][playerX + 1] == (int)ObjectType.STAR)
                    {
                        wallBase[playerY][playerX + 1] = (int)ObjectType.SPACE;
                        wallBase[playerY][playerX + 2] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.RIGHT);
                    }
                    else
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.RIGHT);
                    }
                    break;
                case PlayerMoveNum.LEFT:

                    if (wallBase[playerY][playerX - 1] == (int)ObjectType.STAR &&
                       wallBase[playerY][playerX - 2] == (int)ObjectType.FINISH)
                    {

                        wallBase[playerY][playerX - 1] = (int)ObjectType.SPACE;
                        wallBase[playerY][playerX - 2] = (int)ObjectType.LOOKSTAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.LEFT);

                        break;
                    }

                    // 골대에서 나온다면 
                    if (wallBase[playerY][playerX - 1] == (int)ObjectType.LOOKSTAR&&
                        wallBase[playerY][playerX - 2] != (int)ObjectType.WALL)
                    {
                        wallBase[playerY][playerX - 1] = (int)ObjectType.FINISH;
                        wallBase[playerY][playerX - 2] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.LEFT);
                    }
                    if (wallBase[playerY][playerX - 1] == (int)ObjectType.STAR &&
                       (wallBase[playerY][playerX - 2] == (int)ObjectType.STAR ||
                        wallBase[playerY][playerX - 2] == (int)ObjectType.WALL)
                        )
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.STOP);

                    }
                    else if (wallBase[playerY][playerX - 1] == (int)ObjectType.STAR)
                    {
                        wallBase[playerY][playerX - 1] = (int)ObjectType.SPACE;
                        wallBase[playerY][playerX - 2] = (int)ObjectType.STAR;
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.LEFT);
                    }
                    else
                    {
                        PlayerMoveCheck(playerX, playerY, PlayerMoveNum.LEFT);
                    }
                    break;
            }
        }

        // 창 클리어// 벽만들기 // 키 인풋 할당 
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
                    {
                        // 플레이어 위에 벽이 있다면 멈춤
                        BoxMoveCheck(PlayerMoveNum.DOWN);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    {
                        // 플레이어 위에 벽이 있다면 멈춤

                        BoxMoveCheck(PlayerMoveNum.UP);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    {
                        // 플레이어 위에 벽이 있다면 멈춤
                        BoxMoveCheck(PlayerMoveNum.RIGHT);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    {
                        // 플레이어 위에 벽이 있다면 멈춤
                        BoxMoveCheck(PlayerMoveNum.LEFT);
                    }
                    break;
            }
        }

        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
            SokoBanClass sbc = new SokoBanClass();

            sbc.PlaySokoBan();
            
        }
    }
}
