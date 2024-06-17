using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverArea : MonoBehaviour
{
    [SerializeField] Cover[] covers;
    
    void Awake()
    {
        covers = GetComponentsInChildren<Cover>();
    }

    public Cover GetRandCover(Vector3 agentLocation)
    {
        return covers[Random.Range(0, covers.Length -1)];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
