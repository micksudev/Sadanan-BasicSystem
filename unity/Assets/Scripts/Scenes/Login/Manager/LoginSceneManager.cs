using System;
using System.Threading.Tasks;
using Basic.Service;
using UnityEngine;

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
            gameService = gameObject.AddComponent<GameService>();
            
            ui = goDialogRoot.AddComponent<LoginUIManager>();
            ui.OnSignUp = OnSignUp;
            ui.Init();
            
        }
        #endregion


        #region Service

        private async void OnSignUp(string username, string password)
        {
            ui.OpenAccessLoading("Registering...");
            await Task.Delay(1000);
            
            gameService.Register(username,password,json =>
            {
                DebugLog(json);
                ui.CloseAccessLoading(() =>
                {
                    ui.OpenPrompt("Register Success!",ui.CloseSignUpAndOpenLogin);
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
