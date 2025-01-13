using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.Minigame
{
    public abstract class MinigameView : MonoBehaviour
    {
        private Minigame _minigame;
        public void Initialize(Minigame minigame)
        {
            _minigame = minigame;
            _minigame.OnDispose += Dispose;
            _minigame.OnGameEnd += Hide;
            _minigame.OnDataLoaded += OnDataLoaded;
        }
        private void Dispose()
        {
            _minigame.OnDispose -= OnMinigameDispose;
            _minigame.OnGameEnd -= Hide;
            _minigame.OnDataLoaded -= OnDataLoaded;
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
        public void EndGame()
        {
            _minigame.EndGame();
        }
    }
}
