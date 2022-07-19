/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    [System.Serializable]
    public class Channel : MonoBehaviour {

        public Clef m_Clef;
        public Synth m_Synth;
        public int m_Index;
        public float m_Time;

        public Channel(Clef clef, Synth synth) {
            this.m_Time = 0f;
            this.m_Index = 0;
            this.m_Clef = clef;
            synth.m_Playable = false;
            this.m_Synth = synth;
        }

    }

}