using System;
using UnityEngine;
using TMPro;

namespace NumberDigger.Visuals
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Clock : MonoBehaviour
    {
        TMP_Text Txt;

        // Start is called before the first frame update
        void Start()
        {
            Txt = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            string Time = DateTime.Now.ToShortTimeString();
            Txt.text = Time;
        }
    }
}