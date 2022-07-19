/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// Interactable button for the bullet pattern sequencer.
    /// Requires a sequencer to be attached to the parent object.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Draggable : MonoBehaviour {

        /* --- Variables --- */
        #region Variables

        // Components.
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public BoxCollider2D box;

        // Settings.
        [SerializeField, ReadOnly] private bool active;
        [SerializeField, ReadOnly] private bool mouseOver;

        // offset
        public Draggable offset;
        public Vector2 Offset => offset != null ? (Vector2)offset.transform.localPosition : Vector2.zero;
        public float OffsetAngle => offset != null ? Vector2.SignedAngle(transform.position, offset.transform.position) : 0f;

        #endregion

        /* --- Unity --- */
        #region Unity

        void Start() {
            Init();
        }

        void Update() {
            GetInput();
            Move();
        }

        void OnMouseEnter() {
            mouseOver = true;
        }

        void OnMouseExit() {
            mouseOver = false;
        }

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public Draggable Create(Vector2 position) {
            Draggable draggable = Instantiate(gameObject, position, Quaternion.identity, transform.parent).GetComponent<Draggable>();
            draggable.gameObject.SetActive(true);
            return draggable;
        }

        public virtual void Init() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            box = GetComponent<BoxCollider2D>();
        }

        #endregion

        /* --- Activation --- */
        #region Activation

        private void GetInput() {
            bool inputDown0 = Input.GetMouseButtonDown(0);
            bool inputUp0 = Input.GetMouseButtonUp(0);

            if (inputDown0 && mouseOver) {
                active = true;
            }

            if (inputUp0) {
                active = false;
            }

            spriteRenderer.material.SetFloat("_OutlineWidth", active ? 1f / 16f : 0f);

            if (offset != null) {
                offset.transform.position = offset.transform.position.normalized * transform.position.magnitude;
            }
        }

        protected void Move() {
            if (!active) { return; }
            transform.position = (Vector3)(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion

        /* --- Editing --- */
        #region Editing

        public void Set(Vector2 velocity) {
            transform.position = velocity;
        }

        #endregion

    }

}

