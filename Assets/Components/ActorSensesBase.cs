using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSensesBase : MonoBehaviour
{
    //variables go here
    bool UpSense=false, DownSense=false, LeftSense=false, RightSense=false;
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
        }else if(other.gameObject.tag=="Obstacle" || other.gameObject.tag=="Hitbox"){//pickups' tags: Pickup_Heal, Pickup_Timer, Pickup_Trap,
                if(Mathf.Abs(OtherActorLocation.x-CurrentActorLocation.x)<15 && Mathf.Abs(OtherActorLocation.x-CurrentActorLocation.x)>5 && OtherActorLocation.x>CurrentActorLocation.x){RightSense=true;CurrentTriggeredSense = 1;}
                if(Mathf.Abs(OtherActorLocation.x-CurrentActorLocation.x)<15 && Mathf.Abs(OtherActorLocation.x-CurrentActorLocation.x)>5 && OtherActorLocation.x<CurrentActorLocation.x){LeftSense=true;CurrentTriggeredSense = 2;}
                if(Mathf.Abs(OtherActorLocation.y-CurrentActorLocation.y)<15 && Mathf.Abs(OtherActorLocation.y-CurrentActorLocation.y)>5 && OtherActorLocation.y>CurrentActorLocation.y){UpSense=true;CurrentTriggeredSense = 3;}
                if(Mathf.Abs(OtherActorLocation.y-CurrentActorLocation.y)<15 && Mathf.Abs(OtherActorLocation.y-CurrentActorLocation.y)>5 && OtherActorLocation.y<CurrentActorLocation.y){DownSense=true;CurrentTriggeredSense = 4;}
                if(OnSensesTriggered!=null){
                    OnSensesTriggered(UpSense, DownSense, LeftSense, RightSense, other.gameObject.tag, CurrentTriggeredSense);
                }
            }else{/*There was pickup effects realisation, but it was moved into ActorHitBoxBase component*/}
        
    }
    private void OnTriggerExit(Collider other){
        Debug.Log("Sense Triggered: Exit!");
        var CurrentActorLocation = transform.position;
        var OtherActorLocation = other.gameObject.transform.position;
        var CurrentTriggeredSense = 0;
        if(other.gameObject.tag=="Obstacle" || other.gameObject.tag=="Hitbox"){
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
