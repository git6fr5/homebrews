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

        [SerializeField] private string m_Name;
        public string Name => m_Name;

        [SerializeField] private bool m_IsAttack;
        [SerializeField] private Attack m_Attack;

        [SerializeField] private bool m_Proficient;

        [SerializeField] private bool m_HasEffect;
        [SerializeField] private Effect m_Effect;

        public string ToTag(Stat stat) {

            string temp = "";

            if (m_IsAttack) {
                string targets = m_Attack.Targets.ToString() + " target";
                if (m_Attack.Targets > 1) {
                    targets += "s";
                }

                string attackType = m_Attack.AttackType.ToString();
                string attackBonus = "+" + m_Attack.GetAttackBonus(stat, m_Proficient).ToString() + " ATK";

                string dmg  = m_Attack.DamageLevel.ToString() + Stat.ToDice((int)m_Attack.Size);
                dmg += "+" + m_Attack.GetAttackBonus(stat, false).ToString();
                string dmgtype = " " + m_Attack.DamageType.ToString();

                string comma = ", ";
                temp = targets + comma + attackType + comma + attackBonus + comma + dmg + dmgtype;

            }

            if (m_HasEffect) {
                if (m_IsAttack) {
                    temp += "\n";
                }
                temp += m_Effect.ToTag();
            }

            return temp;
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

        [Space(2), Header("Range/Thrown")]
        [SerializeField] private int m_ShortRange;
        [SerializeField] private int m_LongRange;

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

        // public int GetRangeBonus() {
        //     switch (m_AttackType) {
        //         return AttackType.Melee:
        //             return "(5ft.)";
        //         case AttackType.Reach:
        //             return "(10ft.)";
        //         case AttackType.Thrown:
        //             return "(" + m_ShortRange.ToString() + "ft./" + m_LongRange.ToString() + "ft.)";
        //         case AttackType.Ranged:
        //             return "(" + m_ShortRange.ToString() + "ft./" + m_LongRange.ToString() + "ft.)";
        //         default:
        //             return "";
        //     }
        // }

    }

    [System.Serializable]
    public class Effect {

        // DC.
        public int m_DC;
        public SkillBase m_SkillBase;

        // 
        public TargetType m_Target;
        public AffectedType m_Affected;

        [Space(2), Header("Chosen Based Effects")]
        public int m_ChosenCount;

        [Space(2), Header("Distance Based Effects")]
        public int m_Distance;
        
        ///----------------------
        public Condition m_FailureCondition;

        [SerializeField] private int m_DamageLevel;
        public int DamageLevel => m_DamageLevel;

        [SerializeField] private Size m_Size;
        public Size Size => m_Size; 

        [SerializeField] private DamageType m_DamageType;
        public DamageType DamageType => m_DamageType;

        public string ToTag() {

            string affected = AffectedString(m_ChosenCount);
            string target = TargetString(m_Distance);
            string dcString = DCToString();

            string dmgString = DamageString(m_DamageLevel, m_Size, m_DamageType);
            string condString = ConditionString(m_FailureCondition);
            string failureString = CombineDamageAndCondition(dmgString, condString);

            string space = " ";
            return affected + space + target + space + dcString + space + failureString;

        }

        public string DamageString(int dmgLvl, Size size, DamageType dmgType) {
            if (dmgLvl == 0) {
                return "";
            }
            string dmg  = "take " + dmgLvl.ToString() + Stat.ToDice((int)size);
            dmg += " " + dmgType.ToString();
            return dmg;
        }

        public string ConditionString(Condition condition) {
            if (condition != Condition.None) {
                return "are " + condition.ToString();
            }
            return "";
        }

        public string CombineDamageAndCondition(string dmg, string cond) {
            string temp = "On fail, they ";
            if (dmg != "") {
                temp += dmg;
                if (cond != "") {
                    temp += " and ";
                }
            }
            else {
                temp += cond;
            }
            return temp;
        }

        public string TargetString(int distance) {
            switch (m_Target) {
                case TargetType.AOE:
                    return "creatures within " + distance.ToString() + "ft. radius";
                case TargetType.Cone:
                    return "creatures within " + distance.ToString() + "ft. cone";
                case TargetType.LineOfSight:
                    return "creatures with line of sight";
                case TargetType.Earshot:
                    return "creatures within earshot";
                case TargetType.Aware:
                    return "creatures aware of this presence";
                case TargetType.Target:
                    return "target";
                default:
                    return "this creature";
            }
        }

        public string AffectedString(int count) {
            if (m_Affected == AffectedType.Chosen) {
                string temp = "";
                if (count > 0) {
                    temp += "Upto " + count.ToString();
                }
                else {
                    temp += "All ";
                }
                temp += m_Affected.ToString();
                return temp;
            }
            return m_Affected.ToString();
        }

        public string DCToString() {
            return "makes a DC " + m_DC.ToString() + " " + m_SkillBase.ToString() + " Save ";
        }

    }

    public enum TargetType {
        AOE,
        Cone,
        LineOfSight,
        Earshot,
        Aware,
        Target,
        Self
    }

    public enum AffectedType {
        All,
        Allied,
        Hostile,
        Chosen,
        ChosenAllies,
        ChosenHostiles,
        Hit
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

    public enum Condition {
        None,
        Frightened,
        Charmed,
        Poisoned,
        knocked_Prone,
    }


}