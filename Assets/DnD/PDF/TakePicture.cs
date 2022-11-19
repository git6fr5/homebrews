/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonsNDragons;

public class TakePicture : MonoBehaviour {

    public Canvas canvasToSreenShot;

    // Use this for initialization
    void Start() {
        //Subscribe
        CanvasScreenShot.OnPictureTaken += receivePNGScreenShot;
        CanvasScreenShot screenShot = GetComponent<CanvasScreenShot>();

        screenShot.takeScreenShot(canvasToSreenShot, SCREENSHOT_TYPE.IMAGE_ONLY, false);
        //take ScreenShot(Text only)
        // screenShot.takeScreenShot(canvasToSreenShot, SCREENSHOT_TYPE.TEXT_ONLY, false);

    }

    public void OnEnable() {
        //Un-Subscribe
        CanvasScreenShot.OnPictureTaken -= receivePNGScreenShot;
    }

    void receivePNGScreenShot(byte[] pngArray) {
        Debug.Log("Picture taken");

        //Do Something With the Image (Save)
        string path = Application.persistentDataPath + "/CanvasScreenShot.png";
        System.IO.File.WriteAllBytes(path, pngArray);
        Debug.Log(path);
    }

}
