/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    // Might have to come back to this.
    // public class Subdivision {

    //     public int m_Beat;
    //     public int m_BeatOffset;
    //     public Note[] m_Notes;

    // }

    [System.Serializable]
    public class Clef {

        [SerializeField] private Bar[] m_Bars;
        public Bar[] Bars => m_Bars;

        public void Clean() {
            for (int i = 0; i < m_Bars.Length; i++) {
                m_Bars[i].Clean();
            }
        }

        public Clef(Bar[] bars) {
            for (int i = 0; i < bars.Length; i++) {
                bars[i].Clean();
            }
            m_Bars = bars;
        }

        public static Clef Metronome(int barCount = 1, int subdivision = 4, int beats = 4) {
            Bar[] bars = new Bar[barCount];
            for (int i = 0; i < barCount; i++) {
                bars[i] = Bar.Metronome(subdivision, beats);
            }
            return new Clef(bars);
        }

        public static Clef Pulse(int barCount = 1, int subdivision = 4, int beats = 4) {
            Bar[] bars = new Bar[barCount];
            for (int i = 0; i < barCount; i++) {
                bars[i] = Bar.Pulse(subdivision, beats);
            }
            return new Clef(bars);
        }

        public static Clef RandomClef(int barCount = 1, int subdivision = 4, int beats = 4) {
            Bar[] bars = new Bar[barCount];
            for (int i = 0; i < barCount; i++) {
                bars[i] = Bar.RandomBar(subdivision, beats);
            }
            return new Clef(bars);
        }

        public static Clef ArpeggiatedClef(int barCount = 1, int subdivision = 4, int beats = 4) {
            Bar[] bars = new Bar[barCount];
            for (int i = 0; i < barCount; i++) {
                bars[i] = Bar.RandomArpeggio(subdivision, beats);
            }
            return new Clef(bars);
        }

        public static Clef MelodicClef(int barCount = 1, int subdivision = 4, int beats = 4) {
            Bar[] bars = new Bar[barCount];
            for (int i = 0; i < barCount; i++) {
                bars[i] = Bar.RandomMelody(subdivision, beats);
            }
            return new Clef(bars);
        }

        public float GetStartTime(int barIndex) {
            return GetTimeForStartOfBar(barIndex, m_Bars);
        }

        public static float GetTimeForStartOfBar(int barIndex, Bar[] bars)  {
            barIndex = barIndex % bars.Length;

            float time = 0f;
            for (int i = 0; i < barIndex; i++) {
                time += (float)bars[i].Beats;
            }
            return time;
        }

        public float GetEndTime() {
            return GetEndTime(m_Bars.Length - 1);
        }

        public float GetEndTime(int barIndex) {
            return GetTimeForEndOfBar(barIndex, m_Bars);
        }

        public static float GetTimeForEndOfBar(int barIndex, Bar[] bars) {
            barIndex = barIndex % bars.Length;

            float time = 0f;
            for (int i = 0; i <= barIndex; i++) {
                time += (float)bars[i].Beats;
            }
            return time;
        }

        public void AddNote(Score.Tone tone, float time, int duration) {
            for (int i = 0; i < m_Bars.Length; i++) {
                if (GetStartTime(i) < time && time < GetEndTime(i)) {
                    m_Bars[i].Add(tone, time - GetStartTime(i), duration);
                }
            }
        }

    }

}