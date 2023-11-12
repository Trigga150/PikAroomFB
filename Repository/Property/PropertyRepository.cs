using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PikAroomFB.Repository.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PikAroomFB.Repository.Property
{
    public class PropertyRepository : IProperty, IDisposable
    {
        private FirebaseConnect _connect;
        private Firebase.Auth.IFirebaseAuthProvider _authProvider;
        private IFirebaseClient _firebaseClient;
        public PropertyRepository()
        {
            _connect = new FirebaseConnect();
            _firebaseClient = _connect.firebaseClient;
            _authProvider = _connect.authProvider;
        }
        public void AddProperty(Models.Property property)
        {
            var propertyData = property;
            SetResponse setResponse = _firebaseClient.Set("Property/"+ propertyData.PropertyName, propertyData);
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public void EditProperty(Models.Property property)
        {
            var propertyData = property;
            SetResponse firbaseSetResponse = _firebaseClient.Set("Property/"+ propertyData.PropertyName, propertyData);
        }

        public void RemoveProperty(string propertyId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Property/" +  propertyId);

        }

        public List<Models.Property> PropertyList()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Property");
            dynamic propertyData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var propertyList = new List<Models.Property>();
            if (propertyData != null)
            {
                foreach (var property in propertyData)
                {
                    propertyList.Add(JsonConvert.DeserializeObject<Models.Property>(((JProperty)property).Value.ToString()));
                }
            }
            return propertyList;

        }
        public Models.Property ShowProperty(string propertyId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Property/" + propertyId);
            Models.Property property = JsonConvert.DeserializeObject<Models.Property>(firebaseResponse.Body);
            return property;
        }
    }
}