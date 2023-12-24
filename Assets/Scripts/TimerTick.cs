using System.Collections;
using UnityEngine;

public class TimerTicke : MonoBehaviour {
    private float timeLeft = 0;
    private bool isTimerRunning = false;
    public bool timerIsTick = false;
    public static TimerTicke TimerScript => _timer;
    private static TimerTicke _timer;



    private void Awake() {
        if (_timer == null) {
            _timer = this;
        } else {
            Destroy(this);
        }
    }

    private void Update() {
        if (timerIsTick) {
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
            } else {
                timerIsTick = false;
            }
        }
    }

    public void SetSeconds(float seconds) {
        timerIsTick = true;
        timeLeft = seconds;

        Debug.Log("timeLeft" + timeLeft);
    }

    //public bool StartTimer(float timeInSeconds) {
    //    if (!isTimerRunning) {
    //        timeLeft = timeInSeconds;
    //        isTimerRunning = true;
    //        StartCoroutine(TimerCoroutine());
    //    }

    //return false;
    //}

    //private IEnumerator TimerCoroutine() {
    //    while (timeLeft > 0) {
    //        yield return new WaitForSeconds(1);
    //        timeLeft--;
    //    }

    //    isTimerRunning = false;
    //    yield return true;
    //}
}

