using System;
using Basic.Common;
using UnityEngine;

namespace Basic.Scenes.Login.UI
{
    public class UILogin : BaseDialog
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
