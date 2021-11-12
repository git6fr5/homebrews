using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Mob {

    public float idleInterval;

    protected override List<Behaviour> SetBehaviourCycle() {

        Behaviour idle = new Behaviour.Idle(this, 1f);
        // Behaviour move = new Behaviour.RangedCardinalMovement(this, 1, 3);
        Behaviour move = new Behaviour.FixedDiagonalMovement(this, 0, 1);

        List<Behaviour> newBehaviourCycle = new List<Behaviour>();
        newBehaviourCycle.Add(idle);
        newBehaviourCycle.Add(move);

        return newBehaviourCycle;
    }

}
