using System;
using TMPro;
using UnityEngine;

namespace Basic.UICommon
{
    public class UIPrompt : UIDialog
    {
        #region Inspector
        [SerializeField] private TextMeshProUGUI txtTitle; 
        [SerializeField] private TextMeshProUGUI txtMessage;

        [SerializeField] private GameObject goBtn1;
        [SerializeField] private TextMeshProUGUI txtBtn1;

        [SerializeField] private GameObject goBtn2;
        [SerializeField] private TextMeshProUGUI txtBtn2;
        #endregion

        #region Runtime Data
        private Action onPressedBtn1;
        private Action onPressedBtn2;
        #endregion

        #region Init

        public void Open(string message,Action onClosed=null,
            string btn1Label=null, Action onPressedBtn1=null,
            string btn2Label=null, Action onPressedBtn2=null)
        {
            //Debug.Log("UIPrompt.Init(" + message + ", " + btn1Label + ", " + onPressedBtn1 + ", " + btn2Label + ", " + onPressedBtn2 + ")");
            
            txtMessage.SetText(message);
            
            this.onPressedBtn1 = onPressedBtn1;
            this.onPressedBtn2 = onPressedBtn2;
            
            if(onClosed!=null)
                OnClosed += onClosed;
            
            if (!string.IsNullOrEmpty(btn1Label))
                txtBtn1.SetText(btn1Label);

            if (!string.IsNullOrEmpty(btn2Label))
                txtBtn2.SetText(btn2Label);

            goBtn2.SetActive(onPressedBtn2 != null);
            Open();
        }
        #endregion

        #region UI
        public void Btn1()
        {
            Close();
            onPressedBtn1?.Invoke();
        }

        public void Btn2()
        {
            Close();
            onPressedBtn2?.Invoke();
        }
        #endregion
    }
}
