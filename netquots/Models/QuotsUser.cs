//
//  Copyright 2019  Pantelis Karatzas
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
