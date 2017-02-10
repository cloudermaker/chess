using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public abstract class Piece
    {
        protected int _x { get; set; }
        protected int _y { get; set; }

        public char _couleur { get; set; }
        public char _type { get; set; }
        public int _cout { get; set; }

        public abstract List<Position> GetAllPos(Map m);

        public Piece()
        {
        }

        public Piece(Piece p)
        {
            this._x = p._x;
            this._y = p._y;
            this._type = p._type;
            this._cout = p._cout;
            this._couleur = p._couleur;
        }

        public void Move(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public Position GetPos()
        {
            return new Position(this._x, this._y);
        }

        public string ToPrint()
        {
            return string.Concat(this._type, this._couleur);
        }

        public void Print()
        {
            Console.ForegroundColor = this._couleur == 'B' ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ForegroundColor = this._type == 'R' ? ConsoleColor.Blue : Console.ForegroundColor;

            Console.Out.Write(this.ToPrint());

            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Roi : Piece
    {
        public Roi(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = 'R';
            this._couleur = col;
            this._cout = 10;
        }

        public override List<Position> GetAllPos(Map m)
        {
            List<Position> allPos = new List<Position>();
            
            for (int i = this._x - 1; i < this._y + 1; i++)
                for (int j = this._y - 1; j < this._y + 1; j++)
                    if (m.IsFreePlace(i, j, this._couleur) && i != this._x && j != this._y)
                        allPos.Add(new Position(i, j));
            
            return allPos;
        }
    }

    public class Dame : Piece
    {
        public Dame(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = 'D';
            this._couleur = col;
            this._cout = 8;
        }

        public override List<Position> GetAllPos(Map m)
        {
            List<Position> allPos = new List<Position>();

            // Get Fou movement
            Fou f = new Fou(this._x, this._y, this._couleur);
            List<Position> allPosFou = f.GetAllPos(m);

            // Get Tour movement
            Tour t = new Tour(this._x, this._y, this._couleur);
            List<Position> allPosTour = t.GetAllPos(m);

            // Union of both Fou and Tour movement
            allPos.AddRange(allPosFou);
            allPos.AddRange(allPosTour);

            return allPos;
        }
    }

    public class Fou : Piece
    {
        public Fou(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = 'F';
            this._couleur = col;
           this._cout = 5;
        }

        public override List<Position> GetAllPos(Map m)
        {
            List<Position> allPos = new List<Position>();
            int x = this._x;
            int y = this._y;

            // on glisse sur +1 +1
            List<Position> posList1 = new List<Position>();
            for (int i = 1; x + i < Program.size && y + i < Program.size; i++)
                if (m.IsFreePlace(x + i, y + i, this._couleur))
                    posList1.Add(new Position(x + i, y + i));
                else if (m.GetPiece(x + i, y + i)._couleur != this._couleur)
                {
                    posList1.Add(new Position(x + i, y + i));
                    break;
                }
                else
                    break;

            // on glisse sur -1 -1
            List<Position> posList2 = new List<Position>();
            for (int i = 1; x - i < Program.size && y - i < Program.size; i++)
                if (m.IsFreePlace(x - i, y - i, this._couleur))
                    posList2.Add(new Position(x - i, y - i));
                else if (m.IsInMap(x - i, y - i) && m.GetPiece(x - i, y - i)._couleur != this._couleur)
                {
                    posList2.Add(new Position(x - i, y - i));
                    break;
                }
                else
                    break;

            // on glisse sur -1 +1
            List<Position> posList3 = new List<Position>();
            for (int i = 1; x - i < Program.size && y + i < Program.size; i++)
                if (m.IsFreePlace(x - i, y + i, this._couleur))
                    posList3.Add(new Position(x - i, y + i));
                else if (m.IsInMap(x - i, y + i) && m.GetPiece(x - i, y + i)._couleur != this._couleur)
                {
                    posList3.Add(new Position(x - i, y + i));
                    break;
                }
                else
                    break;

            // on glisse sur +1 -1
            List<Position> posList4 = new List<Position>();
            for (int i = 1; x + i < Program.size && y - i < Program.size; i++)
                if (m.IsFreePlace(x + i, y - i, this._couleur))
                    posList4.Add(new Position(x + i, y - i));
                else if (m.IsInMap(x + i, y - i) && m.GetPiece(x + i, y - i)._couleur != this._couleur)
                {
                    posList4.Add(new Position(x + i, y - i));
                    break;
                }
                else
                    break;

            posList1.AddRange(posList2);
            posList1.AddRange(posList3);
            posList1.AddRange(posList4);

            return posList1;
        }
    }

    public class Cavalier : Piece
    {
        public Cavalier(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = 'C';
            this._couleur = col;
           this._cout = 5;
        }

        public override List<Position> GetAllPos(Map m)
        {
            List<Position> allPos = new List<Position>();
            List<Position> allPosTmp = new List<Position>();
            int x = this._x;
            int y = this._y;

            allPosTmp.Add(new Position(x + 2, y + 1));
            allPosTmp.Add(new Position(x + 2, y - 1));
            allPosTmp.Add(new Position(x - 2, y + 1));
            allPosTmp.Add(new Position(x - 2, y - 1));

            allPosTmp.Add(new Position(x + 1, y + 2));
            allPosTmp.Add(new Position(x + 1, y - 2));
            allPosTmp.Add(new Position(x - 1, y + 2));
            allPosTmp.Add(new Position(x - 1, y - 2));

            foreach (Position p in allPosTmp)
                if (m.IsFreePlace(p.x, p.y, this._couleur))
                    allPos.Add(p);

            return allPos;
        }
    }

    public class Tour : Piece
    {
        public Tour(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = 'T';
            this._couleur = col;
           this._cout = 6;
        }

        public override List<Position> GetAllPos(Map m)
        {
            List<Position> allPos = new List<Position>();

            // on glisse sur y de y-1 à 0 (x fixe)
            List<Position> posList1 = new List<Position>();
            for (int i = this._y - 1; i >= 0; i--)
                if (m.IsFreePlace(this._x, i, this._couleur))
                    posList1.Add(new Position(this._x, i));
                else if (m.GetPiece(this._x, i)._couleur != this._couleur)
                {
                    posList1.Add(new Position(this._x, i));
                    break;
                }
                else
                    break;

            // on glisse sur y de y + 1 à 7 (x fixe)
            List<Position> posList2 = new List<Position>();
            for (int i = this._y + 1; i < m._Size; i++)
                if (m.IsFreePlace(this._x, i, this._couleur))
                    posList2.Add(new Position(this._x, i));
                else if (m.GetPiece(this._x, i)._couleur != this._couleur)
                {
                    posList2.Add(new Position(this._x, i));
                    break;
                }
                else
                    break;

            // on glisse sur x de x-1 à 0 (y fixe)
            List<Position> posList3 = new List<Position>();
            for (int i = this._x - 1; i >= 0; i--)
                if (m.IsFreePlace(i, this._y, this._couleur))
                    posList1.Add(new Position(i, this._y));
                else if (m.GetPiece(i, this._y)._couleur != this._couleur)
                {
                    posList3.Add(new Position(i, this._y));
                    break;
                }
                else
                    break;

            // on glisse sur x de x + 1 à 7 (y fixe)
            List<Position> posList4 = new List<Position>();
            for (int i = this._x + 1; i < m._Size; i++)
                if (m.IsFreePlace(i, this._y, this._couleur))
                    posList1.Add(new Position(i, this._y));
                else if (m.GetPiece(i, this._y)._couleur != this._couleur)
                {
                    posList4.Add(new Position(i, this._y));
                    break;
                }
                else
                    break;

            posList1.AddRange(posList2);
            posList1.AddRange(posList3);
            posList1.AddRange(posList4);

            return posList1;
        }
    }

    public class Pion : Piece
    {
        public Pion(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = 'P';
            this._couleur = col;
           this._cout = 2;
        }

        public override List<Position> GetAllPos(Map m)
        {
            List<Position> allPos = new List<Position>();

            if (this._couleur == 'B')
            {
                // cas normal
                if (m.IsFreePlace(this._x, this._y + 1, this._couleur))
                    allPos.Add(new Position(this._x, this._y + 1));
                if (this._y == 1 && m.IsFreePlace(this._x, this._y + 2, this._couleur))
                    allPos.Add(new Position(this._x, this._y + 2));

                // cas pour manger
                if (m.IsInMap(this._x + 1, this._y + 1) && m.GetPiece(this._x + 1, this._y + 1)._type != ' ' && m.GetPiece(this._x + 1, this._y + 1)._couleur != this._couleur)
                    allPos.Add(new Position(this._x + 1, this._y + 1));
                if (m.IsInMap(this._x - 1, this._y + 1) && m.GetPiece(this._x - 1, this._y + 1)._type != ' ' && m.GetPiece(this._x - 1, this._y + 1)._couleur != this._couleur)
                    allPos.Add(new Position(this._x - 1, this._y + 1));
            }
            else if (this._couleur == 'N')
            {
                // cas normal
                if (m.IsFreePlace(this._x, this._y - 1, this._couleur))
                    allPos.Add(new Position(this._x, this._y - 1));
                if (this._y == 6 && m.IsFreePlace(this._x, this._y - 2, this._couleur))
                    allPos.Add(new Position(this._x, this._y - 2));

                // cas pour manger
                if (m.IsInMap(this._x - 1, this._y + 1) && m.GetPiece(this._x - 1, this._y + 1)._type != ' ' && m.GetPiece(this._x - 1, this._y + 1)._couleur != this._couleur)
                    allPos.Add(new Position(this._x - 1, this._y + 1));
                if (m.IsInMap(this._x - 1, this._y - 1) && m.GetPiece(this._x - 1, this._y - 1)._type != ' ' && m.GetPiece(this._x - 1, this._y - 1)._couleur != this._couleur)
                    allPos.Add(new Position(this._x - 1, this._y - 1));
            }

            return allPos;
        }
    }

    public class Vide : Piece
    {
        public Vide(int x, int y, char col)
        {
            this._x = x;
            this._y = y;
            this._type = ' ';
            this._couleur = col;
            this._cout = 1;
        }

        public override List<Position> GetAllPos(Map m)
        {
            return new List<Position>();
        }
    }
}
