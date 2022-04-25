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
            StartButton.onClick.RemoveAllListeners();
            ExitButton.onClick.RemoveAllListeners();

            StartButton.onClick.AddListener(() =>
            {
                SceneManagementController.Instance.LoadScenes(StartButtonContainer.Title);
                audioStation.StartNewMusicPlayer(audioStation.music.asset[0].audioClips[0], true);
            });

            ExitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        void Start()
        {
            audioStation = AudioStation.Instance;
            audioStation.StartNewMusicPlayer(audioStation.music.asset[1].audioClips[0], true);
        }
    }
}
