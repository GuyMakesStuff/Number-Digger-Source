using System.Collections.Generic;
using System.Collections;
using NumberDigger.Audio;
using NumberDigger.IO;
using UnityEngine;

namespace NumberDigger.Managers
{
    public class SettingsManager : Manager<SettingsManager>
    {
        [System.Serializable]
        public class Settings : SaveFile
        {
            [Space]
            public float MusicVol;
            public float SFXVol;
            public int ResIndex;
            public bool FS;
        }
        [Space]
        public Settings settings;
        Resolution[] Resolutions;
        [HideInInspector]
        public List<string> Res2String;
        [HideInInspector]
        public int CurResIndex;

        // Start is called before the first frame update
        protected void Start()
        {
            Init(this);

            Settings LoadedSettings = Saver.Load(settings) as Settings;
            CurResIndex = 0;
            InitRes(out CurResIndex);
            if(LoadedSettings != null)
            {
                settings = LoadedSettings;
            }
            else
            {
                settings.MusicVol = AudioManager.Instance.GetMusicVolume();
                settings.SFXVol = AudioManager.Instance.GetSFXVolume();
                settings.ResIndex = CurResIndex;
                settings.FS = Screen.fullScreen;
            }
            UpdateVolume();
            UpdateScreen();
        }
        void InitRes(out int CurRes)
        {
            Resolutions = Screen.resolutions;
            Res2String = new List<string>();
            Resolution CurScreenRes = Screen.currentResolution;
            int curResIndex = 0;
            for (int R = 0; R < Resolutions.Length; R++)
            {
                Resolution Res = Resolutions[R];
                string String = Res.width + "x" + Res.height;
                Res2String.Add(String);

                if(Res.width == CurScreenRes.width && Res.height == CurScreenRes.height)
                {
                    curResIndex = R;
                }
            }
            CurRes = curResIndex;
        }

        // Update is called once per frame
        void Update()
        {
            settings.Save();
        }
        public void UpdateVolume()
        {
            AudioManager.Instance.SetMusicVolume(settings.MusicVol);
            AudioManager.Instance.SetSFXVolume(settings.SFXVol);
        }
        public void UpdateScreen()
        {
            Resolution Res = Resolutions[settings.ResIndex];
            Screen.SetResolution(Res.width, Res.height, settings.FS);
        }
    }
}