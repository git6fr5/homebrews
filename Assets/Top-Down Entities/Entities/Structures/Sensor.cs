using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A type of block that detects if another block is near it.
/// </summary>
public class Sensor : Block {

    /* --- Variables --- */
    [SerializeField] public bool isActivated = false;

    /* --- Unity --- */
    void Start() {
        box.isTrigger = true;
    }

    /* --- Override --- */
    protected override void Think() {
        if (group.Count != 0) {
            isActivated = true;
        }
        else {
            isActivated = false;
        }
    }

}
