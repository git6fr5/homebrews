/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Warcaster;

namespace Warcaster {

    ///<summary>
    /// Defines the overarching behavior for the card text.
    ///<summary>
    [ExecuteInEditMode]
    public class TextController : MonoBehaviour {

        #region Data Structures

        #endregion

        #region Fields

        // Instantiating the singleton.
        public static TextController Instance;

        // The font size distribution.
        public static int LargeText = 60;
        public static int MediumText = 50;
        public static int SmallText = 40;
        public static int VerySmallText = 25;
        public static int VeryVerySmallText = 15;

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

        public static void Set(Text textbox, Font font, Color fontColor, int fontSize) {
            textbox.font = font;
            textbox.color = fontColor;
            textbox.fontSize = fontSize;
        }

        #endregion
        

    }

}