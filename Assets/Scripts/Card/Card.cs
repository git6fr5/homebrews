/* --- Libraries --- */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Warcaster;

/* --- Enumerations --- */
using Archetype = Warcaster.Cards.CardKeywords.Archetype;
using SummoningType = Warcaster.Cards.CardKeywords.SummoningType;
using AttackType = Warcaster.Cards.CardKeywords.AttackType;
using ProtectionType = Warcaster.Cards.CardKeywords.ProtectionType;
using Effect = Warcaster.Cards.CardKeywords.Effect;
using EffectTrigger = Warcaster.Cards.CardKeywords.EffectTrigger;
using EffectTarget = Warcaster.Cards.CardKeywords.EffectTarget;

namespace Warcaster.Cards {

    ///<summary>
    /// The base for a card.
    ///<summary>
    [ExecuteInEditMode]
    public class Card : MonoBehaviour {

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
        private string m_CardName = "Unnamed";
        public string Name {
            get { return m_CardName; }
            set { m_CardName = value.ToUpper(); }
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
            get { return CardKeywords.ToString<Archetype>(m_Archetype); }
            set { m_Archetype = CardKeywords.FromString<Archetype>(value); }
        }

        // The summoning type of this card.
        [SerializeField, ReadOnly] 
        private SummoningType m_SummoningType;
        public string CardSummoningType {
            get { return CardKeywords.ToString<SummoningType>(m_SummoningType); }
            set { m_SummoningType = CardKeywords.FromString<SummoningType>(value); }
        }

        // The summoning type of this card.
        [SerializeField, ReadOnly] 
        private int m_SummoningCost = 0;
        public string SummoningCost {
            get { return CardKeywords.CostToString(m_SummoningCost); }
            set {  m_SummoningCost = CardKeywords.FromString(value); }
        }

        // The triggering effect of this card.
        [SerializeField, ReadOnly] 
        private EffectTrigger m_EffectTrigger;
        public string CardEffectTrigger {
            get { return CardKeywords.ToString(m_EffectTrigger); }
            set { m_EffectTrigger = CardKeywords.FromString<EffectTrigger>(value); }
        }

        // The effect of this card.
        [SerializeField, ReadOnly] 
        private Effect m_Effect;
        public string CardEffect {
            get { return CardKeywords.ToString<Effect>(m_Effect); }
            set { m_Effect = CardKeywords.FromString<Effect>(value); }
        }

        // The target of the effect of this card.
        [SerializeField, ReadOnly] 
        private EffectTarget m_EffectTarget;
        public string CardEffectTarget {
            get { return CardKeywords.ToString<EffectTarget>(m_EffectTarget); }
            set { m_EffectTarget = CardKeywords.FromString<EffectTarget>(value); }
        }

        // The number of targetsthis card effects
        [SerializeField, ReadOnly] 
        private int m_EffectTargetCount = 0;
        public string EffectTargetCount {
            get { return CardKeywords.CostToString(m_EffectTargetCount); }
            set { m_EffectTargetCount = CardKeywords.FromString(value); }
        }

        // The type of attack this card takes.
        [SerializeField, ReadOnly] 
        private AttackType m_AttackType;
        public string CardAttackType {
            get { return CardKeywords.ToString<AttackType>(m_AttackType); }
            set { m_AttackType = CardKeywords.FromString<AttackType>(value); }
        }

        // The amount of attacks this card gets.
        [SerializeField, ReadOnly] 
        private int m_AttackCount = 1;
        public string AttackCount {
            get { return CardKeywords.CountToString(m_AttackCount, 1); }
            set { m_AttackCount = CardKeywords.FromString(value); }
        }

        // The attack power of this card.
        [SerializeField, ReadOnly] 
        private int m_AttackPower = 0;
        public string AttackPower {
            get { return CardKeywords.StatToString(m_AttackPower); }
            set { m_AttackPower = CardKeywords.FromString(value); }
        }

        // The type of protections this card gets.
        [SerializeField, ReadOnly] 
        private ProtectionType m_ProtectionType;
        public string CardProtectionType {
            get { return CardKeywords.ToString<ProtectionType>(m_ProtectionType); }
            set { m_ProtectionType = CardKeywords.FromString<ProtectionType>(value); }
        }

        // The amount of protections this card gets.
        [SerializeField, ReadOnly] 
        private int m_ProtectionCount = 0;
        public string ProtectionCount {
            get { return CardKeywords.CountToString(m_ProtectionCount, 0); }
            set { m_ProtectionCount = CardKeywords.FromString(value); }
        }

        // The health points of this card.
        [SerializeField, ReadOnly] 
        private int m_HealthPoint = 0;
        public string HealthPoint {
            get { return CardKeywords.StatToString(m_HealthPoint); }
            set { m_HealthPoint = CardKeywords.FromString(value); }
        }

        #endregion

        #region Methods.

        void Update() {
            if (CardArchetypeSettings.Instance == null) { return; }
            if (CardTextSettings.Instance == null) { return; }

            if (!Application.isPlaying && m_Redraw) {
                CardDataReader.GetCard(this, m_ID);
                Draw();
            }
        }

        void Draw() {

            // Read the name into the card.
            m_NameText.text = Name;
            Color nameColor = CardArchetypeSettings.GetNameColor(m_Archetype);
            CardTextSettings.Set(m_NameText, CardTextSettings.DefaultFont, nameColor, CardTextSettings.SmallText);
            
            // Read the effect into the card.
            m_SummoningText.text = CardSummoningType + " " + SummoningCost;
            Color summoningColor = CardArchetypeSettings.GetStatColor(m_Archetype);
            CardTextSettings.Set(m_SummoningText, CardTextSettings.DetailFont, summoningColor, CardTextSettings.VerySmallText);

            // Read the effect into the card.
            m_EffectText.text = CardEffect + " " + CardEffectTarget + " " + EffectTargetCount;
            Color keywordColor = CardArchetypeSettings.GetEffectColor(m_Archetype);
            CardTextSettings.Set(m_EffectText, CardTextSettings.KeywordFont, keywordColor, CardTextSettings.LargeText);
            
            m_EffectTriggerText.text = CardEffectTrigger;
            CardTextSettings.Set(m_EffectTriggerText, CardTextSettings.KeywordFont, summoningColor, CardTextSettings.VerySmallText);

            // Read the stats into this card.
            m_AttackPowerText.text = AttackPower;
            m_HealthPointText.text = HealthPoint;
            Color statColor = CardArchetypeSettings.GetStatColor(m_Archetype);
            CardTextSettings.Set(m_AttackPowerText, CardTextSettings.NumericalFont, statColor, CardTextSettings.MediumText);
            CardTextSettings.Set(m_HealthPointText, CardTextSettings.NumericalFont, statColor, CardTextSettings.MediumText);
            
            m_AttackTypeText.text = CardAttackType + " " + AttackCount;
            CardTextSettings.Set(m_AttackTypeText, CardTextSettings.DetailFont, summoningColor, CardTextSettings.VeryVerySmallText);
            m_ProtectionTypeText.text = CardProtectionType + " " + ProtectionCount;
            CardTextSettings.Set(m_ProtectionTypeText, CardTextSettings.DetailFont, summoningColor, CardTextSettings.VeryVerySmallText);

            // Color the background with the correct archetype color.
            Color bkgColor =  CardArchetypeSettings.GetBackgroundColor(m_Archetype);
            m_BackgroundImage.material.SetColor("_Color", bkgColor);

            // Draw the correct splash art image.
            m_SplashArtImage.sprite = m_SplashArt;

        }

        public bool TriggerEvent(EffectTrigger eventTrigger) {
            if (m_EffectTrigger == EffectTrigger.None) { return false; }
            if (m_Effect == Effect.None) { return false; }

            bool triggered = m_EffectTrigger == eventTrigger;
            if (triggered) {
                return true;
            }
            return false;

        }

        // public void OnSummon()

        // Event OnTurn() {}

        // Event OnAttack

        // Event OnDeath


        #endregion

    }
}