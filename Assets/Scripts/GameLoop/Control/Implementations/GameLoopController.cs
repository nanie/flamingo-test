using Flamingo.GameLoop.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Flamingo.GameLoop
{
    public class GameLoopController : IGameLoop
    {
        readonly SignalBus _signalBus;

        public event Action OnGameStarted;
        public event Action OnTurnEnded;
        public event Action<int> OnPlayerRoll;

        private BoardLoadedSignal.Tile[] _tiles;
        private int _currentPosition = 0;

        public GameLoopController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Dispose()
        {

        }
        public void Initialize()
        {

        }
        public void OnBoardLoaded(BoardLoadedSignal boardLoadedSignal)
        {
            _tiles = boardLoadedSignal.Tiles;
            OnGameStarted?.Invoke();
        }
        public void OnPlayerMoved(PlayerMovedSignal playerMovement)
        {
            if (!_tiles[_currentPosition].Minigame.HasValue)
            {
                _signalBus.Fire(new TurnEndedSignal { minigamePlayed = false, Score = 100 });
                OnTurnEnded?.Invoke();
                return;
            }
            _signalBus.Fire(new MinigameRequestedSignal { MinigameIndex = _tiles[_currentPosition].Minigame.Value });
        }
        public void OnMinigameCompleted(MinigameCompletedSignal minigameCompleteData)
        {
            _signalBus.Fire(new TurnEndedSignal { minigamePlayed = true, Score = minigameCompleteData.Score });
            OnTurnEnded?.Invoke();
        }
        public void StartNewTurn()
        {
            _signalBus.Fire(GetNewTurnSignal());
        }
        private TurnStartedSignal GetNewTurnSignal()
        {
            int roll = 2;//UnityEngine.Random.Range(1, 7);
            OnPlayerRoll.Invoke(roll);
            Debug.Log($"Player Rolled {roll}");
            Vector3[] pos = new Vector3[roll];
            for (int i = 0; i < roll; i++)
            {
                pos[i] = _tiles[((i + _currentPosition + 1) % _tiles.Length)].Position;
            }
            _currentPosition = roll + _currentPosition % _tiles.Length;
            return new TurnStartedSignal { TilePositions = pos };
        }
    }
}
