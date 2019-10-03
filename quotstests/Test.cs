using netquots;
using netquots.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;

namespace quotstests
{
    [TestFixture()]
    public class Test
    {

        private readonly INetQuots _netQuots;

        public Test() 
        {
            _netQuots = new NetQuots("http://localhost:8002", "csharp", "gnn3tAiYSt2waiSvw3");
        }


        [Test()]
        public async System.Threading.Tasks.Task TestCreateUser()
        {
            Console.WriteLine("Running create user test!");
            System.Net.Http.HttpResponseMessage response = await _netQuots.CreateUser("csuser", "csemail", "csusername");
            HttpStatusCode status = response.StatusCode;
            var content = response.Content;
            string json = await response.Content.ReadAsStringAsync();
           
            if (status.ToString() == "Conflict") 
            {
                ErrorReport errorReport = JsonConvert.DeserializeObject<ErrorReport>(json);
                Assert.AreEqual(409, errorReport.status);
            }
            else 
            {
                QuotsUser qus = JsonConvert.DeserializeObject<QuotsUser>(json);
                Assert.AreEqual("csuser", qus.Id);
            }
        }

        [Test()]
        public async System.Threading.Tasks.Task TestGetUser()
        {
            Console.WriteLine("Running get user test!");
            System.Net.Http.HttpResponseMessage response = await _netQuots.GetUser("csuser");
            HttpStatusCode status = response.StatusCode;
            var content = response.Content;
            string json = await response.Content.ReadAsStringAsync();

            if (status.ToString() == "NotFound")
            {
                ErrorReport errorReport = JsonConvert.DeserializeObject<ErrorReport>(json);
                Assert.AreEqual(404, errorReport.status);
            }
            else
            {
                QuotsUser qus = JsonConvert.DeserializeObject<QuotsUser>(json);
                Assert.AreEqual("csuser", qus.Id);
            }
        }

        [Test()]
        public async System.Threading.Tasks.Task TestCanUserProceed()
        {
            Console.WriteLine("Running can user proceed!");
            System.Net.Http.HttpResponseMessage response = await _netQuots.CanUserProceed("csuser", "TASK", "1");
            HttpStatusCode status = response.StatusCode;
            var content = response.Content;
            string json = await response.Content.ReadAsStringAsync();

            if (status.ToString() == "BadRequest")
            {
                ErrorReport errorReport = JsonConvert.DeserializeObject<ErrorReport>(json);
            }
            else
            {
                CanProceed cp = JsonConvert.DeserializeObject<CanProceed>(json);
                Assert.AreEqual("csuser", cp.userid);
            }
        }

        [Test()]
        public async System.Threading.Tasks.Task TesUpdateUserCredits()
        {
            Console.WriteLine("Running updating users credits!");
            QuotsUser qu = new QuotsUser();
            qu.Id = "csuser";
            qu.Email = "csemail";
            qu.Username = "csusername";
            qu.Credits = 40.0f;
            System.Net.Http.HttpResponseMessage response = await _netQuots.UpdateUserCredits(qu);
            HttpStatusCode status = response.StatusCode;
            var content = response.Content;
            string json = await response.Content.ReadAsStringAsync();
            if (status.ToString() == "NotFound")
            {
                ErrorReport errorReport = JsonConvert.DeserializeObject<ErrorReport>(json);
            }
            else
            {
                QuotsUser cp = JsonConvert.DeserializeObject<QuotsUser>(json);
                Assert.AreEqual(40.0f, cp.Credits);
            }
        }


        [Test()]
        public async System.Threading.Tasks.Task TestDeleteUser()
        {
            Console.WriteLine("Running delete user test!");
            System.Net.Http.HttpResponseMessage response = await _netQuots.DeleteUser("csuser");
            HttpStatusCode status = response.StatusCode;
            var content = response.Content;
            string json = await response.Content.ReadAsStringAsync();
            if (status.ToString() != "Gone")
            {
                ErrorReport errorReport = JsonConvert.DeserializeObject<ErrorReport>(json);
            }
            else
            {
                int cp = JsonConvert.DeserializeObject<int>(json);
                Assert.AreEqual("Gone", status.ToString());
            }
        }


    }
}
