using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Mob {


    protected override Behaviour DecideBehaviour() {
        if (behaviour == Behaviour.Move) {
            if ((transform.position - targetPoint).magnitude)
        }
        return Behaviour.Move;
    }


}
