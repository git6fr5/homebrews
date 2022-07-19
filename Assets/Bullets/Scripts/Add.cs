/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// Adds a bullet pattern to the sequencer.
    /// </summary>
    public class Add : Button {

        /* --- Variables --- */
        #region Variables

        // Components.
        [HideInInspector] public Sequencer sequencer;

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public override void Init() {
            base.Init();
            sequencer = transform.parent.GetComponent<Sequencer>();
        }

        #endregion

        /* --- Activation --- */
        #region Activation

        protected override void Activate() {
            sequencer.Add();
        }

        #endregion

    }

}

