using Flamingo.GameLoop.Signals;
using System;
using Zenject;

namespace Flamingo.GameLoop
{
    public interface IGameLoop : IInitializable, IDisposable
    {
        public void OnBoardLoaded(BoardLoadedSignal boardLoadedSignal);
        public void OnPlayerMoved(PlayerMovedSignal playerMovement);
        public void OnMinigameCompleted(MinigameCompletedSignal minigameCompleteData);
        public void StartNewTurn();

        public event Action OnGameStarted;
        public event Action OnTurnEnded;
        public event Action<int> OnPlayerRoll;
    }
}
