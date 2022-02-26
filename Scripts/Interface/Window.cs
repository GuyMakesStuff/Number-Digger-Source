using NumberDigger.Audio;
using TMPro;
using UnityEngine;

namespace NumberDigger.Interface
{
    public class Window : MonoBehaviour
    {
        bool IsDragging;
        public string WindowName;
        public Vector2 Size;
        public TMP_Text Title;
        Vector3 Gap;
        RectTransform rectTransform;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            PostAwake();
        }
        protected virtual void PostAwake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(IsDragging)
            {
                transform.position = Input.mousePosition + Gap;
            }
            rectTransform.sizeDelta = Size;
            Title.text = WindowName;
            LocalUpdate();
        }
        protected virtual void LocalUpdate()
        {

        }

        public void StartDragging()
        {
            IsDragging = true;
            Gap = transform.position - Input.mousePosition;
            transform.SetAsLastSibling();
        }
        public void StopDragging()
        {
            IsDragging = false;
        }

        public void Close()
        {
            AudioManager.Instance.PlaySelectSound();
            Destroy(gameObject);
        }
    }
}