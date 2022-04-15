namespace FreddyNewton.Utility.SceneManagement
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// Singleton Scene Manager.
    /// A centralized script to control clean loading of additive scenes.
    /// With this it should be all encapsulated. 
    /// <see cref="SceneContainer"/> should be located in the Resources folder.
    /// </summary>
    public class SceneManagementController : Singleton<SceneManagementController>
    {
        [SerializeField] private string loadingScene = "LoadingScene";
        [SerializeField] private SceneContainer startUpSceneContainer;

        private List<SceneContainer> sceneContainers = new List<SceneContainer>();
        private SceneContainer currentSceneContainer;

        /// <summary>
        /// Unloads first all additive loaded Scenes.
        /// Loads based of the title all new Scenes.
        /// </summary>
        /// <param name="title">Search parameter to get the right <see cref="SceneContainer"/></param>
        public void LoadScenes(string title)
        {
            // find the searched SceneContainer by the title
            var searchedSceneContainer = this.sceneContainers.Find(scene => scene.Title == title);

            if (searchedSceneContainer == null || searchedSceneContainer.Scenes.Length == 0)
            {
                Debug.LogWarning("SceneContainer " + title + " not found or empty!");
                return;
            }

            // After Loading Scene is loaded, unload all current scenes and load the new ones
            SceneManager.LoadSceneAsync(this.loadingScene, LoadSceneMode.Additive).completed += async _ =>
            {
                await this.UnloadAllScenes();

                foreach (var scene in searchedSceneContainer.Scenes)
                {
                    var asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                    while (!asyncOperation.isDone)
                    {
                        await Task.Yield();
                    }
                }

                // Setup new current scene container
                this.currentSceneContainer = searchedSceneContainer;

                // Unload loading scene
                var asyncOperationLoadingScene = SceneManager.UnloadSceneAsync(this.loadingScene);
                while (!asyncOperationLoadingScene.isDone)
                {
                    await Task.Yield();
                }
            };
        }

        public void LoadScenes(string title, Slider slider)
        {
            // find the searched SceneContainer by the title
            var searchedSceneContainer = this.sceneContainers.Find(scene => scene.Title == title);

            if (searchedSceneContainer == null || searchedSceneContainer.Scenes.Length == 0)
            {
                Debug.LogWarning("SceneContainer " + title + " not found or empty!");
                return;
            }

            // After Loading Scene is loaded, unload all current scenes and load the new ones
            SceneManager.LoadSceneAsync(this.loadingScene, LoadSceneMode.Additive).completed += async _ =>
            {
                // Setup Slider
                slider.value = 0;
                slider.maxValue = 1;
                slider.wholeNumbers = false;

                await this.UnloadAllScenes();

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
                this.currentSceneContainer = searchedSceneContainer;

                // Unload loading scene
                var asyncOperationLoadingScene = SceneManager.UnloadSceneAsync(this.loadingScene);
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

            this.currentSceneContainer = searchedSceneContainer;

            SceneManager.UnloadSceneAsync(this.loadingScene);
        }

        /// <summary>
        /// Await function.
        /// </summary>
        private async void Awake()
        {
            this.sceneContainers = Resources.LoadAll<SceneContainer>("Utility/SceneContainers").ToList();
            this.LoadScenes(this.startUpSceneContainer.Title);
        }

        /// <summary>
        /// Unloads all scenes from the <see cref="currentSceneContainer"/>.
        /// </summary>
        /// <returns>AsyncTaks</returns>
        private async Task UnloadAllScenes()
        {
            if (currentSceneContainer == null) return;

            foreach (var scene in currentSceneContainer.Scenes)
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
