/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monet;
using Monet.UI;

namespace Monet.UI {

    ///<summary>
    ///
    ///<summary>
    public class FilenameInput : MonoBehaviour {
        
        public Synth m_Synth;
        public InputField m_InputField;

        void Update() {
            m_Synth.SetFilename(m_InputField.text);
        }

    }
    
}