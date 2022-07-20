using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet;

namespace Monet.UI {

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Toggle : UIComponent {

        [SerializeField] private Display m_Display;
        [SerializeField] private bool m_Macro;

        void OnMouseDown() {
            m_Display.SetMode(m_Macro);
        }

        void Update() {
            if (m_Display.Macro == m_Macro) {
                GetComponent<SpriteRenderer>().material.SetFloat("_OutlineWidth", 0.05f);
            }
            else {
                GetComponent<SpriteRenderer>().material.SetFloat("_OutlineWidth", 0f);
            }
        }

    }

}

