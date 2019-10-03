using System;
using System.Collections.Generic;

namespace netquots.Models
{
    public class Spenton
    {

        private String appid;
        public String Appid { get { return appid; } set { appid = value; } }
        private Dictionary<String, Object> usage;
        public Dictionary<String, Object> Usage { get { return usage; } set { usage = value; } }
        public Spenton()
        {
        }
    }
}
