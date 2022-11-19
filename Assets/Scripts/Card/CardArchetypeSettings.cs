/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warcaster.Cards;

/* --- Enumerations --- */
using Archetype = Warcaster.Cards.CardKeywords.Archetype;

namespace Warcaster.Cards {

    ///<summary>
    /// Defines the overarching behavior for the different archetypes
    ///<summary>
    [ExecuteInEditMode, DefaultExecutionOrder(1000)]
    public class CardArchetypeSettings : MonoBehaviour {

        #region Data Structures

        // A small class to pair archetypes with relevant data.
        [System.Serializable]
        public class ArchetypeData {

            // Static data.
            public static Color DefaultColor = Color.white;

            // Local data.
            public Archetype Archetype;
            public Color BackgroundColor;
            public Color NameColor;
            public Color EffectColor;
            public Color StatColor;

        }

        #endregion

        #region Fields

        // Instantiating the singleton.
        public static CardArchetypeSettings Instance;

        // A list of all the archetype colors to be defined in the inspector.
        [SerializeField]
        private List<ArchetypeData> m_ArchetypeData;
        public static List<ArchetypeData> MainArchetypeData => Instance.m_ArchetypeData;

        // A default 

        #endregion

        #region Methods

        void Start() {
            Instance = this;
        }

        void Update() {
            if (!Application.isPlaying) {
                Instance = this;
            }
        }

        public static Color GetBackgroundColor(Archetype archetype) {
            ArchetypeData archetypeData = MainArchetypeData.Find(data => data.Archetype == archetype);
            if (archetypeData != null) {
                return archetypeData.BackgroundColor;
            }
            return ArchetypeData.DefaultColor;
        }

        public static Color GetNameColor(Archetype archetype) {
            ArchetypeData archetypeData = MainArchetypeData.Find(data => data.Archetype == archetype);
            if (archetypeData != null) {
                return archetypeData.NameColor;
            }
            return ArchetypeData.DefaultColor;
        }

        public static Color GetEffectColor(Archetype archetype) {
            ArchetypeData archetypeData = MainArchetypeData.Find(data => data.Archetype == archetype);
            if (archetypeData != null) {
                return archetypeData.EffectColor;
            }
            return ArchetypeData.DefaultColor;
        }

        public static Color GetStatColor(Archetype archetype) {
            ArchetypeData archetypeData = MainArchetypeData.Find(data => data.Archetype == archetype);
            if (archetypeData != null) {
                return archetypeData.StatColor;
            }
            return ArchetypeData.DefaultColor;
        }

        #endregion
        

    }

}