

using sokoban;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static sokoban.GameObjectFactory;
namespace Sokoban
{
    //개체 지향 프로그램 : 개체
    // ㄴ 상태와 행위를 가진 것
    //어떤 데이터끼리 연관이 있는가? Render (박스,플레이어,벽,)_
    //그 데이터를 조작하는 함수는 무엇인가?

    internal class Program
    {
        enum Direction
        {

            None,
            Left,
            Right,
            Up,
            Down
        }

        static void Main(string[] args)
        {

            // ----------- 초기화 -------------
            // 콘솔 창 초기화
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Title = "My Sokoban";
            Console.CursorVisible = false;
            Console.Clear();

            // 게임 데이터 초기화
            /*Position mapminSize = Position.At(0, 0);
            Position mapMaxSize = Position.At(10, 10);*/

            GameObject mapMinSize = GameObject.NewObj(Position.At(0, 0));
            GameObject mapMaxSize = GameObject.NewObj(Position.At(10, 10));
            /*            int mapSizeMaxX = 10;
                        int mapSizeMaxY = 10;*/

            bool isGameOver = false;

            // 플레이어 데이터
            // Position playerPos = Position.At(5, 10);

            //GameObject player = GameObject.NewObj(Position.At(5, 10), "P");

            GameObject player = GameObjectFactory.CreateObj(Position.At(5, 10),GameObjectFactory.ObjType.player);
            /*    int playerX = 5;
                int playerY = 10;*/
            Direction playerDirection = Direction.None;

            // 벽 데이터
            /*  List<GameObject> wallPos = new()
              {
                  GameObject.NewObj(Position.At(3, 3),"#"),
                  GameObject.NewObj(Position.At(4, 3),"#"),
                  GameObject.NewObj(Position.At(5, 3),"#"),
                  GameObject.NewObj(Position.At(6, 3),"#"),
                  GameObject.NewObj(Position.At(7, 3),"#"),

              };*/
            List<GameObject> wallPos = new(){
                GameObjectFactory.CreateObj(Position.At(3, 3),GameObjectFactory.ObjType.wall),
                GameObjectFactory.CreateObj(Position.At(4, 3),GameObjectFactory.ObjType.wall),
                GameObjectFactory.CreateObj(Position.At(5, 3),GameObjectFactory.ObjType.wall),
                GameObjectFactory.CreateObj(Position.At(6, 3),GameObjectFactory.ObjType.wall),
                GameObjectFactory.CreateObj(Position.At(7, 3),GameObjectFactory.ObjType.wall),
            };


        /*    var wallPos = GameObjectFactory.CreateObejcts("#",
                  Position.At(3, 3),
                  Position.At(4, 3),
                  Position.At(5, 3),
                  Position.At(6, 3),
                  Position.At(7, 3)
                );*/




            /* Position[] wallPos =
             {
                 Position.At(3, 3),
                 Position.At(4, 3),
                 Position.At(5, 3),
                 Position.At(6, 3),
                 Position.At(7, 3)

             };*/
            /*int[] wallX = { 3, 4, 5, 6, 7 };
             int[] wallY = { 3, 3, 3, 3, 3 };*/
            //  int wallCount = wallPos.Length;

            // 박스 데이터
            List<GameObject> boxPos = new()
            {
                GameObject.NewObj(Position.At(6, 8),"@"),
                GameObject.NewObj(Position.At(6, 7),"@"),
            };
            /*    Position[] boxPos =
                {
                    Position.At(6, 8),
                    Position.At(6, 7),
                };*/

            /* int[] boxX = { 6, 8 };
             int[] boxY = { 6, 7 };
             int boxCount = boxX.Length;*/


            // 골 데이터
            /*    List<GameObject> goalPos = GameObjectFactory.CreateObejcts("O",
                    Position.At(4,7),
                    Position.At(5,10)
                    );*/

            List<Goal> goalPos = new()
            {
                new Goal(GameObject.NewObj(Position.At(4,7))),
                new Goal(GameObject.NewObj(Position.At(5,10))),
            };

            /*Position[] goalPos =
            {
                Position.At(4, 7 ),
                Position.At(5, 10),

            };*/
            /*int[] goalX = { 4, 7 };
            int[] goalY = { 5, 10 };*/
           // bool[] isBoxOnGoal = { false, false };
            //bool isGameClear = goalPos.All(g => g.hasBox);
            /* int goalCount = goalX.Length;*/

            // 플레이어 이동 처리
            int newPlayerX = player.pos.x;
            int newPlayerY = player.pos.y;

            // ------------ 게임 루프 -----------
            while (isGameOver == false)
            {
                Render();
                ConsoleKeyInfo keyInfo = ProcessInput();

                playerDirection = GetDirectionFromkey(keyInfo);

                Update();
            }

            ShowClearMessage();
            // Render---------------------------------------------------
            void Render()
            {
                // ------------ Render -----------
                // 이전 화면 지움
                Console.Clear();
                // 박스 출력
                RenderObjects(boxPos, boxPos.Count); // i => "@"
                // 골 출력
                RenderGoal(goalPos, goalPos.Count);
                // 플레이어 출력
                RenderObject(player);
                // 벽 출력
                RenderObjects(wallPos, wallPos.Count);
            }

            void RenderGoal(List<Goal> position, int count)
            {
                for (int i = 0; i < count; ++i)
                {
                    RenderObject(position[i]);
                }
            }

            void RenderObjects(List<GameObject> position, int count)
            {
                for (int i = 0; i < count; ++i)
                {
                    RenderObject(position[i]);
                }
            }
            /*void RenderObject(GameObject position, string symbol)
            {
                Console.SetCursorPosition(position.pos.x, position.pos.y); ;
                Console.Write(symbol);
            }*/
            void RenderObject(IRenderingObject obj)
            {
                Console.SetCursorPosition(obj.x, obj.y); ;
                Console.Write(obj.symbol);
            }
            // ProcessInput---------------------------------------------------
            ConsoleKeyInfo ProcessInput()
            {
                return Console.ReadKey();
            }

            // GetDirectionFromkey---------------------------------------------------
            Direction GetDirectionFromkey(ConsoleKeyInfo keyInfo)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        return Direction.Down;
                    case ConsoleKey.UpArrow:
                        return Direction.Up;
                    case ConsoleKey.LeftArrow:
                        return Direction.Left;
                    case ConsoleKey.RightArrow:
                        return Direction.Right;
                    default:
                        return Direction.None;
                }
            }


            //(int px,int y) GetNewPositionFrom(Direction dir , int currentX, int currentY)---------------------------------------------------
            // 만약 Direction이 유효하지 않다면 이전 위치값을 반환 
            GameObject GetNewPositionFrom(Direction dir, GameObject current) => dir switch
            {
                Direction.Left => GameObject.NewObj(Position.At(Math.Max(mapMinSize.pos.x, current.pos.x - 1), current.pos.y)),
                Direction.Right => GameObject.NewObj(Position.At(Math.Min(mapMaxSize.pos.x, current.pos.x + 1), current.pos.y)),
                Direction.Up => GameObject.NewObj(Position.At(current.pos.x, Math.Max(mapMinSize.pos.y, current.pos.y - 1))),
                Direction.Down => GameObject.NewObj(Position.At(current.pos.x, Math.Min(mapMaxSize.pos.y, current.pos.y + 1))),
                _ => GameObject.NewObj(Position.At(current.pos.x, current.pos.y))
                // 그 외의 경우는?
                // 어떻게 외부에 실패했다는 것을 알릴 수 있을까

            };

            /*bool IsCollider(GameObject player, int boxX, int boxY)
            {
                return (player.x == boxX) && (player.y == boxY);
            }
            int ISCollidedIndex(GameObject pos, List<GameObject> position, int count)
            {
                int found = -1;
                for (int i = 0; i < count; ++i)
                {
                    if (IsCollider(pos, position[i].x, position[i].y))
                    {
                        found = i;
                        return found;
                    }
                }
                return found;
            }

            bool IsCollided(GameObject pos, List<GameObject> position, int count)
            {
                return -1 != ISCollidedIndex(pos, position, count);
            }*/
            // Update---------------------------------------------------
            void IsCollededChack()
            {
                // 1. 플레이어가 이동할 새로운 위치 계산
                GameObject nextP = GetNewPositionFrom(playerDirection, player);

                // 2. 벽과 충돌하는지 확인
                if (nextP.IsCollided(wallPos, wallPos.Count))
                {
                    return; // 벽이면 이동하지 않고 종료
                }

                // 3. 박스와 충돌하는지 확인
                int boxIdx = nextP.ISCollidedIndex(boxPos, boxPos.Count);

                if (boxIdx != -1) // 박스를 만났다면
                {
                    // 3-1. 박스가 이동할 새로운 위치 계산
                    GameObject nextB = GetNewPositionFrom(playerDirection, boxPos[boxIdx]);
                    // 3-2. 박스가 이동할 곳에 벽이나 다른 박스가 있는지 확인
                    bool isBlocked = nextB.IsCollided(wallPos, wallPos.Count) ||
                                     nextB.IsCollided(boxPos, boxPos.Count);

                    if (isBlocked)
                    {
                        return; // 박스가 막혀있으면 플레이어도 못 움직임
                    }
                    // 3-3. 박스 이동 확정
                    boxPos[boxIdx] = GameObject.NewObj(Position.At(nextB.pos.x, nextB.pos.y), "@");
                }
                // 4. 플레이어 위치 최종 갱신
                //playerPos = Position.At(nextPX, nextPY);
                player = GameObject.NewObj(Position.At(nextP.pos.x, nextP.pos.y), "P");
            }
            // Update 함수 내부의 박스 충돌 부분
            void Update()
            {
                if (playerDirection == Direction.None) return;
                try
                {
                    // 모든 충돌 처리
                    IsCollededChack();

                    // 5. 골 체크 및 승리 판정 (기존 코드 유지)
                    CheckGoalStatus();
                }
                catch
                (Exception e)
                {
                    Console.Write(e.ToString());
                }
            }
            /*       void CheckGoalStatus()
                   {
                       int clearedCount = 0;
                       for (int g = 0; g < goalPos.Length; g++)
                       {
                           isBoxOnGoal[g] = IsCollided(goalPos, boxX, boxY, boxCount);
                           if (isBoxOnGoal[g]) clearedCount++;
                       }

                       if (clearedCount == goalPos.Length)
                       {
                           isGameOver = true;
                       }
                   }*/
            void CheckGoalStatus()
            {
                int clearedCount = 0;
                for (int g = 0; g < goalPos.Count; g++)
                {
                    // 1. g번째 골의 위치를 가져옵니다.
                    GameObject currentGoal = goalPos[g];

                    // 2. 그 위치에 '박스들' 중 하나라도 있는지 확인합니다.
                    // IsCollided(체크할좌표X, 체크할좌표Y, 대상배열, 대상개수)
                    goalPos[g].hasBox = currentGoal.IsCollided(boxPos, boxPos.Count);

                    if (goalPos[g].hasBox)
                    {
                        clearedCount++;
                    }
                }
                if (clearedCount == goalPos.Count)
                {
                    isGameOver = true;
                }
            }
            // ---------------------------------------------------
            void ShowClearMessage()
            {
                Console.Clear();
                Console.WriteLine("축하합니다! 게임을 클리어하셨습니다!");
            }
            // ---------------------------------------------------
            /*void Update()
            {
                oldPlayerX = newPlayerX;
                oldPlayerY = newPlayerY;

                if (playerDirection == Direction.None)
                    return;

                // 키 입력후 플레이어 이동 방향 선택 
                var (x, y) = GetNewPositionFrom(playerDirection, playerX, playerY);
                newPlayerX = x; // 이 대입 과정이 반드시 필요합니다!
                newPlayerY = y;


                // 3. [추가] mapSizeMaxX, mapSizeMaxY를 사용한 경계 체크
                // 플레이어가 맵의 최대 범위를 넘어가려고 하면 이전 위치로 되돌립니다.
                if (newPlayerX > mapSizeMaxX || newPlayerY > mapSizeMaxY ||
                    newPlayerX < mapSizeMinX || newPlayerY < mapSizeMinY)
                {
                    newPlayerX = oldPlayerX;
                    newPlayerY = oldPlayerY;
                }

                // oldPlayerX와 oldPlayerY를 받아서 plyer이동 전의 위치를 저장 
                ResolvePlayerWallCollision();


                // 플레이어와 박스의 충돌 처리
                // 1. 플레이어가 어떤 박스와 충돌했는지 찾는다.

                const int NoCollidedBox = -1;
                int collidedBoxIndex = NoCollidedBox;
                collidedBoxIndex =  ISCollidedIndex(newPlayerX,newPlayerY,boxX,boxY, boxCount);


                // 2. 충돌했다면
                if (collidedBoxIndex != NoCollidedBox)
                {
                    // 2-1. 박스의 새로운 좌표를 구한다.
                    int currentBoxX = boxX[collidedBoxIndex];
                    int currentBoxY = boxY[collidedBoxIndex];


                    var (xBox, yBox) = GetNewPositionFrom(playerDirection, currentBoxX, currentBoxY);
                    int newBoxX = xBox;
                    int newBoxY = yBox;

                    // 2-2. 벽과의 충돌 처리

                    // 1. 박스와 벽의 충돌을 감지한다.

                    // 2. 충돌했다면 좌표 갱신을 하지 않는다.
                    if (IsCollided(newBoxX, newBoxY, wallX, wallY, wallCount))
                    {
                        return;
                    }

                    // 2-3. 박스끼리의 충돌 처리
                    if (IsCollided(newBoxX, newBoxY, boxX, boxY, boxCount))
                    {
                        return;
                    }

                    // 2-4. 박스의 좌표를 갱신한다.
                    boxX[collidedBoxIndex] = newBoxX;
                    boxY[collidedBoxIndex] = newBoxY;
                }

                // 박스와 골의 충돌 처리
                // ㄴ 골 위에 박스가 있는지 확인한다.
                for (int goalIdx = 0; goalIdx < goalCount; ++goalIdx)
                {
                    // 우선 해당 골 위에는 박스가 없다고 가정 (초기화)
                    isBoxOnGoal[goalIdx] = false;

                    // 해당 골 좌표(goalX[goalIdx], goalY[goalIdx])에 박스들 중 하나라도 있는지 확인
                    if (IsCollided(goalX[goalIdx], goalY[goalIdx], boxX, boxY, boxCount)) // 박스가 있다면
                    {
                        isBoxOnGoal[goalIdx] = true;
                    }
                }

                // 게임의 종료 여부
                // ㄴ 모든 골 위에 박스가 올려져 있는 것
                bool isAllGoalCleared = true;
                for (int i = 0; i < goalCount; ++i)
                {
                    if (isBoxOnGoal[i] == false)
                    {
                        isAllGoalCleared = false;
                        break;
                    }
                }

                if (isAllGoalCleared)
                {
                    isGameOver = true;
                }

                // 플레이어의 좌표를 갱신한다.
                playerX = newPlayerX;
                playerY = newPlayerY;
            }*/
        }
    }
}