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

        void Awake() {
            m_Overtones.Reset();
            SetShape(m_Shape);
        }

        public void OnUpdate() {
            m_Volume.OnUpdate();
            m_Attack.OnUpdate();
            m_Sustain.OnUpdate();
            m_Decay.OnUpdate();
            m_Overtones.OnUpdate();
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

        public float[] Generate(int packetSize, int channels, int timeOffset, float sampleRate, float fundamental, bool shiftOctave = true, bool modify = true, bool shape = true) {

            // Get the octave.
            if (shiftOctave) {
                float octaveFactor = ((m_OctaveShift >= 0) ? Mathf.Pow(2, m_OctaveShift) : 1f / Mathf.Pow(2, Mathf.Abs(m_OctaveShift)));
                fundamental *= octaveFactor;
            }

            // Debug.Log(sampleRate);
            float[] wavePacket = new float[packetSize];

            // Itterate through the data.
            float normal = -1f;
            for (int i = 0; i < packetSize; i += channels) {
                float time = (float)(timeOffset + i) / (float)sampleRate / (float)channels;

                float value = 1f;
                if (shape) {
                    value = DelegatedWaveFuntion(fundamental, time);
                }
                if (Mathf.Abs(value) > normal) {
                    normal = Mathf.Abs(value);
                }

                // Put that value into both the channels.
                for (int j = 0; j < channels; j++) {
                    wavePacket[i + j] = value;
                }
            }

            float volume = m_Volume.Value;
            for (int i = 0; i < wavePacket.Length; i++) {
                wavePacket[i] *= volume / normal;
            }
            
            if (modify) {
                Modify(wavePacket, channels, timeOffset, sampleRate);
            }

            return wavePacket;
        }

        public float[] Modify(float[] data, int channels, int timeOffset, float sampleRate) {

            float attack = m_Attack.Value;
            float sustain = m_Sustain.Value;
            float decay = m_Decay.Value;

            // Apply the modifiers
            for (int i = 0; i < data.Length; i += channels) {
                float time = (float)(i + timeOffset) / (float)sampleRate / (float)channels;
                float factor = 1f;

                if (time < attack) {
                    factor *= Mathf.Pow((time / attack), 2);
                }
                if (time > sustain) {
                    factor *= Mathf.Exp(-decay * (time - sustain));
                }

                for (int j = 0; j < channels; j++) {
                    data[i + j] *= factor;
                }

            }

            return data;

        }

        public void Load(WaveData data) {
            SetShape(data.Shape);
            m_OctaveShift = data.OctaveShift;
            m_Overtones.Load(data.Overtones);
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
                float volume = Overtones.Values[i];
                float frequency = fundamental * (i + 1);
                value += volume * Mathf.Sin(time * 2 * Mathf.PI * frequency);
            }

            return value;
        }

        private float SquareFunction(float fundamental, float time) {
            // Get the value for this index.
            float value = 0f;
            for (int i = 0; i < Overtones.Values.Length; i++) {
                // The factors for this overtone.
                float volume = Overtones.Values[i];
                float frequency = fundamental * (i + 1);
                float period = 1 / frequency;
                value += (time % period < period / 2f) ? volume : -volume;
            }

            return value;
        }

        private float TriangleFunction(float fundamental, float time) {
            // Get the value for this index.
            float value = 0f;
            for (int i = 0; i < Overtones.Values.Length; i++) {
                // The factors for this overtone.
                float volume = Overtones.Values[i];
                float frequency = fundamental * (i + 1);
                float period = 1 / frequency;

                float sign = (time % period < period / 2f) ? 1f : -1f;
                float offset = sign * -volume;

                value += offset + sign * (time % (period / 2f) / (period / 2f)) * 2f * volume;
            }

            return value;
        }

        #endregion

    }

}
