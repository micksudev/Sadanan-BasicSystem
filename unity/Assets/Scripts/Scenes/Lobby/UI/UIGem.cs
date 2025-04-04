using Basic.UICommon;
using TMPro;

namespace Basic.Scenes.Lobby.UI
{
    public class UIGem : UIDialog
    {
        public TextMeshProUGUI txtGem;
        
        public void SetGem(string gem) => txtGem.text = gem;
    }
}
