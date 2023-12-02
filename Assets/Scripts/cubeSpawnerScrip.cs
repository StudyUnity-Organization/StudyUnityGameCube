using UnityEngine;

public class cubeSpawnerScrip : MonoBehaviour {
    // Start is called before the first frame update
    public LogicScript logic;
    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        logic.SpawnCubeGeneator();
        logic.ScorePlus(1);
        Destroy(gameObject);
    }
}
