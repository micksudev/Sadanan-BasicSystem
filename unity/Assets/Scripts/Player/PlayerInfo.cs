using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;

namespace Basic.Player
{
    public class PlayerInfo : MonoBehaviour
    {
        public static PlayerInfo Instance { get; private set; }
        private void Awake() => Instance = this;
        
        [SerializeField] private string username;
        [SerializeField] private int playerId;
        [SerializeField, TextArea] private string token;
        [SerializeField] private PlayerData playerData;
        
        public string Username => username;
        public int PlayerId => playerId;
        public string Token => token;
        public PlayerData PlayerData => playerData;

        public void SetInfo(JObject json,string token)
        {
            string username = (string)json["username"];
            int userId = (int)json["user_id"];
            
            this.username = username;
            this.token = token;
            this.playerId = userId; 
        }

        public void SetData(JObject json)
        {
            if(json["diamond"] == null || json["heart"] == null)
                return;
            
            playerData = new PlayerData(json["diamond"].ToString(), json["heart"].ToString());
        }
    }
}
