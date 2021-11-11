/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collider to be able to collect stuff.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Collectionbox : MonoBehaviour {

    /* --- Components --- */
    public CircleCollider2D detectionArea;
    public BoxCollider2D collectionArea;

    /* --- Variables ---*/
    [SerializeField] public Inventory inventory; // A reference to the parent controlling this hitbox.
    public List<Equipable> equipables = new List<Equipable>();

    /* --- Unity --- */
    // Runs once on instantiation
    void Awake() {
        // Set up the attached components.
        detectionArea.isTrigger = true;
        collectionArea.isTrigger = true;
    }

    // Called when the attached collider intersects with another collider.
    void OnTriggerEnter2D(Collider2D collider) {
        if (collectionArea.IsTouching(collider)) {
            ScanItem(collider, true);
        }
    }

    // Called when the attached collider intersects with another collider.
    void OnTriggerExit2D(Collider2D collider) {
        ScanItem(collider, false);
    }

    /* --- Methods --- */
    // Scans for whether to trigger an hit event.
    void ScanItem(Collider2D collider, bool collect) {
        
        // If we intersected with a new collectible or equipable.
        if (collider.GetComponent<Collectible>() != null && collect) {
            // Collecting a collectible should destroy the object, so we don't need to track it.
            Collectible collectible = collider.GetComponent<Collectible>();
            OnAdd(collectible);
        }
        // However, we should track the equipables.
        if (collider.GetComponent<Equipable>() != null) {
            Equipable equipable = collider.GetComponent<Equipable>();
            if (!equipables.Contains(equipable) && collect) {
                equipables.Add(equipable);
                OnAdd(equipable);
            }
            else if (equipables.Contains(equipable) && !collect) {
                equipables.Remove(equipable);
            }
        }
    }

    // Reset the container.
    public void Reset() {
        equipables = new List<Equipable>();
    }

    // Reset the container.
    public bool IsEmpty() {
        return (equipables.Count == 0);
    }

    protected virtual void OnAdd(Collectible collectible) {
        inventory.Collect(collectible);
    }

    protected virtual void OnAdd(Equipable equipable) {
        //
    }

}
