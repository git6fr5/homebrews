/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// Adds a bullet pattern to the sequencer.
    /// </summary>
    public class Remove : Button {

        /* --- Variables --- */
        #region Variables

        // Components.
        [HideInInspector] public Indicator indicator;

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public override void Init() {
            base.Init();
            indicator = transform.parent.GetComponent<Indicator>();
        }

        #endregion


        /* --- Activation --- */
        #region Activation

        protected override void Activate() {
            indicator.sequencer.Remove(indicator.Index);
        }

        #endregion

    }

}

