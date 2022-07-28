// Libraries.
using System;
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
    public class DAW : MonoBehaviour {

        [Space(2), Header("File Paths")]
        [SerializeField] private string m_ScoreFilePath;
        [SerializeField] private List<Channel> m_Channels = new List<Channel>();
        public List<Channel> Channels => m_Channels;

        [Space(2), Header("Settings")]
        [SerializeField] private bool m_Editing;
        [SerializeField] private Knob m_BeatsPerMinuteKnob;
        [SerializeField, ReadOnly] private float m_BeatInterval;
        [SerializeField, ReadOnly] private float m_BeatsPerMinute;
        [SerializeField] private float m_MaximumBPM;
        [SerializeField] private float m_MinimumBPM;

        [SerializeField] private bool m_Playing;
        public bool Playing => m_Playing;

        [SerializeField, ReadOnly] private float m_BeatTime = 0f;
        public float BeatTime => m_BeatTime;
        // Measured in beats... e.g. this will loop after 8 beats have been played.
        // e.g. a bar with 3 beats 3 beats 2 beats
        [SerializeField] private bool m_TieLoopToLongestChannel = true;
        [SerializeField] private float m_LoopTime = 8f; 
        public float LoopTime => m_LoopTime;

        [HideInInspector] private Score m_Score;
        [SerializeField] private Score.Key m_Key; // When I'm eventually reading from a score.
        // The DAW will read the key from the key of the score.

        [SerializeField] private Score.Scales.ScaleList m_Scale;
        [HideInInspector] private Score.Scales.ScaleList m_CachedScale;
        
        void Awake() {
            // m_Score.Open(m_ScoreFilePath);
            // temp.
            Score.Scales.Set(m_Scale);
        }

        void Update() {

            Score.Scales.Set(m_Scale);
            if (m_Scale != m_CachedScale) {
                for (int i = 0; i < m_Channels.Count; i++) {
                    m_Channels[i].Refresh(Score.Scales.Get(m_CachedScale), Score.Scales.CurrentScale);
                }
                m_CachedScale = m_Scale;
            }

            bool focused = !UIComponent.IsAnInputFieldFocused();

            if (focused && UnityEngine.Input.GetKeyDown(KeyCode.Space)) {
                m_Playing = !m_Playing;
            }

            if (focused && UnityEngine.Input.GetKeyDown(KeyCode.Backspace)) {
                m_BeatTime = 0f;
            }

            if (m_TieLoopToLongestChannel) {
                m_LoopTime = 0f;
                for (int i = 0; i < m_Channels.Count; i++) {
                    m_LoopTime = Mathf.Max(m_LoopTime, m_Channels[i].ChannelClef.GetEndTime());
                }
            }
            
        }

        void FixedUpdate() {
            if (m_Editing) {
                m_BeatsPerMinute = (int)(m_BeatsPerMinuteKnob.Value * (m_MaximumBPM - m_MinimumBPM)) + m_MinimumBPM;
            }
            m_BeatInterval = 60f / m_BeatsPerMinute;

            if (m_Playing) {
                m_BeatTime += Time.fixedDeltaTime / m_BeatInterval;
                if (m_BeatTime > m_LoopTime) {
                    m_BeatTime -= m_LoopTime;
                }
            }

            for (int i = 0; i < m_Channels.Count; i++) {
                m_Channels[i].PlayAt(m_BeatTime, m_Key, m_Playing);
            }

        }

        public void Play(bool play) {
            m_Playing = play;
        }

        public void ShiftKey(int value) {
            int newKey = (int)m_Key + value;
            newKey = newKey >= (int)Score.Key.Count ? 0 : newKey < 0 ? (int)Score.Key.Count - 1 : newKey;
            m_Key = (Score.Key)newKey;
        }


    }

}