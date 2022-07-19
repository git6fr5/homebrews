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
    public class Draggable : MonoBehaviour {

        // Settings.
        public static Vector2 Size = new Vector2(0.25f, 0.25f);
        public static float Scale = 3f;
        public static float LineWidth = 0.1f;
        public static float DragFactor = 0.25f;

        // Components.
        public LineRenderer m_LineRenderer => GetComponent<LineRenderer>();
        public BoxCollider2D m_BoxCollider => GetComponent<BoxCollider2D>();

        [SerializeField, ReadOnly] private Vector2 m_Origin;
        [SerializeField, Range(0f, 1f)] private float m_Value = 0f;
        public float Value => m_Value;
        [SerializeField, ReadOnly] private bool m_Dragging = false;
        [SerializeField, ReadOnly] private Vector2 m_CacheMousePosition;


        public void Init() {
            m_Origin = transform.localPosition;
            m_BoxCollider.size = Size;
            m_LineRenderer.startWidth = LineWidth;
            m_LineRenderer.endWidth = LineWidth;
            gameObject.SetActive(true);
        }

        void Update() {
            m_Dragging = UnityEngine.Input.GetMouseButtonUp(0) ? false : m_Dragging;
            if (m_Dragging) {
                Drag();
            }
        }

        void OnMouseDown() {
            m_Dragging = true;
            m_CacheMousePosition = UIComponent.MousePosition;
        }

        void Drag() {

            // Check how much the mouse has moved.
            Vector2 mousePosition = UIComponent.MousePosition;
            float valueIncrement = mousePosition.y - m_CacheMousePosition.y;
            
            // Set the draggable value.
            m_Value += valueIncrement * DragFactor;
            if (m_Value > 1f) { 
                m_Value = 1f; 
            }
            if (m_Value < 0f) { 
                m_Value = 0f; 
            }
            m_CacheMousePosition = mousePosition;

            // Set the position.
            transform.localPosition = (Vector3)m_Origin + m_Value * Scale * Vector3.up;

        }

        public void SetValue(float value) {
            m_Value = value;
        }

    }

}