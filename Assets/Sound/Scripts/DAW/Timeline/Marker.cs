/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.UI;

namespace Monet.UI {

    public class Marker : MonoBehaviour {

        [SerializeField] protected float m_CachedTime;
        public float CachedTime => m_CachedTime;

        public void SetTime(float time, float spacing) {
            m_CachedTime = time;
            Vector3 position = transform.localPosition;
            position.x = time * spacing;
            transform.localPosition = position; 
        }

        public virtual Marker Duplicate() {
            GameObject newObject = Instantiate(gameObject, transform.position, transform.localRotation, transform.parent);
            return newObject.GetComponent<Marker>();
        }

        public virtual void SetEditing(bool editing) {
            if (GetComponent<Button>() != null) {
                GetComponent<Button>().enabled = editing;
            };
        }

    }

}