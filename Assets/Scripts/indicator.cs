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

    private Color colorButtonDistance;
    private Color colorButtonRot;

    private Renderer renderer—ircle;
    private Color colorObject;

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

    void Start()
    {
        colorButtonDistance = buttonDistance.GetComponent<Image>().color;
        colorButtonRot = buttonRot.GetComponent<Image>().color;
        //   renderer—ircle = button.GetComponent<Renderer>();
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



        //var colors = button.GetComponent<Button>().colors;
        //colors.normalColor = Color.Lerp(colorHot, colorCold, distance);
        //colors.highlightedColor = Color.Lerp(colorHot, colorCold, distance);
        //colors.pressedColor = Color.Lerp(colorHot, colorCold, distance);
        //colors.selectedColor = Color.Lerp(colorHot, colorCold, distance);
        //colors.disabledColor = Color.Lerp(colorHot, colorCold, distance);
        //button.GetComponent<Button>().colors = colors;


        //button.GetComponent<Image>().color = color;
    }


}
