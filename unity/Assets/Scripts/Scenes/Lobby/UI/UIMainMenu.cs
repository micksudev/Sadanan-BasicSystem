using System;
using Basic.UICommon;
using UnityEngine;

namespace Basic.Scenes.Lobby.UI
{
    public class UIMainMenu : UIDialog
    {
        public Action OnBtnStart;
        
        [SerializeField] private UIButton btnStart;

        public void Init()
        {
            btnStart.OnBtnPress = OnBtnStart;
        }
        
    }
}
