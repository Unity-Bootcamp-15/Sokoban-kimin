using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace sokoban
{

    public struct Position : IEquatable<Position> //: IEquatable<Position>을 구현하면, 박싱 없이 메모리에 있는 값 그대로 직접 비교하기 때문에 훨씬 빠르고 메모리 낭비가 없습니다.
    {
        // 16byte 이하는 작은 값이라고 표현한다.
        private int posX { get; set; } //4byte
        private int posY { get; set; } // 4byte

        public int x => posX; //4byte geter
        public int y => posY; // 4byte

        private Position(int x, int y)
        {
            posX = x; 
            posY = y;
        }
        public static Position At(int x, int y)
        {
            return new Position(x, y);
        }
        public bool IsAt(int x, int y) => x == posX && y == posY;

        public bool Equals(Position other)
        {
            return other.x == posX && other.y == posY;
        }
     
        public Position Add(Position other)
        {
            return new Position(x+other.x,y + other.y);
        }
        public static bool operator ==(Position left, Position right) => left.Equals(right);
        public static bool operator !=(Position left, Position right) => !(left == right);
        public static Position operator + (Position left, Position right) => left.Add(right);
    }

    /*
    구분,IEquatable 없음,IEquatable 있음
    작동 여부,잘 작동함,잘 작동함
    비교 방식,object로 변환 후 비교 (느림),Position 타입 그대로 비교 (빠름)
    메모리,임시 객체 생성 (GC 부담),추가 메모리 사용 없음*/


}
