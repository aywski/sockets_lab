using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sockets_lab
{
    internal class Algorithms
    {
        static public int MonteCarlo(int randomNum, int time) // time in milliseconds
        {
            DateTime dtStatic = DateTime.Now;
            DateTime dtDynamic = DateTime.Now;
            Random rnd = new Random();
            int mcNum = rnd.Next(0, 2000000);
            int temp = 0;

            while (dtDynamic <= dtStatic.AddMilliseconds(time))
            {
                if (mcNum == randomNum)
                {
                    return mcNum;
                    break;
                }
                else if (mcNum < randomNum)
                {
                    temp = mcNum;
                    mcNum = rnd.Next(temp, 2000000);
                }
                else if (mcNum > randomNum)
                {
                    temp = mcNum;
                    mcNum = rnd.Next(0, temp);
                }
                dtDynamic = DateTime.Now;
            }
            return mcNum;

        }
    }
}


