using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHeroy : MonoBehaviour {
    [SerializeField]
    private GameObject[] spawner;
    [SerializeField]
    private GameObject cube;

    private int _count = 0;
    private Transform _transformCube;
    private Transform _transformSpawner;

    private void Start() {
        _transformCube = cube.transform;
        _transformSpawner = spawner[0].transform;
        _transformCube.position = _transformSpawner.position;
        _transformCube.rotation = _transformSpawner.rotation;
    }


    public void ChoosingSpawnLocationRight() {
        _count++;
        while (_count < 0) {
            _count += spawner.Length;
        }
        _transformSpawner = spawner[_count % spawner.Length].transform;
        _transformCube.position = _transformSpawner.position;
        _transformCube.rotation = _transformSpawner.rotation;
    }
    public void ChoosingSpawnLocationLeft() {
        _count--;
        while (_count < 0) {
            _count += spawner.Length;
        }
        _transformSpawner = spawner[_count % spawner.Length].transform;
        _transformCube.position = _transformSpawner.position;
        _transformCube.rotation = _transformSpawner.rotation;
    }
}
