using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class BridgeCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject bridgePlatform;
    [SerializeField]
    private int countPlatforms = 1;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> bridge = new List<GameObject>();
        for (int i = 0; i < countPlatforms; i++) {
            bridge.Add(bridgePlatform);
        }
    }
    

}
