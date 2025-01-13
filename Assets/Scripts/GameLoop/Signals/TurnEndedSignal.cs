using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.GameLoop.Signals
{
    public class TurnEndedSignal 
    {
        public int Score { get; set; }
        public bool minigamePlayed { get; set; }
    }
}
