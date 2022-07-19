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
    [RequireComponent(typeof(BoxCollider2D))]
    public class OctavePedal : UIComponent {

        [SerializeField] private Wave m_Wave;
        [SerializeField] private int m_Value;

        void OnMouseDown() {
            m_Wave.ShiftOctave(m_Value);
        }

    }

}