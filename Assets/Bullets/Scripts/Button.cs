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
    public abstract class Button : MonoBehaviour {

        /* --- Variables --- */
        #region Variables

        // Components.
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public BoxCollider2D box;

        // Settings.
        [SerializeField, ReadOnly] private bool mouseOver;

        #endregion

        /* --- Unity --- */
        #region Unity

        void Start() {
            Init();
        }

        void Update() {
            GetInput();
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

        public virtual void Init() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            box = GetComponent<BoxCollider2D>();
        }

        #endregion

        /* --- Activation --- */
        #region Activation

        private void GetInput() {
            bool input0 = Input.GetMouseButtonDown(0);
            if (input0 && mouseOver) {
                Activate();
            }
        }

        protected abstract void Activate();

        #endregion

    }

}

