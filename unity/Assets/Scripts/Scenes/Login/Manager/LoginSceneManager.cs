using System;
using UnityEngine;

namespace Basic.Scenes.Login.Manager
{
    public class LoginSceneManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private GameObject goDialogRoot;
        #endregion

        #region Runtime
        private LoginUIManager ui;
        #endregion

        #region Init
        private void Start()
        {
            ui = goDialogRoot.AddComponent<LoginUIManager>();
        }
        #endregion
        
        
        
    }
}
