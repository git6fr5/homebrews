/* --- Libraries --- */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warcaster.Cards;

namespace Warcaster.Cards {

    ///<summary>
    ///
    ///<summary>
    public class CardDataReader {

        public static string DefaultFilePath = "/Resources/Cards/Cards_Celestials.csv";
        public static string DefaultSplashArtPath = "SplashArt/";

        public static void GetCard(Card card, int id) {
            string fileData = System.IO.File.ReadAllText(Application.dataPath + DefaultFilePath);
            string[] lines = fileData.Split("\n");

            int index = Get(id, lines);
            string[] headerData  = (lines[0].Trim()).Split(","[0]);
            string[] data  = (lines[index].Trim()).Split(","[0]);

            card.Name = Get("Name", data, headerData);
            card.CardArchetype = Get("Archetype", data, headerData);
            card.CardSummoningType = Get("SummoningType", data, headerData);
            card.SummoningCost = Get("SummoningCost", data, headerData);

            card.CardEffectTrigger = Get("EffectTrigger", data, headerData);
            card.CardEffect = Get("Effect", data, headerData);
            card.CardEffectTarget = Get("EffectTarget", data, headerData);
            card.EffectTargetCount = Get("EffectTargetCount", data, headerData);

            card.AttackPower = Get("AttackPower", data, headerData);
            card.CardAttackType = Get("AttackType", data, headerData);
            card.AttackCount = Get("AttackCount", data, headerData);

            card.HealthPoint = Get("HealthPoints", data, headerData);
            card.CardProtectionType = Get("ProtectionType", data, headerData);
            card.ProtectionCount = Get("ProtectionCount", data, headerData);
            
            card.SplashArt = Resources.Load<Sprite>(DefaultSplashArtPath + Get("SplashArt", data, headerData));
        }

        public static int Get(int id, string[] lines) {
            for (int i = 0; i < lines.Length; i++) {
                string[] data  = (lines[i].Trim()).Split(","[0]);
                if (data[0] == id.ToString()) {
                    return i;
                }
            }
            return 1;
        }

        public static string Get(string field, string[] data, string[] headerData) {
            for (int i = 0; i < headerData.Length; i++) {
                if (headerData[i] == field) {
                    return data[i];
                }
            }
            return "N/A";
        }

    }

}