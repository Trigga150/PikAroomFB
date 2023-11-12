using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikAroomFB.Repository.Apply
{
    public interface IApply
    {
        void AddApplication(PikAroomFB.Models.Apply apply);
        void RemoveApplication(string ApplyId);
        PikAroomFB.Models.Apply ShowApplication(string applyId);

        List<Models.Apply> GetAllApplications();
        Task EditApplication(PikAroomFB.Models.Apply apply);
    }
}
