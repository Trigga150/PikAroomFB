using Firebase.Auth;
using FireSharp;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PikAroomFB.Repository.DataConnection
{
    public class FirebaseConnect :IDisposable
    {
        public IFirebaseClient firebaseClient;
        public IFirebaseAuthProvider authProvider;

        public FirebaseConnect()
        {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig()
            {
                AuthSecret = FirebaseConstants.AuthorizationSecret,
                BasePath = FirebaseConstants.FirebaseDatabaseAdress

            };

            firebaseClient = new FireSharp.FirebaseClient(config);

            authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(FirebaseConstants.Web_ApiKey));
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}