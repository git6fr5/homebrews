/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warcaster.Cards;
using Warcaster.Collections;

namespace Warcaster.Collections {

    ///<summary>
    ///
    ///<summary>
    [ExecuteInEditMode]
    public class Collection : MonoBehaviour {

        [SerializeField] 
        private Card m_CardBase;

        [SerializeField] 
        private float m_HorizontalSpacing = 1f;

        [SerializeField] 
        private float m_VerticalSpacing = 1f;

        [SerializeField] 
        private float m_CardScale = 1f;

        [SerializeField] 
        private int m_Columns = 4;

        [System.Serializable]
        public class CardData {

            [SerializeField, Range(0, 20)] 
            private int m_ID;

            [SerializeField, ReadOnly] 
            private bool m_Created = false;
            public bool Created => m_Created;

            [SerializeField, ReadOnly] 
            private string m_CardName;
            public string CardName => m_CardName;

            [SerializeField, ReadOnly] 
            private Card m_Card;
            public Card Card => m_Card;

            public void Create(Card cardBase) {
                m_Card = Instantiate(cardBase).GetComponent<Card>();
            }

            public void Refresh(int index, int columns, float scale, float horSpacing, float vertSpacing) {
                CardDataReader.GetCard(m_Card, m_ID);
                float x = horSpacing * (index % columns);
                float y = vertSpacing * Mathf.Floor(index / columns);
                float z = -index * 0.01f;
                m_Card.transform.localPosition = new Vector3(x, y, z);
                m_Card.transform.localScale = new Vector3(scale, scale, 1f);
                m_CardName = m_Card.Name;
            }

        }

        [SerializeField] 
        private List<CardData> m_Cards = new List<CardData>();

        void Update() {

            for (int i = 0; i < m_Cards.Count; i++) {
                if (m_Cards[i].Card == null) {
                    m_Cards[i].Create(m_CardBase);
                }
                m_Cards[i].Refresh(i, m_Columns, m_CardScale, m_HorizontalSpacing, m_VerticalSpacing);
            }

            Card[] cardObjects = (Card[])GameObject.FindObjectsOfType(typeof(Card));
            for (int i = 0; i < cardObjects.Length; i++) {
                CardData dataPoint = m_Cards.Find(point => point.Card == cardObjects[i]);
                if (dataPoint == null && cardObjects[i] != m_CardBase) {
                    DestroyImmediate(cardObjects[i].gameObject);
                }
            }

        }

    }
}