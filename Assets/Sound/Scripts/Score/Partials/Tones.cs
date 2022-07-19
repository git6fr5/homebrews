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
            min2, 
            Maj2, 
            min3, 
            Maj3, 
            P4, 
            b5, 
            P5, 
            min6, 
            Maj6, 
            min7, 
            Maj7, 
            P8, 
            min9, 
            Maj9, 
            min10, 
            Maj10, 
            P11, 
            b12, 
            P12, 
            Count
        }

        public static Dictionary<Tone, float> JustTemperedRatios = new Dictionary<Tone, float>(){
            { Tone.Rest, 0f },
            { Tone.P1 , 1f },
            { Tone.Maj2 , 9f/8f },
            { Tone.Maj3 , 5f/4f },
            { Tone.P4 , 4f/3f },
            { Tone.P5 , 3f/2f },
            { Tone.Maj6 , 5f/3f },
            { Tone.Maj7 , 15f/8f },
            { Tone.P8 , 2f },
            { Tone.Maj9 , 18f/8f },
            { Tone.Maj10 , 10f/4f },
            { Tone.P11 , 8f/3f },
            { Tone.P12 , 6f/2f }
        };

        public static float Frequency(Tone tone, Key key) {
            if (JustTemperedRatios.ContainsKey(tone)) {
                return JustTemperedRatios[tone] * Frequency(key);
            }
            return Frequency(key);
        }

    }
}