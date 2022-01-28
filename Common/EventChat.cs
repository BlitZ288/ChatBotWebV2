using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coman
{
    public enum EventChat
    {
        CreateChat=1,
        JoinChat, 
        DeleteChat,
        InvitePerson,
        DeletePerson,
        InviteBot,
        DeleteBot,
        SendMessage,
        DeleteMessage
    }
}
