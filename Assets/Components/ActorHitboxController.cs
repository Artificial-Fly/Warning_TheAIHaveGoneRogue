using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHitboxController : MonoBehaviour
{
    //variables go here
    //-----------------
    //event dispatchers go here
    //-----------------
    //methods go here
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Substring(0,6)=="Pickup"){
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
