using Flamingo.GameLoop.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Flamingo.Player
{
    public class PlayerMovement : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        internal event Action<Vector3[]> OnPlayerStartMovement;

        public PlayerMovement(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Dispose()
        {
         
        }
        public void Initialize()
        {
            
        }
        internal void OnTurnStarted(TurnStartedSignal args)
        {
            OnPlayerStartMovement?.Invoke(args.TilePositions);
        }
        internal void FinishMovement()
        {
            _signalBus.Fire(new PlayerMovedSignal());
        }        
    }
}
