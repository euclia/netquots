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
using System.Net.Http;
using System.Threading.Tasks;
using netquots.Models;

namespace netquots
{
    public interface INetQuots
    {
        /// <summary>This method Creates a user if he does not exist on the Quots Application
        /// <example>For example:
        /// <code>
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be the user created or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        Task<HttpResponseMessage> CreateUser(string id, string username, string password);
        /// <summary>This method Get's a user from the Quots Application
        /// <example>For example:
        /// <code>
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be the user or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        Task<HttpResponseMessage> GetUser(string id);
        /// <summary>Checks if a user can continue further down with task 
        /// <example>For example:
        /// <code>
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be a CanProceed object or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        Task<HttpResponseMessage> CanUserProceed(string id, string usageType, string usageSize);
        /// <summary>Updates the user credits. The passed credits will be the new users credits
        /// <example>For example:
        /// <code>
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be the user created or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        Task<HttpResponseMessage> UpdateUserCredits(QuotsUser qu);
        /// <summary>Deletes the user.
        /// <example>For example:
        /// <code>
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be "1" if everything went well or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        Task<HttpResponseMessage> DeleteUser(string id);
    }
}
