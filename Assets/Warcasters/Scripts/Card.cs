/* --- Libraries --- */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Warcaster;

/* --- Enumerations --- */
using Archetype = Warcaster.ArchetypeController.Archetype;
using SummoningType = Warcaster.Keywords.SummoningType;
using AttackType = Warcaster.Keywords.AttackType;
using ProtectionType = Warcaster.Keywords.ProtectionType;
using Effect = Warcaster.Keywords.Effect;
using EffectTrigger = Warcaster.Keywords.EffectTrigger;
using EffectTarget = Warcaster.Keywords.EffectTarget;

namespace Warcaster {

    ///<summary>
    /// The base for a card.
    ///<summary>
    [ExecuteInEditMode]
    public class Card : MonoBehaviour {

        // Event OnSummon()

        // Event OnTurn() {}

        // Event OnAttack

        // Event OnDeath

        // Effects
        // public void Kill(bool friendlies = false, bool random = false, int count = -1) {

        // }

        // // Effects
        // public void Fear(bool friendlies = false, bool random = false, int count = -1) {

        // }

        // public void Draw(CardCollection deck, int count = 1) {

        // }

        // public void Search(CardCollection deck, 
        //     string specificCard = "None", 
        //     string containsKeyword = "None",
        //     Archetype isArchetype = Archetype.Any,
        //     Type isType = Type.Any) {

        // }

        #region Fields.

        /* --- Editor Controls --- */
        [SerializeField] 
        private bool m_Redraw = false;

        /* --- Member Compnents --- */

        // The name text of this card.
        [SerializeField] 
        private Text m_NameText;

        // The summoning requirements of this card.
        [SerializeField] 
        private Text m_SummoningText;

        // The effect of this card.
        [SerializeField] 
        private Text m_EffectText;

        // The effect trigger of this card.
        [SerializeField] 
        private Text m_EffectTriggerText;

        // The name text of this card.
        [SerializeField] 
        private Text m_AttackPowerText;

        // The name text of this card.
        [SerializeField] 
        private Text m_AttackTypeText;

        // The name text of this card.
        [SerializeField] 
        private Text m_HealthPointText;

        // The name text of this card.
        [SerializeField] 
        private Text m_ProtectionTypeText;

        // The background image of this card.
        [SerializeField] 
        private Image m_BackgroundImage;

        // The splash art image of this card.
        [SerializeField] 
        private Image m_SplashArtImage;


        /* --- Member Variables --- */

        // The id of this card.
        [SerializeField, Range(0, 20)] 
        private int m_ID = 0;

        // The name of this card.
        [SerializeField, ReadOnly] 
        private string m_Name = "Unnamed";
        public string Name {
            get { return m_Name; }
            set { m_Name = value.ToUpper(); }
        }

        // The splash art for this card.
        [SerializeField, ReadOnly] 
        private Sprite m_SplashArt = null;
        public Sprite SplashArt {
            get { return m_SplashArt; }
            set { m_SplashArt = value; }
        }

        // The archetype of this card.
        [SerializeField, ReadOnly] 
        private Archetype m_Archetype = Archetype.Any;
        public string CardArchetype {
            get { return Keywords.ToString<Archetype>(m_Archetype); }
            set { m_Archetype = Keywords.FromString<Archetype>(value); }
        }

        // The summoning type of this card.
        [SerializeField, ReadOnly] 
        private SummoningType m_SummoningType;
        public string CardSummoningType {
            get { return Keywords.ToString<SummoningType>(m_SummoningType); }
            set { m_SummoningType = Keywords.FromString<SummoningType>(value); }
        }

        // The summoning type of this card.
        [SerializeField, ReadOnly] 
        private int m_SummoningCost = 0;
        public string SummoningCost {
            get { return Keywords.CostToString(m_SummoningCost); }
            set {  m_SummoningCost = Keywords.FromString(value); }
        }

        // The triggering effect of this card.
        [SerializeField, ReadOnly] 
        private EffectTrigger m_EffectTrigger;
        public string CardEffectTrigger {
            get { return Keywords.ToString(m_EffectTrigger); }
            set { m_EffectTrigger = Keywords.FromString<EffectTrigger>(value); }
        }

        // The effect of this card.
        [SerializeField, ReadOnly] 
        private Effect m_Effect;
        public string CardEffect {
            get { return Keywords.ToString<Effect>(m_Effect); }
            set { m_Effect = Keywords.FromString<Effect>(value); }
        }

        // The target of the effect of this card.
        [SerializeField, ReadOnly] 
        private EffectTarget m_EffectTarget;
        public string CardEffectTarget {
            get { return Keywords.ToString<EffectTarget>(m_EffectTarget); }
            set { m_EffectTarget = Keywords.FromString<EffectTarget>(value); }
        }

        // The number of targetsthis card effects
        [SerializeField, ReadOnly] 
        private int m_EffectTargetCount = 0;
        public string EffectTargetCount {
            get { return Keywords.CostToString(m_EffectTargetCount); }
            set { m_EffectTargetCount = Keywords.FromString(value); }
        }

        // The type of attack this card takes.
        [SerializeField, ReadOnly] 
        private AttackType m_AttackType;
        public string CardAttackType {
            get { return Keywords.ToString<AttackType>(m_AttackType); }
            set { m_AttackType = Keywords.FromString<AttackType>(value); }
        }

        // The amount of attacks this card gets.
        [SerializeField, ReadOnly] 
        private int m_AttackCount = 1;
        public string AttackCount {
            get { return Keywords.CountToString(m_AttackCount, 1); }
            set { m_AttackCount = Keywords.FromString(value); }
        }

        // The attack power of this card.
        [SerializeField, ReadOnly] 
        private int m_AttackPower = 0;
        public string AttackPower {
            get { return Keywords.StatToString(m_AttackPower); }
            set { m_AttackPower = Keywords.FromString(value); }
        }

        // The type of protections this card gets.
        [SerializeField, ReadOnly] 
        private ProtectionType m_ProtectionType;
        public string CardProtectionType {
            get { return Keywords.ToString<ProtectionType>(m_ProtectionType); }
            set { m_ProtectionType = Keywords.FromString<ProtectionType>(value); }
        }

        // The amount of protections this card gets.
        [SerializeField, ReadOnly] 
        private int m_ProtectionCount = 0;
        public string ProtectionCount {
            get { return Keywords.CountToString(m_ProtectionCount, 0); }
            set { m_ProtectionCount = Keywords.FromString(value); }
        }

        // The health points of this card.
        [SerializeField, ReadOnly] 
        private int m_HealthPoint = 0;
        public string HealthPoint {
            get { return Keywords.StatToString(m_HealthPoint); }
            set { m_HealthPoint = Keywords.FromString(value); }
        }

        #endregion

        #region Methods.

        void Update() {
            if (!Application.isPlaying && m_Redraw) {
                CardManager.GetCard(this, m_ID);
                Draw();
            }
        }

        void Draw() {

            // Read the name into the card.
            m_NameText.text = Name;
            Color nameColor = ArchetypeController.GetNameColor(m_Archetype);
            TextController.Set(m_NameText, TextController.DefaultFont, nameColor, TextController.SmallText);
            
            // Read the effect into the card.
            m_SummoningText.text = CardSummoningType + " " + SummoningCost;
            Color summoningColor = ArchetypeController.GetStatColor(m_Archetype);
            TextController.Set(m_SummoningText, TextController.DetailFont, summoningColor, TextController.VerySmallText);

            // Read the effect into the card.
            m_EffectText.text = CardEffect + " " + CardEffectTarget + " " + EffectTargetCount;
            Color keywordColor = ArchetypeController.GetEffectColor(m_Archetype);
            TextController.Set(m_EffectText, TextController.KeywordFont, keywordColor, TextController.LargeText);
            
            m_EffectTriggerText.text = CardEffectTrigger;
            TextController.Set(m_EffectTriggerText, TextController.KeywordFont, summoningColor, TextController.VerySmallText);

            // Read the stats into this card.
            m_AttackPowerText.text = AttackPower;
            m_HealthPointText.text = HealthPoint;
            Color statColor = ArchetypeController.GetStatColor(m_Archetype);
            TextController.Set(m_AttackPowerText, TextController.NumericalFont, statColor, TextController.MediumText);
            TextController.Set(m_HealthPointText, TextController.NumericalFont, statColor, TextController.MediumText);
            
            m_AttackTypeText.text = CardAttackType + " " + AttackCount;
            TextController.Set(m_AttackTypeText, TextController.DetailFont, summoningColor, TextController.VeryVerySmallText);
            m_ProtectionTypeText.text = CardProtectionType + " " + ProtectionCount;
            TextController.Set(m_ProtectionTypeText, TextController.DetailFont, summoningColor, TextController.VeryVerySmallText);

            // Color the background with the correct archetype color.
            Color bkgColor =  ArchetypeController.GetBackgroundColor(m_Archetype);
            m_BackgroundImage.material.SetColor("_Color", bkgColor);

            // Draw the correct splash art image.
            m_SplashArtImage.sprite = m_SplashArt;

        }

        #endregion

    }
}