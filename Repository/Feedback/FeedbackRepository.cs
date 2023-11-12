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

namespace PikAroomFB.Repository.Feedback
{
    public class FeedbackRepository : IApply, IDisposable
    {
        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public FeedbackRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }
        public void AddFeedback(Models.Feedback feedback)
        {
            var feedbackData = feedback;
            PushResponse pushResponse = _firebaseClient.Push("Feedback/", feedbackData);
            feedbackData.FeedbackId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("Feedback/" + feedbackData.FeedbackId, feedbackData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public Task EditFeedback(Models.Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public List<Models.Feedback> GetAllFeedbacks()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Feedback");
            dynamic feedbackData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var feedBackList = new List<Models.Feedback>();
            if(feedbackData != null)
            {
                foreach(var  feedback in feedbackData)
                {
                    feedBackList.Add(JsonConvert.DeserializeObject<Models.Feedback>(((JProperty)feedback).Value.ToString()));
                }
            }
            return feedBackList;

        }

        public void RemoveFeedback(string FeedbackId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Contact/"+ FeedbackId);
        }

        public Models.Feedback ShowFeedBack(string feedbackId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Contact/" + feedbackId);
            dynamic feedback = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            return feedback;

        }
    }
}