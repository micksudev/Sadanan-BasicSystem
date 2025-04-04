using System;
using UnityEngine;

namespace Basic.UICommon
{
    public class UIButton : UIDialog
    {
        public Action OnBtnPress;

        public void BtnPress()
        {
            OnBtnPress?.Invoke();
        }

        public void OnHoverStart()
        {
            this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        
        public void OnHoverExit()
        {
            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
