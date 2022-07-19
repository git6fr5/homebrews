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
    public class Distribution {

        [SerializeField, ReadOnly] public float[] m_Values;
        public float[] Values => m_Values;
        [SerializeField] private Draggable[] m_Draggables;
        [SerializeField] private float m_MaxValue = 1f;
        public float MaxValue => m_MaxValue;
        public float[] Ratios => GetRatios();

        public void Reset() {
            for (int i = 0; i < m_Draggables.Length; i++) {
                m_Draggables[i].SetValue(Mathf.Exp(-i));
                m_Draggables[i].Init();
            }
        }

        public void OnUpdate() {
            m_Values = new float[m_Draggables.Length];
            for (int i = 0; i < m_Draggables.Length; i++) {
                m_Values[i] = m_Draggables[i].Value * MaxValue;
            }
        }

        public void Load(DistributionData data) {
            for (int i = 0; i < m_Draggables.Length; i++) {
                if (i < data.Ratios.Length) {
                    m_Draggables[i].SetValue(data.Ratios[i]);
                }
            }
            m_MaxValue = data.MaxValue;
        }

        public float[] GetRatios() {
            float[] ratios = new float[m_Values.Length];
            for (int i = 0; i < m_Values.Length; i++) {
                ratios[i] = m_Values[i] / MaxValue;
            }
            return ratios;
        }

    }

}