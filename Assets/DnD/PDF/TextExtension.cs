using UnityEngine;
using UnityEngine.UI;
using DungeonsNDragons;

public static class TextExtension {

    public static void DynamicFontSize(this Text text, int reduction = -1) {
        int maximum = 300;
        int fontSize = 1;
        text.fontSize = fontSize;
        while (!text.IsOverflowing() && fontSize < maximum) {
            fontSize += 1;
            text.fontSize = fontSize;
        }
        text.fontSize = fontSize + reduction;
    }

    public static void DynamicFontVertical(this Text text, int reduction = -1) {
        int maximum = 300;
        int fontSize = 1;
        text.fontSize = fontSize;
        while (!text.IsOverflowingVertical() && fontSize < maximum) {
            fontSize += 1;
            text.fontSize = fontSize;
        }
        text.fontSize = fontSize + reduction;
        text.ShaveExcessHorizontal();
    }

    public static void ReduceUntilFitsVertically(this Text text, int fontSize) {
        int minimum = 1;
        text.fontSize = fontSize;
        while (text.IsOverflowingVertical() && fontSize > minimum) {
            fontSize -= 1;
            text.fontSize = fontSize;
        }
    }

    public static void DynamicScaleVertical(this Text text, float minHeight, float maxHeight) {
        RectTransform rt = text.rectTransform;

        // First attempt to adjust the size to the font.
        float preferredHeight = LayoutUtility.GetPreferredHeight(rt);
        float scale = text.rectTransform.localScale.y;
        Debug.Log(preferredHeight);
        Debug.Log(minHeight / scale);
        if (preferredHeight >= minHeight / scale && preferredHeight <= maxHeight / scale) {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, preferredHeight);
        }

        // Then attempt to adjust the font to the bounded sizes.
        if (preferredHeight > maxHeight / scale) {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, maxHeight / scale);
            text.DynamicFontVertical(-1);
        }
        else if (preferredHeight < minHeight / scale) {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, minHeight / scale);
            text.DynamicFontVertical(-1);
        }

    }

    public static void SetSingleLineFontSize(this Text text) {
        string contents = text.text;
        text.text = "Aa";
        text.DynamicFontSize(-1);
        text.text = contents;
    }

    public static void ShaveExcessHorizontal(this Text text) {
        RectTransform rt = text.GetComponent<RectTransform>();
        float preferredWidth = LayoutUtility.GetPreferredWidth(rt);
        rt.sizeDelta = new Vector2(preferredWidth, rt.sizeDelta.y);
    }

    public static bool IsOverflowing(this Text text) {
        return text.IsOverflowingVertical() || text.IsOverflowingHorizontal();
    }

    public static bool IsOverflowingVertical(this Text text) {
        RectTransform rt = text.GetComponent<RectTransform>();
        float preferredHeight = LayoutUtility.GetPreferredHeight(rt);
        // Debug.Log(preferredHeight);
        // Debug.Log(rt.rect.height * (1f / rt.localScale.y));
        // rt.rect.height * (1f / rt.localScale.y);
        return preferredHeight > rt.rect.height;
    }

    public static bool IsOverflowingHorizontal(this Text text) {
        RectTransform rt = text.GetComponent<RectTransform>();
        float preferredWidth = LayoutUtility.GetPreferredWidth(rt);
        return preferredWidth > rt.rect.width;
    }

    ///
    public static void SetText(this Text text, string contents, Font font, Color color) {
        text.text = contents;
        text.font = font;
        text.color = color;
    }

    public static void SetProportionalSize(this Text text, float heightRatio, float widthRatio, float scalar) {
        RectTransform rt = text.rectTransform;
        float height = Settings.Workspace.y * heightRatio;
        float width = Settings.Workspace.x * widthRatio;
        rt.localScale = new Vector2(scalar, scalar);
        rt.sizeDelta = new Vector2(width, height) / scalar;
    }

    public static void PlaceUnder(this Text text, RectTransform aboveRt) {
        RectTransform rt = text.rectTransform;
        float bottomborder = aboveRt.position.y - (aboveRt.localScale.y * aboveRt.sizeDelta.y) / 2f;
        float y = bottomborder - (rt.localScale.y * rt.sizeDelta.y) / 2f;
        rt.position = new Vector2(rt.position.x, y);
    }

    public static void PlaceAfter(this Text text, RectTransform beforeRt) {
        RectTransform rt = text.rectTransform;
        float rightborder = beforeRt.position.x + (beforeRt.localScale.x * beforeRt.sizeDelta.x) / 2f;
        float x = rightborder + (rt.localScale.x * rt.sizeDelta.x) / 2f;
        rt.position = new Vector2(x, rt.position.y);
    }

    public static void PlaceAlong(this Text text, float widthRatio, int index, float spacing) {
        RectTransform rt = text.rectTransform;
        float offset = Settings.Workspace.x / 2f;
        float realSpacing = Settings.Workspace.x * spacing;
        float width = Settings.Workspace.x * widthRatio;
        float perIndex = realSpacing + width + realSpacing;
        float prev = index * perIndex;
        float x = prev + realSpacing + width / 2f - offset;
        rt.position = new Vector2(x, rt.position.y);
    }

    public static void SnapToLeftMargin(this Text text, float offset) {
        RectTransform rt = text.rectTransform;
        float border = -Settings.HorizontalBorderPosition + (rt.localScale.x * rt.sizeDelta.x) / 2f;
        rt.position = new Vector2(border, rt.position.y);
    }

    public static float WidthRatio(this Text text) {
        RectTransform rt = text.rectTransform;
        return (rt.sizeDelta.x * rt.localScale.x) / Settings.Workspace.x;
    }

}