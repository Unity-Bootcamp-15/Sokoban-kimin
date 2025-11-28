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
            Console.WriteLine(Console.ReadLine());
            Console.Clear(); 


        }
    }
}
