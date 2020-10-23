using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class User
    {
        public string ClientNo { get; set; }
        public bool Login { get; set; }

        public DateTime ConnectingTime { get; set; }
        public DateTime ConnectedTime { get; set; }
        public DateTime SendLoginCmdTime { get; set; }
        public DateTime ReceiveLogonTime { get; set; }
    }
}
