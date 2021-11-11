/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behaviour of an object.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour {

    /* --- Static Variables --- */
    public static float DynamicFriction = 0.05f;

    /* --- Components --- */
    [HideInInspector] public Rigidbody2D body;

    /* --- Properties --- */
    // Action Controls
    [SerializeField] [ReadOnly] protected float moveSpeed; // The internal speed at which this entity is moving.
    [SerializeField] [ReadOnly] protected Vector2 movementVector; // The internal movement control.
    [SerializeField] [ReadOnly] protected Vector2 momentumVector; // The external movement control.
    [SerializeField] [ReadOnly] protected Vector2 orientationVector; // The direction this entity is facing.

    /* --- Unity --- */
    // Runs once on instantiation.
    void Awake() {
        // Cache these references.
        body = GetComponent<Rigidbody2D>();
        // Set up the attached components.
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.gravityScale = 0f;
        body.angularDrag = 0f;
    }

    // Runs every frame.
    void Update() {
        Think();
        DebugLines();
    }

    // Runs every fixed interval
    void FixedUpdate() {
        Move();
    }

    /* --- Thinking Actions --- */
    // Sets the action controls
    protected virtual void Think() {
        // Determined by the particular type of controller.
    }

    // Adjusts the velocity of this entity with respect to internal and external movement controls.
    void Move() {
        momentumVector *= (1f - DynamicFriction);
        body.velocity = moveSpeed * movementVector.normalized + momentumVector;
    }

    /* --- Debug --- */
    private void DebugLines() {
        Debug.DrawRay(transform.position, movementVector, Color.yellow);
    }

}
