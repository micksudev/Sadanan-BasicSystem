using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Basic.Player
{
    public class PlayerInfo : MonoBehaviour
    {
        private static PlayerInfo _instance;

        public static PlayerInfo Instance
        {
            get
            {
                if(_instance != null)
                    return _instance;
            
                GameObject gameService = new GameObject();
                gameService.name = "PlayerInfo";
                DontDestroyOnLoad(gameService);
                
                _instance = gameService.AddComponent<PlayerInfo>();
                return _instance;
            }
        }
        
        [SerializeField] private string username;
        [SerializeField] private int playerId;
        [SerializeField, TextArea] private string token;
        [SerializeField] private PlayerData playerData;
        
        public string Username => username;
        public int PlayerId => playerId;
        public string Token => token;
        public PlayerData PlayerData => playerData;
        
        public bool IsLogin => playerId != 0;

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
            if(playerData == null)
                playerData = new PlayerData(json["diamond"].ToString(), json["heart"].ToString());
            else
            {
                playerData.SetDiamond(json["diamond"].ToString());
                playerData.SetHeart(json["heart"].ToString());
            }
        }
    }
}
