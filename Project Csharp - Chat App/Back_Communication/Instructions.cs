using System;
using System.Collections.Generic;
using System.Text;

namespace Back_Communication
{
    public enum Instructions
    {
        Exit_Software,

        LogIn,
        SignIn,
        LogOut,

        Profile_GetAll,
        Profile_GetByID,
        Profile_GetByName,

        Topic_GetAll,
        Topic_GetByID,
        Topic_New,

        Chat_GetAll,
        Chat_GetByID,
        Chat_New,

        Member_Join,
        Member_Leave,

        Message_New,
        Message_GetByID
    }
}
