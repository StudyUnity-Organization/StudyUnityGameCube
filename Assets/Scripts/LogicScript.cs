using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using CustomMath;
using System;
using static UnityEngine.GraphicsBuffer;
using TreeEditor;

public class LogicScript : MonoBehaviour {
    // Start is called before the first frame update
    /// <summary>
    //TODO добавить сохранение данных в файл. данные - это рекорд время/счет
    //TODO добавить в UI после проигрыша счет и время + место? Ник?
    /// </summary>


    public GameObject Cube;
    public GameObject CubeGenerator;
    private GameObject CubeGeneratorClone;

    public cubeScript cubeScript;
    public UIScript UIScript;
    
    private Indicator indicatorScript;

    [SerializeField]
    private float minutes = 0;
    [SerializeField]
    private float seconds = 30;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int randomX_Highest = 25;
    [SerializeField]
    private int randomX_Lowest = -25;
    [SerializeField]
    private int randomZ_Highest = 25;
    [SerializeField]
    private int randomZ_Lowest = -25;


    public bool start = false;
    [SerializeField]
    private bool clone = false;   

    private float kDistance = 0;    
    private float kRotation = 0;
    [SerializeField]
    private float angleBeetwenCubeAndCubeClone = 0;

    public Vector3 direction; // Вектор направления до цели

    private void Start() {
        SpawnCubeGeneator();
        UIScript.TimerPaint();
        UIScript.BestRresults();
        //      cubeScript = GameObject.FindGameObjectWithTag("Cube").GetComponent<cubeScript>();
        indicatorScript = Indicator.IndicatorScript;
    }

    private void Update() {
        if (start) {
            UIScript.TimerTick();
        }
    }


    public void ScorePlus(int plus) {
        score += plus;
        // Debug.Log(score);
        UIScript.setScore(score);
    }


    public void SpawnCubeGeneator() {

        CubeGeneratorClone = Instantiate(CubeGenerator, new Vector3(UnityEngine.Random.Range(randomX_Highest, randomX_Lowest), 0, UnityEngine.Random.Range(randomZ_Highest, randomZ_Lowest)), transform.rotation);
    }

    public void GameOver() {
        start = false;
        UIScript.GameOver(score);
        cubeScript.can = start;

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Started();
    }

    public void Started() {
        score = 0;
        UIScript.setScore(score);
        UIScript.StartedGame(minutes * 60 + seconds);
        start = true;
        cubeScript.can = start;
        // cubeScript.startPosition();
        //    TimerTick();
        СhangingСolor();
    }

    public void СhangingСolor() {
        float sideLength = randomX_Highest * 2;
        float maxDestance = sideLength * sideLength + sideLength * sideLength;       
        float destanceForGoal = Vector3.Distance(Cube.transform.position, CubeGeneratorClone.transform.position);
        destanceForGoal *= destanceForGoal;     

        Vector3 differencePosition = CubeGeneratorClone.transform.position - Cube.transform.position;
        direction = differencePosition;
        differencePosition.Normalize();        
        Vector3 forwardCube = Cube.transform.forward;  //--тут была ошибка + Cube.transform.position
        forwardCube.Normalize();
        float angleBeetweenZeroAndGoalFloat = Vector3D.AngleBetweenVectorsDegrees(Vector3D.ConversionVector3InVector3D(forwardCube),
                                             Vector3D.ConversionVector3InVector3D(differencePosition)); 

        kDistance = Interpolation.Remap3D(0, maxDestance, 0, 1, destanceForGoal);
        kRotation = Interpolation.Remap3D(0, 180, 0, 1, angleBeetweenZeroAndGoalFloat);

        indicatorScript.СhangingСolorDistance(kDistance, kRotation);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;       
        Gizmos.DrawLine(Cube.transform.position, (Cube.transform.forward * 3 + Cube.transform.position));
       
        Gizmos.color = Color.green;
        //Gizmos.DrawLine(Cube.transform.position, CubeGeneratorClone.transform.position);
        Gizmos.DrawLine(Vector3.zero, CubeGeneratorClone.transform.position - Cube.transform.position);
       
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, Cube.transform.forward * 3);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(CubeGeneratorClone.transform.position, (Cube.transform.forward * 3 + Cube.transform.position));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Cube.transform.position, (direction + Cube.transform.position));
    }




}
