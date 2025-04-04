using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Basic.UICommon
{
    public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 originalScale;
        private Vector3 targetScale;
        private Coroutine scaleCoroutine;
        public float scaleSpeed = 5f;

        void Start()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartScaling(originalScale * 1.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StartScaling(originalScale);
        }

        private void StartScaling(Vector3 newTarget)
        {
            targetScale = newTarget;

            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }

            scaleCoroutine = StartCoroutine(ScaleToTarget());
        }

        private IEnumerator ScaleToTarget()
        {
            while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
                yield return null;
            }

            transform.localScale = targetScale;
        }
    }
}