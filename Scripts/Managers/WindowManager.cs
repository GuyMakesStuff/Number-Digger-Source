using System;
using NumberDigger.Interface;
using UnityEngine;

namespace NumberDigger.Managers
{
    public class WindowManager : Manager<WindowManager>
    {
        [Space]
        public Transform WindowContainer;
        Vector2Int ScreenCenter;

        [Header("Window Presets")]
        public GameObject MainWindow;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
        }

        // Update is called once per frame
        void Update()
        {
            ScreenCenter = new Vector2Int(Screen.width / 2, Screen.height / 2);
        }

        public bool WindowExists(Window window)
        {
            return Array.Exists<Window>(FindObjectsOfType<Window>(true), Window => Window.WindowName == window.WindowName);
        }

        public void CreateWindow(GameObject WindowPrefab, bool CheckIfWindowExists)
        {
            Window window = WindowPrefab.GetComponent<Window>();
            if(window == null)
            {
                Debug.LogError("Invalid Window Prefab!");
                return;
            }

            if(CheckIfWindowExists && WindowExists(window))
            {
                return;
            }

            Debug.Log(WindowExists(window));
            Instantiate(WindowPrefab, (Vector2)ScreenCenter, Quaternion.identity, WindowContainer);
        }
    }
}