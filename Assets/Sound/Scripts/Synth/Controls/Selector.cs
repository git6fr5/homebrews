using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet;

namespace Monet.UI {

    [RequireComponent(typeof(BoxCollider2D))]
    public class Selector : UIComponent {

        [SerializeField] private Wave m_Wave;
        [SerializeField] private WaveShape m_Shape;

        void OnMouseDown() {
            m_Wave.SetShape(m_Shape);
        }

        void Update() {
            if (m_Wave.Shape == m_Shape) {
                GetComponent<SpriteRenderer>().material.SetFloat("_OutlineWidth", 0.05f);
            }
            else {
                GetComponent<SpriteRenderer>().material.SetFloat("_OutlineWidth", 0f);
            }
        }

    }

}

