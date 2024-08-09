using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //variables go here
    //-----------------
    //event dispatchers go here
    //-----------------
    //methods go here
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.transform.parent.gameObject.tag=="PlayerCharacter"){
            //
            Debug.Log("Onto the next level we go..");
            try{
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RestartLevel();
            }catch{
                Debug.Log("Failed to go onto the next level!");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
