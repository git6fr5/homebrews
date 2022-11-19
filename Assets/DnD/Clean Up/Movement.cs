/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonsNDragons;

namespace DungeonsNDragons {

    [System.Serializable]
    public class Movement {

        [SerializeField] private int m_Speed;
        [SerializeField] private int m_BurrowSpeed;
        [SerializeField] private int m_ClimbSpeed;
        [SerializeField] private int m_FlySpeed;
        [SerializeField] private int m_SwimSpeed;

        public string ToTag() {
            string temp = "";
            temp = ToString(temp, m_Speed, "");
            temp = ToString(temp, m_BurrowSpeed, " Burrowing");
            temp = ToString(temp, m_ClimbSpeed, " Climbing");
            temp = ToString(temp, m_FlySpeed, " Flying");
            temp = ToString(temp, m_SwimSpeed, " Swimming");
            return temp;
        }

        private static string ToString(string tag, int speed, string type) {
            if (speed != 0) { 
                string temp = speed.ToString() + "ft" + type;
                if (tag != "") {
                    return tag + ", " + temp;
                }
                return temp;
            }
            return tag;
        }

    }

}