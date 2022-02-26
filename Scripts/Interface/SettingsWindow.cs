using System.Collections.Generic;
using NumberDigger.Managers;
using NumberDigger.Audio;
using NumberDigger.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace NumberDigger.Interface
{
    public class SettingsWindow : Window
    {
        [Header("Audio")]
        public Slider MusicSlider;
        public Slider SFXSlider;

        [Header("Graphics")]
        public TMP_Dropdown ResDropdown;
        Resolution[] Resolutions;
        public Toggle FSToggle;
        float OpenTime;

        protected override void PostAwake()
        {
            InitResDrop();
            MusicSlider.value = SettingsManager.Instance.settings.MusicVol;
            SFXSlider.value = SettingsManager.Instance.settings.SFXVol;
            ResDropdown.value = SettingsManager.Instance.settings.ResIndex;
            FSToggle.isOn = SettingsManager.Instance.settings.FS;
            OpenTime = 0f;
        }
        void InitResDrop()
        {
            ResDropdown.ClearOptions();
            ResDropdown.AddOptions(SettingsManager.Instance.Res2String);
        }

        protected override void LocalUpdate()
        {
            base.LocalUpdate();
            OpenTime += Time.deltaTime;
            if(OpenTime > 0.1f)
            {
                SettingsManager.Instance.settings.MusicVol = MusicSlider.value;
                SettingsManager.Instance.settings.SFXVol = SFXSlider.value;
                SettingsManager.Instance.settings.ResIndex = ResDropdown.value;
                SettingsManager.Instance.settings.FS = FSToggle.isOn;
            }
        }
        public void UpdateVolume()
        {
            if(OpenTime > 0.1f)
            {
                SettingsManager.Instance.settings.MusicVol = MusicSlider.value;
                SettingsManager.Instance.settings.SFXVol = SFXSlider.value;
            }
            SettingsManager.Instance.UpdateVolume();
        }
        public void UpdateScreen()
        {
            if(OpenTime > 0.1f)
            {
                SettingsManager.Instance.settings.ResIndex = ResDropdown.value;
                SettingsManager.Instance.settings.FS = FSToggle.isOn;
            }
            SettingsManager.Instance.UpdateScreen();
        }

        public void Reset()
        {
            AudioManager.Instance.InteractWithSFX("Reset", SoundEffectBehaviour.Play);
            ProgressManager.Instance.ResetProgress();
        }
    }
}