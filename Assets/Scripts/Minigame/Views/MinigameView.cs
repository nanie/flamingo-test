using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.Minigame
{
    public abstract class MinigameView : MonoBehaviour
    {
        protected Minigame _minigame;

        private void OnDisable()
        {
            _minigame.OnDispose -= OnMinigameDispose;
            _minigame.OnGameEnd -= Hide;
            _minigame.OnDataLoaded -= OnDataLoaded;
        }
        public void Initialize(Minigame minigame)
        {
            _minigame = minigame;
            _minigame.OnDispose += Dispose;
            _minigame.OnGameEnd += Hide;
            _minigame.OnDataLoaded += OnDataLoaded;
            Initialize();
        }
        private void Dispose()
        {            
            OnMinigameDispose();
        }
        private void OnDataLoaded()
        {         
            LoadData();
            Show();
        }
        public abstract void Show();
        public abstract void Hide();
        public abstract void OnMinigameDispose();
        public abstract void LoadData();
        public abstract void Initialize();
        public void EndGame()
        {
            _minigame.EndGame();
        }
    }
}
