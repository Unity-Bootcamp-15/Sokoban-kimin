using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sokoban
{
    public  interface IRenderingObject
    {
        public int x { get;}
        public int y { get;}
        public string symbol { get; }
    }
}
