using System.Collections;
using System.Collections.Generic;
using NumberDigger.Audio;
using UnityEngine.UI;
using NumberDigger.Managers;
using NumberDigger.Interface;
using NumberDigger.Visuals;
using NumberDigger.Utils;
using UnityEngine;
using TMPro;

namespace NumberDigger.Gameplay
{
    public class MainWindow : Window
    {
        public enum GameStates { Playing, Paused, GameOver, Standby }

        [Header("General")]
        public int NumRange;
        public int RealNumAmount;
        public GameObject NumButtonPrefab;
        public RectTransform NumButtonConatiner;
        public GameObject ButtonBlocker;
        List<int> PossibleNumbers;
        List<NumButton> NumButtons;
        List<int> CurCombo;
        List<int> RealNumCombo;

        [Header("UI")]
        public TMP_Text ScoreText;
        int Score;
        public TMP_Text HIScoreText;
        int HIScore;
        bool NewHI;
        public TMP_Text RealNumsText;
        public TMP_Text TimeText;
        public TMP_Text TressuresCollectedText;
        public GameObject ImagePrefab;
        public Sprite TressureImage;
        public Sprite WrongImage;

        [Header("Difficulty")]
        public int CombosPerLevel;
        public int MaxLevel;
        int TressuresToCollect;
        public int MaxTressuresToCollect;
        public float BaseTime;
        float TargetTime;
        float Timer;
        int TressuresCollected;
        int ComboIndex = 1;
        int Level = 1;

        [Header("Menus")]
        public GameStates GameState;
        public GameObject Main;
        public GameObject PauseMenu;
        public GameObject GameOverMenu;
        public TMP_Text GameOverInfo;
        public GameObject NewHIText;

        // Start is called before the first frame update
        void Start()
        {
            PossibleNumbers = new List<int>();
            NumButtons = new List<NumButton>();
            TressuresToCollect = 1;
            for (int Num = 1; Num <= NumRange; Num++)
            {
                PossibleNumbers.Add(Num);
            }
            TargetTime = BaseTime;
            Timer = TargetTime;

            HIScore = ProgressManager.Instance.progress.HighScore;

            AudioManager.Instance.SetMusicTrack("Music" + Random.Range(1, 3).ToString("00"));

            Init(Level + 2);
        }

        void Init(int ButtonsPerLine)
        {
            if(NumButtons.Count > 0)
            {
                foreach (NumButton NB in NumButtons)
                {
                    Destroy(NB.gameObject);
                }
                NumButtons.Clear();
                foreach (GameObject OBJ in GameObject.FindGameObjectsWithTag("NumButtonImage"))
                {
                    Destroy(OBJ);
                }
            }
            const float ButtonSize = 83.33334f;
            float PerLine = (ButtonSize * (float)ButtonsPerLine);
            Size.x = PerLine + 70f;
            Size.y = PerLine + 225f;
            for (int Y = 0; Y < ButtonsPerLine; Y++)
            {
                for (int X = 0; X < ButtonsPerLine; X++)
                {
                    Vector2 Pos = new Vector2((-(ButtonSize * (float)ButtonsPerLine) + (ButtonSize / 2f) + (ButtonSize * (float)X)) + PerLine / 2f, (-(ButtonSize * (float)ButtonsPerLine) + (ButtonSize / 2f) + (ButtonSize * (float)Y)) + PerLine / 2f);
                    NumButton NewNumButton = Instantiate(NumButtonPrefab, Vector3.zero, Quaternion.identity, NumButtonConatiner).GetComponent<NumButton>();
                    NewNumButton.GetComponent<RectTransform>().anchoredPosition = Pos;
                    NumButtons.Add(NewNumButton);
                }
            }
            NewCombo();
        }

        void NewCombo()
        {
            TressuresCollected = 0;
            List<int> Nums = new List<int>();
            ListUtils.CloneList<int>(Nums, PossibleNumbers);
            CurCombo = ListUtils.RandomItemsFromList<int>(Nums, NumButtons.Count);
            List<int> CurComboClone = new List<int>();
            ListUtils.CloneList<int>(CurComboClone, CurCombo);
            RealNumCombo = ListUtils.RandomItemsFromList<int>(CurComboClone, (NumButtons.Count / 2));
            string RealNumFeedback = "Real Numbers:\n";
            foreach (int Num in RealNumCombo)
            {
                RealNumFeedback += Num.ToString() + ",";
            }
            RealNumsText.text = RealNumFeedback;
            for (int NB = 0; NB < NumButtons.Count; NB++)
            {
                NumButtons[NB].Num = CurCombo[NB];
            }
        }

        protected override void LocalUpdate()
        {
            ButtonBlocker.transform.SetAsLastSibling();
            ButtonBlocker.SetActive(GameState == GameStates.Standby);
            ScoreText.text = "Score:\n" + Score.ToString("000");
            HIScoreText.text = "High Score:\n" + HIScore.ToString("000");
            if(Score > HIScore)
            {
                HIScore = Score;
                NewHI = true;
            }
            ProgressManager.Instance.progress.HighScore = HIScore;
            if(GameState == GameStates.Playing) { Timer -= Time.deltaTime; }
            TimeText.text = "Time:" + Timer.ToString("0.0");
            if(Timer <= 0f && GameState == GameStates.Playing)
            {
                ProgressManager.Instance.progress.Deaths++;
                AudioManager.Instance.InteractWithSFX("Dig Fake", SoundEffectBehaviour.Play);
                AudioManager.Instance.MuteMusic();
                GameOver();
            }
            TressuresCollectedText.text = "Tressures Found:\n" + TressuresCollected + "/" + TressuresToCollect;

            if(GameState != GameStates.Standby)
            {
                Main.SetActive(GameState == GameStates.Playing);
                PauseMenu.SetActive(GameState == GameStates.Paused);
                GameOverMenu.SetActive(GameState == GameStates.GameOver);
            }
            if(Input.GetKeyDown(KeyCode.Escape) && GameState == GameStates.Playing)
            {
                AudioManager.Instance.PlaySelectSound();
                AudioManager.Instance.InteractWithMusic(SoundEffectBehaviour.Pause);
                GameState = GameStates.Paused;
            }
        }

        public void SubmitNum(int num, NumButton numButton)
        {
            StartCoroutine(submitNum(num, numButton));
        }
        IEnumerator submitNum(int Value, NumButton NB)
        {
            if(!RealNumCombo.Contains(Value))
            {
                TressuresCollected++;
                ReplaceNumButtonWithImage(NB, TressureImage, true, 6, 0.166666f);
                Score++;
                AudioManager.Instance.InteractWithSFX("Dig Real", SoundEffectBehaviour.Play);
                if(TressuresCollected == TressuresToCollect)
                {
                    GameState = GameStates.Standby;
                    foreach (FlashingImage FI in FindObjectsOfType<FlashingImage>(true))
                    {
                        FI.Flash(6, 0.166666f);
                    }
                    AudioManager.Instance.InteractWithSFX("Next Level", SoundEffectBehaviour.Play);
                    yield return new WaitForSeconds(1f);
                    GameState = GameStates.Playing;
                    ProgressForward();
                }
            }
            else
            {
                ReplaceNumButtonWithImage(NB, WrongImage, true, 6, 0.25f);
                GameState = GameStates.Standby;
                ProgressManager.Instance.progress.Deaths++;
                AudioManager.Instance.InteractWithSFX("Dig Fake", SoundEffectBehaviour.Play);
                AudioManager.Instance.MuteMusic();
                yield return new WaitForSeconds(1.5f);
                GameOver();
            }
        }
        void ReplaceNumButtonWithImage(NumButton numButton, Sprite IMG, bool Flash, int FlashAmount, float FlashDelay)
        {
            if(NumButtons.Contains(numButton))
            {
                NumButtons.Remove(numButton);
                GameObject NewImage = Instantiate(ImagePrefab, numButton.transform.position, Quaternion.identity, NumButtonConatiner);
                NewImage.GetComponent<Image>().sprite = IMG;
                if(Flash) { NewImage.GetComponent<FlashingImage>().Flash(FlashAmount, FlashDelay); }
                Destroy(numButton.gameObject);
            }
        }
        void ProgressForward()
        {
            if(ComboIndex == CombosPerLevel)
            {
                ComboIndex = 1;
                if(TressuresToCollect < MaxTressuresToCollect)
                {
                    TressuresToCollect++;
                }
                if(Level < MaxLevel)
                {
                    Level++;
                    TargetTime += BaseTime;
                }
                else
                {
                    if(TargetTime > (MaxTressuresToCollect * BaseTime) / 2)
                    {
                        TargetTime -= BaseTime / 2f;
                    }
                }
            }
            else
            {
                ComboIndex++;
            }
            Timer = TargetTime;
            Init(Level + 2);
        }
        public void GameOver()
        {
            GameState = GameStates.GameOver;
            GameOverInfo.text = "Deaths:" + ProgressManager.Instance.progress.Deaths.ToString("0") + "\nScore:" + Score.ToString("000") + "\nHigh Score:" + HIScore.ToString("000");
            NewHIText.SetActive(NewHI);
        }

        public void Resume()
        {
            AudioManager.Instance.PlaySelectSound();
            AudioManager.Instance.InteractWithMusic(SoundEffectBehaviour.Resume);
            GameState = GameStates.Playing;
        }
        public void Retry()
        {
            AudioManager.Instance.InteractWithSFX("Retry", SoundEffectBehaviour.Play);
            WindowManager.Instance.CreateWindow(WindowManager.Instance.MainWindow, false);
            Close();
        }

        public void Quit()
        {
            AudioManager.Instance.MuteMusic();
            Close();
        }
    }
}