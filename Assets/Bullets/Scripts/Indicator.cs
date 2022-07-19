/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// Adds a bullet pattern to the sequencer.
    /// </summary>
    public class Indicator : Button {

        /* --- Variables --- */
        #region Variables

        // Components.
        [HideInInspector] public Sequencer sequencer;

        // Settings.
        [SerializeField, ReadOnly] private int index;
        public int Index => index;
        [SerializeField, ReadOnly] private Vector3 origin;

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public void Create(Sequencer sequencer, int index) {
            Indicator newIndicator = Instantiate(gameObject, transform.position + Vector3.right * index, Quaternion.identity, sequencer.transform).GetComponent<Indicator>();
            newIndicator.Init(index, transform.position);
        }

        public void Init(int index, Vector3 origin) {
            base.Init();
            sequencer = transform.parent.GetComponent<Sequencer>();
            this.origin = origin;
            SetIndex(index);
            SetPosition();
            gameObject.SetActive(true);
        }

        #endregion

        /* --- Activation --- */
        #region Activation

        private void SetIndex(int index) {
            this.index = index;
        }

        protected override void Activate() {
            sequencer.SetActivePattern(index);
        }

        public void Highlight(bool highlight) {
            spriteRenderer.material.SetFloat("_OutlineWidth", highlight ? 1f / 16f : 0f);
        }

        public void SetPosition() {
            transform.position = origin + Vector3.right * 1.25f * index;
        }

        public void MoveTo(int index) {
            this.index = index;
            SetPosition();
        }

        #endregion

    }

}

