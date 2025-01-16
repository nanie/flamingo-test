using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flamingo.SceneLoader
{
    public class SimpleLoadSceneService : ILoadSceneService
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}