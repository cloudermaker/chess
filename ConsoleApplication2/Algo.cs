using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Algo
    {
        private Map _m;

        public Algo(Map m)
        {
            this._m = m;
        }

        public KeyValuePair<Position, Position> GetNextMove()
        {
            KeyValuePair<Position, Position> move = new KeyValuePair<Position, Position>();

            return move;
        }
    }
}
