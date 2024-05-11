using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleFacebookSignIn.Scripts;

namespace Assets.SimpleFacebookSignIn
{
    public class Example : MonoBehaviour
    {
        public FacebookAuth FacebookAuth;
        public Text Log;
        public Text Output;
        
        public void Start()
        {
            Application.logMessageReceived += (condition, _, _) => Log.text += condition + '\n';
            FacebookAuth = new FacebookAuth();
        }

        public void SignIn()
        {
            FacebookAuth.FBSignIn(OnSignIn, caching: true);
        }

        public void SignOut()
        {
            FacebookAuth.FBSignOut(revokeAccessToken: true);
            Output.text = "Not signed in";
        }

        public void GetAccessToken()
        {
            FacebookAuth.FBGetAccessToken(OnGetAccessToken);
        }

        private void OnSignIn(bool success, string error, UserInfo userInfo)
        {
            Output.text = success ? $"Hello, {userInfo.name}!" : error;
        }

        private void OnGetAccessToken(bool success, string error, TokenResponse tokenResponse)
        {
            Output.text = success ? $"Access token: {tokenResponse.AccessToken}" : error;

            if (!success) return;

            var jwt = new JWT(tokenResponse.IdToken);

            Debug.Log($"JSON Web Token (JWT) Payload: {jwt.Payload}");
            
            jwt.ValidateSignature(FacebookAuth.ClientId, OnValidateSignature);
        }

        private void OnValidateSignature(bool success, string error)
        {
            Output.text += Environment.NewLine;
            Output.text += success ? "JWT signature validated" : error;
        }

        public void Navigate(string url)
        {
            Application.OpenURL(url);
        }
    }
}