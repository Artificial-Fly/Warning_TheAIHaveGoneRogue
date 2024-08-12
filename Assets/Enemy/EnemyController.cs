using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject ActorSenses;
    public GameObject AttackZone;
    void HandleSensesTriggered(bool UpSense, bool DownSense, bool LeftSense, bool RightSense, string LastTriggeredActorTag, int lastTriggeredActorSense){
        //
        if((UpSense||DownSense||LeftSense||RightSense)&&LastTriggeredActorTag=="PlayerCharacter"){
            AttackZone.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        try{
            ActorSenses.GetComponent<ActorSensesBase>().OnSensesTriggered+= HandleSensesTriggered;
            Debug.Log("Successfuly located ActorSenses component, listening now..");
        }catch{
            Debug.Log("Failed to locate ActorSenses component..");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
