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
    public class SynthFormDisplay : Display {

        [SerializeField] private Synth m_Synth;
        [SerializeField, ReadOnly] private float m_CombinedVolume = 1f;

        void Update() {
            m_CombinedVolume = m_Synth.Volume.Value;
        }
        
        protected override float[] WaveForm() {

            float fundamental = Score.Frequency(Score.Key.A);
            float sampleRate = !m_Macro ? Samples * fundamental : Samples * m_MacroTimeInterval;

            float[][] wavePackets = new float[m_Synth.Waves.Length][];
            for (int i = 0; i < m_Synth.Waves.Length; i++) {
                float[] wavePacket;
                if (!m_Macro) {
                    wavePacket = m_Synth.Waves[i].Generate(Samples, 1, 0, sampleRate, fundamental, true, false, true);
                }
                else {
                    wavePacket = m_Synth.Waves[i].Generate(Samples, 1, 0, sampleRate, fundamental, true, true, false);
                }
                wavePackets[i] = wavePacket;
            }

            float[] values = new float[Samples];
            for (int i = 0; i < Samples; i++) {
                values[i] = 0f;
                for (int j = 0; j < m_Synth.Waves.Length; j++) {
                    values[i] += m_CombinedVolume / m_Synth.Waves.Length * wavePackets[j][i];
                }
            }

            return values;

        }

    }

}