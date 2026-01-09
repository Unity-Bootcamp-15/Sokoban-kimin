using System.Diagnostics.SymbolStore;

namespace sokoban
{
    public class GameObject : IRenderingObject
    {
        // 위치, 심볼
        public Position pos { get; set; }
        protected string _symbol;

        /*    public int x => posX;
            public int y => posY;*/
        public virtual string symbol => _symbol;

        public int x => pos.x;

        public int y => pos.y;

        private GameObject(Position pos, string symbol)
        {
            this.pos = pos;
            _symbol = symbol;
        }
        private GameObject(Position pos)
        {
            this.pos = pos;
            _symbol = symbol;
        }
        public static GameObject NewObj(Position pos, string symbol)
        {
            return new GameObject(pos, symbol);
        }
        public static GameObject NewObj(Position pos)
        {
            return new GameObject(pos);
        }




    }
    public static class GameObjectExtensions
    {
        public static bool ExistsAt(this GameObject obj, List<GameObject> targets)
        {
            return false;
        }

        public static bool IsCollider(this GameObject player, int boxX, int boxY)
        {
            return (player.pos.x == boxX) && (player.pos.y == boxY);
        }
        public static int ISCollidedIndex(this GameObject target, List<GameObject> position, int count)
        {
            int found = -1;
            for (int i = 0; i < count; ++i)
            {
                if (IsCollider(target, position[i].pos.x, position[i].pos.y))
                {
                    found = i;
                    return found;
                }
            }
            return found;
        }

        public static bool IsCollided(this GameObject target, List<GameObject> position, int count)
        {
            return -1 != ISCollidedIndex(target, position, count);
        }
    }

   public static class GameObjectFactory
    {
        // 각 오브젝트를 생성하는 메소드를 정의 
        public enum ObjType
        {
            wall,
            player,
            Goal,
            box
        }
        public static GameObject CreateObj(Position pos, ObjType type) => type switch
        {
            ObjType.wall => GameObject.NewObj(pos,"#"),
            ObjType.player => GameObject.NewObj(pos, "P"),
            ObjType.Goal => GameObject.NewObj(pos, "0"),
            ObjType.box => GameObject.NewObj(pos, "@"),
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"지원하지 않는 타입입니다: {type}")
        };

        public static GameObject CreatePlayer(string symbol, Position pos)
        {
            return GameObject.NewObj(pos, symbol);
        }

        public static GameObject CreateObject(string symbol, Position pos)
        {
            return GameObject.NewObj(pos, symbol);
        }
        // 버전 2: 좌표를 '여러 개' 넣었을 때 (List<GameObject>를 돌려줌)
        public static List<GameObject> CreateObejcts(string symbol, params Position[] positions)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (var pos in positions)
            {
                list.Add(GameObject.NewObj(pos, symbol));
            }
            return list;
        }
    }
}

