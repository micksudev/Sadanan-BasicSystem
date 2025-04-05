using System;
using System.Threading.Tasks;
using Basic.Player;
using Basic.Service;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basic.Scenes.Login.Manager
{
    public class LoginSceneManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private GameObject goDialogRoot;
        #endregion

        #region Runtime
        private LoginUIManager ui;
        private GameService gameService;
        #endregion

        #region Init
        private void Start()
        {
            gameService = GameService.Instance;
            
            ui = goDialogRoot.AddComponent<LoginUIManager>();
            ui.OnSignUp = OnSignUp;
            ui.OnLogin = OnLogin;
            ui.Init();
            
        }
        #endregion

        #region Service

        private async void OnSignUp(string username, string password)
        {
            ui.OpenAccessLoading("Registering...");
            await Task.Delay(1000);
            
            gameService.Register(username,password,jsonString =>
            {
                DebugLog(jsonString);
                var json = JObject.Parse(jsonString);
                string message = (string)json["message"];
                int code = (int)json["code"];

                ui.CloseAccessLoading(() =>
                {
                    ui.OpenPrompt(message,() =>
                    {
                        if (code == 200)
                            ui.CloseSignUpAndOpenLogin();
                    });
                });
            }, OnServiceError);
            
        }
        
        private async void OnLogin(string username, string password)
        {
            ui.OpenAccessLoading("Logging in...");
            await Task.Delay(1000);
            gameService.Login(username,password,jsonString =>
            {
                DebugLog(jsonString);
                var json = JObject.Parse(jsonString);
                string message = (string)json["message"];
                int code = (int)json["code"];
                
                ui.CloseAccessLoading(()=>
                {
                    ui.OpenPrompt(message, () =>
                    { 
                        if (code != 200)
                            return;
                        
                        var playerInfo = PlayerInfo.Instance;
                    
                        playerInfo.SetInfo((JObject)json["user"], (string)json["token"]);
                        playerInfo.SetData((JObject)json["user_data"]);
                        
                        ui.CloseLogin(() =>
                        {
                            SceneManager.LoadScene("s_Lobby");
                        });
                    });
                });
            }, OnServiceError);
        }

        private void OnServiceError(string error)
        {
            DebugLog("ServiceError: " + error, true);
            ui.CloseAccessLoading();
        }
        #endregion

        #region Debug
        protected void DebugLog(object log, bool isError = false)
        {
            string textColor = isError ? "<color=red>" : "<color=cyan>";
            string message = $"[{Time.frameCount}] {textColor}{name}</color>.{log};";
            Debug.Log(message);
        }
        #endregion
        
    }
}
