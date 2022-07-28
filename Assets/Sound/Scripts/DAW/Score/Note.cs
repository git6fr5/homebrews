/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    // It's possible to do this with more primitive data types but
    // I think it's just clearer to write things out like this.

    [System.Serializable]
    public class Note {

        public float TimeID;
        public Score.Tone Tone;
        public int Duration; // Number of subdivisions this plays for.

        [SerializeField] private int m_HoldTime;
        public int HoldTime => m_HoldTime;

        public Note(float timeID, Score.Tone tone, int duration) {
            TimeID = timeID;
            Tone = tone;
            Duration = duration;
        }

        public float EndTimeID(float subdivision) {
            return TimeID + (float)Duration / subdivision;
        }

        public void Held(int dt) {
            m_HoldTime += dt;
        }

        public void SetHoldTime(int holdTime) {
            m_HoldTime = holdTime;
        }

        public void Ascend() {
            Tone = Score.Scales.Ascend(Tone);
        }

        public void Descend() {
            Tone = Score.Scales.Descend(Tone);
        }

        public void Longer() {
            Duration += 1;
        }

        public void Shorter() {
            Duration -= 1;
            if (Duration < 1) {
                Duration = 1;
            }
        }

        public bool Move(float increment, float maxTime) {
            if (0f <= TimeID + increment && TimeID + increment < maxTime) {
                TimeID += increment;
                return true;
            }
            return false;
        }

    }

}