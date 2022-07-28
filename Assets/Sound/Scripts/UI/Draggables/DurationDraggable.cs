// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class DurationDraggable : Draggable {

        // Components.
        [SerializeField] private NoteMarker Note;
        public static float DurationDragFactor = 0.75f;

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
            return DurationDragFactor;
        }

        protected override void UpperLimit() {
            Note.Longer();
            m_Value = 0.5f;
        }

        protected override void LowerLimit() {
            Note.Shorter();
            m_Value = 0.5f;
        }

        public static void Spline(SpriteShapeController controller, BoxCollider2D collider, float width, float height) {
            // Edits the spline of an obstacle
            controller.spline.Clear();
            controller.spline.InsertPointAt(0, Vector3.zero);
            controller.spline.InsertPointAt(1, width * Vector3.right);
            controller.spline.SetTangentMode(0, ShapeTangentMode.Continuous);
            controller.spline.SetTangentMode(1, ShapeTangentMode.Continuous);

            // Edits the collider.
            float offset = 0.25f;
            collider.size = new Vector2(width - offset, height);
            collider.offset = new Vector2(offset + width / 2f, 0f);
        }

    }
}