/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class Timeline : MonoBehaviour {

        #region Variables.

        // The spacing between beat markers.
        private static float BeatSpacing = 2f;
        // The height in which note markers can be placed.
        public static float WorkspaceHeight = 4.375f;

        // The different markers.
        [SerializeField] private NoteMarker m_Note;
        [SerializeField] private Marker m_Subdivision;
        [SerializeField] private Marker m_Beat;
        [SerializeField] private Marker m_Bar;
        [SerializeField] private Marker m_Current;

        [SerializeField, ReadOnly] private Marker m_ActiveMarker;
        public Marker ActiveMarker => m_ActiveMarker;


        // In order to store all the markers.
        [HideInInspector] private List<Marker> m_Markers = new List<Marker>();

        #endregion

        public void OnUpdate(float beatTime) {
            m_Current.SetTime(beatTime, BeatSpacing);
        }

        public void DrawClef(Clef clef) {
            bool validClef = clef.Bars.Length > 0 && clef.Bars[0].Beats > 0 && clef.Bars[0].Subdivision > 0;
            if (validClef) {
                Clear();
                Show(clef);
            }
        }

        private void Show(Clef clef) {
            int previousBeats = 0;
            for (int n = 0; n < clef.Bars.Length; n++) {
                for (int i = 0; i < clef.Bars[n].Beats; i++) {
                    for (int j = 0; j < clef.Bars[n].Subdivision; j++) {
                        float barTime = BarTime(i, j, clef.Bars[n].Subdivision);
                        RhythmMarker(previousBeats, i, j, barTime);
                        NoteMarkers(clef.Bars[n], previousBeats, n, barTime);
                    }
                }
                previousBeats += clef.Bars[n].Beats;
            }

            for (int i = 0; i < m_Markers.Count; i++) {
                if (m_Markers[i] != null) {
                    m_Markers[i].gameObject.SetActive(true);
                }
            }
        }

        private void Clear() {
            for (int i = 0; i < m_Markers.Count; i++) {
                if (m_Markers[i] != null) {
                    Destroy(m_Markers[i].gameObject);
                }
            }
            m_Markers = new List<Marker>();
        }

        private static float BarTime(int beat, int offset, float subdivision) {
            return beat + (float)offset / subdivision;
        }

        private void RhythmMarker(int previousBeats, int i, int j, float barTime) {
            Marker marker = GetMarker(i, j);
            marker.SetTime(previousBeats + barTime, BeatSpacing);
            m_Markers.Add(marker);
        }

        private void NoteMarkers(Bar bar, int previousBeats, int n, float barTime) {
            List<Note> notes = bar.Notes.FindAll(note => note.TimeID == barTime);
            foreach (Note note in notes) {
                if (note.Tone != Score.Tone.Rest) {
                    NoteMarker noteMarker = m_Note.DuplicateNote();
                    noteMarker.SetTime(previousBeats + barTime, BeatSpacing);
                    noteMarker.SetTone((int)note.Tone, WorkspaceHeight);
                    noteMarker.SetDuration(note.Duration, BeatSpacing, bar.Subdivision);
                    noteMarker.Cache(note, WorkspaceHeight, BeatSpacing, bar.Subdivision);
                    m_Markers.Add(noteMarker);
                }
            }
        }

        public Marker GetMarker(int i, int j) {
            if (j == 0 && i == 0) {
                return m_Bar.Duplicate();
            }
            else if (j == 0 && i != 0) {
                return m_Beat.Duplicate();
            }
            return m_Subdivision.Duplicate();
        }

        public void SetEditing(bool editing) {
            for (int i = 0; i < m_Markers.Count; i++) {
                if (m_Markers[i] != null) {
                    m_Markers[i].SetEditing(editing);
                }
            }
        }

        public void SetActiveMarker(Marker marker) {
            m_ActiveMarker = marker;
        }

    }

}