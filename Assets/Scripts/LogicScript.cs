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
    private GameObject targetGunSourcePrefab;  //CubeGenerator
    [SerializeField]
    private GameObject targetPositionSourcePrefab;  //CubeGenerator
    [SerializeField]
    private GameObject platform;

    private GameObject _targetInstance;  //CubeGeneratorClone


    public Cube CubeScript;
    //public UI Ui;

    private Indicator indicatorScript;

    [SerializeField]
    private float minutes = 0;
    [SerializeField]
    private float seconds = 30;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int lengthPlatform = 50;
  
    public bool StartGame = false; //public потому что нужен в других скриптах
    [SerializeField]
    private bool clone = false;
    [SerializeField]
    private int GameMode = 2;

    private float _distanseAspect = 0;
    private float _distanseRotation = 0;
   
    private Vector3 _direction; // Вектор направления до цели

    public static LogicScript Logic => _logicScript;
    private static LogicScript _logicScript;



    private void Awake() {
        if (_logicScript == null) {
            _logicScript = this;
        } else {
            Destroy(this);
        }
    }


    private void Start() {
        SpawnCubeGeneator();
        UI.Ui.TimerPaint();
        UI.Ui.BestRresults();
        //      cubeScript = GameObject.FindGameObjectWithTag("Cube").GetComponent<cubeScript>();
        indicatorScript = Indicator.IndicatorScript;
    }

    private void Update() {
        platform.transform.localScale = new Vector3(lengthPlatform, 1, lengthPlatform);
        platform.transform.position = new Vector3(0, -1, 0);
        if (StartGame) {
            UI.Ui.TimerTick();
        }
    }


    public void ScorePlus(int plus) {
        score += plus;
        // Debug.Log(score);
        UI.Ui.SetScore(score);
    }


    public void SpawnCubeGeneator() {

        if (GameMode == 1) {
            _targetInstance = Instantiate(targetPositionSourcePrefab, new Vector3(Random.Range(lengthPlatform / 2, -lengthPlatform / 2), 0, Random.Range(lengthPlatform / 2, -lengthPlatform / 2)), transform.rotation);
        }
        if (GameMode == 2) {
            _targetInstance = Instantiate(targetGunSourcePrefab, new Vector3(Random.Range(lengthPlatform / 2, -lengthPlatform / 2), 0, Random.Range(lengthPlatform / 2, -lengthPlatform / 2)), transform.rotation);
        }
        if (GameMode == 3) {
            int random = Random.Range(0, 100);
            if (random % 2 == 0) {
                _targetInstance = Instantiate(targetPositionSourcePrefab, new Vector3(Random.Range(lengthPlatform / 2, -lengthPlatform / 2), 0, Random.Range(lengthPlatform / 2, -lengthPlatform / 2)), transform.rotation);
            } else {
                _targetInstance = Instantiate(targetGunSourcePrefab, new Vector3(Random.Range(lengthPlatform / 2, -lengthPlatform / 2), 0, Random.Range(lengthPlatform / 2, -lengthPlatform / 2)), transform.rotation);
            }
        }

    }

    public void GameOver() {
        StartGame = false;
        UI.Ui.GameOver(score);
        CubeScript.Can = StartGame;

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Started();
    }

    public void Started() {
        score = 0;
        UI.Ui.SetScore(score);
        UI.Ui.StartedGame(minutes * 60 + seconds);
        StartGame = true;
        CubeScript.Can = StartGame;
        // cubeScript.startPosition();
        //    TimerTick();
        ChangingColor();
    }

    public void ChangingColor() {
        float sideLength = lengthPlatform;
        float maxDestance = sideLength * sideLength + sideLength * sideLength;
        float destanceForGoal = Vector3.Distance(cube.transform.position, _targetInstance.transform.position);
        destanceForGoal *= destanceForGoal;

        Vector3 differencePosition = _targetInstance.transform.position - cube.transform.position;
        _direction = differencePosition;
        differencePosition.Normalize();
        Vector3 forwardCube = cube.transform.forward;  //--тут была ошибка + Cube.transform.position
        forwardCube.Normalize();
        float angleBeetweenZeroAndGoalFloat = Vector3D.AngleBetweenVectorsDegrees(Vector3D.ConversionVector3InVector3D(forwardCube),
                                             Vector3D.ConversionVector3InVector3D(differencePosition));

        _distanseAspect = Interpolation.Remap3D(0, maxDestance, 0, 1, destanceForGoal);
        _distanseRotation = Interpolation.Remap3D(0, 180, 0, 1, angleBeetweenZeroAndGoalFloat);

        indicatorScript.ChangingColorDistance(_distanseAspect, _distanseRotation);
    }
    
    private void OnDrawGizmosSelected() {
        try {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(cube.transform.position, (cube.transform.forward * 3 + cube.transform.position));

            Gizmos.color = Color.green;
            //Gizmos.DrawLine(Cube.transform.position, CubeGeneratorClone.transform.position);
            Gizmos.DrawLine(Vector3.zero, _targetInstance.transform.position - cube.transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, cube.transform.forward * 3);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_targetInstance.transform.position, (cube.transform.forward * 3 + cube.transform.position));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(cube.transform.position, (_direction + cube.transform.position));
        } catch (Exception e) {

        }
    }




}
