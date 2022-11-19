/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonsNDragons;

namespace DungeonsNDragons {

    ///<summary>
    ///
    ///<summary>
    public class Settings : MonoBehaviour {

        public static Settings Instance;

        // Card Size.
        [SerializeField] private Vector2 m_Size;
        public static Vector2 Size => Instance.m_Size;
        [SerializeField] private float m_Border;
        public static float Border => Instance.m_Border;
        [SerializeField] private float m_Margin;
        public static float Margin => Instance.m_Margin;
        public static float VerticalBorderPosition => Instance.m_Size.y / 2f - Instance.m_Margin;
        public static float HorizontalBorderPosition => Instance.m_Size.x / 2f - Instance.m_Margin;
        public static Vector2 Background => Instance.m_Size - 2f * new Vector2(Instance.m_Border, Instance.m_Border);
        public static Vector2 Workspace => Instance.m_Size - 2f * new Vector2(Instance.m_Margin, Instance.m_Margin);
        
        // Fonts
        [SerializeField] protected Font m_AlphabetFont;
        public static Font AlphabetFont => Instance.m_AlphabetFont;
        [SerializeField] protected Font m_NumericFont;
        public static Font NumericFont => Instance.m_NumericFont;
        [SerializeField] private Color m_FontColor;
        public static Color FontColor => Instance.m_FontColor;

        // Title Font Size.
        [SerializeField] private bool m_BoldTitle;
        public static bool BoldTitle => Instance.m_BoldTitle;

        // Sizes.
        [SerializeField] private float m_TitleHeightRatio = 0.075f;
        public static float TitleHeightRatio => Instance.m_TitleHeightRatio;

        [SerializeField] private float m_PropertyHeightRatio = 0.035f;
        public static float PropertyHeightRatio => Instance.m_PropertyHeightRatio;

        [SerializeField] private float m_PropertyWidthRatio = 0.1f;
        public static float PropertyWidthRatio => Instance.m_PropertyWidthRatio;
        
        [SerializeField] private float m_StatHeightRatio = 0.035f;
        public static float StatHeightRatio => Instance.m_StatHeightRatio;

        [SerializeField] private float m_StatSpacingRatio = 0.025f;
        public static float StatSpacingRatio => Instance.m_StatSpacingRatio;

        [SerializeField] private float m_DescriptionHeightRatio = 0.05f;
        public static float DescriptionHeightRatio => Instance.m_DescriptionHeightRatio;

        [SerializeField] private float m_DescriptionWidthRatio = 0.1f;
        public static float DescriptionWidthRatio => Instance.m_DescriptionWidthRatio;
        
        [SerializeField] private float m_AttackHeightRatio = 0.035f;
        public static float AttackHeightRatio => Instance.m_AttackHeightRatio;

        [SerializeField] private float m_AttackSpacingRatio = 0.035f;
        public static float AttackSpacingRatio => Instance.m_AttackSpacingRatio;

        // Runs once before the first frame.
        void Awake() {
            Instance = this;
        }

    }
}