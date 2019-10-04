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
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using netquots.Models;

namespace netquots
{
    /// <summary>INetQuots implemtation
    /// </summary>
    public class NetQuots : INetQuots
    {

        private String quotsBase;
        private String appid;
        private String appSecret;
        private readonly HttpClient httpClient;


        /// <summary>NetQuots constructor. Needs the Quots base url, the app id and secret claimed from Quots app
        /// <example>For example:
        /// <code>
        ///  _netQuots = new NetQuots("http://quotshost:8002", "appid", "appsecret");
        /// </code>
        /// </example>
        /// </summary>
        public NetQuots(String quotsBase, String appid, String appSecret)
        {
            this.quotsBase = quotsBase;
            this.appid = appid;
            this.appSecret = appSecret;
            httpClient = new HttpClient();
            httpClient
            .DefaultRequestHeaders
            .Add("Authorization", "QUOTSAPP");
            httpClient.DefaultRequestHeaders.Add("app-id", this.appid);
            httpClient.DefaultRequestHeaders.Add("app-secret", this.appSecret);
        }

        /// <summary>This method Creates a user if he does not exist on the Quots Application
        /// <example>For example:
        /// <code>
        ///    System.Net.Http.HttpResponseMessage response = await _netQuots.CreateUser("csuser", "csemail", "csusername");
        ///    HttpStatusCode status = response.StatusCode;
        ///    var content = response.Content;
        ///    string json = await response.Content.ReadAsStringAsync();
        ///   
        ///    if (status.ToString() == "Conflict") 
        ///    {
        ///        ErrorReport errorReport = JsonConvert.DeserializeObject ErrorReport (json);
        ///        Assert.AreEqual(409, errorReport.status);
        ///    }
        ///    else 
        ///  {
        ///        QuotsUser qus = JsonConvert.DeserializeObject QuotsUser (json);
        ///        Assert.AreEqual("csuser", qus.Id);
        ///    }
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be the user created or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        public async Task<HttpResponseMessage> CreateUser(string id, string username, string email)
        {
            string path = this.quotsBase + "/users";
            QuotsUser qu = new QuotsUser();
            qu.Id = id;
            qu.Email = email;
            qu.Username = username;
            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(QuotsUser));
            ser.WriteObject(stream1, qu);
            var sr = new StreamReader(stream1);
            byte[] json = stream1.ToArray();
            stream1.Close();
            ByteArrayContent jsonContent = new ByteArrayContent(json);

            await httpClient.PostAsync(path, jsonContent);
            return httpClient.PostAsync(path, jsonContent).Result;
        }


        /// <summary>This method Gets a user from quots application
        /// <example>For example:
        /// <code>
        /// System.Net.Http.HttpResponseMessage response = await _netQuots.GetUser("csuser");
        /// HttpStatusCode status = response.StatusCode;
        /// var content = response.Content;
        /// string json = await response.Content.ReadAsStringAsync();
        ///    if (status.ToString() == "NotFound")
        ///    {
        ///        ErrorReport errorReport = JsonConvert.DeserializeObject ErrorReport (json);
        ///        Assert.AreEqual(404, errorReport.status);
        ///    }
        ///    else
        ///    {
        ///        QuotsUser qus = JsonConvert.DeserializeObject QuotsUser (json);
        ///        Assert.AreEqual("csuser", qus.Id);
        ///    }
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be the user created or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>

        public async Task<HttpResponseMessage> GetUser(string id)
        {
            string path = this.quotsBase + "/users/" + id;
            await httpClient.GetAsync(path);
            return httpClient.GetAsync(path).Result;
        }


        /// <summary>This method Creates a user if he does not exist on the Quots Application
        /// <example>For example:
        /// <code>
        /// System.Net.Http.HttpResponseMessage response = await _netQuots.CanUserProceed("csuser", "TASK", "1");
        /// HttpStatusCode status = response.StatusCode;
        /// var content = response.Content;
        /// string json = await response.Content.ReadAsStringAsync();
        ///    if (status.ToString() == "NotFound")
        ///    {
        ///            ErrorReport errorReport = JsonConvert.DeserializeObject ErrorReport (json);
        ///            Assert.AreEqual(404, errorReport.status);
        ///        }
        ///        else
        ///        {
        ///            QuotsUser qus = JsonConvert.DeserializeObject QuotsUser (json);
        ///             Assert.AreEqual("csuser", qus.Id);
        ///        }
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be a CanProceed object or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        public async Task<HttpResponseMessage> CanUserProceed(string id, string usageType, string usageSize)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["appid"] = appid;
            query["usage"] = usageType;
            query["size"] = usageSize;
            string queryString = query.ToString();
            string path = quotsBase + "/users/" + id + "/quots?" + queryString;
            await httpClient.GetAsync(path);
            return httpClient.GetAsync(path).Result;
        }

        /// <summary>This method Updates a user credits. The credits provided will be the new credits of the user!
        /// <example>For example:
        /// <code>
        /// System.Net.Http.HttpResponseMessage response = await _netQuots.UpdateUserCredits(qu);
        /// HttpStatusCode status = response.StatusCode;
        /// var content = response.Content;
        ///string json = await response.Content.ReadAsStringAsync();
        ///    if (status.ToString() == "NotFound")
        ///    {
        ///        ErrorReport errorReport = JsonConvert.DeserializeObject ErrorReport (json);
        ///     }
        ///     else
        ///     {
        ///   QuotsUser cp = JsonConvert.DeserializeObject QuotsUser (json);
        ///   Assert.AreEqual(40.0f, cp.Credits);
        ///     }
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be the user updated or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        public async Task<HttpResponseMessage> UpdateUserCredits(QuotsUser qu)
        {
            string path = quotsBase + "/users/credits";
            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(QuotsUser));
            ser.WriteObject(stream1, qu);
            var sr = new StreamReader(stream1);
            byte[] json = stream1.ToArray();
            stream1.Close();
            ByteArrayContent jsonContent = new ByteArrayContent(json);
            await httpClient.PutAsync(path, jsonContent);
            return httpClient.PutAsync(path, jsonContent).Result;
        }

        /// <summary>This method Deletes a user from quots! Status Gone is returned if everything went well
        /// <example>For example:
        /// <code>
        /// System.Net.Http.HttpResponseMessage response = await _netQuots.UpdateUserCredits(qu);
        /// HttpStatusCode status = response.StatusCode;
        /// var content = response.Content;
        ///string json = await response.Content.ReadAsStringAsync();
        ///     if (status.ToString() != "Gone")
        ///     {
        ///         ErrorReport errorReport = JsonConvert.DeserializeObject ErrorReport (json);
        ///     }
        ///     else
        ///     {
        ///          int cp = JsonConvert.DeserializeObject int (json);
        ///          Assert.AreEqual("Gone", status.ToString());
        ///     }
        /// </code>
        /// results in <c>p</c> The HttpResponseMessage will either be Gone or an ErrorReport if sommething went wrong.
        /// </example>
        /// </summary>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            string path = quotsBase + "/users/" + id;
            await httpClient.DeleteAsync(path);
            return httpClient.DeleteAsync(path).Result;
        }
    }
}
