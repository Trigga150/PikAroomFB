using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikAroomFB.Repository.Property
{
    internal interface IProperty
    {
        void AddProperty(PikAroomFB.Models.Property property);
        void RemoveProperty(string Id);
        PikAroomFB.Models.Property ShowProperty(string Id);

        List<Models.Property> PropertyList();
        void EditProperty(PikAroomFB.Models.Property property);

    }
}
