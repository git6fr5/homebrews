/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(State))]
public class Mob : Controller {

    /* --- Enumerations --- */

    /* --- Dictionary --- */
    public List<Behaviour> behaviourCycle = new List<Behaviour>();

    /* --- Components --- */
    [SerializeField] [ReadOnly] public State state;

    /* --- Properties --- */
    [SerializeField] [ReadOnly] private int behaviourIndex;
    [SerializeField] [ReadOnly] public Vector2 targetPoint;

    /* --- Unity --- */
    void Start() {
        // Cache these references.
        state = GetComponent<State>();
        behaviourCycle = SetBehaviourCycle();
        behaviourCycle[behaviourIndex].OnBehaviour();
    }

    /* --- Override --- */
    protected override void Think() {

        DecideBehaviour();
        if (behaviourIndex < behaviourCycle.Count) {
            behaviourCycle[behaviourIndex].WhileBehaviour();
        }

    }

    private void DecideBehaviour() {
        if (behaviourCycle.Count > 0 && behaviourCycle[behaviourIndex].EndCondition()) {
            print("Ending Behaviour");
            behaviourIndex = (behaviourIndex + 1) % behaviourCycle.Count;
            behaviourCycle[behaviourIndex].OnBehaviour();
        }
    }

    protected virtual List<Behaviour> SetBehaviourCycle() {
        //
        return new List<Behaviour>();
    }

}
