using Flamingo.GameLoop.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.Minigame
{
    public class Minigame
    {
        private readonly SignalBus _signalBus;
        public int Score { get; set; }
        public event Action OnDispose;
        public event Action OnGameEnd;
        public event Action OnDataLoaded;
        public Minigame(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void EndGame()
        {
            _signalBus.Fire(new MinigameCompletedSignal { Score = Score });
            OnGameEnd?.Invoke();
        }
        private void Reset()
        {
            Score = 0;
        }
        public class Pool : MemoryPool<Minigame>
        {
            protected override void Reinitialize(Minigame minigame)
            {
                minigame.Reset();
            }
        }
        internal void LoadData(string text)
        {
            OnDataLoaded.Invoke();
        }
    }
}
