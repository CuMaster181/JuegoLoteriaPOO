using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal static class PatronVictoria
    {
        public static readonly Dictionary<TipoVictoria, int[,]>
            Patrones = new()
        {
            {
                TipoVictoria.Cruzita,
                new int[,]
                {
                    {0,1},
                    {1,0},
                    {1,1},
                    {1,2},
                    {2,1}
                }
            },

            {
                TipoVictoria.T,
                new int[,]
                {
                    {0,0},
                    {0,1},
                    {0,2},
                    {1,1},
                    {2,1}
                }
            },

            {
                TipoVictoria.Pollita,
                new int[,]
                {
                    {0,0},
                    {0,2},
                    {1,1},
                    {2,0},
                    {2,2}
                }
            },

            {
                TipoVictoria.L,
                new int[,]
                {
                    {0,0},
                    {1,0},
                    {2,0},
                    {2,1},
                    {2,2}
                }
            },

            {
                TipoVictoria.J,
                new int[,]
                {
                    {0,2},
                    {1,2},
                    {2,0},
                    {2,1},
                    {2,2}
                }
            }
        };
    }
}
