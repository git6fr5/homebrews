// Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet;
using Monet.IO;

namespace Monet {
    
    /// <summary>
    /// Generates noises.
    /// </summary>
    public class Synth : MonoBehaviour {

        #region Variables

        // The amount of times per second that the value of the wave is queried.
        public static float SampleRate; 
        public static float WaveCount = 2; 
        public static float MaxVolume = 0.5f;

        // Components.
        public AudioSource m_AudioSource => GetComponent<AudioSource>();

        // Settings.
        [Space(2), Header("Settings")]
        [SerializeField] private bool m_Editable;
        public bool Editable => m_Editable;
        [SerializeField] private bool m_Playable;
        public bool Playable => m_Playable;
        
        [SerializeField] private Knob m_VolumeKnob;
        [SerializeField, ReadOnly] private float m_Volume;
        public float VolumeRatio => m_Volume / MaxVolume;

        [SerializeField, ReadOnly] private Score.Key m_Key;
        [SerializeField, ReadOnly] private Score.Tone m_Tone;
        [SerializeField, ReadOnly] private Wave[] m_Waves;
        public Wave[] Waves => m_Waves;

        // Controls.
        [Space(2), Header("Controls")]
        [SerializeField] private List<Input> m_Inputs; // The current key being pressed.
        [SerializeField, ReadOnly] private int m_TimeOffset = 0; // The time since at which the last note was played.
        [SerializeField, ReadOnly] private bool m_NewButton; // Triggers if a new key is pressed.

        #endregion

        // Runs once on instantiation.
        void Awake() {
            // Cache this value.
            SampleRate = AudioSettings.outputSampleRate;
        }

        // Runs once every frame.
        void Update() {
            if (m_Editable) {
                m_Volume = m_VolumeKnob.Value * m_MaxVolume;
                waveA.GetWave();
                waveB.GetWave();
            }

            if (m_Playable && !m_AudioSource.isPlaying) {
                m_AudioSource.Play();
            }
            else if (!m_Playable && m_AudioSource.isPlaying) {
                m_AudioSource.Stop();
            }
        }

        // Runs once every time the audio data is read.
        void OnAudioFilterRead(float[] data, int channels) {
            // Stores the wavepackets.
            List<float[]> wavePackets = new List<float[]>();

            // Itterate through the inputs.
            foreach (Input input in m_Inputs) {
                if (input.Pressed) {
                    // Get the wave data.
                    for (int i = 0; i < m_Waves.Length; i++) {
                        float[] wavePacket = m_Waves[i].Generate(data.Length, channels, input.HoldTime, SampleRate, input.Frequency(m_Key));
                        wavePackets.Add(wavePacket);
                    }
                    // Increment the time.
                    input.Held((int)((float)data.Length)); // channels ??? I don't get it.
                }
                else {
                    input.Held(false, 0f);
                }
            }
            ReadPackets(ref data, wavePackets);

        }

        // Reads the packets into the data array.
        private static void ReadPackets(ref float[] data, List<float[]> wavePackets) {
            for (int i = 0; i < data.Length; i++) {
                data[i] = 0f;
                for (int j = 0; j < wavePackets.Count; j++) {
                    data[i] += m_Volume * wavePackets[j][i];
                }
            }
        }

        public void Save(string filename) {
            SynthData data = new SynthData(this, filename);
            Data.SaveJSON(data, filename, SynthData.Directory, SynthData.Format);
        }

        public void Open(string filename) {
            SynthData data = Data.OpenJSON(filename, SynthData.Directory, SynthData.Format) as SynthData;
            if (data != null) {
                Load(data);
            }
        }

        public void Load(SynthData data) {
            m_Volume = data.Volume;
            for (int i = 0; i < m_Waves.Waves; i++) {
                if (data.Waves.Length < i) {
                    data.Waves[i].Load(m_Waves[i], m_Editable);
                }
            }
            if (m_Editable) {
                m_VolumeKnob.SetValue(VolumeRatio);
            }
        }

    }

}