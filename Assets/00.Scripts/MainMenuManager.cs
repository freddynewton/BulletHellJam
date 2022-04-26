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

        SoundStation audioStation;

        private void Awake()
        {
            StartButton.onClick.RemoveAllListeners();
            ExitButton.onClick.RemoveAllListeners();

            StartButton.onClick.AddListener(() =>
            {
                SceneManagementController.Instance.LoadScenes(StartButtonContainer.Title);
                audioStation.Stop(SoundStation.Asset.Music, "MainMenu");
                audioStation.Play(SoundStation.Asset.Music, "Game");
            });

            ExitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        void Start()
        {
            audioStation = SoundStation.Instance;
            audioStation.Play(SoundStation.Asset.Music, "MainMenu");

        }
    }
}
