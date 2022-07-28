/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    ///<summary>
    ///
    ///<summary>
    public class ToneDraggable : Draggable {

        // Components.
        public NoteMarker Note => GetComponent<NoteMarker>();
        public static float ToneDragFactor = 1f;

        public override void Init() {
            m_Value = 0.5f;
        }

        // On hover.
        protected override void Hover(bool hover, float dt) {
            // Scale(1.15f, !hover);
        }

        protected override void OnDrag(bool dragging) {
            Highlight(dragging);
        }

        protected override float GetDragFactor() {
            return ToneDragFactor;
        }

        protected override void UpperLimit() {
            Note.Ascend();
            m_Value = 0.5f;
        }

        protected override void LowerLimit() {
            Note.Descend();
            m_Value = 0.5f;
        }

    }

}