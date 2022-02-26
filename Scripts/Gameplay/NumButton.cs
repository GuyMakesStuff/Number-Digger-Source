using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NumberDigger.Gameplay
{
    public class NumButton : MonoBehaviour
    {
        public int Num;
        public TMP_Text NumText;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            NumText.text = Num.ToString("00");
        }

        public void Submit()
        {
            FindObjectOfType<MainWindow>().SubmitNum(Num, this);
        }
    }
}