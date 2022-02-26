using NumberDigger.Managers;
using NumberDigger.Audio;
using UnityEngine;

namespace NumberDigger.Interface
{
    public class StartMenu : MonoBehaviour
    {
        public GameObject BGObject;
        public GameObject AboutDialog;
        public GameObject SettingsWindow;
        bool IsOn;

        // Update is called once per frame
        void Update()
        {
            // if(Input.GetMouseButtonUp(0) && IsOn)
            // {
            //     IsOn = false;
            // }
            BGObject.SetActive(IsOn);
        }

        public void Toggle()
        {
            AudioManager.Instance.PlaySelectSound();
            IsOn = !IsOn;
        }

        public void QuitGame()
        {
            IsOn = false;
            AudioManager.Instance.InteractWithSFX("Quit", SoundEffectBehaviour.Play);
            QuitManager.Instance.QuitGame();
        }
        public void About()
        {
            IsOn = false;
            AudioManager.Instance.PlaySelectSound();
            WindowManager.Instance.CreateWindow(AboutDialog, true);
        }
        public void Settings()
        {
            IsOn = false;
            AudioManager.Instance.PlaySelectSound();
            WindowManager.Instance.CreateWindow(SettingsWindow, true);
        }
        public void Play()
        {
            IsOn = false;
            AudioManager.Instance.InteractWithSFX("Retry", SoundEffectBehaviour.Play);
            WindowManager.Instance.CreateWindow(WindowManager.Instance.MainWindow, true);
        }
    }
}