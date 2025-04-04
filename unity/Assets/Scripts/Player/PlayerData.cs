using UnityEngine;

namespace Basic.Player
{
    [System.Serializable]
    public class PlayerData
    {
        [SerializeField] private string diamond;
        [SerializeField] private string heart;

        public string Diamond => diamond;
        public string Heart => heart;
        
        public PlayerData(string diamond, string heart)
        {
            this.diamond = diamond;
            this.heart = heart;
        }
        
        public void SetDiamond(string diamond) => this.diamond = diamond;
        public void SetHeart(string heart) => this.heart = heart;
    }
}
