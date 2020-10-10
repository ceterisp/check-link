using CheckLinkCLI2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinkCLI2.General
{
    internal static class Utility
    {
        public static void ProgressBar(int progressCount, int totalCount)
        {
            //Draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / totalCount;

            //Draw filled part
            int position = 1;
            for (int i = 0; i <= onechunk * progressCount; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
                //if (progressCount == totalCount)
                //    Console.CursorLeft = (int)(onechunk * progressCount);
            }

            //Draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //Draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progressCount.ToString() + " of " + totalCount.ToString() + " links parsed    "); //blanks at the end remove any excess
        }
    }
}
