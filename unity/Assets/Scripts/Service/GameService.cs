using System;
using UnityEngine;

namespace Basic.Service
{
    public class GameService : MonoBehaviour
    {
        private const string SERVICE_URL = "http://localhost/restful/";
    
        public void Register(string username, string password, Action<string> onRequestComplete, Action<string> onRequestError)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);
        
            string serviceName = "users/register";
        
            ServiceManager.SendPostUnityWebRequest(this,SERVICE_URL + serviceName,
                form,onRequestComplete, onRequestError);
        }
        
        public void Login(string username, string password, Action<string> onRequestComplete, Action<string> onRequestError)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);
        
            string serviceName = "users/login";
        
            ServiceManager.SendPostUnityWebRequest(this,SERVICE_URL + serviceName,
                form,onRequestComplete, onRequestError);
        }
    
    }
}