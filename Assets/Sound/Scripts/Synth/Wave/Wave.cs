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

    [SerializeField] public Knob m_VolumeKnob;
    [SerializeField, ReadOnly] private float m_Volume;
    public float Volume => m_Volume;
    public static float MaxVolume = 1f;
    public float VolumeRatio => m_Volume / MaxVolume;

    [SerializeField] private Knob m_AttackKnob;
    [SerializeField, ReadOnly] private float m_Attack;
    public float Attack => m_Attack;
    public static float MaxAttack = 1f;
    public float AttackRatio => m_Attack / MaxAttack;

    [SerializeField] private Knob m_SustainKnob;
    [SerializeField, ReadOnly] private float m_Sustain;
    public float Sustain => m_Sustain;
    public static float MaxSustain = 1f;
    public float SustainRatio => m_Sustain / MaxSustain;

    [SerializeField] private Knob m_DecayKnob;
    [SerializeField, ReadOnly] private float m_Decay;
    public float Decay => m_Decay;
    public static float MaxDecay = 20f;
    public float DecayRatio => m_Decay / MaxDecay;
    
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
            float octaveFactor = (octaveShift == 0) ? 1 : ((octaveShift > 0) ? Mathf.Pow(2, octaveShift) : 1f / Mathf.Pow(2, Mathf.Abs(octaveShift)));
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

        // Apply the modifiers
        for (int i = 0; i < data.Length; i += channels) {
            float time = (float)(i + timeOffset) / (float)sampleRate / (float)channels;
            float factor = Volume;

            if (addModifiers) {

                if (time < Attack) {
                    factor *= Mathf.Pow((time / Attack), 2);
                }
                if (time > Sustain) {
                    factor *= Mathf.Exp(-Decay * (time - Sustain));
                }
            }

            for (int j = 0; j < channels; j++) {
                data[i + j] *= factor;
            }

        }

        return data;

    }

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


}
