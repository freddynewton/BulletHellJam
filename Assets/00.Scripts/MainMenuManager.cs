using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FreddyNewton.Utility.SceneManagement;

namespace HellsKitchen.Ui
{
    public class MainMenuManager : MonoBehaviour
    {
        public Button StartButton;
        public SceneContainer StartButtonContainer;

        public Button ExitButton;

        AudioStation audioStation;

        private void Awake()
        {
            StartButton.onClick.AddListener(() =>
            {
                SceneManagementController.Instance.LoadScenes(StartButtonContainer.Title);
                audioStation.StartNewMusicPlayer(audioStation.music.asset[0], true);
            });

            StartButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        void Start()
        {
            audioStation = AudioStation.Instance;
        }
    }
}
