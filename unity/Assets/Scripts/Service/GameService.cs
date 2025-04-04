using System;
using UnityEngine;

namespace Basic.Service
{
    public class GameService : MonoBehaviour
    {
        private static GameService _instance;
        public static GameService Instance 
        {
            get
            {
                if(_instance != null)
                    return _instance;
            
                GameObject gameService = new GameObject();
                gameService.name = "GameService";
                DontDestroyOnLoad(gameService);
                
                _instance = gameService.AddComponent<GameService>();
                return _instance;
            }
        }
        
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
        
        public void AddDiamond(int userId, int amount, string token, Action<string> onRequestComplete, Action<string> onRequestError)
        {
            WWWForm form = new WWWForm();
            form.AddField("user_id", userId);
            form.AddField("amount", amount);
            form.AddField("token", token);
        
            string serviceName = "items/diamond/add";
        
            ServiceManager.SendPostUnityWebRequest(this,SERVICE_URL + serviceName,
                form,onRequestComplete, onRequestError);
        }
    
    }
}