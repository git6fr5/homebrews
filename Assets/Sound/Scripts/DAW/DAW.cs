// Libraries.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class DAW : MonoBehaviour {

        [Space(2), Header("File Paths")]
        [SerializeField] private string m_ScoreFilePath;
        [SerializeField] private string[] m_SynthFiles;

        [SerializeField] private Knob m_BeatsPerMinuteKnob;
        [SerializeField] private float m_BeatsPerMinute;
        [SerializeField] private float m_MaximumBPM;
        [SerializeField] private float m_MinimumBPM;

        [HideInInspector] private float m_QuarterNoteInterval;
        [HideInInspector] private Score m_Score;
        [HideInInspector] private List<Channel> m_Channels = new List<Channel>();

        [SerializeField] private bool Editing;

        public int subdividedIndex;
        public float timeInterval = 0f;
        public int maxIndex;

        float barLength = 4f;

        void Awake() {
            m_Score.Open(m_ScoreFilePath);

        }

        void Update() {

            if (Editing) {
                m_BeatsPerMinute = (int)(m_BeatsPerMinuteKnob.Value * (m_MaximumBPM - m_MinimumBPM)) + m_MinimumBPM;
            }
            m_QuarterNoteInterval = 60f / m_BeatsPerMinute;

        }


    }

}