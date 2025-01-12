using Flamingo.GameLoop.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Flamingo.Player
{
    public class PlayerMovement : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;

        public PlayerMovement(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Dispose()
        {
            _signalBus.Subscribe<TurnStartedSignal>(OnTurnStarted);
        }

        public void Initialize()
        {
            _signalBus.Unsubscribe<TurnStartedSignal>(OnTurnStarted);
        }

        private void OnTurnStarted(TurnStartedSignal args)
        {
            DoPlayerMovement(args.TilePositions);
        }

        private void DoPlayerMovement(Vector3[] tilePositions)
        {
            //TODO do player movement and then Fire event
            _signalBus.Fire(new PlayerMovedSignal());
        }        
    }
}
