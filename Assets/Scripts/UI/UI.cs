using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject GameOverTEXT;
    public GameObject GameOverButton;
    public GameObject StartButton;
    public GameObject Aim;

    public Text TextScore;
    public Text TextTimer;
    public Text TextResult;
    public Text TextBestRresults;


    [SerializeField]
    private Color colorText = new Color(0.1960784f, 0.1960784f, 0.1960784f); //505050
                                                                             // Start is called before the first frame update

    private int _score = 0;
    private float _timesTimerAll = 0;  //счетчик времени
    private float _timesTimer = 0;  //переменная для хранения

    //   public Logic.LogicScript Logic.Logic;

    private Image _aimImage;


    public static UI UiSpace => _ui;
    private static UI _ui;



    private void Awake() {
        if (_ui == null) {
            _ui = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        // Debug.Log("Sprite _____ " + UI.Ui.Aim.GetComponent<Image>().sprite.name);
        _aimImage = UI.UiSpace.Aim.GetComponent<Image>();
    }

    public ScoreTopResult scoreTopResult;

    public void TimerTick() {
        _timesTimerAll = _timesTimerAll - 1 * Time.deltaTime;
        TimerPaint();
        if (_timesTimerAll <= 0 && LogicScript.Logic.StartGame) {
            LogicScript.Logic.GameOver();
        }
    }

    public void SetTimesTimerAll(float secondsAll) {
        _timesTimerAll = secondsAll;
    }

    public void SetScore(int scoreSet) {
        _score = scoreSet;
        TextScore.text = _score.ToString();
    }





    public void TimerPaint() {
        TextTimer.text = ((int)(_timesTimerAll / 60)).ToString() + ':' + ((int)(_timesTimerAll % 60)).ToString();

        if (_timesTimerAll % 60 < 10) {
            TextTimer.text = ((int)(_timesTimerAll / 60)).ToString() + ":0" + ((int)(_timesTimerAll % 60)).ToString();
        }


        if (_timesTimerAll <= 5f) {
            TextTimer.color = Color.red;
        } else {
            TextTimer.color = colorText;
        }

    }

    public void BestRresults() {
        ReadTopResultFromFiles();
        string result = "Best results:\n";
        for (int i = 0; i < scoreTopResult.number.Length; i++) {
            if (scoreTopResult.time[i] < 10) {
                result += scoreTopResult.number[i] + ". " + scoreTopResult.scoreResult[i] + "   (" + (int)(scoreTopResult.time[i] / 60f) + ":0" + (int)(scoreTopResult.time[i] % 60f) + ")\n";
            } else {
                result += scoreTopResult.number[i] + ". " + scoreTopResult.scoreResult[i] + "   (" + (int)(scoreTopResult.time[i] / 60f) + ':' + (int)(scoreTopResult.time[i] % 60f) + ")\n";
            }
        }

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
        int score_result = _score;
        int score_temporary = 0;
        float time_temporary;
        float time_temporary2 = _timesTimer;

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
        _score = scoreWin;
        GameOverTEXT.SetActive(true);
        GameOverButton.SetActive(true);
        TextResult.gameObject.SetActive(true);
        WriteTopResultFromFiles();
        TextResult.text = "Your result:\n " + _score + " points";
        Aim.SetActive(false);



    }

    public void StartedGame(float secondsAll) {
        _timesTimer = secondsAll;
        SetTimesTimerAll(secondsAll);
        StartButton.SetActive(false);
        GameOverTEXT.SetActive(false);
        GameOverButton.SetActive(false);
        TextResult.gameObject.SetActive(false);
        Aim.SetActive(true);
        //    TimerTick();

    }


    public void HandleTarget(bool aimed) {
        if (aimed) {
            _aimImage.color = Color.green;
        } else {
            _aimImage.color = Color.black;
        }
    }
}
