/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class SynthIOButton : MonoBehaviour {

        [SerializeField] private string m_SynthName;
        [SerializeField] private Synth m_Synth;
        [SerializeField] private bool m_Save;
        [SerializeField] private bool m_Load;

        void OnMouseDown() {
            if (m_Save) {
                m_Synth.Save(m_SynthName);
            }
            if (m_Load) {
                m_Synth.Open(m_SynthName);
            }
        }
        

    }
}