/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonsNDragons;

namespace DungeonsNDragons {

    public enum SkillBase {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    [System.Serializable]
    public class Skill {

        [HideInInspector] private string m_Name;
        public string Name => m_Name;
        [HideInInspector] private SkillBase m_Base;
        public SkillBase Base => m_Base;
        [SerializeField] private bool m_Proficient;
        public bool Proficient => m_Proficient;

        public Skill(string name, SkillBase skillBase) {
            m_Name = name;
            m_Base = skillBase;
        }

    }

    [System.Serializable]
    public class SkillSet {

        [Space(2), Header("Saving Throw Proficiencies")]
        [SerializeField] private Skill m_StrengthSaves = new Skill("STR", SkillBase.Strength);
        [SerializeField] private Skill m_DexteritySaves = new Skill("DEX", SkillBase.Dexterity);
        [SerializeField] private Skill m_ConstitutionSaves = new Skill("WIS", SkillBase.Constitution);
        [SerializeField] private Skill m_WisdomSaves = new Skill("WIS", SkillBase.Wisdom);
        [SerializeField] private Skill m_IntelligenceSaves = new Skill("INT", SkillBase.Intelligence);
        [SerializeField] private Skill m_CharismaSaves = new Skill("CHA", SkillBase.Charisma);


        [Space(2), Header("Skill Check Proficiencies")]
        // Athletics.
        [SerializeField] private Skill m_Athletics = new Skill("Athletics", SkillBase.Strength);

        // Dexterity.
        [SerializeField] private Skill m_Acrobatics = new Skill("Acrobatics", SkillBase.Dexterity);
        [SerializeField] private Skill m_SleightOfHand = new Skill("Sleight Of Hand", SkillBase.Dexterity);
        [SerializeField] private Skill m_Stealth = new Skill("Stealth", SkillBase.Dexterity);

        // Wisdom
        [SerializeField] private Skill m_AnimalHandling = new Skill("Animal Handling", SkillBase.Wisdom);
        [SerializeField] private Skill m_Insight = new Skill("Insight", SkillBase.Wisdom);
        [SerializeField] private Skill m_Medicine = new Skill("Medicine", SkillBase.Wisdom);
        [SerializeField] private Skill m_Perception = new Skill("Perception", SkillBase.Wisdom);
        [SerializeField] private Skill m_Survival = new Skill("Survival", SkillBase.Wisdom);

        // Intelligence
        [SerializeField] private Skill m_Arcana = new Skill("Arcana", SkillBase.Intelligence);
        [SerializeField] private Skill m_History = new Skill("History", SkillBase.Intelligence);
        [SerializeField] private Skill m_Investigation = new Skill("Investigation", SkillBase.Intelligence);
        [SerializeField] private Skill m_Nature = new Skill("Nature", SkillBase.Intelligence);
        [SerializeField] private Skill m_Religion = new Skill("Religion", SkillBase.Intelligence);

        // Charisma
        [SerializeField] private Skill m_Deception = new Skill("Deception", SkillBase.Charisma);
        [SerializeField] private Skill m_Intimidation = new Skill("Intimidation", SkillBase.Charisma);
        [SerializeField] private Skill m_Performance = new Skill("Performance", SkillBase.Charisma);
        [SerializeField] private Skill m_Persuasion = new Skill("Persuasion", SkillBase.Charisma);

        public string GetSavingThrowTag(Stat stat) {
            string temp = "";
            temp = ToString(temp, m_StrengthSaves, stat);
            temp = ToString(temp, m_DexteritySaves, stat);
            temp = ToString(temp, m_ConstitutionSaves, stat);
            temp = ToString(temp, m_IntelligenceSaves, stat);
            temp = ToString(temp, m_WisdomSaves, stat);
            temp = ToString(temp, m_CharismaSaves, stat);
            if (temp != "") {
                return temp; 
            }
            return "";
        }

        public string GetSkillChecksTag(Stat stat) {
            string temp = "";
            temp = ToString(temp, m_Athletics, stat);

            temp = ToString(temp, m_Acrobatics, stat);
            temp = ToString(temp, m_SleightOfHand, stat);
            temp = ToString(temp, m_Stealth, stat);

            temp = ToString(temp, m_AnimalHandling, stat);
            temp = ToString(temp, m_Insight, stat);
            temp = ToString(temp, m_Medicine, stat);
            temp = ToString(temp, m_Perception, stat);
            temp = ToString(temp, m_Survival, stat);

            temp = ToString(temp, m_Arcana, stat);
            temp = ToString(temp, m_History, stat);
            temp = ToString(temp, m_Investigation, stat);
            temp = ToString(temp, m_Nature, stat);
            temp = ToString(temp, m_Religion, stat);

            temp = ToString(temp, m_Deception, stat);
            temp = ToString(temp, m_Intimidation, stat);
            temp = ToString(temp, m_Performance, stat);
            temp = ToString(temp, m_Persuasion, stat);

            if (temp != "") {
                return temp; 
            }
            return "";
        }

        private static string ToString(string tag, Skill skill, Stat stat) {
            int statBonus = GetStatBonus(skill.Base, stat);
            int bonus = statBonus + stat.PROF;
            if (skill.Proficient) { 
                string temp = skill.Name + " (+" + bonus.ToString() + ")";
                if (tag == "") {
                    return temp;
                }
                return tag + ", " + temp;
            }
            return tag;
        }

        private static int GetStatBonus(SkillBase skillBase, Stat stat) {
            switch (skillBase) {
                case SkillBase.Strength:
                    return stat.STR;
                case SkillBase.Dexterity:
                    return stat.DEX;
                case SkillBase.Constitution:
                    return stat.CON;
                case SkillBase.Intelligence:
                    return stat.INT;
                case SkillBase.Wisdom:
                    return stat.WIS;
                case SkillBase.Charisma:
                    return stat.CHA;
            }
            return 0;
        }

    }

}