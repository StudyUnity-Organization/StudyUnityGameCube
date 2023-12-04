using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject GameOverTEXT;
    public GameObject GameOverButton;
    public GameObject StartButton;

    public Text TextScore;
    public Text TextTimer;
    public Text TextResult;
    public Text TextBestRresults;


    [SerializeField]
    private Color colorText = new Color(0.1960784f, 0.1960784f, 0.1960784f); //505050
                                                                             // Start is called before the first frame update

    private int score = 0;
    private float timesTimerAll = 0;  //счетчик времени
    private float timesTimer = 0;  //переменная для хранения

    public LogicScript Logic;


    void Start() {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    public ScoreTopResult scoreTopResult;

    public void TimerTick() {
        timesTimerAll = timesTimerAll - 1 * Time.deltaTime;
        TimerPaint();
        if (timesTimerAll <= 0 && Logic.StartGame) {
            Logic.GameOver();
        }
    }

    public void SetTimesTimerAll(float secondsAll) {
        timesTimerAll = secondsAll;
    }

    public void SetScore(int scoreSet) {
        score = scoreSet;
        TextScore.text = score.ToString();
    }





    public void TimerPaint() {
        TextTimer.text = ((int)(timesTimerAll / 60)).ToString() + ':' + ((int)(timesTimerAll % 60)).ToString(); 
        // Debug.Log(timesTimerAll + "____   " + ((int)(timesTimerAll / 60)).ToString() + ':' + ((int)(timesTimerAll % 60)).ToString());

        if (timesTimerAll % 60 < 10) {
            TextTimer.text = ((int)(timesTimerAll / 60)).ToString() + ":0" + ((int)(timesTimerAll % 60)).ToString();
        }


        if (timesTimerAll <= 5f) {
            TextTimer.color = Color.red;
        }
        else {
            TextTimer.color = colorText;
            //colorText = new Color(red, green, blue);
            // Debug.Log("text color " + red + "  " + green + "  " + blue); // почему если присваивать переменные с цветом цвет - черный 
        }

    }

    public void BestRresults() {
        ReadTopResultFromFiles();
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

        TextBestRresults.text = result;
    }

    public void ReadTopResultFromFiles() {
        scoreTopResult = JsonUtility.FromJson<ScoreTopResult>(File.ReadAllText(Application.streamingAssetsPath + "/JSON_Result.json"));
    }

    public void WriteTopResultFromFiles() {
        SearchBestResult();
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_Result.json", JsonUtility.ToJson(scoreTopResult));
        BestRresults();
    }

    public void SearchBestResult() {
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

    public void RemoveData() {
        for (int i = 0; i < scoreTopResult.scoreResult.Length; i++) {
            scoreTopResult.scoreResult[i] = 0;
            scoreTopResult.time[i] = 0;
        }
        WriteTopResultFromFiles();

    }

    public void GameOver(int scoreWin) {
        score = scoreWin;
        GameOverTEXT.SetActive(true);
        GameOverButton.SetActive(true);
        TextResult.gameObject.SetActive(true);
        WriteTopResultFromFiles();
        TextResult.text = "Your result:\n " + score + " points";


    }

    public void StartedGame(float secondsAll) {
        timesTimer = secondsAll;
        SetTimesTimerAll(secondsAll);
        StartButton.SetActive(false);
        GameOverTEXT.SetActive(false);
        GameOverButton.SetActive(false);
        TextResult.gameObject.SetActive(false);
        //    TimerTick();

    }
}
