using FreddyNewton.Utility.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI HighscoreText;

    public Button RestartButton;
    public Button MainMenuButton;
    public Button QuitButton;

    public SceneContainer MainMenuContainer;
    public SceneContainer RestartContainer;

    private void Awake()
    {
        QuitButton.onClick.RemoveAllListeners();
        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        MainMenuButton.onClick.RemoveAllListeners();
        MainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManagementController.Instance.LoadScenes(MainMenuContainer.Title);
        });

        RestartButton.onClick.RemoveAllListeners();
        RestartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManagementController.Instance.LoadScenes(RestartContainer.Title);
        });
    }

    public IEnumerator ShowGameOverScreen(float delay)
    {
        HighscoreText.text = $"Recipes completed: {FindObjectOfType<TaskManager>().recipesCompleted}";

        yield return new WaitForSecondsRealtime(delay);
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
}
