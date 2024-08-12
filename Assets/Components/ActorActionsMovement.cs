using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActionsMovement : MonoBehaviour
{
    //variables go here
    public GameObject ActorSenses;
    public int UpLimit=35, DownLimit=-35, LeftLimit=-35, RightLimit=35;
    public int ActorMoveRange = 10;
    bool CurrentUpStatus, CurrentDownStatus, CurrentLeftStatus, CurrentRightStatus;
    string CurrentTriggeredActorTag;
    int CurrentTriggeredActorSense;
    //-----------------
    //event dispatchers go here
    //-----------------
    //methods go here
    private void HandleSensesTriggered(bool UpSense, bool DownSense, bool LeftSense, bool RightSense, string LastTriggeredActorTag, int lastTriggeredActorSense){
        CurrentUpStatus = UpSense;
        CurrentDownStatus = DownSense;
        CurrentLeftStatus = LeftSense;
        CurrentRightStatus = RightSense;
        CurrentTriggeredActorTag = LastTriggeredActorTag;
        CurrentTriggeredActorSense = lastTriggeredActorSense;
    }
    public void MoveUP(){
        if(transform.position.y+ActorMoveRange<=UpLimit && !CurrentUpStatus){
            Debug.Log("Up");
            transform.Translate(0,ActorMoveRange,0);
        }else{
            Debug.Log("Action Failed..");
        }
    }
    public void MoveDOWN(){
        if(transform.position.y-ActorMoveRange>=DownLimit && !CurrentDownStatus){
            Debug.Log("Down");
            transform.Translate(0,-ActorMoveRange,0);
        }else{
            Debug.Log("Action Failed..");
        }
    }
    public void MoveLEFT(){
        if(transform.position.x-ActorMoveRange>=LeftLimit && !CurrentLeftStatus){
            Debug.Log("Left");
            transform.Translate(-ActorMoveRange,0,0);
        }else{
            Debug.Log("Action Failed..");
        }
    }
    public void MoveRIGHT(){
        if(transform.position.x+ActorMoveRange<=RightLimit  && !CurrentRightStatus){
            Debug.Log("Right");
            transform.Translate(ActorMoveRange,0,0);
        }else{
            Debug.Log("Action Failed..");
        }
    }
    /**/
    //-----------------
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
