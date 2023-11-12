using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikAroomFB.Repository.Feedback
{
    public interface IApply
    {
        void AddFeedback(PikAroomFB.Models.Feedback feedback);
        void RemoveFeedback(string FeedbackId);
        PikAroomFB.Models.Feedback ShowFeedBack(string feedbackId);

        List<Models.Feedback> GetAllFeedbacks();
        Task EditFeedback(PikAroomFB.Models.Feedback feedback);
    }
}
