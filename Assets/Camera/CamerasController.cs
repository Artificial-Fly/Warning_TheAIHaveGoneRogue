using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public GameObject GameplayCamera, HUDCamera;
    // Start is called before the first frame update
    void Start()
    {
        GameplayCamera.gameObject.SetActive(true);
        HUDCamera.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
