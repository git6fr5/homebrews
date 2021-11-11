using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Action = State.Action;

public class Equipable : MonoBehaviour {

    public Action action;
    public float slowFactor; // The factor by which using this will slow down the user.

    public void Activate() {

    }
}
