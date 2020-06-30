using System;

namespace Back_Communication
{
	public static class PrefabMessage
    {
        public static string INCOMPLETE_FIELDS = "INCOMPLETE FIELDS !!";

        public static string INCORRECT_INPUT = "you didn't entered a correct value !!";
        public static string INCORRECT_INPUT_CHARACTER = "you entered a character which wasn't allowed !!";
        public static string INCORRECT_INPUT_SIZE = "your input(s) are too long !!";

        public static string LOGIN_FAILURE = "The Credentials are incorrect : please try again";

        public static string SIGNIN_PASSWORD_DONT_MATCH = "The password and its verification are different : please try again";
        public static string SIGNIN_FAILURE = "There already is a user with your name and / or email !";

        public static string MESSAGE_EMPTY = "This conversation is empty ! Be the first to add a comment !";
        public static string MESSAGE_SUCCESS = "You successfully added a message !";

        public static string TOPIC_CREATION_SUCCESS = "The new Topic was successfully created !";
        public static string TOPIC_CREATION_FAILURE = "The Topic hasn't been created...";

        public static string CHAT_CREATION_SUCCESS = "The new Chat was successfully created !";
        public static string CHAT_CREATION_FAILURE = "The Chat hasn't been created...";
        public static string CHAT_CREATION_2ND_MEMBER_IS_SELF = "You cannot add yourself as the 2nd member of this Chat !";
        public static string CHAT_CREATION_2ND_MEMBER_NOT_FOUND = "Your correspondent's name was not found !";

        public static string NEWCHATMEMBER_SUCCESS = "The new member was successfully added !";
        public static string NEWCHATMEMBER_FAILURE = "The profile hasn't been added...";
        public static string NEWCHATMEMBER_ADD_SELF = "You cannot add yourself as another new member of this Chat !";

        // Maximum Input sizes
        public static int INPUT_MAXSIZE_PROFILE_ID = 6;
        public static int INPUT_MAXSIZE_PROFILE_NAME = 25;
        public static int INPUT_MAXSIZE_PROFILE_PASSWORD = 25;
        public static int INPUT_MAXSIZE_PROFILE_EMAIL = 50;

        public static int INPUT_MAXSIZE_STRUCTURE_NAME = 50;
        public static int INPUT_MAXSIZE_STRUCTURE_DESCRIPTION = 100;

        public static int INPUT_MAXSIZE_PROFILE_CONTENT = 100;




        /// <summary>
        /// Returns whether a string contains correct inputs
        /// </summary>
        /// <param name="input"> The string entered by the user </param>
        /// <returns> Whether the input verifies some conditions or not </returns>
        public static bool CorrectInput(string input)
        {
            // Foreach character in the string
            foreach (char c in input)
            {
                // We get the ascii value of the current char
                int unicode = c;

                // We do some verifications
                bool space = (unicode == 32);
                bool number = (48 <= unicode && unicode <= 57);
                bool letterUpper = (65 <= unicode && unicode <= 90);
                bool letterLower = (97 <= unicode && unicode <= 122);
                bool punctuation = (c == '?' || c == '!' || c == '.' || c == ',' || c == ';');
                bool specialChars = (c == '@' || c == '\'' || c == '-' || c == '<' || c == '>');


                // If the current char doesn't pas a single verification, we return false
                if (!space && !number && !letterUpper && !letterLower && !punctuation && !specialChars)
                {
                    return false;
                }
            }

            // Otherwise, we return true
            return true;
        }
    }
}