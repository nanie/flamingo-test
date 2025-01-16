using System;

namespace Flamingo.SceneLoader
{
    public interface ILoadSceneService
    {
        public void KeepWaitingForAnimation(bool waitAnimation);
        public void OnAnimationFinish();
        public event Action OnSceneStartLoad;
        public event Action OnSceneLoaded;
        public void LoadGameScene();
        public void LoadMenuScene();
    }
}