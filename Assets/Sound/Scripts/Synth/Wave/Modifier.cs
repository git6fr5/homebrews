/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;
using Monet.IO;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    [System.Serializable]
    public class Modifier {
        
        [SerializeField, ReadOnly] public float m_Value;
        public float Value => m_Value;
        [SerializeField] private Knob m_Knob;
        [SerializeField] private float m_MaxValue = 1f;
        public float MaxValue => m_MaxValue;
        public float Ratio => Value / MaxValue;

        public void OnUpdate() {
            m_Value = m_Knob.Value * MaxValue;
        }

        public void Load(ModifierData data) {
            m_Knob.SetValue(data.Ratio);
            m_MaxValue = data.MaxValue;
        }

    }

}