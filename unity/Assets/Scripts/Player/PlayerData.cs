using System;
using UnityEngine;

namespace Basic.Player
{
    [System.Serializable]
    public class PlayerData
    {
        public Action<string> OnDiamondUpdate;
        public Action<string> OnHeartUpdate;
        
        [SerializeField] private string diamond;
        [SerializeField] private string heart;

        public string Diamond => diamond;
        public string Heart => heart;
        
        public PlayerData(string diamond, string heart)
        {
            this.diamond = diamond;
            this.heart = heart;
        }
        
        public void SetDiamond(string diamond)
        {
            this.diamond = diamond;
            OnDiamondUpdate?.Invoke(this.diamond);
        }
        public void SetHeart(string heart)
        {
            this.heart = heart;
            OnHeartUpdate?.Invoke(this.heart);
        }
    }
}
