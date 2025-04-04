using Basic.Player;
using Basic.Service;
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
            ui.Init();

            LoadedComplete();
        }
        #endregion

        private void LoadedComplete()
        {
            if (playerInfo.PlayerData == null)
            {
                return;
            }
            
            ui.SetHeart(int.Parse(playerInfo.PlayerData.Heart));
            ui.SetGem(playerInfo.PlayerData.Diamond);
        }
        
    }
}
