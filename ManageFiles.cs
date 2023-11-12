using Firebase.Storage;
using FireSharp;
using FireSharp.Interfaces;
using PikAroomFB.Repository.DataConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PikAroomFB
{
    public class ManageFiles : IDisposable
    {
        private FirebaseConnect _connect;
        private Firebase.Auth.IFirebaseAuthProvider _authProvider;
        private IFirebaseClient _firebaseClient;
        public ManageFiles() 
        {
            _connect = new FirebaseConnect();
            _firebaseClient = _connect.firebaseClient;
            _authProvider = _connect.authProvider;
        }

        public async Task<string> Upload(FileStream stream, string fileName)
        {
            string returnLink_error = string.Empty;
            var getUserToken = await _authProvider.SignInWithEmailAndPasswordAsync(FirebaseConstants.FromMail,FirebaseConstants.FromPsw);
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                FirebaseConstants.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(getUserToken.FirebaseToken),
                    ThrowOnCancel = true
                }
                )
                .Child("images")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task;
                returnLink_error = link;
            }
            catch (Exception ex)
            {
                returnLink_error = "Error occured during upload" + ex.Message;
            }

            return returnLink_error;

        }
        public async Task Delete(string fileName)
        {
            string returnLink_error = string.Empty;
            var getUserToken = await _authProvider.SignInWithEmailAndPasswordAsync(FirebaseConstants.FromMail, FirebaseConstants.FromPsw);
            var cancellation = new CancellationTokenSource();

            var storage = new FirebaseStorage(
                FirebaseConstants.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(getUserToken.FirebaseToken),
                    ThrowOnCancel = true
                }
            );

            var fileReference = storage
                .Child("images")
                .Child(fileName).DeleteAsync();

            // Delete the file
            //await fileReference.DeleteAsync(cancellation.Token);
        }


       

        public void Dispose()
        {
            this.Dispose();
        }
    }
}