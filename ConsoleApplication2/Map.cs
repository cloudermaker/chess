using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /*    
     * BLACK
     *    y0 y1 y2 y3 y4 y5 y6 y7
     * x0 TB PB             PN TN
     * x1 CB PB             PN CN
     * x2 FV PB             PN FN
     * x3 RB PB             PN DN
     * x4 DB PB             PN RN
     * x5 FB PB             PN FN
     * x6 CB PB             PN CN
     * x7 TB PB             PN TN
     *  
     * WHITE
     * */

    public class Map
    {
        private Piece[,] _AllPiece { get; set; }
        public int _Size {get;set;}
        
        public Map(int size)
        {
            this._Size = size;
            this.InitMap();
        }

        public Map(Map m)
        {
            this._Size = m._Size;
            this.InitMap();

            for (int i = 0; i < this._Size; i++)
            {
                for (int j = 0; j < this._Size; j++)
                {
                    this._AllPiece[i, j] = this.NewPieceFactory( m._AllPiece[i, j] );
                }
            }
        }

        public void InitMap()
        {
            this._AllPiece = new Piece[_Size, _Size];
            for (int i = 0; i < this._Size; i++)
                for (int j = 0; j < this._Size; j++)
                    this._AllPiece[i, j] = new Vide(i, j, ' ');
        }

        public Piece GetPiece(int x, int y)
        {
            return this._AllPiece[x, y];
        }

        public void InitGame()
        {
            // Init Black
            this._AllPiece[0, 0] = new Tour(0, 0, 'B');
            this._AllPiece[1, 0] = new Cavalier(1, 0, 'B');
            this._AllPiece[2, 0] = new Fou(2, 0, 'B');
            this._AllPiece[3, 0] = new Roi(3, 0, 'B');
            this._AllPiece[4, 0] = new Dame(4, 0, 'B');
            this._AllPiece[5, 0] = new Fou(5, 0, 'B');
            this._AllPiece[6, 0] = new Cavalier(6, 0, 'B');
            this._AllPiece[7, 0] = new Tour(7, 0, 'B');
            for (int i = 0; i < this._Size; i++)
                this._AllPiece[i, 1] = new Pion(i, 1, 'B');

            // Init White
            this._AllPiece[0, this._Size - 1] = new Tour(0, this._Size - 1, 'N');
            this._AllPiece[1, this._Size - 1] = new Cavalier(1, this._Size - 1, 'N');
            this._AllPiece[2, this._Size - 1] = new Fou(2, this._Size - 1, 'N');
            this._AllPiece[3, 7] = new Dame(3, 7, 'N');
            this._AllPiece[4, 7] = new Roi(4, 7, 'N');
            this._AllPiece[5, 7] = new Fou(5, 7, 'N');
            this._AllPiece[6, 7] = new Cavalier(6, 7, 'N');
            this._AllPiece[7, 7] = new Tour(7, 7, 'N');
            for (int i = 0; i < this._Size; i++)
                this._AllPiece[i, this._Size - 2] = new Pion(i, this._Size - 2, 'N');
        }

        public void PrintMap()
        {
            Console.Out.Write("   ");
            for (int i = 0; i < this._Size; i++)
                Console.Out.Write(string.Concat("  ", i + 1, "  "));
            Console.Out.WriteLine();

            Console.Out.Write("   ");
            for (int k = 1; k < this._Size; k++)
                Console.Out.Write("-----");
            Console.Out.Write("------");
            Console.Out.WriteLine();

            // print map
            for (int i = 0; i < this._Size; i++)
            {
                Console.Out.Write(string.Concat(i + 1, "  | "));
                for (int j = 0; j < this._Size; j++)
                {
                    if (this._AllPiece[i, j] != null)
                    {
                        this._AllPiece[i, j].Print();
                        Console.Out.Write(" | ");
                    }
                }

                Console.Out.WriteLine();
                Console.Out.Write("   ");
                for (int k = 1; k < this._Size; k++)
                    Console.Out.Write("-----");
                Console.Out.Write("------");
                Console.Out.WriteLine();
            }
        }

        public void PrintAllMove()
        {
            for (int i = 0; i < this._Size; i++)
                for (int j = 0; j < this._Size; j++)
                {
                    List<Position> list = this._AllPiece[i, j].GetAllPos(this);
                    if (list.Count > 0 && this._AllPiece[i, j]._type != ' ')
                    {
                        Console.Out.Write(this._AllPiece[i, j].ToPrint() + ' ');                        
                        foreach (Position p in list)
                            p.Print();
                        Console.Out.WriteLine();
                    }
                }
        }

        public bool IsInMap(int x, int y)
        {
            if (x >= 0 && x < this._Size && y >= 0 && y < this._Size)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Regarde si la case est dans la carte et si elle est une pièce vide: type = ' '
        /// </summary>
        public bool IsFreePlace(int x, int y, char col)
        {
            if (this.IsInMap(x, y)) // in map size
            {
                if (this._AllPiece[x, y]._type == ' '  //case disponible
                    || this._AllPiece[x, y]._couleur != col) // case mangeable
                    return true;
            }

            return false;
        }

        public bool IsEnnemy(int x, int y, char myColor)
        {
            return this._AllPiece[x, y]._couleur != myColor;
        }

        public bool Move(Move mo)
        {
            int x1 = mo._pos1.x;
            int y1 = mo._pos1.y;
            int x2 = mo._pos2.x;
            int y2 = mo._pos2.y;

            Position p1 = new Position(x1, y1);
            Position p2 = new Position(x2, y2);
            Piece p = this.GetPiece(x1, y1);

            List<Position> allPos = p.GetAllPos(this);
            if (allPos.Any(x => x == p2))
            {
                p.Move(x2, y2);

                this._AllPiece[x2, y2] = this._AllPiece[x1, y1];
                this._AllPiece[x1, y1] = new Vide(x1, y1, ' ');

                // Special move: pion devient Dame
                if (this._AllPiece[x2, y2]._type == 'P')
                {
                    if ((p.GetPos().x == this._Size - 1 && p.GetPos().y == this._Size - 1) // cas blanc
                        || (p.GetPos().x == 0 && p.GetPos().y == 0)) // cas noir
                        this._AllPiece[x2, y2] = new Dame(x2, y2, p._couleur);
                }

                return true;
            }
            else
                return false;
        }

        public char IsGameOver()
        {
            int countRoiB = 0;
            int countRoiN = 0;
            int count = 0;
            for (int i = 0; i < this._Size; i++)
                for (int j = 0; j < this._Size; j++)
                {
                    if (this._AllPiece[i, j]._type == 'R' && this._AllPiece[i,j]._couleur == 'B')
                        countRoiB++;
                    if (this._AllPiece[i, j]._type == 'R' && this._AllPiece[i, j]._couleur == 'N')
                        countRoiN++;
                    count++;
                }

            if (count == 2)
                return 'D';
            else if (countRoiB == 0)
                return 'N';
            else if (countRoiN == 0)
                return 'B';
            else
                return ' ';
        }

        public int CountPiece(char col)
        {
            int count = 0;

            for (int i = 0; i < this._Size; i++)
            {
                for (int j = 0; j < this._Size; j++)
                    if (this._AllPiece[i, j]._couleur == col)
                        count++;
            }

            return count;
        }

        public Move GetRandomMove(char col)
        {
            Random r = new Random();
            int nb = r.Next(1, this.CountPiece(col) + 1);
            int inc = 1;
            bool leaveLoop = false;

            Piece p = null;
            List<Position> allPosMove = null;

            Move m = null;

            // cherche une piece qui peut bouger
            for (int i = 0; i < this._Size && !leaveLoop; i++)
            {
                for (int j = 0; j < this._Size && !leaveLoop; j++)
                {
                    Piece tmpP = this._AllPiece[i, j];
                    List<Position> tmpAllPosMove = tmpP.GetAllPos(this);

                    if (inc == nb && tmpP._couleur == col && tmpAllPosMove.Count > 0)
                    {
                        p = tmpP;
                        allPosMove = tmpAllPosMove;
                        leaveLoop = true;
                    }
                    else if (tmpP._couleur == col && tmpAllPosMove.Count > 0)
                    {
                        p = tmpP;
                        allPosMove = tmpAllPosMove;
                        inc++;
                    }
                }
            }

            // si on ne trouve pas de mouvement => abandon
            if (p == null)
                return null;

            // cherche un mouvement à faire sur cette pièce
            Position pos1 = p.GetPos();
            nb = r.Next(0, allPosMove.Count);
            foreach (Position pos2 in allPosMove)
            {
                if (nb == 0)
                    m = new Move(pos1, pos2);
                nb--;
            }

            return m;
        }

        public List<Piece> GetAllPiece(char col)
        {
            List<Piece> list = new List<Piece>();

            for (int i = 0; i < this._Size; i++)
            {
                for (int j = 0; j < this._Size; j++)
                {
                    if (this._AllPiece[i,j]._couleur == col)
                        list.Add(this._AllPiece[i,j]);
                }
            }

            return list;
        }

        public int GetNote(int x, int y)
        {
            return this._AllPiece[x, y]._cout;
        }

        private Piece NewPieceFactory(Piece p)
        {
            int x = p.GetPos().x;
            int y = p.GetPos().y;

            if (p._type == 'T')
                return new Tour(x, y, p._couleur);
            else if (p._type == 'C')
                return new Cavalier(x, y, p._couleur);
            else if (p._type == 'F')
                return new Fou(x, y, p._couleur);
            else if (p._type == 'R')
                return new Roi(x, y, p._couleur);
            else if (p._type == 'D')
                return new Dame(x, y, p._couleur);
            else if (p._type == 'P')
                return new Pion(x, y, p._couleur);
            else
                return new Vide(x, y, p._couleur);

        }
    }
}
