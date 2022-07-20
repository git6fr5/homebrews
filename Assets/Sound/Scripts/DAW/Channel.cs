/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class Channel : MonoBehaviour {

        [SerializeField] private Clef m_Clef;
        public Clef ChannelClef => m_Clef;
        [SerializeField] private Synth m_Synth;
        public Synth ChannelSynth => m_Synth;

        void GetVolumes() {
            // for (int i = 0; i < m_Channels.Count; i++) {
            //     m_Channels[i].synth.volume = m_Channels[i].synth.volumeKnob.value * maxVolume;
            // }
        }

        void PlayAt(int timeOffset) {
            
        }

    }

}