using System;
using System.Collections.Generic;

namespace netquots.Models
{
    public class QuotsUser
    {
        private String id;
        public String Id { get { return id; } set { id = value; } }
        private String email;
        public String Email { get { return email; } set { email = value; } }
        private String username;
        public String Username { get { return username; } set { username = value; } }
        private float credits;
        public float Credits { get { return credits; } set { credits = value; } }
        private List<Spenton> spenton;
        public List<Spenton> Spenton { get { return spenton; } set { spenton = value; } }

        public QuotsUser()
        {

        }
    }
}
