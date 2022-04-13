using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace FreddyNewton.Utility.SceneManagement
{
    /// <summary>
    /// Singleton Scene Manager.
    /// A centralized script to control clean loading of additive scenes.
    /// With this it should be all encapsulated. 
    /// <see cref="SceneContainer"/> should be located in the Resources folder.
    /// </summary>
    public class SceneManagementController : Singleton<SceneManagementController>
    {
        [SerializeField] private string LoadingScene = "LoadingScene";
        [SerializeField] private SceneContainer StartUpSceneContainer;

        private List<SceneContainer> SceneContainers = new List<SceneContainer>();
        private SceneContainer CurrentSceneContainer;

        /// <summary>
        /// Unloads first all additive loaded Scenes.
        /// Loads based of the title all new Scenes
        /// </summary>
        /// <param name="title">Search parameter to get the right <see cref="SceneContainer"/></param>
        public void LoadScenes(string title)
        {
            // find the searched SceneContainer by the title
            var searchedSceneContainer = SceneContainers.Find(scene => scene.Title == title);

            if (searchedSceneContainer == null || searchedSceneContainer.Scenes.Length == 0)
            {
                Debug.LogWarning("SceneContainer " + title + " not found or empty!");
                return;
            }

            // After Loading Scene is loaded, unload all current scenes and load the new ones
            SceneManager.LoadSceneAsync(LoadingScene, LoadSceneMode.Additive).completed += async _ =>
            {
                await UnloadAllScenes();

                foreach (var scene in searchedSceneContainer.Scenes)
                {
                    var asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                    while (!asyncOperation.isDone)
                    {
                        await Task.Yield();
                    }
                }

                // Setup new current scene container
                CurrentSceneContainer = searchedSceneContainer;

                // Unload loading scene
                var asyncOperationLoadingScene = SceneManager.UnloadSceneAsync(LoadingScene);
                while (!asyncOperationLoadingScene.isDone)
                {
                    await Task.Yield();
                }
            };
        }

        public void LoadScenes(string title, Slider slider)
        {
            // find the searched SceneContainer by the title
            var searchedSceneContainer = SceneContainers.Find(scene => scene.Title == title);

            if (searchedSceneContainer == null || searchedSceneContainer.Scenes.Length == 0)
            {
                Debug.LogWarning("SceneContainer " + title + " not found or empty!");
                return;
            }

            // After Loading Scene is loaded, unload all current scenes and load the new ones
            SceneManager.LoadSceneAsync(LoadingScene, LoadSceneMode.Additive).completed += async _ =>
            {
                // Setup Slider
                slider.value = 0;
                slider.maxValue = 1;
                slider.wholeNumbers = false;

                await UnloadAllScenes();

                foreach (var scene in searchedSceneContainer.Scenes)
                {
                    var asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                    asyncOperation.completed += _ =>
                    {
                        slider.value += 1 / searchedSceneContainer.Scenes.Length;
                    };

                    while (!asyncOperation.isDone)
                    {
                        await Task.Yield();
                    }
                }

                // Setup new current scene container
                CurrentSceneContainer = searchedSceneContainer;

                // Unload loading scene
                var asyncOperationLoadingScene = SceneManager.UnloadSceneAsync(LoadingScene);
                while (!asyncOperationLoadingScene.isDone)
                {
                    await Task.Yield();
                }
            };

            foreach (var scene in searchedSceneContainer.Scenes)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive).completed += _ =>
                {
                    slider.value += 1 / searchedSceneContainer.Scenes.Length;
                };
            }

            CurrentSceneContainer = searchedSceneContainer;

            SceneManager.UnloadSceneAsync(LoadingScene);
        }

        /// <summary>
        /// Await function
        /// </summary>
        private async void Awake()
        {
            this.SceneContainers = Resources.LoadAll<SceneContainer>("Utility/SceneContainers").ToList();
            LoadScenes(StartUpSceneContainer.Title);
        }

        /// <summary>
        /// Unloads all scenes from the <see cref="CurrentSceneContainer"/>
        /// </summary>
        /// <returns>AsyncTaks</returns>
        private async Task UnloadAllScenes()
        {
            if (CurrentSceneContainer == null) return;

            foreach (var scene in CurrentSceneContainer.Scenes)
            {
                var asyncOperation = SceneManager.UnloadSceneAsync(scene);
                while (!asyncOperation.isDone)
                {
                    await Task.Yield();
                }
            }
        }
    }
}
