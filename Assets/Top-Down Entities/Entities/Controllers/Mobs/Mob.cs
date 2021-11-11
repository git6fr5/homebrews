/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(State))]
public class Mob : Controller {

    /* --- Enumerations --- */
    public enum Behaviour {
        Idle,
        Move,
        Attack
    }

    /* --- Components --- */
    [SerializeField] [ReadOnly] public State state;

    /* --- Properties --- */
    [SerializeField] [ReadOnly] private Behaviour behaviour;
    [SerializeField] [ReadOnly] private Vector2 targetPoint;

    /* --- Unity --- */
    void Start() {
        // Cache these references.
        state = GetComponent<State>();
    }

    /* --- Override --- */
    protected override void Think() {

        Behaviour newBehaviour = DecideBehaviour();
        OnBehaviour(newBehaviour);
        WhileBehaviour(newBehaviour);

    }

    private void OnBehaviour(Behaviour newBehaviour) {
        if (behaviour != newBehaviour) {

            switch (newBehaviour) {

                case (Behaviour.Idle):
                    OnIdle();
                    return;
                case (Behaviour.Move):
                    OnMove();
                    return;
                case (Behaviour.Attack):
                    OnAttack();
                    return;
            }

        }
    }

    private void WhileBehaviour(Behaviour behaviour) {

        switch (behaviour) {

            case (Behaviour.Idle):
                WhileIdle();
                return;
            case (Behaviour.Move):
                WhileMove();
                return;
            case (Behaviour.Attack):
                WhileAttack();
                return;
        }
    }

    /* --- Methods --- */
    protected virtual Behaviour DecideBehaviour() {
        return Behaviour.Move;
    }

    private void OnMove() {
        targetPoint = GetTargetPoint();
    }

    private void WhileMove() {
        moveSpeed = GetMoveSpeed();
        movementVector = GetMovementVector();
        orientationVector = GetOrientation();
    }

    private void OnAttack() {

    }

    private void WhileAttack() {
        //IndicateAttack();
        //PerformAttack();
    }

    private void OnIdle() {

    }

    private void WhileIdle() {
    }

    /* --- Virtual --- */
    protected virtual Vector2 GetTargetPoint() {
        return transform.position;
    }

    protected virtual float GetMoveSpeed() {
        return state.baseSpeed;
    }

    protected virtual Vector2 GetMovementVector() {
        return Vector2.right;
    }

    protected virtual Vector2 GetOrientation() {
        return Vector2.right;
    }

}
