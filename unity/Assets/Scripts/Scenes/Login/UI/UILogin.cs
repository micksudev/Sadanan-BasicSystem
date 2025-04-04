using System;
using Basic.UICommon;
using TMPro;
using UnityEngine;

namespace Basic.Scenes.Login.UI
{
    public class UILogin : UIUserInput
    {
        #region Callback
        public Action OnBtnSignUp;
        #endregion

        #region Inspector
        [SerializeField] private UIButton btnSignUp;
        #endregion
        
        #region Init
        public override void Init()
        {
            base.Init();
            btnSignUp.OnBtnPress += OnBtnSignUp;
        }
        #endregion
    }
}
