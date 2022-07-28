/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonsNDragons;

namespace DungeonsNDragons {

    ///<summary>
    ///
    ///<summary>
    [ExecuteInEditMode]
    public class Card : MonoBehaviour {

        // Font.
        [SerializeField] private Font m_AlphabetFont;
        [SerializeField] private Font m_NumericFont;

        // Dimensions.

        public void Title(string name) {
            Text textbox = Textbox("Title");
        }

        public static Text Textbox(string objName) {
            GameObject textObject = new GameObject(objName, typeof(Text));
        }

    }
}