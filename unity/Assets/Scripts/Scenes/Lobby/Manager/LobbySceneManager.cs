using System;
using System.Collections;
using Basic.Player;
using Basic.Service;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Basic.Scenes.Lobby.Manager
{
    public class LobbySceneManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private GameObject goDialogRoot;
        #endregion
        
        #region Runtime
        private LobbyUIManager ui;
        private GameService gameService;
        private PlayerInfo playerInfo;
        #endregion

        #region Init
        private void Start()
        {
            gameService = GameService.Instance;
            playerInfo = PlayerInfo.Instance;
            
            ui = goDialogRoot.AddComponent<LobbyUIManager>();
            ui.OnBtnAddDiamond =()=> ui.CloseMainMenu(OnAddDiamond);
            ui.Init();

            LoadedComplete();
        }
        
        private void LoadedComplete()
        {
            if (playerInfo.PlayerData == null)
            {
                ui.SetGem("0");
                return;
            }
            
            playerInfo.PlayerData.OnDiamondUpdate += ui.SetGem;
            playerInfo.PlayerData.OnHeartUpdate +=(value)=> ui.SetHeart(int.Parse(value));
            
            ui.SetHeart(int.Parse(playerInfo.PlayerData.Heart));
            ui.SetGem(playerInfo.PlayerData.Diamond);
        }

        private void OnDestroy()
        {
            if (playerInfo.PlayerData == null)
                return;
            playerInfo.PlayerData.OnDiamondUpdate -= ui.SetGem;
            playerInfo.PlayerData.OnHeartUpdate -=(value)=> ui.SetHeart(int.Parse(value));
        }

        #endregion

        #region Service
        private async void OnAddDiamond()
        {
            if (!playerInfo.IsLogin)
            {
                ui.OpenPrompt("Please login.",() => ui.OpenMainMenu());
                return;
            }
            
            ui.OpenAccessLoading("Simulating Add 100 Diamond...");

            WaitForSecondsRealtime(() =>
            {
                gameService.AddDiamond(playerInfo.PlayerId,100,playerInfo.Token,jsonString =>
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
                            {
                                playerInfo.SetData((JObject)json["user_data"]);
                            }
                        
                            ui.OpenMainMenu();
                        });
                    });
                }, OnServiceError);
            });
        }
        private void WaitForSecondsRealtime(Action onComplete)
        {
            StartCoroutine(WaitForSecondsRealtime(1.5f, onComplete));
        }
        private IEnumerator WaitForSecondsRealtime(float seconds, Action onTimeout)
        {
            yield return new WaitForSeconds(seconds);
            onTimeout.Invoke();
        }
        private void OnServiceError(string error)
        {
            DebugLog("ServiceError: " + error, true);
            ui.CloseAccessLoading(()=>
            {
                ui.OpenMainMenu();
            });
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
