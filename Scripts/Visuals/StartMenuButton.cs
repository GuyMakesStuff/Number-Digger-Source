using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace NumberDigger.Visuals
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StartMenuButton : MonoBehaviour
    {
        TMP_Text Txt;
        Color BaseColor;
        Button button;
        bool IsHovered;

        // Start is called before the first frame update
        void Start()
        {
            Txt = GetComponent<TextMeshProUGUI>();
            button = GetComponentInParent<Button>();
            BaseColor = Txt.color;
        }

        // Update is called once per frame
        void Update()
        {
            Txt.color = (!IsHovered) ? BaseColor : Color.white;
        }

        public void SetHovered(bool Value)
        {
            IsHovered = Value;
        }

        void OnDisable()
        {
            SetHovered(false);
        }
    }
}