/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Warcaster.Cards;

namespace Warcaster.Cards {

    ///<summary>
    /// Defines the overarching behavior for the card text.
    ///<summary>
    [ExecuteInEditMode, DefaultExecutionOrder(1000)]
    public class CardTextSettings : MonoBehaviour {

        #region Fields

        // Instantiating the singleton.
        public static CardTextSettings Instance;

        // The font size distribution.
        [SerializeField]
        private int m_LargeText = 60;
        public static int LargeText => Instance.m_LargeText;

        [SerializeField]
        private int m_MediumText = 50;
        public static int MediumText => Instance.m_MediumText;

        [SerializeField]
        private int m_SmallText = 40;
        public static int SmallText => Instance.m_SmallText;

        [SerializeField]
        private int m_VerySmallText = 25;
        public static int VerySmallText => Instance.m_VerySmallText;

        [SerializeField]
        private int m_VeryVerySmallText = 15;
        public static int VeryVerySmallText => Instance.m_VeryVerySmallText;

        // The main font for normal text.
        [SerializeField]
        private Font m_Font;
        public static Font DefaultFont => Instance.m_Font;

        // The main font for keyword text.
        [SerializeField]
        private Font m_KeywordFont;
        public static Font KeywordFont => Instance.m_KeywordFont;

        // The main font for numerical text.
        [SerializeField]
        private Font m_NumericalFont;
        public static Font NumericalFont => Instance.m_NumericalFont;

        // The main font for numerical text.
        [SerializeField]
        private Font m_DetailFont;
        public static Font DetailFont => Instance.m_DetailFont;

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

        public static void Set(Text textbox, Font font, Color fontColor, int fontSize) {
            textbox.font = font;
            textbox.color = fontColor;
            textbox.ReduceUntilFitsHorizontally(fontSize);
        }

        #endregion
        

    }

}