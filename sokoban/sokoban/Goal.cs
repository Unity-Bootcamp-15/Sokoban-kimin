using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sokoban.GameObjectFactory;

namespace sokoban
{
    public class Goal : GameObject
    {
        public bool hasBox { get; set; }
        public Goal(GameObject obj) : base(obj.pos,"O") 
        {
            hasBox = false;
        }
        public override string symbol => hasBox ? "*" : "O";
    }

  /*  public class GoalG : GameObject
    {
        public bool hasBox { get; set; }

        public GoalG(GameObject obj) : base(obj.pos, obj.symbol)
        {
            hasBox = false;
        }
    }*/

    // Goall 은 GameObject의 모든 기능을 수행할 수 있는가
    // is- a 관계 
    /*public class Goal : GameObject
    {

    }*/
}
