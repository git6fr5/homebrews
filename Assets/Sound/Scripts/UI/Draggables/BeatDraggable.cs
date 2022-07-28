// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class BeatDraggable : Draggable {

        // Components.
        [SerializeField] private Channel m_Channel;
        public NoteMarker Note => GetComponent<NoteMarker>();
        public static float BeatDragFactor = 0.75f;

        // Initialize.
        public override void Init() {
            m_Value = 0.5f;
        }

        // On hover.
        protected override void Hover(bool hover, float dt) {
            //
        }

        protected override void OnDrag(bool dragging) {
            //
        }

        protected override float GetDragFactor() {
            return BeatDragFactor;
        }

        protected override void UpperLimit() {
            Note.Move(1f, m_Channel);
            m_Value = 0.5f;
        }

        protected override void LowerLimit() {
            Note.Move(-1f, m_Channel);
            m_Value = 0.5f;
        }

    }
}