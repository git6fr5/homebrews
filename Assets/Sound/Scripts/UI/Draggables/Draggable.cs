/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public abstract class Draggable : UIComponent {

        protected bool m_Hover = false;
        [SerializeField, ReadOnly] protected Vector2 m_Origin;
        [SerializeField, Range(0f, 1f)] protected float m_Value = 0f;
        public float Value => m_Value;
        [SerializeField, ReadOnly] protected bool m_Dragging = false;
        public float DragFactor => GetDragFactor();
        [SerializeField, ReadOnly] protected Vector2 m_CacheMousePosition;
        public static float DefaultDragFactor = 0.25f;

        [SerializeField] private bool m_Horizontal = false;

        void Update() {
            m_Dragging = UnityEngine.Input.GetMouseButtonUp(0) ? false : m_Dragging;
            Hover(!m_Dragging && m_Hover, Time.deltaTime);
            OnDrag(m_Dragging);
            if (m_Dragging) {
                Drag();
            }
        }

        // Start the dragging.
        void OnMouseDown() {
            if (!enabled) { return; }

            m_Dragging = true;
            m_CacheMousePosition = UIComponent.MousePosition;
        }

        // When the mouse moves over this.
        void OnMouseOver() {
            if (!enabled) { return; }

            m_Hover = true;
        }

        // When the mouse moves leaves this.
        void OnMouseExit() {
            if (!enabled) { return; }

            m_Hover = false;
        }

        void Drag() {

            // Check how much the mouse has moved.
            Vector2 mousePosition = UIComponent.MousePosition;
            float valueIncrement = mousePosition.y - m_CacheMousePosition.y;
            if (m_Horizontal) {
                valueIncrement = mousePosition.x - m_CacheMousePosition.x;
            }

            // Set the draggable value.
            m_Value += valueIncrement * DragFactor;
            if (m_Value > 1f) {
                UpperLimit();
            }
            if (m_Value < 0f) {
                LowerLimit();
            }
            m_CacheMousePosition = mousePosition;

        }

        public abstract void Init();

        protected abstract void OnDrag(bool dragging);

        protected abstract void Hover(bool hover, float dt);

        protected virtual void UpperLimit() {
            m_Value = 1f;
        }

        protected virtual void LowerLimit() {
            m_Value = 0f; 
        }

        protected virtual float GetDragFactor() {
            return DefaultDragFactor;
        }

        public void SetValue(float value) {
            m_Value = value;
            OnDrag(true);
        }

    }

}