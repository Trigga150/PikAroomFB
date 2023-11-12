using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PikAroomFB.Repository.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PikAroomFB.Repository.Apply
{
    public class ApplyRepository : IApply, IDisposable
    {
        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public ApplyRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }
        public void AddApplication(Models.Apply apply)
        {
            var applyData = apply;
            PushResponse pushResponse = _firebaseClient.Push("Apply/", applyData);
            applyData.ApplyId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("Apply/" + applyData.ApplyId, applyData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public Task EditApplication(Models.Apply apply)
        {
            throw new NotImplementedException();
        }

        public List<Models.Apply> GetAllApplications()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Apply");
            dynamic applyData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var applicationList = new List<Models.Apply>();
            if(applyData != null)
            {
                foreach(var  application in applyData)
                {
                    applicationList.Add(JsonConvert.DeserializeObject<Models.Apply>(((JProperty)application).Value.ToString()));
                }
            }
            return applicationList;

        }

        public void RemoveApplication(string applyId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Contact/"+ applyId);
        }

        public Models.Apply ShowApplication(string applyId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Contact/" + applyId);
            dynamic apply = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            return apply;

        }
    }
}