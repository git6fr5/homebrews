// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet.UI;

namespace Monet.UI {

    [RequireComponent(typeof(BoxCollider2D))]
    public class Knob : UIComponent {

        // Settings.
        public static float RotationFactor = 0.35f;
        public static float MinimumAngle = -120f;
        public static float AngleRange = 240f;

        [SerializeField, Range(0f, 1f)] private float m_Value = 0f;
        public float Value => m_Value;
        [SerializeField, ReadOnly] protected bool m_Turning = false;
        [SerializeField, ReadOnly] protected Vector2 m_CacheMousePosition;

        void Start() {
            SetValue(0.5f);
        }

        void Update() {
            m_Turning = UnityEngine.Input.GetMouseButtonUp(0) ? false : m_Turning;
            if (m_Turning) {
                Turn();
            }
        }

        void OnMouseDown() {
            m_Turning = true;
            m_CacheMousePosition = UIComponent.MousePosition;
        }

        void Turn() {

            // Check how much the mouse has moved.
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            float valueIncrement = mousePosition.y - m_CacheMousePosition.y;

            // Set the knob value.
            m_Value += valueIncrement * RotationFactor;
            if (m_Value > 1f) { 
                m_Value = 1f; 
            }
            if (m_Value < 0f) { 
                m_Value = 0f; 
            }
            m_CacheMousePosition = mousePosition;

            // Set the knob angle.
            transform.eulerAngles = Vector3.forward * (-m_Value * AngleRange - MinimumAngle);

        }

        public void SetValue(float value) {
            m_Value = value;
            transform.eulerAngles = Vector3.forward * (-m_Value * AngleRange - MinimumAngle);
        }

    }

}
