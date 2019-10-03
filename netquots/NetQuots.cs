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
    public class NetQuots : INetQuots
    {

        private String quotsBase;
        private String appid;
        private String appSecret;
        private readonly HttpClient httpClient;

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

        public async Task<HttpResponseMessage> GetUser(string id)
        {
            string path = this.quotsBase + "/users/" + id;
            await httpClient.GetAsync(path);
            return httpClient.GetAsync(path).Result;
        }

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

        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            string path = quotsBase + "/users/" + id;
            await httpClient.DeleteAsync(path);
            return httpClient.DeleteAsync(path).Result;
        }
    }
}
