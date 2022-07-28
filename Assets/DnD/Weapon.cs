/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonsNDragons;

namespace DungeonsNDragons {

    [System.Serializable]
    public class MonsterAttack {

        //     {x} target(s), Melee ({Xft}), {STR+PROF} ATK, {ndN+STR} Damage Type",
        //   "bullet | On condition met: DC {X} MOD Save/Opposed.",

        [SerializeField] private Attack m_Attack;
        [SerializeField] private bool m_Proficient;

        public string ToTag(Stat stat) {

            string targets = m_Attack.Targets.ToString() + " target";
            if (m_Attack.Targets > 1) {
                targets += "s";
            }

            string attackType = m_Attack.AttackType.ToString();
            string attackBonus = "+" + m_Attack.GetAttackBonus(stat, m_Proficient).ToString() + " ATK";

            string dmg  = m_Attack.DamageLevel.ToString() + Stat.ToDice((int)m_Attack.Size) + " " + m_Attack.DamageType.ToString();

            string comma = ", ";
            return targets + comma + attackType + comma + attackBonus + comma + dmg;

        }
        

    }

    [System.Serializable]
    public class Attack {

        //     {x} target(s), Melee ({Xft}), {STR+PROF} ATK, {ndN+STR} Damage Type",
        //   "bullet | On condition met: DC {X} MOD Save/Opposed.",

        [SerializeField] private int m_Targets;
        public int Targets => m_Targets;

        [SerializeField] private AttackType m_AttackType;
        public AttackType AttackType => m_AttackType;

        [SerializeField] private int m_DamageLevel;
        public int DamageLevel => m_DamageLevel;

        [SerializeField] private Size m_Size;
        public Size Size => m_Size;

        [SerializeField] private DamageType m_DamageType;
        public DamageType DamageType => m_DamageType;

        public int GetAttackBonus(Stat stat, bool proficient) {
            int attackBonus = proficient ? stat.PROF : 0;
            switch (m_AttackType) {
                case AttackType.Melee:
                    return attackBonus + stat.STR;
                case AttackType.Reach:
                    return attackBonus + stat.STR;
                case AttackType.Thrown:
                    return attackBonus + stat.DEX;
                case AttackType.Ranged:
                    return attackBonus + stat.DEX;
                default:
                    return 0;
            }
        }

    }

    public enum DamageType {
        Slashing,
        Piercing,
        Bludgeoning
    }

    public enum AttackType {
        Melee,
        Reach,
        Thrown,
        Ranged,
    }


}