// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet.IO {

    [System.Serializable]
    public class SynthData : Data {

        // Settings.
        public static string Directory = "Synths/";
        public static string Format = ".synth";

        // Data.
        [SerializeField, ReadOnly] private string Name;
        [Serializable, ReadOnly] public float Volume;
        [Serializable, ReadOnly] public WaveData[] Waves;

        // Constructor.
        public SynthData(Synth synth, string filename) {
            // Settings.
            Name = filename;
            Volume = synth.Volume;
            // Wave Forms.
            Waves = new WaveData[synth.Waves.Length];
            for (int i = 0; i < synth.Waves; i++) {
                Waves = new WaveData(synth.Waves[i]);
            }
        }

    }

}