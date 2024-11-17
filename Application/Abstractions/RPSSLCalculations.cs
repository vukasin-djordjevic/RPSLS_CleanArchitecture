using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public class RPSSLCalculations
    {
        public static int GetChoiceByRandomNumber(int randomNumber)
        {
            int choice = 0;

            switch (randomNumber)
            {
                case int n when (n >= 1 && n <= 20):
                    choice = 1;
                    break;
                case int n when (n >= 21 && n <= 40):
                    choice = 2;
                    break;
                case int n when (n >= 41 && n <= 60):
                    choice = 3;
                    break;
                case int n when (n >= 61 && n <= 80):
                    choice = 4;
                    break;
                case int n when (n >= 81 && n <= 100):
                    choice = 5;
                    break;
            }

            return choice;
        }


        /// <summary>
        /// Calculates result of Rock-Paper-Scissors-Spock-Lizard game.
        /// How it works: When order of choices is Rock-Paper-Scissors-Spock-Lizard
        /// or any cyclic permutation of it, and if we index choices positions with
        /// indexes from 1 to 5, than choice on position 1 will allways win choices in 
        /// positions 3 and 5, and will allways lose from choices in positions 2 and 4.
        /// 
        /// Since our choices are given in a bit "wrong order" (instead of 1-2-3-4-5, it 
        /// is 1-2-3-5-4), we firstly have to remap our inputs (this is done by using
        /// TransformChoiceToBeInLineWithCyclicPermutation method, by simply swapping 
        /// choice 4 with choice 5 and vice versa; any other choice will not be remaped) 
        /// and after that, we "rotate" or "shift" player to position 1, as well as computer 
        /// for the same amount of places, and than we compare new indexes of player and computer, 
        /// applying previously mentioned rule: 
        /// In order [Rock-Paper-Scissors-Spock-Lizard]/[1-2-3-4-5] 1 loses of [2, 4] and  1 wins [3, 5]
        /// (again, since our original order is [Rock-Paper-Scissors-Spock-Lizard]/[1-2-3-4-5], then
        /// 1 loses of [2,5] and wins [3,4] and this is what clients will get as response)
        /// </summary>
        /// <param name="playersChoice">Player's choice</param>
        /// <param name="computersChoice"></param>
        /// <returns></returns>
        public static string PlayTheGame(int playersChoice, int computersChoice)
        {
            var transformedPlayerIndex = TransformChoiceToBeInLineWithCyclicPermutation(playersChoice);

            // this variable will keep the infromation
            // about rotation of the player's choice
            int deltaPlayer = 0;

            if (transformedPlayerIndex > 1)
            {
                // 
                deltaPlayer = transformedPlayerIndex - 1;
            }

            var transformedCopmuterIndex = TransformChoiceToBeInLineWithCyclicPermutation(computersChoice);

            // here we "rotate" computer's choice
            // as much as we "rotated" player's choice
            var rotatedComputerIndex = transformedCopmuterIndex - deltaPlayer;

            while (rotatedComputerIndex < 1)
            {
                rotatedComputerIndex += 5;
            }

            string retVal = string.Empty;

            if (rotatedComputerIndex == 1)
            {
                retVal = "tie";
            }
            else if (rotatedComputerIndex == 2 || rotatedComputerIndex == 4)
            {
                retVal = "lose";
            }
            else
            {
                retVal = "win";
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        private static int TransformChoiceToBeInLineWithCyclicPermutation(int choice)
        {
            int retVal = choice;

            if (choice == 4)
            {
                retVal = 5;
            }
            else if (choice == 5)
            {
                retVal = 4;
            }

            return retVal;
        }
    }
}
