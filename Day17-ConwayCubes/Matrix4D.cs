using System.Linq;

namespace Day17_ConwayCubes
{
    public class Matrix4D
    {
        private bool[][][][] Cubes;
        public (int _min, int _max) x;
        public (int _min, int _max) y;
        public (int _min, int _max) z;
        public (int _min, int _max) w;

        public int[] xRange => Enumerable.Range(x._min, x._max - x._min).ToArray();
        public int[] yRange => Enumerable.Range(y._min, y._max - y._min).ToArray();
        public int[] zRange => Enumerable.Range(z._min, z._max - z._min).ToArray();
        
        public int[] wRange => Enumerable.Range(w._min, w._max - w._min).ToArray();

        public Matrix4D(int x_min, int x_max, int y_min, int y_max, int z_min, int z_max, int w_min, int w_max)
        {
            bool[][][][] tem = new bool[x_max - x_min][][][];
            for (int i = 0; i < x_max - x_min; i++)
            {
                tem[i] = new bool[y_max - y_min][][];
                for (int j = 0; j < y_max - y_min; j++)
                {
                    tem[i][j] = new bool[z_max - z_min][];
                    for (int k = 0; k < z_max - z_min; k++)
                    {
                        tem[i][j][k] = new bool[w_max - w_min];
                    }
                }
            }

            Cubes = tem;
            x = (x_min, x_max);
            y = (y_min, y_max);
            z = (z_min, z_max);
            w = (w_min, w_max);
        }

        public bool this[int i, int j, int k, int l]
        {
            get
            {
                int x_i = i + (x._min*-1);
                int y_i = j + (y._min*-1);
                int z_i = k + (z._min*-1);
                int w_i = l + (w._min*-1);

                return Cubes[x_i][y_i][z_i][w_i];
            }
            set
            {
                int x_i = i + (x._min*-1);
                int y_i = j + (y._min*-1);
                int z_i = k + (z._min*-1);
                int w_i = l + (w._min*-1);

                Cubes[x_i][y_i][z_i][w_i] = value;
            }
        }

        public Matrix4D Expand()
        {
            return new Matrix4D(x._min - 1, x._max + 1,
                y._min - 1, y._max + 1,
                z._min - 1, z._max + 1,
                w._min - 1, w._max +1);
        }

        public Matrix4D Cycle()
        {
            var tem = this.Expand();
            foreach (var i in tem.xRange)
            {
                foreach (var j in tem.yRange)
                {
                    foreach (var k in tem.zRange)
                    {
                        foreach (var l in tem.wRange)
                        {
                            int active = ActiveNeighbors(i, j, k, l);
                            bool status = IsIndexValid(i, j, k, l) ? this[i, j, k, l] : false;
                            if (status)
                            {
                                if (active == 2 || active == 3)
                                {
                                    tem[i, j, k, l] = true;
                                }
                                else
                                {
                                    tem[i, j, k, l] = false;
                                }
                            }
                            else
                            {
                                if (active == 3)
                                {
                                    tem[i, j, k, l] = true;
                                }
                                else
                                {
                                    tem[i, j, k, l] = false;
                                }
                            }
                        }
                        
                    }
                }
            }

            return tem;
        }

        private bool IsIndexValid(int i, int j, int k, int l)
        {
            return xRange.Contains(i) && yRange.Contains(j) && zRange.Contains(k) && wRange.Contains(l);
        }

        private int ActiveNeighbors(int i, int j, int k, int l)
        {
            int[] l1 = new[] {i - 1, i, i + 1};
            int[] l2 = new[] {j - 1, j, j + 1};
            int[] l3 = new[] {k - 1, k, k + 1};
            int[] l4 = new[] {l - 1, l, l + 1};

            int res = 0;

            foreach (var c in l1)
            {
                foreach (var d in l2)
                {
                    foreach (var e in l3)
                    {
                        foreach (var f in l4)
                        {
                            if (i != c || j != d || k != e || l != f)
                            {
                                if (IsIndexValid(c,d,e, f))
                                {
                                    if (this[c,d,e,f])
                                    {
                                        res += 1;
                                    }
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
                .Sum(m => m.Sum(n => n.Sum(o => o.Count(p => p))));
        }
    }
}