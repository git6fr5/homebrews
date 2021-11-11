/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores stuff.
/// </summary>
public class Inventory : MonoBehaviour {

    /* --- Components --- */
    public Transform equipmentParentTransform;

    /* --- Variables --- */
    public int maxEquipment;
    public int maxCollectibles;

    /* --- Properties --- */
    public Equipable active = null;
    public List<Equipable> equipment = new List<Equipable>();
    public Dictionary<Collectible.Type, int> collection = new Dictionary<Collectible.Type, int>();

    /* --- Unity --- */
    void Update() {
        active = GetActiveEquipment();
    }

    /* --- Methods --- */
    // Activates a piece of equipment.
    public void Equip(Equipable equipable) {
        if (equipment.Count < maxEquipment) {
            equipment.Add(equipable);
        }
    }

    public void ActivateEquipable(int index = 0) {
        if (index < equipment.Count && active == null) {
            equipment[index].Activate();
        }
    }

    public Equipable GetActiveEquipment() {
        for (int i = 0; i < equipment.Count; i++) {
            if (equipment[i].gameObject.activeSelf) {
                return equipment[i];
            }
        }
        return null;
    }

    // Collects the collectible.
    public void Collect(Collectible collectible) {
        if (collection.ContainsKey(collectible.type)) {
            collection[collectible.type] += collectible.value;
        }
        else {
            collection.Add(collectible.type, collectible.value);
        }
        Debug.Log("Collected " + collectible.value + " of " + collectible.type);
        Destroy(collectible.gameObject);
    }

}
