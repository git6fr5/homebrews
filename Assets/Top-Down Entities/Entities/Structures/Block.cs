/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Block : MonoBehaviour {

    /* --- Components --- */
    [SerializeField] [ReadOnly] public Collider2D box;

    /* --- Variables --- */
    [SerializeField] private int precision = 8; // The number of rays.
    [SerializeField] private float length = 1f; // The length of each ray.
    [SerializeField] private float outset = 0.35f; // The offset from the center of each ray.

    /* --- Properties ---*/
    [SerializeField] [ReadOnly] public List<Block> group = new List<Block>();
    [SerializeField] [ReadOnly] protected List<Vector2> rayVectors;
    [SerializeField] [ReadOnly] private List<Vector2> castedVectors = new List<Vector2>();
    [SerializeField] [ReadOnly] private List<Collider2D> castedCollisions = new List<Collider2D>();

    void Awake() {
        // Cache these components.
        box = GetComponent<Collider2D>();
        CreateRays();
    }

    void Update() {
        Think();
        Group();
    }

    // Determined by the particular type of block.
    protected virtual void Think() {
        //
    }

    private void CreateRays() {
        // Set up the list of rays.
        rayVectors = new List<Vector2>();
        for (int i = 0; i < precision; i++) {
            // Rotate by the appropriate amount.
            Vector2 newVector = Quaternion.Euler(0, 0, i * 360f / (float)precision) * Vector2.right;
            rayVectors.Add(newVector);
        }
    }

    private void Group() {

        // Get the ray cast collisions.
        castedVectors = new List<Vector2>();
        castedCollisions = new List<Collider2D>();
        for (int i = 0; i < rayVectors.Count; i++) {
            CastRay(i);
        }

        group = new List<Block>();
        for (int i = 0; i < castedCollisions.Count; i++) {
            if (castedCollisions[i] != null && castedCollisions[i].GetComponent<Block>() != null) {
                Block adjacentBlock = castedCollisions[i].GetComponent<Block>();
                group.Add(adjacentBlock);
            }
        }

    }

    private void CastRay(int orientation) {

        // Set the ray properties.
        Vector3 origin = transform.position + (Vector3)rayVectors[orientation] * outset;
        Vector2 direction = rayVectors[orientation];
        float distance = length - outset;

        // Check if the ray is colliding with anything.
        Color col = Color.green;
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider != null && hits[i].collider != box && !hits[i].collider.isTrigger) {
                col = Color.red;
                if (!castedVectors.Contains(rayVectors[orientation])) {
                    castedVectors.Add(rayVectors[orientation]);
                }
                if (!castedCollisions.Contains(hits[i].collider)) {
                    castedCollisions.Add(hits[i].collider);
                }
                break;
            }
        }

        Debug.DrawRay(origin, direction * distance, col);

    }
    
}
