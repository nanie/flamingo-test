using Flamingo.GameLoop.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Flamingo.GameLoop
{
    public class GameLoopController : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;

        internal event Action StartGame;
        internal event Action<bool> TurnEnded;
        private Vector3[] _positions;
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
        internal void OnBoardLoaded(BoardLoadedSignal boardLoadedSignal)
        {
            _positions = boardLoadedSignal.TilePositions;
            StartGame?.Invoke();
        }
        public void StartNewTurn()
        {
            _signalBus.Fire(GetNewTurnSignal());
        }

        private TurnStartedSignal GetNewTurnSignal()
        {
            int roll = UnityEngine.Random.Range(1, 7);
            Debug.Log($"Player Rolled {roll}");
            Vector3[] pos = new Vector3[roll];
            for (int i = 0; i < roll; i++)
            {
                pos[i] = _positions[((i + _currentPosition) % _positions.Length)];
            }
            _currentPosition = roll + _currentPosition % _positions.Length;
            return new TurnStartedSignal { TilePositions = pos };
        }
    }
}
