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
    }
}
