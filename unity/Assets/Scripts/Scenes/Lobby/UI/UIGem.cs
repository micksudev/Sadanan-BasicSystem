using System;
using Basic.UICommon;
using TMPro;
using UnityEngine;

namespace Basic.Scenes.Lobby.UI
{
    public class UIGem : UIDialog
    {
        public Action OnBtnAddDiamond;
        
        [SerializeField] private TextMeshProUGUI txtGem;
        
        public void SetGem(string gem) => txtGem.text = gem;

        public void BtnAddDiamond()
        {
            OnBtnAddDiamond?.Invoke();
        }
    }
}
