/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    public class Noise {

        /* Some cool settings,
         * at i = 20
         * lo = 50f, inc = 50f, intensities = [1] (sounds kinda like a detuned bass string) a0,s0,d0.3
         * lo = 50f, inc = 27f, intensities = [1] (sounds like a wub wub alien) a0,s0,d0.3
         * lo = 200f, inc = 27f, intensities = [1] (with a little on the attack, sounds like a growl) a0.1,s0,d0.3
         * at i = 100
         * lo = 50f, inc = 20f, intensities = [1] (very alien sounding) a0,s0,d0.3
         * lo = 54.9131f, inc = 27.13131f, intensities = [mean = 100, upperBound = Mathf.Abs(mean / (mean + (mean - frequencies[i])))], a0.25,s0.5,d0.5
         */

        public static NoiseSettings DetunedBass = new NoiseSettings(20, 50f, 50f, new Modifier(0f, 0f, 0.3f));
        public static NoiseSettings Growl = new NoiseSettings(20, 200f, 27f, new Modifier(0.1f, 0f, 0.3f));
        public static NoiseSettings Alien = new NoiseSettings(20, 50f, 27f, new Modifier(0f, 0f, 0.3f));
        public static NoiseSettings ZingyAlien = new NoiseSettings(100, 50f, 20f, new Modifier(0f, 0f, 0.3f));

        public int Intensity;
        public float Low;
        public float Increment;
        public float Mean;
        public float Attack;
        public float Sustain;
        public float Decay;

        // float[] frequencies = new float[0];
        // float[] intensities = new float[0];

        public NoiseSettings(int i, float lo, float inc, Modifiers modifiers, float mean = -1f) {
            this.Intensity = i;
            this.Low = lo;
            this.Increment = inc;
            this.Mean = mean;

            // These are ratios
            Attack = modifiers.AttackRatio;
            Sustain = modifiers.SustainRatio;
            Decay = modifiers.DecayRatio;
        }

        public void Init() {
            // NoiseSettings currSettings = DetunedBass;
            // if (frequencies.Length == 0) {
            //     frequencies = new float[currSettings.i];
            //     intensities = new float[currSettings.i];
            //     for (int i = 0; i < frequencies.Length; i++) {
            //         frequencies[i] = currSettings.lo + currSettings.inc * i;
            //         if (currSettings.mean > 0) {
            //             intensities[i] = Mathf.Abs(currSettings.mean / (currSettings.mean + (currSettings.mean - frequencies[i])));
            //         }
            //         else {
            //             intensities[i] = 1f;
            //         }
            //     }
            // }
        }

    }

}