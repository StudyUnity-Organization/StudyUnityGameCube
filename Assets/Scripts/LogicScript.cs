using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomMath;
using System;
using static UnityEngine.GraphicsBuffer;
using TreeEditor;
using Random = UnityEngine.Random;

public class LogicScript : MonoBehaviour {
    // Start is called before the first frame update
    /// <summary>
    //TODO добавить Cохранение данных в файл. данные - это рекорд время/Cчет
    //TODO добавить в UI поCле проигрыша Cчет и время + меCто? Ник?
    /// </summary>

    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject targetSourcePrefab;  //CubeGenerator
    [SerializeField]
    private GameObject platform;

    private GameObject targetInstance;  //CubeGeneratorClone


    public CubeScript CubeScript;
    public UIScript UiScript;

    private Indicator indicatorScript;

    [SerializeField]
    private float minutes = 0;
    [SerializeField]
    private float seconds = 30;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int lengthPlatform = 50;

    public bool StartGame = false;
    [SerializeField]
    private bool clone = false;

    private float _distanseAspect = 0;
    private float _distanseRotation = 0;

    [SerializeField]
    private Vector3 direction; // Вектор направления до цели

    private void Start() {
        SpawnCubeGeneator();
        UiScript.TimerPaint();
        UiScript.BestRresults();
        //      cubeScript = GameObject.FindGameObjectWithTag("Cube").GetComponent<cubeScript>();
        indicatorScript = Indicator.IndicatorScript;
    }

    private void Update() {
        platform.transform.localScale = new Vector3(lengthPlatform, 1, lengthPlatform);
        platform.transform.position = new Vector3(0, -1, 0);
        if (StartGame) {
            UiScript.TimerTick();
        }
    }


    public void ScorePlus(int plus) {
        score += plus;
        // Debug.Log(score);
        UiScript.SetScore(score);
    }


    public void SpawnCubeGeneator() {

        targetInstance = Instantiate(targetSourcePrefab, new Vector3(Random.Range(lengthPlatform / 2, -lengthPlatform / 2), 0, Random.Range(lengthPlatform / 2, -lengthPlatform / 2)), transform.rotation);
    }

    public void GameOver() {
        StartGame = false;
        UiScript.GameOver(score);
        CubeScript.Can = StartGame;

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Started();
    }

    public void Started() {
        score = 0;
        UiScript.SetScore(score);
        UiScript.StartedGame(minutes * 60 + seconds);
        StartGame = true;
        CubeScript.Can = StartGame;
        // cubeScript.startPosition();
        //    TimerTick();
        ChangingColor();
    }

    public void ChangingColor() {
        float sideLength = lengthPlatform;
        float maxDestance = sideLength * sideLength + sideLength * sideLength;
        float destanceForGoal = Vector3.Distance(cube.transform.position, targetInstance.transform.position);
        destanceForGoal *= destanceForGoal;

        Vector3 differencePosition = targetInstance.transform.position - cube.transform.position;
        direction = differencePosition;
        differencePosition.Normalize();
        Vector3 forwardCube = cube.transform.forward;  //--тут была ошибка + Cube.transform.position
        forwardCube.Normalize();
        float angleBeetweenZeroAndGoalFloat = Vector3D.AngleBetweenVectorsDegrees(Vector3D.ConversionVector3InVector3D(forwardCube),
                                             Vector3D.ConversionVector3InVector3D(differencePosition));

        _distanseAspect = Interpolation.Remap3D(0, maxDestance, 0, 1, destanceForGoal);
        _distanseRotation = Interpolation.Remap3D(0, 180, 0, 1, angleBeetweenZeroAndGoalFloat);

        indicatorScript.ChangingColorDistance(_distanseAspect, _distanseRotation);
    }

    private void OnDrawGizmos() {
        try {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(cube.transform.position, (cube.transform.forward * 3 + cube.transform.position));

            Gizmos.color = Color.green;
            //Gizmos.DrawLine(Cube.transform.position, CubeGeneratorClone.transform.position);
            Gizmos.DrawLine(Vector3.zero, targetInstance.transform.position - cube.transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, cube.transform.forward * 3);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(targetInstance.transform.position, (cube.transform.forward * 3 + cube.transform.position));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(cube.transform.position, (direction + cube.transform.position));
        } catch (Exception e) {

        }
    }




}
