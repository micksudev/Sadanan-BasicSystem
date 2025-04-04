using System;
using Basic.UICommon;
using UnityEngine;

namespace Basic.Scenes.Login.UI
{
    public class UILogin : UIDialog
    {
        public Action OnBtnSignUp;
        public Action OnBtnLogin;


        public void BtnSignUp()
        {
            OnBtnSignUp?.Invoke();
        }

        public void BtnLogin()
        {
            OnBtnLogin?.Invoke();
        }

    }
}
