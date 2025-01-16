using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Flamingo.SceneLoader
{
    public class SimpleLoadSceneService : ILoadSceneService, IInitializable
    {
        public event Action OnSceneStartLoad;
        public event Action OnSceneLoaded;
        private bool _waitAnimation = false;
        private int? _loadingSceneIndex;
        public void Initialize()
        {

        }
        public void LoadGameScene()
        {
            LoadScene(1);
        }
        public void LoadMenuScene()
        {
            LoadScene(0);
        }
        public void KeepWaitingForAnimation(bool waitAnimation)
        {
            _waitAnimation = waitAnimation;
        }

        public void OnAnimationFinish()
        {
            _waitAnimation = false;
            LoadSceneAsync(_loadingSceneIndex.Value).ConfigureAwait(false);
        }

        private void LoadScene(int index)
        {
            _loadingSceneIndex = index;
            OnSceneStartLoad?.Invoke();
            if (!_waitAnimation)
            {
                LoadSceneAsync(index).ConfigureAwait(false);
            }
        }
        public async Task LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
            OnSceneLoaded?.Invoke();
        }
    }
}