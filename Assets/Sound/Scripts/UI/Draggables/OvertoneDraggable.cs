/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class OvertoneDraggable : Draggable {

        // Settings.
        public static Vector2 Size = new Vector2(0.25f, 0.25f);
        public static float LineScale = 2.5f;
        public static float LineWidth = 0.1f;
        public NoteMarker Note => GetComponent<NoteMarker>();

        public override void Init() {
            m_Origin = transform.localPosition;
            UICollider.size = Size;
            UILineRenderer.startWidth = LineWidth;
            UILineRenderer.endWidth = LineWidth;
            UILineRenderer.useWorldSpace = true;
            gameObject.SetActive(true);
        }

        // On hover.
        protected override void Hover(bool hover, float dt) {
            Scale(1.15f, !hover);
        }

        protected override void OnDrag(bool dragging) {
            Highlight(dragging);

            if (!dragging) { return; }
            transform.localPosition = (Vector3)m_Origin + m_Value * LineScale * Vector3.up;
            UILineRenderer.SetPositions(new Vector3[2] { transform.position - m_Value * LineScale * Vector3.up, transform.position });
        }

    }

}