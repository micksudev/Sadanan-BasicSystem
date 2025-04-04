using System;
using Basic.UICommon;
using TMPro;
using UnityEngine;

namespace Basic.Scenes.Login.UI
{
    public class UIUserInput : UIDialog
    {
        #region Callback
        public Action<string,string> OnBtnConfirmInput;
        #endregion

        #region Inspector
        [SerializeField] private UIButton btnConfirmInput;

        [SerializeField] private TMP_InputField txtUsername;
        [SerializeField] protected TMP_InputField txtPassword;
        #endregion
        
        #region Init
        public virtual void Init()
        {
            btnConfirmInput.OnBtnPress = OnBtnConfirm;
        }
        #endregion

        #region Interface

        public override void Open()
        {
            base.Open();
            txtUsername.text = string.Empty;
            txtPassword.text = string.Empty;
        }

        #endregion

        #region Internal
        protected virtual void OnBtnConfirm()
        {
            if(txtUsername.text == string.Empty || txtPassword.text == string.Empty) 
                return;
            
            OnBtnConfirmInput?.Invoke(txtUsername.text,txtPassword.text);
        }
        #endregion
    }
}
