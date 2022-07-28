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
    public class WaveFormDisplay : Display {

        [SerializeField] private Wave m_Wave;
  
        protected override float[] WaveForm() {

            float fundamental = Score.Frequency(Score.Key.A);
            float sampleRate = !m_Macro ? Samples * fundamental : Samples * m_MacroTimeInterval;

            float[] wavePacket = new float[Samples];
            if (!m_Macro) {
                wavePacket = m_Wave.Generate(Samples, 1, 0, sampleRate, fundamental, true, false, true);
            }
            else {
                wavePacket = m_Wave.Generate(Samples, 1, 0, sampleRate, fundamental, true, true, false);
            }

            float[] values = new float[Samples];
            for (int i = 0; i < Samples; i++) {
                values[i] = wavePacket[i];
            }

            return values;

        }

    }

}