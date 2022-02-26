using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace NumberDigger.Visuals
{
    [RequireComponent(typeof(Image))]
    public class FlashingImage : MonoBehaviour
    {
        Image image;

        // Start is called before the first frame update
        void Awake()
        {
            image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Flash(int Amount, float Delay)
        {
            StopAllCoroutines();
            image.enabled = true;
            StartCoroutine(flash(Amount, Delay));
        }
        IEnumerator flash(int amount, float delay)
        {
            for (int F = 0; F < amount; F++)
            {
                image.enabled = false;
                yield return new WaitForSeconds(delay / 2f);
                image.enabled = true;
                yield return new WaitForSeconds(delay / 2f);
            }
        }
    }
}