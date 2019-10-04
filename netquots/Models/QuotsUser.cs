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
    /// <summary>A Quots user
    /// </summary>
    public class QuotsUser
    {
        private String id;
        /// <summary>Users id
        /// </summary>
        public String Id { get { return id; } set { id = value; } }
        private String email;
        /// <summary>Users email
        /// </summary>
        public String Email { get { return email; } set { email = value; } }
        private String username;
        /// <summary>Users username
        /// </summary>
        public String Username { get { return username; } set { username = value; } }
        private float credits;
        /// <summary>Users credits
        /// </summary>
        public float Credits { get { return credits; } set { credits = value; } }
        private List<Spenton> spenton;
        /// <summary>Users history of spent
        /// </summary>
        public List<Spenton> Spenton { get { return spenton; } set { spenton = value; } }
    }
}
