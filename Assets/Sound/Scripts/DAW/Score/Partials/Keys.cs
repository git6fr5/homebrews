/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    public partial class Score {

        public enum Key { 
            A, 
            B, 
            C, 
            D, 
            E, 
            F, 
            G, 
            Count 
        }

        public static Dictionary<Key, float> KeyFrequencies = new Dictionary<Key, float>() {
            { Key.A, 440f },
            { Key.B, 493.88f },
            { Key.C, 523.25f },
            { Key.D, 587.33f },
            { Key.E, 659.25f },
            { Key.F, 698.46f },
            { Key.G, 783.99f },
        };

        public static float Frequency(Key key) {
            if (KeyFrequencies.ContainsKey(key)) {
                return KeyFrequencies[key];
            }
            return 440f;
        }

    }
}