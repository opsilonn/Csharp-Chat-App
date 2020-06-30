using System;
using Back_Communication;


namespace Front_Console
{
    /// <summary>
    /// This Class serves to ask the user to make his choice amongs a list of proposals.
    /// </summary>
    public static class Choice_Selection
    {
        public static int MIN = 0;
        public static int MAX;


        public static int GetChoice(Choice choice)
        {
            // We initialize our variables
            ConsoleKeyInfo input;
            int value = 0;
            MAX = choice.choices.Count - 1;


            // While the user doesn't press Enter, the loop continues
            do
            {
                // We clear the console, and display a given message
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, choice.message + "\n\n");


                // We display all our choices (and we highlight the current choice)
                int index = 0;
                ConsoleColor color = ConsoleColor.White;

                foreach (string s in choice.choices)
                {
                    if (index == value)
                        Console.Write("     --> ");
                    else
                        Console.Write("         ");

                    // We change the color to Blue if we reach the final statement (usually one saying "Go back" or "Log out")
                    if(index == MAX)
                    {
                        color = ConsoleColor.Blue;
                    }
                    
                    CONSOLE.WriteLine(color, choice.choices[index++] + "\n");
                }


                // We read the input
                input = Console.ReadKey();


                // If it is an Array key (UP or DOWN), we modify our choice accordingly
                if (input.Key == ConsoleKey.UpArrow)
                    value--;
                if (input.Key == ConsoleKey.DownArrow)
                    value++;
                // If it is a LEFT Array key : go to the first choice (index = 0)
                if (input.Key == ConsoleKey.LeftArrow)
                    value = 0;
                // If it is a RIGHT Array key : go to the last choice (index = MAX)
                if (input.Key == ConsoleKey.RightArrow)
                    value = MAX;


                // If the value goes too low / too high, it goes to the other extreme
                if (value < MIN)
                    value = MAX;
                if (value > MAX)
                    value = MIN;
            }
            while (input.Key != ConsoleKey.Enter);


            // We return the value
            return value;
        }
    }
}