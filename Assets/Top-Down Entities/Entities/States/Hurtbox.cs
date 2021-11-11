/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collider for hostile collisions to detect.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour {

    /* --- Components --- */
    [HideInInspector] protected Collider2D area;

    /* --- Variables ---*/
    [SerializeField] public State state; // A reference to the parent controlling this hitbox.

    /* --- Unity --- */
    // Runs once before the first frame.
    void Start() {
        // Cache these references.
        area = GetComponent<Collider2D>();
        // Set up the attached components.
        area.isTrigger = true;
    }

}
