using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Day17_ConwayCubes
{
    public class Matrix3D
    {
        private bool[][][] Cubes;
        public (int _min, int _max) x;
        public (int _min, int _max) y;
        public (int _min, int _max) z;

        public int[] xRange => Enumerable.Range(x._min, x._max - x._min).ToArray();
        public int[] yRange => Enumerable.Range(y._min, y._max - y._min).ToArray();
        public int[] zRange => Enumerable.Range(z._min, z._max - z._min).ToArray();

        public Matrix3D(int x_min, int x_max, int y_min, int y_max, int z_min, int z_max)
        {
            bool[][][] tem = new bool[x_max - x_min][][];
            for (int i = 0; i < x_max - x_min; i++)
            {
                tem[i] = new bool[y_max - y_min][];
                for (int j = 0; j < y_max - y_min; j++)
                {
                    tem[i][j] = new bool[z_max - z_min];
                }
            }

            Cubes = tem;
            x = (x_min, x_max);
            y = (y_min, y_max);
            z = (z_min, z_max);
        }

        public bool this[int i, int j, int k]
        {
            get
            {
                int x_i = i + (x._min*-1);
                int y_i = j + (y._min*-1);
                int z_i = k + (z._min*-1);

                return Cubes[x_i][y_i][z_i];
            }
            set
            {
                int x_i = i + (x._min*-1);
                int y_i = j + (y._min*-1);
                int z_i = k + (z._min*-1);

                Cubes[x_i][y_i][z_i] = value;
            }
        }

        public Matrix3D Expand()
        {
            return new Matrix3D(x._min - 1, x._max + 1, y._min - 1, y._max + 1, z._min - 1, z._max + 1);
        }

        public Matrix3D Cycle()
        {
            var tem = this.Expand();
            foreach (var i in tem.xRange)
            {
                foreach (var j in tem.yRange)
                {
                    foreach (var k in tem.zRange)
                    {
                        int active = ActiveNeighbors(i, j, k);
                        bool status = IsIndexValid(i, j, k) ? this[i, j, k] : false;
                            if (status)
                            {
                                if (active == 2 || active == 3)
                                {
                                    tem[i, j, k] = true;
                                }
                                else
                                {
                                    tem[i, j, k] = false;
                                }
                            }
                            else
                            {
                                if (active == 3)
                                {
                                    tem[i, j, k] = true;
                                }
                                else
                                {
                                    tem[i, j, k] = false;
                                }
                            }
                        
                    }
                }
            }

            return tem;
        }

        private bool IsIndexValid(int i, int j, int k)
        {
            return xRange.Contains(i) && yRange.Contains(j) && zRange.Contains(k);
        }

        private int ActiveNeighbors(int i, int j, int k)
        {
            int[] l1 = new[] {i - 1, i, i + 1};
            int[] l2 = new[] {j - 1, j, j + 1};
            int[] l3 = new[] {k - 1, k, k + 1};

            int res = 0;

            foreach (var c in l1)
            {
                foreach (var d in l2)
                {
                    foreach (var e in l3)
                    {
                        if (i != c || j != d || k != e)
                        {
                            if (IsIndexValid(c,d,e))
                            {
                                if (this[c,d,e])
                                {
                                    res += 1;
                                }
                            }
                        }
                    }
                }
            }

            return res;
        }

        public int TotalActive()
        {
            return Cubes
                .Sum(m => m.Sum(n => n.Count(o => o)));
        }
        
    }
}