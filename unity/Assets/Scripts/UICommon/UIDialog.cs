using System;
using System.Collections;
using UnityEngine;

namespace Basic.UICommon
{
    public class UIDialog : MonoBehaviour
    {
        #region Callback
        public event Action OnClose;
        public event Action OnClosed;
        public event Action OnOpen;
        public event Action OnOpened;
        #endregion

        #region Inspector
        [SerializeField] protected GameObject goRoot;
        [SerializeField] protected Animator animator = default;
        [SerializeField] protected bool hideOnStart;
        #endregion

        #region Runtime
        public bool IsClose { get; private set; } = true;
        public bool IsClosing { get; private set; }

        public bool IsOpen { get; private set; }
        public bool IsOpening { get; private set; }
        
        private Coroutine coroutine;
        #endregion

        #region Constant
        private const string startAnimation = "Start"; 
        private const string inAnimation = "In";
        private const string outAnimation = "Out";
        #endregion

        #region Init
        protected virtual void Start()
        {
            if (hideOnStart)
            {
                State(false);
            }
        }
        #endregion
        
        #region Open
        public virtual void Open()
        {
            if ((IsOpening || IsOpen) && goRoot.activeSelf && gameObject.activeSelf)
            {
                OnOpened?.Invoke();
                return;
            }

            IsOpen = IsOpening = false;

            StopCurrentCoroutine();

            ResetStates();
            RootState(true); 
            State(true);

            OnOpen?.Invoke();
            OnOpen = null;
            if (animator && animator.runtimeAnimatorController)
            {
                coroutine = StartCoroutine(PlayOpen());
                return;
            }
            OpenComplete();
        }

        protected virtual IEnumerator PlayOpen()
        {
            IsOpening = true;
            if (animator)
            {
                animator.StopPlayback();
                animator.enabled = false;
                animator.enabled = true;
                yield return PlayState(inAnimation);
            }
            else
                yield return null;

            OpenComplete();
        }
        protected virtual void OpenComplete()
        {
            ResetStates();
            IsOpen = true;   

            OnOpened?.Invoke();
            OnOpen = null;
            OnOpened = null;
        }
        #endregion

        #region Close
        public virtual void Close()
        {
            ResetStates();

            if (IsClosing || IsClose || !gameObject.activeSelf)
            {
                OnClosed?.Invoke();
                return;
            }
            
            StopCurrentCoroutine();
            ResetStates();

            State(true);
            OnClose?.Invoke();

            if (animator && animator.runtimeAnimatorController)
            {
                coroutine = StartCoroutine(PlayClose());
                return;
            }
            CloseComplete();
        }

        protected virtual IEnumerator PlayClose()
        {
            IsClosing = true;
            if (animator != null)
            {
                animator.StopPlayback();
                animator.enabled = false;
                animator.enabled = true;
                yield return PlayState(outAnimation);
            }
            else
                yield return null;

            CloseComplete();
        }

        private void CloseComplete()
        {
            ResetStates();
            IsClose = true;
            RootState(false);
            
            OnClosed?.Invoke();
            OnClose = null;
            OnClosed = null;
        }
        #endregion

        #region Interface
        public virtual void State(bool state)
        {
            if (!this) { return; }
            gameObject.SetActive(state);
        }
        #endregion
        
        #region Internal
        private void RootState(bool state)
        {
            goRoot.SetActive(state);
        }
        private void StopCurrentCoroutine()
        {
           if (coroutine != null)
               StopCoroutine(coroutine);
        }
        private void ResetStates()
        {
            IsOpen = IsOpening = IsClose = IsClosing = false;
        }
        private IEnumerator PlayState(string state)
        {
            animator.Play(state);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        #endregion
        
        #region UI
        public virtual void BtnClose()
        {
            Close();
        }
        #endregion
 
        #region Debug
        protected void DebugLog(object log, bool isError = false)
        {
            string textColor = isError ? "<color=red>" : "<color=cyan>";
            string message = $"[{Time.frameCount}] {textColor}{name}</color>.{log};";
            Debug.Log(message);
        }
        #endregion
    }
}
