using System.Collections.Generic;


namespace Front_Console
{
    /// <summary>
    /// Structure of a Choice : a message and several propositions
    /// </summary>
    public class Choice
    {
        public string message;
        public List<string> choices;

        /// <summary>
        /// Default constructor of the instance
        /// </summary>
        public Choice()
        {
            message = "";
            choices = new List<string>();
        }


        /// <summary>
        /// Complete constructor of the instance
        /// </summary>
        /// <param name="message"> Message of the instance </param>
        /// <param name="choices"> List of choices of the instance </param>
        public Choice(string message, List<string> choices)
        {
            this.message = message;
            this.choices = choices;
        }
    }
}