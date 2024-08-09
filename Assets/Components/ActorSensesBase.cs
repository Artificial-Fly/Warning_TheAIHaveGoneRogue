using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSensesBase : MonoBehaviour
{
    //variables go here
    bool UpSense, DownSense, LeftSense, RightSense;
    //-----------------
    //event dispatchers go here
    public delegate void SensesTriggered(bool UpSense, bool DownSense, bool LeftSense, bool RightSense, string LastTriggeredActorTag, int lastTriggeredActorSense);
    public event SensesTriggered OnSensesTriggered;
    //-----------------
    //methods go here
    private void OnTriggerEnter(Collider other){
        Debug.Log("Sense Triggered: Enter!");
        var CurrentActorLocation = transform.position;
        var OtherActorLocation = other.gameObject.transform.position;
        var CurrentTriggeredSense = 0;
        if(other.gameObject.tag=="Goal"){
            //Goal Ahead?
            Debug.Log("Goal Ahead");
        }else if(other.gameObject.tag.Substring(0,6)!="Pickup"){//pickups' tags: Pickup_Heal, Pickup_Timer, Pickup_Trap,
                if(OtherActorLocation.x>CurrentActorLocation.x){RightSense=true;CurrentTriggeredSense = 1;}
                if(OtherActorLocation.x<CurrentActorLocation.x){LeftSense=true;CurrentTriggeredSense = 2;}
                if(OtherActorLocation.y>CurrentActorLocation.y){UpSense=true;CurrentTriggeredSense = 3;}
                if(OtherActorLocation.y<CurrentActorLocation.y){DownSense=true;CurrentTriggeredSense = 4;}
                if(OnSensesTriggered!=null){
                    OnSensesTriggered(UpSense, DownSense, LeftSense, RightSense, other.gameObject.tag, CurrentTriggeredSense);
                }
            }else{
                //implement pickup effect here:
                /*if(other.gameObject.tag.Substring(0,6)=="Pickup"){
                    Debug.Log("Trying to start pickup effect..");
                    try{
                        var CurrentActorActions = transform.parent.GetComponent<ActorActions>();
                        if(other.gameObject.tag.Substring(7)=="Heal"){
                            Debug.Log("Trying Heal effect");
                            CurrentActorActions.Heal(3);
                        }else if(other.gameObject.tag.Substring(7)=="Timer"){
                            Debug.Log("Trying Timer effect");
                            CurrentActorActions.Timer(10);
                        }else if(other.gameObject.tag.Substring(7)=="Trap"){
                            Debug.Log("Trying Trap effect");
                            Debug.Log("Caught in the Trap!");
                            CurrentActorActions.Trap(5);
                        }
                        Destroy(other.gameObject);
                    }catch{
                        Debug.Log("Couldn't find ActorActions Componen't or Activate Pickup Effect");
                    }
                }*/
            }
        
    }
    private void OnTriggerExit(Collider other){
        Debug.Log("Sense Triggered: Exit!");
        var CurrentActorLocation = transform.position;
        var OtherActorLocation = other.gameObject.transform.position;
        var CurrentTriggeredSense = 0;
        if(other.gameObject.tag!="pickup"){
            if(OtherActorLocation.x>CurrentActorLocation.x){RightSense=false;CurrentTriggeredSense = -1;}
            if(OtherActorLocation.x<CurrentActorLocation.x){LeftSense=false;CurrentTriggeredSense = -2;}
            if(OtherActorLocation.y>CurrentActorLocation.y){UpSense=false;CurrentTriggeredSense = -3;}
            if(OtherActorLocation.y<CurrentActorLocation.y){DownSense=false;CurrentTriggeredSense = -4;}
            if(OnSensesTriggered!=null){
                OnSensesTriggered(UpSense, DownSense, LeftSense, RightSense, other.gameObject.tag, CurrentTriggeredSense);
            }
        }else{
            //implement pickup effect
        }
        
    }
    //-----------------
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
