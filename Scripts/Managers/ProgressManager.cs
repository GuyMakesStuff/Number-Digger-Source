using NumberDigger.Audio;
using NumberDigger.IO;
using UnityEngine;

namespace NumberDigger.Managers
{
    public class ProgressManager : Manager<ProgressManager>
    {
        [System.Serializable]
        public class Progress : SaveFile
        {
            [Space]
            public int HighScore;
            public int Deaths;
        }
        [Space]
        public Progress progress;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            AudioManager.Instance.InteractWithSFX("Startup", SoundEffectBehaviour.Play);

            Progress LoadedProgress = Saver.Load(progress) as Progress;
            if(LoadedProgress != null)
            {
                progress = LoadedProgress;
            }
            else
            {
                ResetProgress();
            }
        }

        // Update is called once per frame
        void Update()
        {
            progress.Save();
        }

        public void ResetProgress()
        {
            progress.HighScore = 0;
            progress.Deaths = 0;
        }
    }
}