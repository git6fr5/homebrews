/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;

namespace Monet {

    ///<summary>
    ///
    ///<summary>
    public class Modifiers {

        [SerializeField] private float m_Attack;
        public float Attack => m_Attack;
        public static float MaxAttack = 1f;
        public float AttackRatio => m_Attack / MaxAttack;

        [SerializeField] private float m_Sustain;
        public float Sustain => m_Sustain;
        public static float MaxSustain = 1f;
        public float SustainRatio => m_Sustain / MaxSustain;

        [SerializeField] private float m_Decay;
        public float Decay => m_Decay;
        public static float MaxDecay = 20f;
        public float DecayRatio => m_Decay / MaxDecay;

        public Modifiers(float attack, float sustain, float decay) {
            m_Attack = attack;
            m_Sustain = m_Sustain;
            m_Decay = decay;
        }

    }

}