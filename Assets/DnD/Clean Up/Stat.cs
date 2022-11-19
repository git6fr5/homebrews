/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonsNDragons;

namespace DungeonsNDragons {

    public enum Size {
        Small = 4,
        Medium = 6,
        Large = 8,
        Giant = 10,
        Gargantuan = 12,
        Dragon = 20
    }

    [System.Serializable]
    public class Stat {
        
        // LVL.
        [SerializeField] private int m_Level;
        public int PROF => 2 + (int)Mathf.Floor(m_Level / 4f);

        // HP.
        [SerializeField] private Size m_Size;
        public int SIZE => (int)m_Size;
        public int BASEHP => (int)Mathf.Max(SIZE, CON * SIZE);
        public int HP => m_Level * (int)Mathf.Floor(SIZE / 2f) + BASEHP;
        public string HitDie => Stat.ToDice(SIZE);
        public string HPTag => HP.ToString() + " (" + m_Level.ToString() + HitDie + "+" + BASEHP.ToString() + ")";

        // Speed.
        [SerializeField] private Movement m_Movement;
        public string SpeedTag => m_Movement.ToTag();

        // Stats.
        [SerializeField] private int m_Strength;
        public int STR => Stat.ToModifier(m_Strength);
        public string STRTag => ModifierTag(m_Strength);

        [SerializeField] private int m_Dexterity;
        public int DEX => Stat.ToModifier(m_Dexterity);
        public string DEXTag => ModifierTag(m_Dexterity);

        [SerializeField] private int m_Constitution;
        public int CON => Stat.ToModifier(m_Constitution);
        public string CONTag => ModifierTag(m_Constitution);

        [SerializeField] private int m_Intelligence;
        public int INT => Stat.ToModifier(m_Intelligence);
        public string INTTag => ModifierTag(m_Intelligence);

        [SerializeField] private int m_Wisdom;
        public int WIS => Stat.ToModifier(m_Wisdom);
        public string WISTag => ModifierTag(m_Wisdom);

        [SerializeField] private int m_Charisma;
        public int CHA => Stat.ToModifier(m_Charisma);
        public string CHATag => ModifierTag(m_Charisma);

        // Armour.
        [SerializeField] private int m_WornArmour;
        [SerializeField] private int m_NaturalArmour;
        public int AC => 10 + DEX + m_WornArmour + m_NaturalArmour;
        public string ACTag => AC.ToString() + ToArmourType(m_WornArmour, m_NaturalArmour);

        // Skills
        [SerializeField] private SkillSet m_SkillSet;
        public string SavingThrowingTag => m_SkillSet.GetSavingThrowTag(this);
        public string SkillCheckTag => m_SkillSet.GetSkillChecksTag(this);

        public static string ModifierTag(int points) {
            return points.ToString() + "\n(+" + ToModifier(points).ToString() + ")";
        }

        public static int ToModifier(int points) {
            float temp = ((float)points - 10f) / 2f;
            return (int)Mathf.Floor(temp);
        }

        public static string ToDice(int denomination) {
            return "d" + denomination.ToString();
        }

        public static string ToArmourType(int worn, int natural) {
            if (worn != 0 && natural != 0) {
                return " (Worn Armour, Natural Armour)";
            }
            if (worn != 0) {
                return " (Worn Armour)";
            }
            if (natural != 0) {
                return " (Natural Armour)";
            }
            return "";
        }

    }

}