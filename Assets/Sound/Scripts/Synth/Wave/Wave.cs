using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet;

namespace Monet {

    /// <summary>
    /// 
    /// </summary>
    public class Wave : MonoBehaviour {

        [SerializeField] private WaveShape m_Shape;
        public WaveShape Shape => m_Shape;

        [SerializeField] private Distribution m_Overtones;
        public Distribution Overtones => m_Overtones;

        [SerializeField] private Modifier m_Volume;
        public Modifier Volume => m_Volume;
        [SerializeField] private Modifier m_Attack;
        public Modifier Attack => m_Attack;
        [SerializeField] private Modifier m_Sustain;
        public Modifier Sustain => m_Sustain;
        [SerializeField] private Modifier m_Decay;
        public Modifier Decay => m_Decay;
        
        // Wave Adders.
        private delegate float WaveFunction(float fundamental, float time);
        private WaveFunction DelegatedWaveFuntion;

        [SerializeField] private int m_OctaveShift;
        public int OctaveShift => m_OctaveShift;

        void Start() {
            m_Overtones.Reset();
        }

        void Update() {
            m_Volume.OnUpdate();
            m_Attack.OnUpdate();
            m_Sustain.OnUpdate();
            m_Decay.OnUpdate();
            Overtones.OnUpdate();
        }

        public void SetShape(WaveShape shape) {
            m_Shape = shape;
            
            switch (m_Shape) {
                case WaveShape.Sine:
                    DelegatedWaveFuntion = new WaveFunction(SineFunction);
                    break;
                case WaveShape.Square:
                    DelegatedWaveFuntion = new WaveFunction(SquareFunction);
                    break;
                case WaveShape.Triangle:
                    DelegatedWaveFuntion = new WaveFunction(TriangleFunction);
                    break;
                default:
                    break;
            }
        }

        public void ShiftOctave(int shift) {
            m_OctaveShift += shift;
        }
        
        public void GetWave() {
            // Overtones = distributionA.GetValues();
        }

        public float[] Generate(int packetSize, int channels, int timeOffset, float sampleRate, float fundamental, bool shiftOctave = true, bool modify = true) {

            // Get the octave.
            if (shiftOctave) {
                float octaveFactor = ((m_OctaveShift >= 0) ? Mathf.Pow(2, m_OctaveShift) : 1f / Mathf.Pow(2, Mathf.Abs(m_OctaveShift)));
                fundamental *= octaveFactor;
            }

            // Debug.Log(sampleRate);
            float[] wavePacket = new float[packetSize];

            // Itterate through the data.
            for (int i = 0; i < packetSize; i += channels) {
                float time = (float)(timeOffset + i) / (float)sampleRate / (float)channels;

                float value = DelegatedWaveFuntion(fundamental, time);

                // Put that value into both the channels.
                for (int j = 0; j < channels; j++) {
                    wavePacket[i + j] = value;
                }
            }

            Modify(wavePacket, channels, timeOffset, sampleRate, modify);

            return wavePacket;
        }

        public float[] Modify(float[] data, int channels, int timeOffset, float sampleRate, bool modify) {

            float attack = m_Attack.Value;
            float sustain = m_Sustain.Value;
            float decay = m_Decay.Value;
            float volume = m_Volume.Value;

            // Apply the modifiers
            for (int i = 0; i < data.Length; i += channels) {
                float time = (float)(i + timeOffset) / (float)sampleRate / (float)channels;

                if (modify) {
                    if (time < attack) {
                        volume *= Mathf.Pow((time / attack), 2);
                    }
                    if (time > sustain) {
                        volume *= Mathf.Exp(-decay * (time - sustain));
                    }
                }

                for (int j = 0; j < channels; j++) {
                    data[i + j] *= volume;
                }

            }

            return data;

        }

        public void Load(WaveData data) {
            m_Shape = data.Shape;
            m_OctaveShift = data.OctaveShift;
            m_Volume.Load(data.Volume);
            m_Attack.Load(data.Attack);
            m_Sustain.Load(data.Sustain);
            m_Decay.Load(data.Decay);
        }

        #region Hide

        private float SineFunction(float fundamental, float time) {
            // Get the value for this index.
            float value = 0f;
            for (int i = 0; i < Overtones.Values.Length; i++) {
                // The factors for this overtone.
                float volumeFactor = Overtones.Values[i];
                float frequency = fundamental * (i + 1);
                value += volumeFactor * Mathf.Sin(time * 2 * Mathf.PI * frequency);
            }

            return value;
        }

        private float SquareFunction(float fundamental, float time) {
            // Get the value for this index.
            float value = 0f;
            for (int i = 0; i < Overtones.Values.Length; i++) {
                // The factors for this overtone.
                float volumeFactor = Overtones.Values[i];
                float frequency = fundamental * (i + 1);
                float period = 1 / frequency;
                value += (time % period < period / 2f) ? volumeFactor : -volumeFactor;
            }

            return value;
        }

        private float TriangleFunction(float fundamental, float time) {
            // Get the value for this index.
            float value = 0f;
            for (int i = 0; i < Overtones.Values.Length; i++) {
                // The factors for this overtone.
                float volumeFactor = Overtones.Values[i];
                float frequency = fundamental * (i + 1);
                float period = 1 / frequency;

                float sign = (time % period < period / 2f) ? 1f : -1f;
                float offset = sign * -volumeFactor;

                value += offset + sign * (time % (period / 2f) / (period / 2f)) * 2f * volumeFactor;
            }

            return value;
        }

        #endregion

    }

}
