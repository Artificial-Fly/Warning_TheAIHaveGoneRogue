using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSensesController : MonoBehaviour
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
        if(OtherActorLocation.x>CurrentActorLocation.x){RightSense=true;CurrentTriggeredSense = 1;}
        if(OtherActorLocation.x<CurrentActorLocation.x){LeftSense=true;CurrentTriggeredSense = 2;}
        if(OtherActorLocation.y>CurrentActorLocation.y){UpSense=true;CurrentTriggeredSense = 3;}
        if(OtherActorLocation.y<CurrentActorLocation.y){DownSense=true;CurrentTriggeredSense = 4;}
        if(OnSensesTriggered!=null){
            OnSensesTriggered(UpSense, DownSense, LeftSense, RightSense, other.gameObject.tag, CurrentTriggeredSense);
        }
    }
    private void OnTriggerExit(Collider other){
        Debug.Log("Sense Triggered: Exit!");
        var CurrentActorLocation = transform.position;
        var OtherActorLocation = other.gameObject.transform.position;
        var CurrentTriggeredSense = 0;
        if(OtherActorLocation.x>CurrentActorLocation.x){RightSense=false;CurrentTriggeredSense = -1;}
        if(OtherActorLocation.x<CurrentActorLocation.x){LeftSense=false;CurrentTriggeredSense = -2;}
        if(OtherActorLocation.y>CurrentActorLocation.y){UpSense=false;CurrentTriggeredSense = -3;}
        if(OtherActorLocation.y<CurrentActorLocation.y){DownSense=false;CurrentTriggeredSense = -4;}
        if(OnSensesTriggered!=null){
            OnSensesTriggered(UpSense, DownSense, LeftSense, RightSense, other.gameObject.tag, CurrentTriggeredSense);
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
