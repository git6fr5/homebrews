/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonsNDragons;

namespace DungeonsNDragons {

    public class Monster : Card {

        // Name.
        [SerializeField] private string m_MonsterName;
        public string NAME => m_MonsterName.ToUpper();

        [SerializeField] private Stat m_Stats;

        [SerializeField] private Text[] m_StatBlockText; 

        // Header.
        [SerializeField] private Text m_NameText;
        [SerializeField] private Text m_HPText;
        [SerializeField] private Text m_ACText;
        [SerializeField] private Text m_SpeedText;

        // Para 1.
        [SerializeField] private Text m_SavingThrows;
        [SerializeField] private Text m_SkillChecks;

        // Para 2.
        [SerializeField] private MonsterAttack m_Attack;
        [SerializeField] private Text m_AttackText;

        public void Update() {
            Draw();
        }

        public void Draw() {
            // Header.
            Card.Title(NAME);
            m_NameText.text = NAME;
            m_NameText.font = m_AlphabetFont;

            m_HPText.text = m_Stats.HPTag;
            m_HPText.font = m_NumericFont;

            m_ACText.text = m_Stats.ACTag;
            m_ACText.font = m_NumericFont;

            m_SpeedText.text = m_Stats.SpeedTag;
            m_SpeedText.font = m_NumericFont;

            // Stat block.
            m_StatBlockText[0].text = m_Stats.STRTag;
            m_StatBlockText[1].text = m_Stats.DEXTag;
            m_StatBlockText[2].text = m_Stats.CONTag;
            m_StatBlockText[3].text = m_Stats.INTTag;
            m_StatBlockText[4].text = m_Stats.WISTag;
            m_StatBlockText[5].text = m_Stats.CHATag;

            // Body.
            m_SavingThrows.text = m_Stats.SavingThrowingTag;
            m_SkillChecks.text = m_Stats.SkillCheckTag;

            // Attacks.
            m_AttackText.text = m_Attack.ToTag(m_Stats);

        }

    }

}