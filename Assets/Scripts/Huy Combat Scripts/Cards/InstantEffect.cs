using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Instant_Effect {
    //Base class for instant effects
    //Instant effects are activated right away when a card is played.
    //Instant effects do not require winning the chain 
    //Cards can have both Instant Effects and Main Effect (main effect is activated only when winning the chain)
    interface InstantEffect {
        void ActivateInstantEffect();

    }
}


