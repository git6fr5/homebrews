/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    [System.Serializable]
    public class Bar {

        [SerializeField] private int m_Subdivision; // How many times a beat is subdivided.
        public float Subdivision => (float)m_Subdivision;
        [SerializeField] private int m_Beats; // How many beats in a bar.
        public int Beats => m_Beats;
        [SerializeField] private List<Note> m_Notes = new List<Note>();
        public List<Note> Notes => m_Notes;

        public Bar(int subdivision, int beats) {
            m_Subdivision = subdivision; 
            m_Beats = beats;
            m_Notes = new List<Note>();
        }

        public static Bar Metronome(int subdivision, int beats) {
            Bar bar = new Bar(subdivision, beats);
            for (int i = 0; i < beats; i++) {
                if (i == 0) {
                    bar.Add(i, 0, Score.Scales.Get(1), 1);
                }
                else {
                    bar.Add(i, 0, Score.Scales.Get(4), 1);
                }
                for (int j = 1; j < subdivision; j++) {
                    bar.Add(i, j, Score.Scales.Get(2), 1);
                }
            }
            return bar;
        }

        public static Bar RandomBar(int subdivision, int beats) {
            Bar bar = new Bar(subdivision, beats);

            int noteCount = Random.Range(0, beats * subdivision);
            for (int i = 0; i < noteCount; i++) {
                bar.Add(Random.Range(0, beats), Random.Range(0, subdivision), Score.Scales.RandomTone(), Random.Range(1, subdivision));
            }

            return bar;
        }

        public static Bar RandomArpeggio(int subdivision, int beats) {
            Bar bar = new Bar(subdivision, beats);

            Score.Tone tone = Score.Scales.RandomTone();
            for (int i = 0; i < beats; i++) {
                for (int j = 0; j < subdivision; j++) {

                    bar.Add(i, j, tone, 1);
                    int newTone  = Score.Scales.ScaleIndex(tone, Score.Scales.CurrentScale); // (int)tone + Random.Range(-2, 2);
                    newTone += Random.Range(-2, 2);
                    if (newTone < 1) {
                        newTone = Score.Scales.CurrentScale.Length + (newTone - 1);
                    }
                    tone = Score.Scales.CurrentScale[newTone]; // (Score.Tone)(newTone % Score.Scales.CurrentScale.Length);
                }
            }

            return bar;
        }

        public static Bar RandomMelody(int subdivision, int beats) {
            Bar bar = new Bar(subdivision, beats);

            int i = 0;
            while (i < beats * subdivision) {
                float prob = Random.Range(0f, 1f);
                int length = Random.Range(1, 4);
                if (prob > 0.35) {
                    bar.Add(0, i, Score.Scales.RandomTone(), length);
                }
                i += length;
            }

            return bar;
        }

        public static Bar Pulse(int subdivision, int beats) {
            Bar bar = new Bar(subdivision, beats);
            for (int i = 0; i < beats; i++) {
                bar.Add(i, 0, Score.Scales.Get(1), subdivision);
            }
            return bar;
        }

        public void Add(Score.Tone tone, float timeID, int duration) {
            m_Notes.Add(new Note(timeID, tone, duration));
        }

        public void Add(int beat, int offset, Score.Tone tone, int duration) {
            float timeID = (float)beat + (float)offset / (float)m_Subdivision;
            m_Notes.Add(new Note(timeID, tone, duration));
        }

        public void Clean() {
            List<Note> notes = new List<Note>();
            for (int i = 0; i < m_Notes.Count; i++) {
                if (m_Notes[i].Tone != Score.Tone.Rest) {
                    notes.Add(m_Notes[i]);
                }
            }
            m_Notes = notes;
        }

    }
    
}