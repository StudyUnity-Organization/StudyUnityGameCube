using UnityEngine;

public class CubeSpawner : MonoBehaviour {
    // Start is called before the first frame update
    public LogicScript Logic;
    void Start() {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }


    private void OnTriggerEnter(Collider other) {
        Logic.SpawnCubeGeneator();
        Logic.ScorePlus(1);
        Destroy(gameObject);
    }
}
