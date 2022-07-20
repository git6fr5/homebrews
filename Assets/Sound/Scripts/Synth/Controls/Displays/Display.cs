/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class Display : MonoBehaviour {

        [SerializeField] protected bool m_Macro;
        public bool Macro => m_Macro;
        [SerializeField] protected float m_MacroTimeInterval = 3f;

        public static int Samples = 256;
        // public static float Scale = 3f;
        [SerializeField] protected float Scale;
        [Range(0.1f, 1f)] protected float m_UpdateInterval;

        [SerializeField] private GameObject m_Point;
        [SerializeField, ReadOnly] private GameObject[] m_Points;

        
        void Awake() {
            CreatePoints();
            StartCoroutine(IEWaveForm(m_UpdateInterval));
        }

        private void CreatePoints() {
            m_Points = new GameObject[Samples];
            for (int i = 0; i < Samples; i++) {
                GameObject point = Instantiate(m_Point);
                point.transform.parent = transform;
                point.SetActive(true);
                m_Points[i] = point;
            }
        }

        IEnumerator IEWaveForm(float delay) {
            while (true) {
                yield return new WaitForSeconds(delay);
                float[] values = WaveForm();
                MovePoints(values);
            }
        }


        protected virtual float[] WaveForm() {
            return new float[Samples];            
        }

        private void MovePoints(float[] values) {
            for (int i = 0; i < Samples; i++) {
                m_Points[i].transform.localPosition = (0.85f * Scale / 2f * Vector3.up * values[i]) + Vector3.right * ((float)i / Samples * Scale - Scale / 2f);
            }
        }

        public void SetMode(bool macro) {
            m_Macro = macro;
        }

    }

}