using System;
using Basic.Scenes.Lobby.UI;
using Basic.UICommon;
using UnityEngine;

namespace Basic.Scenes.Lobby.Manager
{
    public class LobbyUIManager : MonoBehaviour
    {
        public Action OnBtnAddDiamond;
        
        #region Runtime
        private UIMainMenu uiMainMenu;
        private UIHeart uiHeart;
        private UIGem uiGem;

        private UIPrompt uiPrompt;
        private UIAccessLoading uiAccessLoading;
        #endregion

        #region Init
        public void Init()
        {
            uiMainMenu = GetComponentInChildren<UIMainMenu>(true);
            uiHeart = GetComponentInChildren<UIHeart>(true);
            uiGem = GetComponentInChildren<UIGem>(true);

            uiPrompt = GetComponentInChildren<UIPrompt>(true);
            uiAccessLoading = GetComponentInChildren<UIAccessLoading>(true);
            
            uiGem.OnBtnAddDiamond = OnBtnAddDiamond;

            uiMainMenu.OnBtnStart = () =>
            {
                OpenPrompt(" On Development ", () => { });
            };
            uiMainMenu.Init();
        }
        #endregion

        #region Interface
        public void SetHeart(int heart) => uiHeart.SetSlider(heart);
        
        public void SetGem(string gem) => uiGem.SetGem(gem);
        #endregion
        
        #region Prompt
        public void OpenPrompt(string message, Action onClosed = null)
        {
            uiPrompt.Open(message, onClosed);
        }

        #endregion

        #region Access Loading
        public void OpenAccessLoading(string message)
        {
            uiAccessLoading.SetMessage(message);
        }
        
        public void CloseAccessLoading(Action onClosed = null)
        {
            if(onClosed != null)
                uiAccessLoading.OnClosed += onClosed;
            uiAccessLoading.Close();
        }
        #endregion
    }
}
