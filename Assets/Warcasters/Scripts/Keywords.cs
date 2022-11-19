/* --- Libraries --- */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warcaster;

namespace Warcaster {

    ///<summary>
    /// Defines the overarching behavior for the different archetypes
    ///<summary>
    public class Keywords : MonoBehaviour {

        #region Data Structures

        // The different types of summoning something.
        public enum SummoningType {
            None,
            Tribute,
            Discard,
            HP
        };

        // The different types of attacks.
        public enum AttackType {
            Normal,
            Quick,
            Pierce,
            Guard,
            Rage
        };

        // The different types of attacks.
        public enum ProtectionType {
            None,
            Omni,
            Battle
        };

        // The different types of summong conditions
        public enum EffectTrigger {
            None,
            Summon,
            Turn,
            Death,
            Attack
        };

        // The different types of summong conditions
        public enum Effect {
            None,
            Kill,
            Direct,
            Explode,
            Burn,
            Heal,
            Draw,
            Protect, // Give omni protection
            Guard, // Give battle protection
            Steal
        };

        // The different types of summong conditions
        public enum EffectTarget {
            None,
            This,
            Them,
            Us,
            Both,
            
            Deck,
            Hand,
            Field,
            Grave
        };

        #endregion

        #region Methods

        /* --- From Enumeration To String --- */

        // Standard conversion of enumeration to string
        public static string ToString<E>(E value) where E : struct, Enum {
            if (value.Equals(default(E))) {
                return "";
            }
            return value.ToString().ToUpper();
        }

        // Variation for effect triggers.
        public static string ToString(EffectTrigger effectTrigger) {
            if (effectTrigger == EffectTrigger.None) {
                return "";
            }
            return "On " + effectTrigger.ToString(); 
        }

        // Variation for counts.
        public static string CountToString(int count, int emptyCount) {
            if (count == emptyCount) {
                return "";
            }
            return count.ToString();
        }

        // Variation for costs.
        public static string CostToString(int cost) {
            if (cost < -255) {
                return "ALL";
            }
            else if (cost < -1) {
                return Mathf.Abs(cost).ToString() + "N";
            }
            else if (cost == -1) {
                return "N";
            }
            else if (cost < 2) {
                return "";
            }
            return cost.ToString();
        }

        // Variation for stats.
        public static string StatToString(int stat) {
            if (stat < 0) {
                string val = (Math.Abs(stat)).ToString();
                if (stat != -1) {
                    return val + "Σ";
                }
                return "Σ";
            }
            return stat.ToString(); 
        }

        /* --- From String To Enumeration --- */

        // Standard conversion of string to enumeration
        public static T FromString<T>(string value) where T : struct, Enum {
            T tmp = default(T);
            Enum.TryParse<T>(value, out tmp); 
            return tmp;
        }

        // Standard conversion of string to int.
        public static int FromString(string value) {
            int tmp = 0;
            Int32.TryParse(value, out tmp); 
            return tmp;
        }

        #endregion
        

    }

}