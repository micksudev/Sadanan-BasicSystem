using System;
using Basic.Scenes.Login.UI;
using Basic.UICommon;
using UnityEngine;

namespace Basic.Scenes.Login.Manager
{
    public class LoginUIManager : MonoBehaviour
    {
        #region Callback
        public Action<string, string> OnLogin;
        public Action<string, string> OnSignUp;
        #endregion
        
        #region Runtime
        private UILogin uiLogin;
        private UISignUp uiSignUp;
        
        private UIPrompt uiPrompt;
        private UIAccessLoading uiAccessLoading;
        #endregion

        #region Init
        public void Init()
        {
            uiLogin = gameObject.GetComponentInChildren<UILogin>(true);
            uiSignUp = gameObject.GetComponentInChildren<UISignUp>(true);
            
            uiPrompt = gameObject.GetComponentInChildren<UIPrompt>(true);
            uiAccessLoading = gameObject.GetComponentInChildren<UIAccessLoading>(true);

            UILoginInit();
            UISignUpInit();
        }

        private void UILoginInit()
        {
            uiLogin.OnBtnSignUp = CloseLoginAndOpenSignUp;
            uiLogin.OnBtnConfirmInput = OnLogin;
            uiLogin.Init();
        }

        private void UISignUpInit()
        {
            uiSignUp.OnBtnConfirmInput = OnSignUp;
            uiSignUp.Init();
        }
        #endregion

        #region Close&Open
        private void CloseLoginAndOpenSignUp()
        {
            uiLogin.OnClosed += uiSignUp.Open;
            uiLogin.Close();
        }
        
        public void CloseSignUpAndOpenLogin()
        {
            uiSignUp.OnClosed += uiLogin.Open;
            uiSignUp.Close();
        }

        public void CloseLogin(Action onClosed = null)
        {
            uiLogin.OnClosed += onClosed;
            uiLogin.Close();
        }
        #endregion
        
        #region Prompt
        public void OpenPrompt(string message, Action onClosed = null)
        {
            uiPrompt.Open(message, onClosed);
        }

        public void OpenPrompt(string message, Action onClosed = null,
            string btn1Label = null, Action onPressedBtn1 = null,
            string btn2Label = null, Action onPressedBtn2 = null)
        {
            uiPrompt.Open(message, onClosed, btn1Label, onPressedBtn1, btn2Label, onPressedBtn2);
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
