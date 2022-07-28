/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public partial class Score {

        public class Scales {

            public enum ScaleList {
                Major,
                Minor
            }

            public static Tone[] CurrentScale;
            public static ScaleList CachedScaleList;

            public static Tone[] Major = new Tone[] { 
                Tone.Rest, 
                Tone.P1, 
                Tone.Maj2, 
                Tone.Maj3, 
                Tone.P4, 
                Tone.P5, 
                Tone.Maj6, 
                Tone.Maj7, 
                Tone.P8, 
                // Tone.Maj9, 
                // Tone.Maj10, 
                // Tone.P11, 
                // Tone.P12,
            };

            public static Tone[] Minor = new Tone[] { 
                Tone.Rest, 
                Tone.P1, 
                Tone.Maj2, 
                Tone.min3, 
                Tone.P4, 
                Tone.P5, 
                Tone.min6, 
                Tone.min7, 
                Tone.P8, 
                // Tone.Maj9, 
                // Tone.min10, 
                // Tone.P11, 
                // Tone.P12,
            };

            public static void Set(ScaleList scale) {
                CurrentScale = Get(scale);
            }

            public static Tone[] Get(ScaleList scale) {
                switch (scale) {
                    case ScaleList.Major:
                        return Major;
                    case ScaleList.Minor:
                        return Minor;
                    default:
                        return Major;
                }
            }

            public static Tone Get(int index) {
                index = index % CurrentScale.Length;
                return CurrentScale[index];
            }

            public static Tone RandomTone(bool rest = false) {
                return RandomTone(CurrentScale, rest);
            }

            public static Tone RandomTone(Tone[] scale, bool rest = false) {
                int index = 1;
                if (rest) {
                    index = 0;
                }
                return Major[Random.Range(index, scale.Length)];
            }

            public static Tone Ascend(Tone tone) {
                return Ascend(tone, CurrentScale);
            }

            public static Tone Ascend(Tone tone, Tone[] scale) {
                for (int i = 0; i < scale.Length - 1; i++) {
                    if (tone == scale[i]) {
                        return scale[i + 1];
                    }
                }
                return tone;
            }

            public static Tone Descend(Tone tone) {
                return Descend(tone, CurrentScale);
            }

            public static Tone Descend(Tone tone, Tone[] scale) {
                for (int i = 2; i < scale.Length; i++) {
                    if (tone == scale[i]) {
                        return scale[i - 1];
                    }
                }
                return tone;
            }

            public static Tone Refresh(Tone tone, Tone[] scaleA, Tone[] scaleB) {
                int index = ScaleIndex(tone, scaleA);
                index = index % scaleB.Length;
                return scaleB[index];
            }

            public static int ScaleIndex(Tone tone, Tone[] scale) {
                for (int i = 0; i < scale.Length; i++) {
                    if (tone == scale[i]) {
                        return i;
                    }
                }
                Debug.Log("fucked");
                return 0;
            }

        }

    }
}