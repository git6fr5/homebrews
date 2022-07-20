/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    ///<summary>
    ///
    ///<summary>
    public class UIComponent : MonoBehaviour {

        public static Vector2 MousePosition => Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

        void Awake() {
            gameObject.layer = LayerMask.NameToLayer("UI");
        }

    }
}