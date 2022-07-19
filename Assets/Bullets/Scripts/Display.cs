/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BulletPattern;

namespace BulletPattern {

    /// <summary>
    /// Adds a bullet pattern to the sequencer.
    /// </summary>
    public class Display : MonoBehaviour {

        /* --- Variables --- */
        #region Variables

        // Components.
        [SerializeField] public Text index;
        [SerializeField] public InputField count;
        public int Count => int.Parse(count.text);
        [SerializeField] public InputField interval;
        public float Interval => float.Parse(interval.text);

        #endregion

        /* --- Unity --- */
        #region Unity

        void Start() {
            Init();
        }

        void Update() {
            
        }

        #endregion

        /* --- Initialization --- */
        #region Initialization

        public void Init() {

        }

        #endregion

        /* --- Editing --- */
        #region Editing

        public void Set(Sequencer sequencer) {
            index.text = sequencer.ActivePatternIndex >= 0 ? sequencer.ActivePatternIndex.ToString() : "";
            count.text = sequencer.ActiveCount >= 0 ? sequencer.ActiveCount.ToString() : "";
            interval.text = sequencer.ActiveInterval >= 0 ? sequencer.ActiveInterval.ToString() : "";
        }

        #endregion
    }

}

