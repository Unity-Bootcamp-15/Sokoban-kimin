using System;

// 1. 맵 요소를 정의하는 열거형 (Enum)
public enum ObjectType
{
    WALL = '#',       // 벽
    SPACE = ' ',      // 빈 공간 (바닥)
    PLAYER = 'P',     // 플레이어
    BOX = 'B',        // 박스
    GOAL = 'G',       // 목표 지점
    BOX_ON_GOAL = 'O',// 목표 지점에 있는 박스
    PLAYER_ON_GOAL = 'A' // 목표 지점에 있는 플레이어 (편의상 A로 정의)
}

public class SokobanGame
{
    // 게임 맵 (다차원 배열)
    private char[,] map;

    // 플레이어 위치
    private int playerY;
    private int playerX;

    // 맵 정의
    private string[] initialMap = new string[]
    {
        "##########",
        "#P       #",
        "#   # ## #",
        "# G  BG  #",
        "# #B######",
        "# GB    G#",
        "#        #",
        "##########"
    };

    // 생성자: 맵 초기화 및 플레이어 위치 찾기
    public SokobanGame()
    {
        int height = initialMap.Length;
        int width = initialMap[0].Length;
        map = new char[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[y, x] = initialMap[y][x];
                if (map[y, x] == (char)ObjectType.PLAYER)
                {
                    playerY = y;
                    playerX = x;
                    // 플레이어 초기 위치는 빈 공간으로 간주 (렌더링 시점에 P를 덮어씀)
                    map[y, x] = (char)ObjectType.SPACE;
                }
            }
        }
    }

    // 맵을 콘솔에 출력
    public void Draw()
    {
        // 화면을 깨끗하게 지우고 새로 그립니다.
        Console.Clear();

        int height = map.GetLength(0);
        int width = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char cell = map[y, x];

                // 플레이어 위치에 도달하면 실제 플레이어 기호('P' 또는 'A')를 출력
                if (y == playerY && x == playerX)
                {
                    // 만약 플레이어가 목표 지점에 있다면 ObjectType.PLAYER_ON_GOAL 출력
                    if (map[y, x] == (char)ObjectType.GOAL)
                    {
                        Console.Write((char)ObjectType.PLAYER_ON_GOAL);
                    }
                    else
                    {
                        Console.Write((char)ObjectType.PLAYER);
                    }
                }
                else
                {
                    Console.Write(cell);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("\nWASD로 이동, R키로 재시작");
    }

    // 게임 업데이트 (이동 처리)
    public void Update(ConsoleKey key)
    {
        int nextY = playerY;
        int nextX = playerX;
        int pushY = playerY; // 박스를 밀었을 때 박스가 이동할 위치 Y
        int pushX = playerX; // 박스를 밀었을 때 박스가 이동할 위치 X

        // 이동 방향 설정
        switch (key)
        {
            case ConsoleKey.W:
                nextY--;
                pushY -= 2;
                break;
            case ConsoleKey.S:
                nextY++;
                pushY += 2;
                break;
            case ConsoleKey.A:
                nextX--;
                pushX -= 2;
                break;
            case ConsoleKey.D:
                nextX++;
                pushX += 2;
                break;
            default:
                return; // 다른 키는 무시
        }

        char nextCell = map[nextY, nextX];

        // 1. 벽인지 확인
        if (nextCell == (char)ObjectType.WALL)
        {
            return; // 이동 불가
        }

        // 2. 박스인지 확인 (BOX 또는 BOX_ON_GOAL)
        if (nextCell == (char)ObjectType.BOX || nextCell == (char)ObjectType.BOX_ON_GOAL)
        {
            char pushCell = map[pushY, pushX];

            // 박스 다음 칸이 벽이거나 (다른) 박스이면 밀기 불가
            if (pushCell == (char)ObjectType.WALL ||
                pushCell == (char)ObjectType.BOX ||
                pushCell == (char)ObjectType.BOX_ON_GOAL)
            {
                return;
            }

            // 박스 이동 처리 (박스를 빈 공간 또는 목표 지점으로 옮김)
            if (pushCell == (char)ObjectType.SPACE)
            {
                map[pushY, pushX] = (char)ObjectType.BOX;
            }
            else if (pushCell == (char)ObjectType.GOAL)
            {
                map[pushY, pushX] = (char)ObjectType.BOX_ON_GOAL;
            }

            // 플레이어가 박스를 밀고 이동할 때 박스가 있던 자리 정리
            // 박스가 목표 지점에 있었다면 목표 지점('G')으로 되돌림
            if (nextCell == (char)ObjectType.BOX_ON_GOAL)
            {
                map[nextY, nextX] = (char)ObjectType.GOAL;
            }
            // 박스가 일반 바닥에 있었다면 빈 공간(' ')으로 되돌림
            else
            {
                map[nextY, nextX] = (char)ObjectType.SPACE;
            }
        }

        // 3. 플레이어 이동 처리
        playerY = nextY;
        playerX = nextX;

        // 승리 조건 체크
        if (CheckWinCondition())
        {
            Draw();
            Console.WriteLine("\n🎉 축하합니다! 모든 박스를 목표 지점에 옮겼습니다! 🎉");
            Environment.Exit(0);
        }
    }

    // 승리 조건 확인: 맵에 'B' (일반 박스)가 하나도 없어야 함
    public bool CheckWinCondition()
    {
        foreach (char cell in map)
        {
            if (cell == (char)ObjectType.BOX)
            {
                return false;
            }
        }
        return true;
    }
}

public class Program
{
    public static void Main()
    {
        Console.Title = "C# 콘솔 소코반";
        SokobanGame game = new SokobanGame();

        // 게임 루프
        while (true)
        {
            game.Draw();

            // 사용자 입력 대기
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            // R키로 재시작 (새 게임 객체 생성)
            if (key == ConsoleKey.R)
            {
                game = new SokobanGame();
                continue;
            }

            // WASD 키로 게임 업데이트
            game.Update(key);
        }
    }
}