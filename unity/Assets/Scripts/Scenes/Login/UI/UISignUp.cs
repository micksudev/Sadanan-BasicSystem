using System;
using TMPro;
using UnityEngine;

namespace Basic.Scenes.Login.UI
{
    public class UISignUp : UIUserInput
    {
        #region Inspector
        [SerializeField] private TMP_InputField txtConfirmPassword;
        #endregion

        #region Interface
        public override void Open()
        {
            base.Open();
            txtConfirmPassword.text = string.Empty;
        }
        #endregion
        
        #region Internal
        protected override void OnBtnConfirm()
        {
            if(txtConfirmPassword.text == txtPassword.text)
                base.OnBtnConfirm();
            else
                OnOpenPrompt?.Invoke("Password not match",null);
        }
        #endregion
    }
}
