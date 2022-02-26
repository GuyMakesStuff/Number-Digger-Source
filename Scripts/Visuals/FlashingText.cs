using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NumberDigger.Visuals
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FlashingText : MonoBehaviour
    {
        TMP_Text Txt;
        public Color[] Colors;
        public float FlashDelay;

        // Start is called before the first frame update
        void Start()
        {
            Txt = GetComponent<TextMeshProUGUI>();
            StartCoroutine(Flash());
        }

        IEnumerator Flash()
        {
            for (int C = 0; C < Colors.Length; C++)
            {
                Txt.color = Colors[C];
                yield return new WaitForSeconds(FlashDelay);
            }

            StartCoroutine(Flash());
        }
    }
}