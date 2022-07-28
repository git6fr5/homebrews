// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Button : UIComponent {

        // Variables
        protected bool m_ActiveCondition => ActiveCondition();
        protected bool m_Hover = false;

        // A short duration for user feedback.
        public static float m_PressDuration = 0.15f;
        public static float m_DepressedScale = 0.75f;
        public bool m_Pressed => m_PressTicks != m_PressDuration;
        [SerializeField, ReadOnly] private float m_PressTicks;

        // Activates when this button is selected.
        void OnMouseDown() {
            if (!enabled) { return; }

            m_PressTicks = 0f;
            Activate();
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

        // Any logic that might be necessary while this button is active.
        void Update() {
            Hover(!m_Pressed && m_Hover, Time.deltaTime);
            Active(m_ActiveCondition, Time.deltaTime);
            Press(m_Pressed, Time.deltaTime);
        }

        // On hover.
        protected abstract void Hover(bool hover, float dt);

        // On activation.
        protected abstract void Activate();

        // While active.
        protected abstract void Active(bool active, float dt);

        // The condition to check if this is active.
        protected abstract bool ActiveCondition();

        // The condition to check if this is active.
        protected void Press(bool pressed, float dt) {
            if (pressed) {
                Tick(ref m_PressTicks, m_PressDuration, dt, true, false);
                Fade(m_PressTicks / m_PressDuration);
                Scale(m_DepressedScale + (1f - m_DepressedScale) * m_PressTicks / m_PressDuration, false);
            }
        }

    }

}