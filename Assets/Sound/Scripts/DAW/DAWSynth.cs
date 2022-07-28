/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class DAWSynth : Synth {

        [SerializeField, ReadOnly] private List<Note> m_Notes;

        protected override void Update() {

        }

        public void OnUpdate(List<Note> notes, Score.Key key, bool play) {
            if (play && !m_AudioSource.isPlaying) {
                m_AudioSource.Play();
            }
            else if (!play && m_AudioSource.isPlaying) {
                m_AudioSource.Stop();
            }

            m_Key = key;

            // For all the newly pressed notes.
            for (int i = 0; i < notes.Count; i++) {
                if (!m_Notes.Contains(notes[i])){
                    notes[i].SetHoldTime(0);
                }
            }

            m_Notes = notes;
            m_Volume.OnUpdate();

        }

        protected override void OnAudioFilterRead(float[] data, int channels) {
            // Stores the wavepackets.
            List<float[]> wavePackets = new List<float[]>();

            // Itterate through the inputs.
            foreach (Note note in m_Notes) {
                for (int i = 0; i < m_Waves.Length; i++) {
                    float[] wavePacket = m_Waves[i].Generate(data.Length, channels, note.HoldTime, SampleRate, Score.Frequency(note.Tone, m_Key));
                    wavePackets.Add(wavePacket);
                }
                // Increment the time.
                note.Held((int)((float)data.Length)); // channels ??? I don't get it.
            }
            
            ReadPackets(ref data, wavePackets, m_Volume.Value);

        }

    }
}