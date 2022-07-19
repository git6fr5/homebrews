using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Note = Score.Note;
using Tone = Score.Tone;
using Value = Score.NoteLength;
using Shape = Wave.Shape;
using Clef = Score.Clef;

public class DAW : MonoBehaviour {

   
    public int editingChannel = 1;
    public bool isEditing;

    [Space(2), Header("File Paths")]
    [SerializeField] private string m_ScoreFilePath;
    [SerializeField] private string[] m_SynthFiles;

    [SerializeField] public Knob BPMKnob;
    [SerializeField, ReadOnly] private int m_BeatsPerMinute;

    [HideInInspector] private float m_NoteInterval;
    [HideInInspector] private Score m_Score;
    [HideInInspector] private Synth[] m_Synths;
    [HideInInspector] private List<Channel> m_Channels = new List<Channel>();

    [SerializeField, ReadOnly] private float m_Ticks = 0f;


    void Awake() {

        // Load scores.
        Score.Open(ref m_Score, m_ScoreFilePath);

        // Load synths.
        for (int i = 0; i < m_SynthFiles.Length; i++) {
            Synth synth = Synth.CreateDAWSynth();
            Synth.Open(ref synth, m_SynthFiles[i]);
            m_Channels.Add(new Channel(synth, m_Score.Line[i]));
        }

    }

    void Update() {

        if (isEditing) {
            // Get BPM.
            m_BeatsPerMinute = (int)(BPMKnob.value * (maxBPM - minBPM)) + minBPM;
        }
        // Get the duration of a note with respect to the BPM.
        m_NoteInterval = 60f / m_BeatsPerMinute;

        GetVolumes();

        // The time control logic.
        Tick();

        // Play the music~
        Play();

    }

    public float maxVolume = 0.5f;

    void GetVolumes() {
        for (int i = 0; i < m_Channels.Count; i++) {
            m_Channels[i].synth.volume = m_Channels[i].synth.volumeKnob.value * maxVolume;
        }
    }

    private void Play() {
        m_Channels[0].clef = m_Score.treble;
        m_Channels[1].clef = m_Score.bass;

        for (int i = 0; i < m_Channels.Count; i++) {

            m_Channels[i].synth.root = m_Score.root;

            if (m_Channels[i].index >= m_Channels[i].clef.tones.Count && m_Channels[i].synth.audioSource.isPlaying) {
                ReplayChannel(m_Channels[i]);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !m_Channels[i].synth.audioSource.isPlaying && isEditing) {
                timeInterval = 0f;
                subdividedIndex = 0;
                PlayChannel(m_Channels[i]);
            }

            else if (Input.GetKeyDown(KeyCode.Space) && m_Channels[i].synth.audioSource.isPlaying && isEditing) {
                StopChannel(m_Channels[i]);
            }

            if (m_Channels[i].synth.audioSource.isPlaying) {
                WhilePlayingChannel(m_Channels[i]);
            }

        }
    }

    private void Tick() {
        timeInterval += Time.deltaTime;
        float subdividedInterval = Score.LengthMultipliers[Value.SIXTEENTH];
        if (timeInterval >= subdividedInterval * m_NoteInterval) {
            timeInterval -= subdividedInterval * m_NoteInterval;
            subdividedIndex++;
        }
        maxIndex = (int)(barLength * m_Score.bars / subdividedInterval);
        if (subdividedIndex >= maxIndex) {
            subdividedIndex = 0;
        }
    }

    void PlayChannel(Channel channel) {
        Score.PrintClef(channel.clef);
        channel.synth.audioSource.Play();
        channel.synth.newKey = true;
        channel.index = 0;
    }

    void StopChannel(Channel channel) {
        channel.synth.audioSource.Stop();
    }

    void ReplayChannel(Channel channel) {
        channel.index = 0;
    }

    public int subdividedIndex;
    public float timeInterval = 0f;
    public int maxIndex;

    float barLength = 4f;

    void WhilePlayingChannel(Channel channel) {

        int channelSubdivisionIndex = 0;
        for (int i = 0; i < channel.clef.lengths.Count; i++) {
            channelSubdivisionIndex += (int)(Score.LengthMultipliers[channel.clef.lengths[i]] * barLength);
            if (channelSubdivisionIndex > subdividedIndex) {
                if (channel.index != i) {
                    channel.synth.tone = channel.clef.tones[i];
                    channel.synth.newKey = true;
                    channel.index = i;
                }
                break;
            }
        }
    }

}
