/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonsNDragons;

namespace DungeonsNDragons {

    public class WeaponCard : Card {

        // Name.
        [SerializeField] private string m_WeaponName;
        public string NAME => m_WeaponName.ToUpper();

        [SerializeField] private Attack[] m_Attacks;
        // [SerializeField] private Text m_AttackText;

        public override void Draw() {
            Background();
            Text title = Title(NAME, Settings.TitleHeightRatio);

            // // // Stat block.
            // RectTransform spdRT = speed.rectTransform;
            // Text STR = StatLine("STR", m_Stats.STRTag, spdRT, 0, 6, Settings.StatHeightRatio, Settings.StatSpacingRatio);
            // Text DEX = StatLine("DEX", m_Stats.DEXTag, spdRT, 1, 6, Settings.StatHeightRatio, Settings.StatSpacingRatio);
            // Text CON = StatLine("CON", m_Stats.CONTag, spdRT, 2, 6, Settings.StatHeightRatio, Settings.StatSpacingRatio);
            // Text INT = StatLine("INT", m_Stats.INTTag, spdRT, 3, 6, Settings.StatHeightRatio, Settings.StatSpacingRatio);
            // Text WIS = StatLine("WIS", m_Stats.WISTag, spdRT, 4, 6, Settings.StatHeightRatio, Settings.StatSpacingRatio);
            // Text CHA = StatLine("CHA", m_Stats.CHATag, spdRT, 5, 6, Settings.StatHeightRatio, Settings.StatSpacingRatio);

            // // Body.
            // Text statBonuses = Property("Saves: ",  m_Stats.SavingThrowingTag, STR.rectTransform, Settings.DescriptionWidthRatio, Settings.DescriptionHeightRatio);
            // Text skillthrows = Property("Skills: ",  m_Stats.SkillCheckTag, savingthrows.rectTransform, Settings.DescriptionWidthRatio, Settings.DescriptionHeightRatio);

            // // Attacks.
            // Text aboveAttack = skillthrows;
            // for (int i = 0; i < m_Attacks.Length; i++) {
            //     aboveAttack = Description(m_Attacks[i].Name + " ",  m_Attacks[i].ToTag(m_Stats), aboveAttack.rectTransform, Settings.AttackHeightRatio, Settings.AttackSpacingRatio);
            // }

        }

    }

}