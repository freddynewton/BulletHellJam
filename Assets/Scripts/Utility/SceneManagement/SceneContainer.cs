using UnityEngine;

namespace FreddyNewton.Utility.SceneManagement
{
    /// <summary>
    /// ScriptableObject that contains a list of Scene names that can be loaded by the <see cref="SceneManagementController"/> additive based on the Title.
    /// </summary>
    [CreateAssetMenu(fileName = "Scene Container", menuName = "Utility/SceneManagement/SceneContainer")]
    public class SceneContainer : ScriptableObject
    {
        /// <summary>
        /// Title for searching purpose in the <see cref="SceneManagementController"/>.
        /// </summary>
        public string Title;

        /// <summary>
        /// List of strings with scene names.
        /// </summary>
        public string[] Scenes;
    }
}