/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    public partial class Score {

        public enum Tone {
            Rest, 
            P1, 
            m2, 
            M2, 
            m3, 
            M3, 
            P4, 
            b5, 
            P5, 
            m6, 
            M6, 
            m7, 
            M7, 
            P8, 
            m9, 
            M9, 
            m10, 
            M10, 
            P11, 
            b12, 
            P12, 
            Count
        }

        public static Dictionary<Tone, float> JustTemperedRatios = new Dictionary<Tone, float>(){
            { Tone.Rest, 0f },
            { Tone.P1 , 1f },
            { Tone.M2 , 9f/8f },
            { Tone.M3 , 5f/4f },
            { Tone.P4 , 4f/3f },
            { Tone.P5 , 3f/2f },
            { Tone.M6 , 5f/3f },
            { Tone.M7 , 15f/8f },
            { Tone.P8 , 2f },
            { Tone.M9 , 18f/8f },
            { Tone.M10 , 10f/4f },
            { Tone.P11 , 8f/3f },
            { Tone.P12 , 6f/2f }
        };

        public static float Frequency(Tone tone, Key key) {
            if (JustTemperedRatios.ContainsKey[tone]) {
                return JustTemperedRatios.ContainsKey[tone] * Frequency(key);
            }
            return Frequency(key);
        }

    }
}