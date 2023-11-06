using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    //TODO добавить сохранение данных в файл. данные - это рекорд время/счет
    //TODO добавить в UI после проигрыша счет и время + место? Ник?
    /// </summary>


    public GameObject Cube;
    public GameObject CubeGenerator;
    public GameObject GameOverTEXT;
    public GameObject GameOverButton;
    public GameObject StartButton;
    public cubeScript cubeScript;


    public Text textScore;
    public Text textTimer;
    public Text textResult;
    public Text textBestRresults;


    public ScoreTopResult scoreTopResult;

    public float minutes = 0;
    public float seconds = 30;


    public int score = 0;

    public int randomX_Highest = 25;
    public int randomX_Lowest = -25;
    public int randomZ_Highest = 25;
    public int randomZ_Lowest = -25;

    public bool start = false;
    private float times_second = 0;

    // Color32 colorText = new Color(109f, 117f, 131f);
    public float red = 0.1960784f;
    public float green = 0.1960784f;
    public float blue = 0.1960784f;

    [SerializeField]
    private Color colorText = new Color(0.1960784f, 0.1960784f, 0.1960784f); //??? почему если присваивать переменные с цветом цвет - черный - строка 137
                                                                             // Color32 colorText;  //??? почему если присваивать переменные с цветом цвет - черный - строка 137


    private void Start()    {
        spawnCubeGeneator();
        TimerPaint();
        BestRresults();
        cubeScript = GameObject.FindGameObjectWithTag("Cube").GetComponent<cubeScript>();
    }

    private void Update()     {
        if (start)        {
            TimerTick();
        }
    }


    public void scorePlus(int plus)
    {
        score += plus;
        // Debug.Log(score);
        textScore.text = score.ToString();
    }


    public void spawnCubeGeneator()
    {
        float randomX = transform.position.x;
        float randomY = transform.position.y;
        float randomZ = transform.position.z;

        Instantiate(CubeGenerator, new Vector3(UnityEngine.Random.Range(randomX_Highest, randomX_Lowest), 0, UnityEngine.Random.Range(randomZ_Highest, randomZ_Lowest)), transform.rotation);

    }

    public void GameOver()
    {
        start = false;
        GameOverTEXT.SetActive(true);
        GameOverButton.SetActive(true);
        textResult.gameObject.SetActive(true);
        cubeScript.can = start;
        writeTopResultFromFiles();
        textResult.text = "Your result:\n " + score + " points";


    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Started();
    }

    public void Started()
    {
        times_second = minutes * 60 + seconds;
        start = true;
        StartButton.SetActive(false);
        cubeScript.can = start;
        //    TimerTick();

    }

    public void TimerTick()
    {
        times_second = times_second - 1 * Time.deltaTime;
        TimerPaint();
        if (times_second <= 0 && start)
        {
            GameOver();
        }
    }





    public void TimerPaint()
    {
        textTimer.text = ((int)(times_second / 60)).ToString() + ':' + ((int)(times_second % 60)).ToString();
        // Debug.Log(times_second + "____   " + ((int)(times_second / 60)).ToString() + ':' + ((int)(times_second % 60)).ToString());

        if (times_second % 60 < 10)
        {
            textTimer.text = ((int)(times_second / 60)).ToString() + ":0" + ((int)(times_second % 60)).ToString();
        }


        if (times_second <= 5f)
        {
            textTimer.color = Color.red;
        }
        else
        {
            textTimer.color = colorText;
            //colorText = new Color(red, green, blue);
            // Debug.Log("text color " + red + "  " + green + "  " + blue); // почему если присваивать переменные с цветом цвет - черный 
        }

    }

    public void BestRresults()
    {
        readTopResultFromFiles();
        string result = "Best results:\n";
        for (int i = 0; i < scoreTopResult.number.Length; i++)
        {
            if (scoreTopResult.time[i] < 10)
            {
                result += scoreTopResult.number[i] + ". " + scoreTopResult.scoreResult[i] + "   (" + (int)(scoreTopResult.time[i] / 60f) + ":0" + (int)(scoreTopResult.time[i] % 60f) + ")\n";

            }
            else
            {
                result += scoreTopResult.number[i] + ". " + scoreTopResult.scoreResult[i] + "   (" + (int)(scoreTopResult.time[i] / 60f) + ':' + (int)(scoreTopResult.time[i] % 60f) + ")\n";

            }

        }
        //  Debug.logAssertion("result_refresh: " + result);

        textBestRresults.text = result;


    }

    public void readTopResultFromFiles()
    {
        scoreTopResult = JsonUtility.FromJson<ScoreTopResult>(File.ReadAllText(Application.streamingAssetsPath + "/JSON_Result.json"));
    }
    public void writeTopResultFromFiles()
    {
        searchBestResult();
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_Result.json", JsonUtility.ToJson(scoreTopResult));
        BestRresults();
    }
    public void searchBestResult()
    {
        int score_result = score;
        int score_temporary = 0;
        float time_temporary;
        float time_temporary2 = minutes * 60f + seconds;

        for (int i = 0; i < scoreTopResult.scoreResult.Length; i++)
        {
            if (scoreTopResult.scoreResult[i] == score_result) { break; }
            if (scoreTopResult.scoreResult[i] > score_result) { continue; }
            if (scoreTopResult.scoreResult[i] < score_result)
            {
                score_temporary = scoreTopResult.scoreResult[i];
                time_temporary = scoreTopResult.time[i];
                scoreTopResult.scoreResult[i] = score_result;
                scoreTopResult.time[i] = time_temporary2;
                score_result = score_temporary;
                time_temporary2 = time_temporary;
                //  Debug.log("score: " + score);
                //  Debug.log("THE_BEST_res: " + scoreTopResult.scoreResult[i]);
                //  Debug.log("TIME: " + time_temporary2);
            }

        }
    }


    public class ScoreTopResult
    {
        public int[] number = new int[5];
        public int[] scoreResult = new int[5];
        public float[] time = new float[5];


    }

    public void removeData()
    {
        for (int i = 0; i < scoreTopResult.scoreResult.Length; i++)
        {
            scoreTopResult.scoreResult[i] = 0;
            scoreTopResult.time[i] = 0;
        }
        writeTopResultFromFiles();

    }


}
