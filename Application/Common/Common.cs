using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class Common
    {
        /// <summary>
        /// Calculate result of Rock-Paper-Scissors-Spock-Lizzard game
        /// </summary>
        /// <returns>
        ///     0-if player1 lost,
        ///     2-if player1 won,
        ///     1-if it is tie
        /// </returns>
        public static int PlayTheGame(string player1, string player2)
        {
            int retVal = 1;
            switch (player1)
            {
                case "rock":
                    {
                        if (player2 == "spock" || player2 == "paper")
                        {
                            retVal = 0;
                        }
                        else
                        {
                            retVal = 2;
                        }
                        break;
                    }
                    
                default:
                    break;
            }

            return retVal;
        }
    }
}
