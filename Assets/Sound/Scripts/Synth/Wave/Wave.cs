using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monet;

/// <summary>
/// 
/// </summary>
public class Wave : MonoBehaviour {

    public Shape WaveShape;

    [SerializeField] public Distribution distributionA;
    public float[] Overtones;

    [SerializeField] private Modifier m_Volume;
    [SerializeField] private Modifier m_Attack;
    [SerializeField] private Modifier m_Sustain;
    [SerializeField] private Modifier m_Decay;
    
    // Wave Adders.
    private delegate float WaveFunction(float fundamental, float time);
    private WaveFunction m_WaveFuntion;
    public int octaveShift;
    
    public void GetWave() {
        switch (Shape) {
            case Shape.Sine:
                m_WaveFuntion = new WaveFunction(SineFunction);
                break;
            case Shape.Square:
                m_WaveFuntion = new WaveFunction(SquareFunction);
                break;
            case Shape.Triangle:
                m_WaveFuntion = new WaveFunction(NoiseFunction);
                break;
            default:
                break;
        }

        m_Volume = m_VolumeKnob.Value * MaxVolume;
        m_Attack = m_AttackKnob.value * MaxAttack;
        m_Sustain = m_SustainKnob.value * MaxSustain;
        m_Decay = m_DecayKnob.value * maxDecay;
        Overtones = distributionA.GetValues();

    }

    public float[] Generate(int packetSize, int channels, int timeOffset, float sampleRate, float fundamental, bool shiftOctave = true, bool addModifiers = true) {

        // Get the octave.
        if (shiftOctave) {
            float octaveFactor = ((octaveShift >= 0) ? Mathf.Pow(2, octaveShift) : 1f / Mathf.Pow(2, Mathf.Abs(octaveShift)));
            fundamental *= octaveFactor;
        }

        // Debug.Log(sampleRate);
        float[] wavePacket = new float[packetSize];

        // Itterate through the data.
        for (int i = 0; i < packetSize; i += channels) {
            float time = (float)(timeOffset + i) / (float)sampleRate / (float)channels;

            float value = m_WaveFuntion(fundamental, time);

            // Put that value into both the channels.
            for (int j = 0; j < channels; j++) {
                wavePacket[i + j] = value;
            }
        }

        Modify(wavePacket, channels, timeOffset, sampleRate, addModifiers);

        return wavePacket;
    }

    public float[] Modify(float[] data, int channels, int timeOffset, float sampleRate, bool addModifiers) {

        float attack = m_Attack.Value;
        float sustain = m_Sustain.Value;
        float decay = m_Decay.Value;
        float volume = m_Volume.Value;

        // Apply the modifiers
        for (int i = 0; i < data.Length; i += channels) {
            float time = (float)(i + timeOffset) / (float)sampleRate / (float)channels;

            if (addModifiers) {
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

    #region Hide

    private float SineFunction(float fundamental, float time) {
        // Get the value for this index.
        float value = 0f;
        for (int i = 0; i < Overtones.Length; i++) {
            // The factors for this overtone.
            float volumeFactor = Overtones[i];
            float frequency = fundamental * (i + 1);
            value += volumeFactor * Mathf.Sin(time * 2 * Mathf.PI * frequency);
        }

        return value;
    }

    private float SquareFunction(float fundamental, float time) {
        // Get the value for this index.
        float value = 0f;
        for (int i = 0; i < Overtones.Length; i++) {
            // The factors for this overtone.
            float volumeFactor = Overtones[i];
            float frequency = fundamental * (i + 1);
            float period = 1 / frequency;
            value += (time % period < period / 2f) ? volumeFactor : -volumeFactor;
        }

        return value;
    }

    private float TriangleFunction(float fundamental, float time) {
        // Get the value for this index.
        float value = 0f;
        for (int i = 0; i < Overtones.Length; i++) {
            // The factors for this overtone.
            float volumeFactor = Overtones[i];
            float frequency = fundamental * (i + 1);
            float period = 1 / frequency;

            float sign = (time % period < period / 2f) ? 1f : -1f;
            float offset = sign * -volumeFactor;

            value += offset + sign * (time % (period / 2f) / (period / 2f)) * 2f * volumeFactor;
        }

        return value;
    }

    private float NoiseFunction(float fundamental, float time) {
        // Get the value for this index.
        float value = 0f;       

        for (int i = 0; i < frequencies.Length; i++) {
            // The factors for this overtone.
            value += intensities[i] * Mathf.Sin(time * 2 * Mathf.PI * frequencies[i]);
        }

        return value;
    }

    public float[] GenerateModifiedLine(int packetSize, float sampleRate, float scale) {

        // Debug.Log(sampleRate);
        float[] wavePacket = new float[packetSize];

        // Itterate through the data.
        for (int i = 0; i < packetSize; i++) {
            wavePacket[i] = Volume * scale;
        }

        Modify(wavePacket, 1, 0, sampleRate, true);

        return wavePacket;

    }

    #endregion


}
