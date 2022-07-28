/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monet;
using Monet.UI;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class Channel : MonoBehaviour {

        [SerializeField] private bool m_Editing;
        public bool Editing => m_Editing;

        [SerializeField] private Clef m_Clef;
        public Clef ChannelClef => m_Clef;
        [SerializeField] private DAWSynth m_DAWSynth;
        public Synth ChannelSynth => m_DAWSynth;

        [SerializeField, ReadOnly] private int m_CurrentBarIndex = 0;
        [SerializeField, ReadOnly] private float m_CurrentBeatIndex = 0f;
        [SerializeField] private List<Note> m_PlayedNotes = new List<Note>();

        public int m_BarCount;
        public int m_SubdivisionCount;
        public int m_BeatCount;

        [SerializeField] private Timeline m_Timeline;
        public Timeline Timeline => m_Timeline;

        void Start() {
            m_DAWSynth.Open(m_DAWSynth.Filename);
            m_Clef = Clef.Metronome(m_BarCount, m_SubdivisionCount, m_BeatCount);
            m_Timeline.DrawClef(m_Clef);
        }

        void Update() {

            bool focused = !UIComponent.IsAnInputFieldFocused();

            if (focused && m_Editing) {
                if (UnityEngine.Input.GetKeyDown(KeyCode.R)) {
                    m_Clef = Clef.RandomClef(m_BarCount, m_SubdivisionCount, m_BeatCount);
                    m_Timeline.DrawClef(m_Clef);
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.T)) {
                    m_Clef = Clef.Metronome(m_BarCount, m_SubdivisionCount, m_BeatCount);
                    m_Timeline.DrawClef(m_Clef);
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.A)) {
                    m_Clef = Clef.ArpeggiatedClef(m_BarCount, m_SubdivisionCount, m_BeatCount);
                    m_Timeline.DrawClef(m_Clef);
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.M)) {
                    m_Clef = Clef.MelodicClef(m_BarCount, m_SubdivisionCount, m_BeatCount);
                    m_Timeline.DrawClef(m_Clef);
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.P)) {
                    m_Clef = Clef.Pulse(m_BarCount, m_SubdivisionCount, m_BeatCount);
                    m_Timeline.DrawClef(m_Clef);
                }
            }

        }

        public void PlayAt(float time, Score.Key key, bool play) {
            m_Timeline.OnUpdate(time);

            if (m_CurrentBarIndex >= m_Clef.Bars.Length) {
                m_CurrentBarIndex = FindBar(time);
            }

            if (play) {
                if (m_CurrentBarIndex >= m_Clef.Bars.Length) {
                    m_PlayedNotes = new List<Note>();
                    m_DAWSynth.OnUpdate(m_PlayedNotes, key, play);
                    return;
                }

                bool stillWithinBar = m_Clef.GetStartTime(m_CurrentBarIndex) < time && time < m_Clef.GetEndTime(m_CurrentBarIndex);
                if (!stillWithinBar) {
                    m_CurrentBarIndex = FindBar(time);
                }

                m_CurrentBeatIndex = time - m_Clef.GetStartTime(m_CurrentBarIndex);

                Bar bar = m_Clef.Bars[m_CurrentBarIndex];
                m_PlayedNotes = bar.Notes.FindAll(note => note.TimeID < m_CurrentBeatIndex && m_CurrentBeatIndex < note.EndTimeID(bar.Subdivision));
            }
            
            m_DAWSynth.OnUpdate(m_PlayedNotes, key, play);

        }

        public int FindBar(float time) {
            for (int i = 0; i < m_Clef.Bars.Length; i++) {
                float startTime = m_Clef.GetStartTime(i);
                float endTime = m_Clef.GetEndTime(i);
                if (startTime <= time && time < endTime) {
                    return i;
                }
            }
            return m_CurrentBarIndex;
        }

        public void Refresh(Score.Tone[] prevScale, Score.Tone[] newScale) {
            for (int n = 0; n < m_Clef.Bars.Length; n++) {
                for (int i = 0; i < m_Clef.Bars[n].Notes.Count; i++) {
                    m_Clef.Bars[n].Notes[i].Tone = Score.Scales.Refresh(m_Clef.Bars[n].Notes[i].Tone, prevScale, newScale);
                }
            }
            m_Timeline.DrawClef(m_Clef);
        }

        public void SetEditing(bool editing) {
            m_Editing = editing;
            m_Timeline.SetEditing(editing);
        }

        public void AddNote(Marker marker) {
            m_Clef.AddNote(Score.Tone.P1, marker.CachedTime, 1);
            m_Timeline.DrawClef(m_Clef);
        }

    }

}