using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Compass : MonoBehaviour {

    public static Vector2 SnapVector(Vector2 vector) {
        // Get rid of the the smaller value.
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y)) {
            vector.y = 0f;
        }
        else {
            vector.x = 0f;
        }
        return vector.normalized;
    }

}
