using TMPro;
using UnityEngine;

namespace Basic.UICommon
{
    public class UIAccessLoading : UIDialog
    {
        #region Inspector
        [SerializeField] private TextMeshProUGUI txtMessage;
        #endregion

        public void SetMessage(string message)
        {
            txtMessage.SetText(message);
            Open();
        }
    }
}
