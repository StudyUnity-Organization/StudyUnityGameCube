using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject GameOverTEXT;
    public GameObject GameOverButton;
    public GameObject StartButton;

    public Text textScore;
    public Text textTimer;
    public Text textResult;
    public Text textBestRresults;


    [SerializeField]
    private Color colorText = new Color(0.1960784f, 0.1960784f, 0.1960784f); //505050
                                                                             // Start is called before the first frame update

    private int score = 0;
    private float timesTimerAll = 0;  //счетчик времени
    private float timesTimer = 0;  //переменная для хранения

    public LogicScript logic;


    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }


    public void Update() {

    }

    public ScoreTopResult scoreTopResult;

    public void TimerTick() {
        timesTimerAll = timesTimerAll - 1 * Time.deltaTime;
        TimerPaint();
        if (timesTimerAll <= 0 && logic.start) {
            logic.GameOver();
        }
    }

    public void setTimesTimerAll(float secondsAll) {
        timesTimerAll = secondsAll;
    }

    public void setScore(int score) {
        score = score;
        textScore.text = score.ToString();
    }





    public void TimerPaint() {
        textTimer.text = ((int)(timesTimerAll / 60)).ToString() + ':' + ((int)(timesTimerAll % 60)).ToString();
        // Debug.Log(timesTimerAll + "____   " + ((int)(timesTimerAll / 60)).ToString() + ':' + ((int)(timesTimerAll % 60)).ToString());

        if (timesTimerAll % 60 < 10) {
            textTimer.text = ((int)(timesTimerAll / 60)).ToString() + ":0" + ((int)(timesTimerAll % 60)).ToString();
        }


        if (timesTimerAll <= 5f) {
            textTimer.color = Color.red;
        }
        else {
            textTimer.color = colorText;
            //colorText = new Color(red, green, blue);
            // Debug.Log("text color " + red + "  " + green + "  " + blue); // почему если присваивать переменные с цветом цвет - черный 
        }

    }

    public void BestRresults() {
        readTopResultFromFiles();
        string result = "Best results:\n";
        for (int i = 0; i < scoreTopResult.number.Length; i++) {
            if (scoreTopResult.time[i] < 10) {
                result += scoreTopResult.number[i] + ". " + scoreTopResult.scoreResult[i] + "   (" + (int)(scoreTopResult.time[i] / 60f) + ":0" + (int)(scoreTopResult.time[i] % 60f) + ")\n";
            }
            else {
                result += scoreTopResult.number[i] + ". " + scoreTopResult.scoreResult[i] + "   (" + (int)(scoreTopResult.time[i] / 60f) + ':' + (int)(scoreTopResult.time[i] % 60f) + ")\n";
            }
        }
        //  Debug.logAssertion("result_refresh: " + result);

        textBestRresults.text = result;
    }

    public void readTopResultFromFiles() {
        scoreTopResult = JsonUtility.FromJson<ScoreTopResult>(File.ReadAllText(Application.streamingAssetsPath + "/JSON_Result.json"));
    }
    public void writeTopResultFromFiles() {
        searchBestResult();
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_Result.json", JsonUtility.ToJson(scoreTopResult));
        BestRresults();
    }
    public void searchBestResult() {
        int score_result = score;
        int score_temporary = 0;
        float time_temporary;
        float time_temporary2 = timesTimer;

        for (int i = 0; i < scoreTopResult.scoreResult.Length; i++) {
            if (scoreTopResult.scoreResult[i] == score_result) { break; }
            if (scoreTopResult.scoreResult[i] > score_result) { continue; }
            if (scoreTopResult.scoreResult[i] < score_result) {
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


    public class ScoreTopResult {
        public int[] number = new int[5];
        public int[] scoreResult = new int[5];
        public float[] time = new float[5];
    }

    public void removeData() {
        for (int i = 0; i < scoreTopResult.scoreResult.Length; i++) {
            scoreTopResult.scoreResult[i] = 0;
            scoreTopResult.time[i] = 0;
        }
        writeTopResultFromFiles();

    }

    public void GameOver(int scoreWin) {
        score = scoreWin;
        GameOverTEXT.SetActive(true);
        GameOverButton.SetActive(true);
        textResult.gameObject.SetActive(true);
        writeTopResultFromFiles();
        textResult.text = "Your result:\n " + score + " points";


    }

    public void StartedGame(float secondsAll) {
        timesTimer = secondsAll;
        setTimesTimerAll(secondsAll);
        StartButton.SetActive(false);
        GameOverTEXT.SetActive(false);
        GameOverButton.SetActive(false);
        textResult.gameObject.SetActive(false);
        //    TimerTick();

    }
}
