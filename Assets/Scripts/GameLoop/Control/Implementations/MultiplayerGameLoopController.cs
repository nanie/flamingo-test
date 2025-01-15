using Flamingo.GameLoop.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.GameLoop
{
    public class MultiplayerGameLoopController : IGameLoop
    {
        public event Action OnGameStarted;
        public event Action OnTurnEnded;
        public event Action<int> OnPlayerRoll;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void OnBoardLoaded(BoardLoadedSignal boardLoadedSignal)
        {
            throw new NotImplementedException();
        }

        public void OnMinigameCompleted(MinigameCompletedSignal minigameCompleteData)
        {
            throw new NotImplementedException();
        }

        public void OnPlayerMoved(PlayerMovedSignal playerMovement)
        {
            throw new NotImplementedException();
        }

        public void StartNewTurn()
        {
            throw new NotImplementedException();
        }
    }
}
