using Basic.UICommon;
using UnityEngine;
using UnityEngine.UI;

namespace Basic.Scenes.Lobby.UI
{
    public class UIHeart : UIDialog
    {
        [SerializeField] private Image imgSlider;

        private readonly float max = 100f;
        
        public void SetSlider(float value)
        {
            imgSlider.fillAmount = value / max;
        }
    }
}
