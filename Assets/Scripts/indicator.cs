using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
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
    private GameObject buttonDistance;
    [SerializeField]
    private GameObject buttonRot;


    public static Indicator IndicatorScript => indicatorScript;
    private static Indicator indicatorScript;
    


    private void Awake() {
        if (indicatorScript == null) {
            indicatorScript = this;
        }
        else {
            Destroy(this);
        }

    }


    // Update is called once per frame
    void Update()
    {
        //renderer—ircle.material.color = colorObject;
    }

    public void —hanging—olorDistance(float distance, float rotation) {
        colorDistance = Color.Lerp(colorHot, colorCold, distance);
        buttonDistance.GetComponent<Image>().color = colorDistance;
        colorRot = Color.Lerp(colorHot, colorCold, rotation);
        buttonRot.GetComponent<Image>().color = colorRot;
    }


}
