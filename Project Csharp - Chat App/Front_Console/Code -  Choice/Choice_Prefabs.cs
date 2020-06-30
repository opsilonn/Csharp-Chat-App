﻿using System.Collections.Generic;


namespace Front_Console
{
	public static class Choice_Prefabs
    {
        public static Choice CHOICE_CONNECTION = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
                "Log in",
                "Sign in",
                "Leave the Program"
           }
       );


        public static Choice CHOICE_CONNECTED = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
               "See Profile Data",
               "Browse all topics",
               "Create a new topic",
               "Browse all private chats",
               "Create a new private chat",
               "Log out"
           }
       );


        public static Choice CHOICE_TOPIC = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
               "See all Messages",
               "Add new Message"
           }
       );

        public static string CHOICE_TOPIC_JOIN = "Join the Topic";
        public static string CHOICE_TOPIC_LEAVE = "Leave the Topic";
        public static string CHOICE_TOPIC_GOBACK = "Go Back";



        public static Choice CHOICE_CHAT = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
               "See all Messages",
               "Add new Message",
               "Invite someone to the Chat",
               "Leave the Chat",
               "Go Back"
           }
       );
    }
}
