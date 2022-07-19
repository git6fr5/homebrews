/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    [System.Serializable]
    public class WaveData : Data {

        // Settings.
        [SerializeField, ReadOnly] private Shape WaveShape;
        [SerializeField, ReadOnly] private int OctaveShift;
        [SerializeField, ReadOnly] private float[] Overtones;

        // Modifiers.
        [SerializeField, ReadOnly] private float Volume;
        [SerializeField, ReadOnly] private float Attack;
        [SerializeField, ReadOnly] private float Sustain;
        [SerializeField, ReadOnly] private float Decay;

        // Constructor.
        public WaveData(Wave wave) {
            // Settings.
            WaveShape = wave.WaveShape;
            OctaveShift = wave.OctaveShift;
            Overtones = wave.Overtones;
            
            // Modifiers.
            Volume = wave.Volume;
            Attack = wave.Attack;
            Sustain = wave.Sustain;
            Decay = wave.Decay;
        }

        // public void Load(Wave wave, bool isEditing) {

        //     wave.Shape = this.WaveShape;
        //     wave.GetWaveType();

        //     wave.Overtones = this.Overtones;
        //     wave.octaveShift = this.OctaveShift;

        //     wave.Attack = this.Attack;
        //     wave.Sustain = this.Sustain;
        //     wave.Decay = this.Decay;

        //     wave.Volume = this.Volume;

        //     if (isEditing) {
        //         wave.distributionA.SetValues(wave.Overtones);

        //         wave.attackKnob.value = wave.Attack / wave.maxAttack;
        //         wave.sustainKnob.value = wave.Sustain / wave.maxSustain;
        //         wave.decayKnob.value = wave.Decay / wave.maxDecay;

        //         wave.volumeKnob.value = wave.Volume / wave.maxVolume;
        //     }

        // }

    }
    
}