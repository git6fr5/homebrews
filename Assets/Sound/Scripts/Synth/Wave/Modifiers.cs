/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class Modifier {
        
        [SerializeField] private Knob m_Knob;
        [SerializeField] public float MaxValue = 1f;
        public float Value => m_Knob.Value * MaxValue;
        public float Ratio => Value / MaxValue;

    }

}