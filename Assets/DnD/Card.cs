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
    public class Card : MonoBehaviour {

        void Start() {
            Draw();
            CanvasScreenShot screenShot = GetComponent<CanvasScreenShot>();
            // screenShot.takeScreenShot(GetComponent<Canvas>(), SCREENSHOT_TYPE.IMAGE_ONLY, false);
        }

        public virtual void Draw() {
            Background();
            Title("Title");
        }

        public void Background() {
            gameObject.AddComponent<Canvas>();
            gameObject.AddComponent<CanvasScaler>();
            Edit(gameObject, new Vector2(0f, 0f), Settings.Size, 1f);

            GameObject borderObject = new GameObject("Border", typeof(Image));
            borderObject.transform.SetParent(transform);
            borderObject.GetComponent<Image>().color = Color.black;
            Edit(borderObject, new Vector2(0f, 0f), Settings.Size, 1f);

            GameObject backgroundObject = new GameObject("Background", typeof(Image));
            backgroundObject.transform.SetParent(transform);
            backgroundObject.GetComponent<Image>().color = Color.white;
            Edit(backgroundObject, new Vector2(0f, 0f), Settings.Background, 1f);
        }

        public Text Title(string title, float ratio = 0.1f) {
            Text textbox = Textbox("Title ", TextAnchor.MiddleLeft, transform);
            textbox.SetText(title, Settings.AlphabetFont, Settings.FontColor);
            
            // Right at the top.
            float height =  Settings.Workspace.y * ratio;
            float width = Settings.Workspace.x; 

            float y = Settings.VerticalBorderPosition - height / 2f;
            float x = 0f;

            textbox.transform.SetParent(transform);
            Edit(textbox.gameObject, new Vector2(x, y), new Vector2(width, height), 0.01f);
            textbox.DynamicFontSize(-5);
            return textbox;
        }
        
        public Text Property(string property, string value, RectTransform aboveRt, float widthRatio, float ratio) {
            // Edit the contents.
            Text propertybox = Textbox("Property " + property, TextAnchor.MiddleLeft, transform);
            propertybox.SetText(property, Settings.AlphabetFont, Settings.FontColor);

            // Edit the rect.
            propertybox.SetProportionalSize(ratio, widthRatio, 0.01f);
            propertybox.DynamicFontSize(-1);
            propertybox.PlaceUnder(aboveRt);
            propertybox.SnapToLeftMargin(0f);

            // Edit the contents.
            Text valuebox = Textbox("Property Value " + property, TextAnchor.MiddleLeft, transform);
            valuebox.SetText(value, Settings.NumericFont, Settings.FontColor);

            // Edit the rect.
            valuebox.SetProportionalSize(ratio, (1f-widthRatio), 0.01f);
            valuebox.SetSingleLineFontSize();
            valuebox.DynamicScaleVertical(Settings.Workspace.y * ratio, Settings.Workspace.y);
            valuebox.PlaceUnder(aboveRt);
            valuebox.PlaceAfter(propertybox.rectTransform);
            return valuebox;
        }

        public Text Description(string name, string description, RectTransform aboveRt, float ratio, float divide) {
            // Edit the contents.
            Text namebox = Textbox("Description " + name, TextAnchor.MiddleLeft, transform);
            namebox.SetText(name, Settings.AlphabetFont, Settings.FontColor);

            // Edit the rect.
            namebox.SetProportionalSize(ratio, 1f, 0.01f);
            namebox.DynamicFontVertical(-1);
            namebox.ShaveExcessHorizontal();
            namebox.PlaceUnder(aboveRt);
            namebox.SnapToLeftMargin(0f);

            // Edit the contents.
            Text descriptionbox = Textbox("Description Value " + name, TextAnchor.MiddleLeft, transform);
            descriptionbox.SetText(description, Settings.NumericFont, Settings.FontColor);
            descriptionbox.SetProportionalSize(ratio * 0.75f, 1f, 0.01f);
            descriptionbox.SetSingleLineFontSize(); // how big a font should be take up "ratio" space.
            descriptionbox.DynamicScaleVertical(Settings.Workspace.y * ratio * 0.75f, Settings.Workspace.y);
            descriptionbox.PlaceUnder(namebox.rectTransform);
            descriptionbox.SnapToLeftMargin(0f);

            return descriptionbox;
        }


        public Text StatLine(string stat, string value, RectTransform aboveRt, int index, int count,  float ratio = 0.05f, float spacing = 0.1f) {
            // Edit the contents.
            Text statbox = Textbox("Stat " + stat, TextAnchor.MiddleCenter, transform);
            statbox.SetText(stat, Settings.AlphabetFont, Settings.FontColor);

            // Edit the rect.
            float widthRatio = 1f / count - 2f * spacing; 
            statbox.SetProportionalSize(ratio, widthRatio, 0.01f);
            statbox.DynamicFontSize(-1);
            statbox.PlaceUnder(aboveRt);
            statbox.PlaceAlong(widthRatio, index, spacing);

            Text valuebox = Textbox("Stat Value " + stat, TextAnchor.MiddleCenter, transform);
            valuebox.SetText(value, Settings.NumericFont, Settings.FontColor);
            valuebox.SetProportionalSize(ratio, widthRatio, 0.01f);
            valuebox.SetSingleLineFontSize();
            valuebox.DynamicScaleVertical(Settings.Workspace.y * ratio, Settings.Workspace.y);
            valuebox.PlaceUnder(statbox.rectTransform);
            valuebox.PlaceAlong(widthRatio, index, spacing);
            
            return valuebox;
        }

        public static Text Textbox(string objName, TextAnchor anchor, Transform transform) {
            GameObject textObject = new GameObject(objName, typeof(Text));
            Text textbox = textObject.GetComponent<Text>();
            textbox.alignment = anchor;
            textbox.transform.SetParent(transform);
            return textbox;
        }

        public static void Edit(GameObject obj, Vector2 position, Vector2 size, float scalar) {
            RectTransform rt = obj.GetComponent<RectTransform>();
            if (rt != null) {
                rt.localScale = new Vector2(scalar, scalar);
                rt.sizeDelta = size / scalar;
                rt.position = position;
            }
        }

    }
}