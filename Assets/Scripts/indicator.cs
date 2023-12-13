using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]
    private Color colorHot;
    [SerializeField]
    private Color colorCold;
    [SerializeField]
    private Color colorDistance;
    [SerializeField]
    private Color colorRot;
    [SerializeField]
    private Image buttonDistanceImage;
    [SerializeField]
    private Image buttonRotImage;


    public static Indicator IndicatorScript => indicatorScript;
    private static Indicator indicatorScript;



    private void Awake() {
        if (indicatorScript == null) {
            indicatorScript = this;
        } else {
            Destroy(this);
        }

    }

    public void ChangingColorDistance(float distance, float rotation) {
        colorDistance = Color.Lerp(colorHot, colorCold, distance);
        buttonDistanceImage.color = colorDistance;
        colorRot = Color.Lerp(colorHot, colorCold, rotation);
        buttonRotImage.color = colorRot;
    }

}
