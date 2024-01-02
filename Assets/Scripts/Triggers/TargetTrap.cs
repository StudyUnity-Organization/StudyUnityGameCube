using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.U2D.ScriptablePacker;

public class TargetTrap : MonoBehaviour {
    [SerializeField]
    private GameObject shereEndPrefab;
    [SerializeField]
    private float radius = 1;
    [SerializeField]
    private float maxRangeDistanceTrapActive = 1;
    [SerializeField]
    private float timerTrap = 2;
    [SerializeField]
    private float timerRecharge = 4;

    [SerializeField]
    private float freezTime = 2;
    [SerializeField]
    private float rechargeTime = 2;

    private bool _trapRecharge = false;
    private bool _trapIsActive = false;

    private RaycastHit _hit;
    // Start is called before the first frame update



    public LayerMask LayerMask;
    public Vector3 origin;

    private float _maxDistance = 1;
    private Vector3 _direction;
    private Vector3 _endPointOfTrap;

    private float _globalSeconds = 0;
    private float _sumRechargeAndDreezSeconds = 0;
    private bool _timerStart = false;

    private IEnumerator _coroutineBlocked;
    private IEnumerator _coroutineDecontamination;
    private void Start() {
        CreateEndPointTrap(); //создаем конечную точку ловушки
    }

    // Update is called once per frame
    private void Update() {
        //     TargetRecognition(); //
     //   TimerTrap();
    }
    private void FixedUpdate() {
        TargetRecognition();  //проверка попадания в ловушку
    }

    private void CreateEndPointTrap() {
        while (_maxDistance <= 3) {
            _endPointOfTrap = GenerationEndPointTrap();
            if (CheckingGoingAbroad(_endPointOfTrap, LogicScript.Logic.lengthPlatform / 2)) {
                _maxDistance = Vector3.Distance(transform.position, _endPointOfTrap);
            } else {
                _maxDistance = 0;
            };
        }
        Instantiate(shereEndPrefab, _endPointOfTrap, transform.rotation);
    }

    private Vector3 GenerationEndPointTrap() {
        //генирирует рандомную точку конца ловушки     
        Vector3 end = new Vector3(Random.Range(-maxRangeDistanceTrapActive, +maxRangeDistanceTrapActive),
                         0,
                         Random.Range(-maxRangeDistanceTrapActive, +maxRangeDistanceTrapActive));

        end = end + transform.position;
        return end;
    }


    private void TargetRecognition() {

        Ray ray = new Ray(transform.position, _endPointOfTrap - transform.position);
        Debug.DrawRay(transform.position, _endPointOfTrap - transform.position, Color.yellow);

        if (!_trapIsActive) {
            if (Physics.SphereCast(ray, radius, out _hit, _maxDistance)) {
                if (_hit.collider.gameObject.CompareTag("Player")) {
                    TrapHasWorked(1f);

                    _trapIsActive = true;
                    //_globalSeconds = freezTime + rechargeTime;
                    //_sumRechargeAndDreezSeconds = _globalSeconds;
                    //TimerTrap();
                }
            }
        }
    }

    

        
        // every 2 seconds perform the print()
    private void TrapHasWorked(float a) {

        _coroutineBlocked = BlockingMovement(2.0f);
        StartCoroutine(_coroutineBlocked);

        _coroutineDecontamination = DecontaminationTrap(4.0f);
        StartCoroutine(_coroutineDecontamination);
    }
    private IEnumerator BlockingMovement(float waitTime) {
        HeroController.CubeScript.Can = false;
        while (true) {
            yield return new WaitForSeconds(waitTime);
            HeroController.CubeScript.Can = true;
            StopCoroutine(_coroutineBlocked);
        }
    }

    private IEnumerator DecontaminationTrap(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            _trapIsActive = false;
            StopCoroutine(_coroutineDecontamination);
        }
      
    }    

    private bool CheckingGoingAbroad(Vector3 vector, float distance) {
        if (-distance <= vector.x && vector.x <= distance) {
            if (-distance <= vector.y && vector.y <= distance) {
                if (-distance <= vector.z && vector.z <= distance) {
                    return true;
                }
            }
        }
        return false;
    }


    public void TimerTrap() {
        if (_trapIsActive) {
            TrapHasWorked(_globalSeconds);
            _globalSeconds = _globalSeconds - 1 * Time.deltaTime;
        }
    }

    //private void TrapHasWorked(float seconds) {
    //    if (seconds <= 0) {
    //        _trapIsActive = false;
    //        Cube.CubeScript.Can = true;
    //    }

    //    if (seconds >= _sumRechargeAndDreezSeconds - freezTime) {
    //        Cube.CubeScript.Can = false;
    //    } else {
    //        Cube.CubeScript.Can = true;
    //    }

    //}


}
