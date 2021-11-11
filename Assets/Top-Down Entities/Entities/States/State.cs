/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

    /* --- Enumerations --- */
    // Orientation
    public enum Orientation {
        Right, Up, Left, Down, count
    }

    // Movement Flags
    public enum Movement {
        Idle,
        Moving,
        CarryingIdle,
        CarryingMoving,
        Sliding,
        Talking,
    }

    // Danger Flags(?)
    public enum Vitality {
        Healthy,
        Hurt,
        Dead
    }

    // Action Flags
    public enum Action {
        Inactive,
        Attacking, // Attack etc. as "ing" should be for movement and actions should just be verbs
        Jumping, // Jump
        Carrying, // Carry
        Throwing,
        Pushing,
    }

    /* --- Dictionaries --- */
    public static Dictionary<Orientation, Vector2> OrientationVectors = new Dictionary<Orientation, Vector2>() {
        {Orientation.Up, Vector2.up },
        {Orientation.Right, Vector2.right },
        {Orientation.Left, Vector2.left },
        {Orientation.Down, Vector2.down }
    };

    public static Dictionary<Orientation, Quaternion> OrientationAngles = new Dictionary<Orientation, Quaternion>() {
        {Orientation.Up, Quaternion.Euler(0, 0, 90) },
        {Orientation.Right, Quaternion.Euler(0, 0, 0) },
        {Orientation.Down, Quaternion.Euler(0, 0, 270) },
        {Orientation.Left, Quaternion.Euler(0, 180, 0) }
    };

    public static Dictionary<Vector2, Orientation> VectorOrientations = new Dictionary<Vector2, Orientation>() {
        { Vector2.up, Orientation.Up },
        { Vector2.right, Orientation.Right },
        { Vector2.left,Orientation.Left },
        { Vector2.down, Orientation.Down },
    };

    /* --- Variables --- */
    // Stats
    [SerializeField] [Range(0, 10)] public int maxHealth = 1;
    [SerializeField] [Range(0.05f, 20f)] public float baseSpeed = 1f; // How fast the entity moves.
    [SerializeField] [ReadOnly] public List<string> enemyTags = new List<string>(); // The entities this state considers an enemy.

    /* --- Properties --- */
    [ReadOnly] public int health; // The health 
    [ReadOnly] public Orientation orientation = Orientation.Right;
    [ReadOnly] public Movement movement = Movement.Idle;
    [ReadOnly] public Vitality vitality = Vitality.Healthy;
    [ReadOnly] public Action action = Action.Inactive;

    // Timed Action Buffer
    [HideInInspector] public Equipable equipped = null;
    [HideInInspector] Coroutine actionTimer;
    [HideInInspector] public float actionBuffer;

    // Timed State Buffers
    [HideInInspector] public Coroutine vitalityTimer;
    [Range(0.05f, 1f)] public float vitalityBuffer = 0.4f; // The interval between dying and despawning.

    /* --- Unity --- */
    // Runs once on instantiation.
    void Awake() {
        // Set the required state..
        health = maxHealth;
    }

    // Runs every frame.
    void Update() {
        ActionFlag();
    }

    /* --- External Event Actions --- */
    // Sets the enemies of this state.
    protected virtual void SetEnemies() {
        // Determined by the particular type of controller.
    }

    // Damages the state by the given damage.
    public void Hurt(int damage) {
        if (vitality == Vitality.Healthy) {
            health -= damage;
            vitality = Vitality.Hurt;
            if (health <= 0) {
                Death();
            }
        }
    }

    void ActionFlag() {
        if (equipped != null && equipped.gameObject.activeSelf) {
            action = equipped.action;
        }
    }

    // Deactivates the controller and sets the state to dead.
    public void Death() {
        vitality = Vitality.Dead;
    }

}
