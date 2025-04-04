using Basic.Scenes.Lobby.UI;
using UnityEngine;

namespace Basic.Scenes.Lobby.Manager
{
    public class LobbyUIManager : MonoBehaviour
    {
        #region Runtime
        private UIMainMenu uiMainMenu;
        private UIHeart uiHeart;
        private UIGem uiGem;
        #endregion

        #region Init
        public void Init()
        {
            uiMainMenu = GetComponentInChildren<UIMainMenu>(true);
            uiHeart = GetComponentInChildren<UIHeart>(true);
            uiGem = GetComponentInChildren<UIGem>(true);
        }
        #endregion

        #region Interface
        public void SetHeart(int heart) => uiHeart.SetSlider(heart);
        
        public void SetGem(string gem) => uiGem.SetGem(gem);
        #endregion
    }
}
