// Libraries.
using System;
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
        [SerializeField, ReadOnly] public string Name;
        [SerializeField, ReadOnly] public ModifierData Volume;
        [SerializeField, ReadOnly] public WaveData[] Waves;

        // Constructor.
        public SynthData(Synth synth, string filename) {
            // Settings.
            Name = filename;
            Volume = new ModifierData(synth.Volume, "volume");
            // Wave Forms.
            Waves = new WaveData[synth.Waves.Length];
            for (int i = 0; i < synth.Waves.Length; i++) {
                Waves[i] = new WaveData(synth.Waves[i]);
            }
        }

    }

}