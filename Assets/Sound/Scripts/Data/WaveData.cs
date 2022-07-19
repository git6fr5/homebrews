// Libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet {

    [System.Serializable]
    public class WaveData : Data {

        // Settings.
        [SerializeField, ReadOnly] public WaveShape Shape;
        [SerializeField, ReadOnly] public int OctaveShift;
        // [SerializeField, ReadOnly] public float[] m_Overtones;

        // Modifiers.
        [SerializeField, ReadOnly] public ModifierData Volume;
        [SerializeField, ReadOnly] public ModifierData Attack;
        [SerializeField, ReadOnly] public ModifierData Sustain;
        [SerializeField, ReadOnly] public ModifierData Decay;

        // Constructor.
        public WaveData(Wave wave) {
            // Settings.
            Shape = wave.Shape;
            OctaveShift = wave.OctaveShift;
            // m_Overtones = wave.Overtones;
            
            // Modifiers.
            Volume = new ModifierData(wave.Volume, "volume");
            Attack = new ModifierData(wave.Attack, "attack");
            Sustain = new ModifierData(wave.Sustain, "sustain");
            Decay = new ModifierData(wave.Decay, "decay");
        }

    }
    
}