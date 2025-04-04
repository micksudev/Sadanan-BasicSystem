using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Basic.Service
{
    public static class ServiceManager
    {
        private static bool isSendingRequest;
    
        public static void SendPostUnityWebRequest(MonoBehaviour context,string url, WWWForm form, Action<string> onRequestComplete, Action<string> onRequestError) => 
            context.StartCoroutine(PostUnityWebRequest(url, form, onRequestComplete, onRequestError));
        
        private static IEnumerator PostUnityWebRequest(string url, WWWForm form, Action<string> onRequestComplete, Action<string> onRequestError)
        {
            if (isSendingRequest)
            {
                DebugLog("Unity Already sending request",true);
                onRequestError?.Invoke("Unity Already sending request");
                yield break;
            }
            isSendingRequest = true;
            
            DebugLog("SendUnityWebRequest: " + url);
            
            using (UnityWebRequest request = UnityWebRequest.Post(url, form))
            {
                yield return request.SendWebRequest();

                isSendingRequest = false;
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    DebugLog("Post Success!: " + request.downloadHandler.text);
                    onRequestComplete?.Invoke(request.downloadHandler.text);
                }
                else
                {
                    DebugLog("Post Failed: " + request.downloadHandler.text,true);
                    onRequestError?.Invoke(request.downloadHandler.text);
                }
            }
        }
    
        #region Debug
        private static void DebugLog(object log, bool isError = false)
        {
            string textColor = isError ? "<color=red>" : "<color=cyan>";
            string message = $"[{Time.frameCount}] {textColor}ServiceManager</color>.{log};";
            Debug.Log(message);
        }
        #endregion

    }
}
