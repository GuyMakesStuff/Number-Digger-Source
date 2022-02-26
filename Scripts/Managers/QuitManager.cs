using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumberDigger.Managers
{
    public class QuitManager : Manager<QuitManager>
    {
        [Space]
        public Animator FadeAnim;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            FadeAnim.SetBool("IsFaded", false);
        }

        public void QuitGame()
        {
            StartCoroutine(quit());
        }
        IEnumerator quit()
        {
            FadeAnim.SetBool("IsFaded", true);
            yield return new WaitForSeconds(2f);
            Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #endif
        }
    }
}