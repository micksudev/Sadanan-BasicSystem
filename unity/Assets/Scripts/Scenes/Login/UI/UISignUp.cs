using TMPro;
using UnityEngine;

namespace Basic.Scenes.Login.UI
{
    public class UISignUp : UIUserInput
    {
        #region Inspector
        [SerializeField] private TMP_InputField txtConfirmPassword;
        #endregion

        #region Internal
        protected override void OnBtnConfirm()
        {
            if(txtConfirmPassword.text == txtPassword.text)
                base.OnBtnConfirm();
        }

        public override void Open()
        {
            base.Open();
            txtConfirmPassword.text = string.Empty;
        }

        #endregion
    }
}
