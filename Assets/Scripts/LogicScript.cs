using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour {
    // Start is called before the first frame update
    /// <summary>
    //TODO добавить сохранение данных в файл. данные - это рекорд время/счет
    //TODO добавить в UI после проигрыша счет и время + место? Ник?
    /// </summary>


    public GameObject Cube;
    public GameObject CubeGenerator;

    public cubeScript cubeScript;
    public UIScript UIScript;

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


    private void Start() {
        spawnCubeGeneator();
        UIScript.TimerPaint();
        UIScript.BestRresults();
        cubeScript = GameObject.FindGameObjectWithTag("Cube").GetComponent<cubeScript>();
    }

    private void Update() {
        if (start) {
            UIScript.TimerTick();
        }
    }


    public void scorePlus(int plus) {
        score += plus;
        // Debug.Log(score);
        UIScript.setScore(score);
    }


    public void spawnCubeGeneator() {
        float randomX = transform.position.x;
        float randomY = transform.position.y;
        float randomZ = transform.position.z;

        Instantiate(CubeGenerator, new Vector3(UnityEngine.Random.Range(randomX_Highest, randomX_Lowest), 0, UnityEngine.Random.Range(randomZ_Highest, randomZ_Lowest)), transform.rotation);

    }

    public void GameOver() {
        start = false;
        UIScript.GameOver(score);
        cubeScript.can = start;

    }

    public void restartGame() {
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

    }



}
