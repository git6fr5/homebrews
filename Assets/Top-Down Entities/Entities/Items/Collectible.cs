using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Collectible : MonoBehaviour {

    /* --- Static Variables --- */
    private static float MaxTorque = 500f;
    private static float RotationPrecision = 0.05f;

    /* --- Enumerations --- */
    public enum Type {
        // Some basic collectible examples.
        Coin,
        Key,
        Seed
    }

    /* --- Components --- */
    protected CircleCollider2D area;
    protected Rigidbody2D body;
    public Transform shadow;

    /* --- Variables --- */

    // Collection
    [SerializeField] protected float collectDelay = 0f;
    // Floating
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] [Range(0.05f, 5f)] private float floatPeriod = 0.75f; // The duration it will float in a direction before turning.
    // Magnetize
    [SerializeField] public float magnetizeSpeed = 3f;

    /* --- Properties --- */
    // Settings
    public Type type; // The type of the collectible
    public int value; // The amount that this collectible has.

    // Motion
    [SerializeField] [ReadOnly] public Collectionbox magnet = null; // Move towards this.
    [SerializeField] [ReadOnly] private Vector2 origin; // The original position of this transform.
    [SerializeField] [ReadOnly] private float timeInterval;

    // Shadow
    [SerializeField] [ReadOnly] private Vector2 shadowOffset;

    /* --- Unity --- */
    // Runs once on instantiation.
    void Awake() {
        // Cache these components.
        area = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();

        // Set up the components.
        area.isTrigger = true;
        StartCoroutine(IECollectionDelay(collectDelay));
        body.constraints = RigidbodyConstraints2D.None;
        body.isKinematic = false;
        body.gravityScale = 0f;

        // Store these positions.
        origin = transform.position;
        shadowOffset = (Vector2)shadow.localPosition - origin;
    }

    // Runs when this component is enabled.
    void OnEnable() {
        Reset();
    }

    void Update() {
        if (magnet == null) {
            Brake();
            Float();
            Shadow();
        }
        else {
            Magnetize();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (magnet == null && collider.GetComponent<Collectionbox>() != null) {
            magnet = collider.GetComponent<Collectionbox>();
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.GetComponent<Collectionbox>() != null && collider.GetComponent<Collectionbox>() == magnet) {
            magnet = null;
        }
    }

    /* --- Methods --- */
    private void Reset() {
        // Reset the transform.
        transform.position = origin;
        timeInterval = 0f;
    }

    private void Float() {
        //transform.localScale = new Vector3(1f, 1f, 1f); // Make this scale towards.
        timeInterval += Time.deltaTime;
        body.velocity = floatSpeed * new Vector2(0f, Mathf.Sin((2 * Mathf.PI / floatPeriod) * timeInterval));
    }

    private void Brake() {
        // Brake any remnant torque.
        if (Mathf.Abs(body.angularVelocity) > RotationPrecision) {
            body.angularVelocity *= 0.95f;
        }
        else {
            body.angularVelocity = 0f;
        }
        // Straighten the transform.
        if (Mathf.Abs(transform.eulerAngles.z) > RotationPrecision) {
            transform.eulerAngles += -transform.eulerAngles * Time.deltaTime * 5f;
        }
        else {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void Magnetize() {
        // Rotate.
        if (body.angularVelocity < MaxTorque) {
            body.AddTorque(10f);
        }
        body.velocity += (Vector2)(magnet.transform.position - transform.position).normalized * Time.deltaTime * 5f;
        shadow.gameObject.SetActive(false);
    }

    private void Shadow() {
        shadow.eulerAngles = Vector3.zero;
        // shadow.position = new Vector3(transform.position.x + shadowOffset.x, shadowOffset.y, transform.position.z);
        shadow.gameObject.SetActive(true);
    }

    /* --- Coroutines --- */
    private IEnumerator IECollectionDelay(float delay) {
        // Turn off the collection.
        area.enabled = false;
        // Wait for a few seconds then turn it back on.
        yield return new WaitForSeconds(delay);
        yield return area.enabled = true;
    }

}
