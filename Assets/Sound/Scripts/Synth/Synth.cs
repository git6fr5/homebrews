// Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet;
using Monet.IO;
using Monet.UI;

namespace Monet {
    
    /// <summary>
    /// Generates noises.
    /// </summary>
    public class Synth : MonoBehaviour {

        #region Variables

        [SerializeField] private string m_Filename;
        public string Filename => m_Filename;

        // The amount of times per second that the value of the wave is queried.
        public static float SampleRate; 

        // Components.
        public AudioSource m_AudioSource => GetComponent<AudioSource>();
        [SerializeField] protected Wave[] m_Waves;
        public Wave[] Waves => m_Waves;

        // Settings.
        [Space(2), Header("Settings")]
        [SerializeField] protected bool m_Editable;
        public bool Editable => m_Editable;
        [SerializeField] protected bool m_Playable;
        public bool Playable => m_Playable;
        [SerializeField] protected Modifier m_Volume;
        public Modifier Volume => m_Volume;
        [SerializeField, ReadOnly] protected Score.Key m_Key;
        [SerializeField, ReadOnly] protected Score.Tone m_Tone;

        // Controls.
        [Space(2), Header("Controls")]
        [SerializeField] private List<Input> m_Inputs; // The current key being pressed.

        #endregion

        // Runs once on instantiation.
        void Awake() {
            // Cache this value.
            m_Inputs = Input.DiatonicMajor;
            SampleRate = AudioSettings.outputSampleRate;
        }

        // Runs once every frame.
        protected virtual void Update() {
            if (m_Editable) {
                m_Volume.OnUpdate();
                for (int i = 0; i < m_Waves.Length; i++) {
                    m_Waves[i].OnUpdate();
                }
            }

            // Itterate through the inputs.
            bool disable = UIComponent.IsAnInputFieldFocused();
            foreach (Input input in m_Inputs) {
                input.OnUpdate(disable);
            }

            if (m_Playable && !m_AudioSource.isPlaying) {
                m_AudioSource.Play();
            }
            else if (!m_Playable && m_AudioSource.isPlaying) {
                m_AudioSource.Stop();
            }
        }

        // Runs once every time the audio data is read.
        protected virtual void OnAudioFilterRead(float[] data, int channels) {
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
                    input.Held(0, false);
                }
            }
            ReadPackets(ref data, wavePackets, m_Volume.Value);

        }

        // Reads the packets into the data array.
        protected static void ReadPackets(ref float[] data, List<float[]> wavePackets, float volume) {
            for (int i = 0; i < data.Length; i++) {
                data[i] = 0f;
                for (int j = 0; j < wavePackets.Count; j++) {
                    data[i] += volume * wavePackets[j][i];
                }
            }
        }

        public void SetFilename(string filename) {
            m_Filename = filename;
        }

        public void Save(string filename) {
            SynthData data = new SynthData(this, filename);
            Monet.IO.Data.SaveJSON(data, filename, SynthData.Directory, SynthData.Format);
        }

        public void Open(string filename) {
            string json = Monet.IO.Data.OpenJSON(filename, SynthData.Directory, SynthData.Format);
            SynthData data = JsonUtility.FromJson<SynthData>(json);
            if (data != null) {
                Load(data);
            }
        }

        public void Load(SynthData data) {
            m_Volume.Load(data.Volume);
            for (int i = 0; i < m_Waves.Length; i++) {
                if (i < data.Waves.Length) {
                    m_Waves[i].Load(data.Waves[i]);
                }
            }
        }

    }

}