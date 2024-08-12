using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHitboxBase : MonoBehaviour
{
    //variables go here
    //-----------------
    //event dispatchers go here
    //-----------------
    //methods go here
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Goal" && transform.parent.tag=="PlayerCharacter"){
            Debug.Log("Player Has Reached The Goal!");
        }else {
            //var CurrentActorPickupEffects = transform.parent.GetComponent<ActorPickupEffects>();
            Debug.Log("++CHECK OTHER ACTOR RESOURCES++");
            var OtherActorResources = other.gameObject.GetComponents<ActorResourse>();
            for(int i = 0; i < OtherActorResources.Length; i++){
                if(OtherActorResources[i].GetRType()=="AddTime"){
                    //Add Combat Rounds
                    if(transform.parent.tag=="PlayerCharacter"){
                            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().AddGameplayTime(OtherActorResources[i].GetRValue());
                            Destroy(other.gameObject);
                    }
                }else if(OtherActorResources[i].GetRType()=="Heal"){
                    //Increase Health
                    if(transform.parent.tag=="PlayerCharacter"){
                        transform.parent.gameObject.GetComponent<HealthPoints>().IncreaseHealth(OtherActorResources[i].GetRValue());
                        Destroy(other.gameObject);
                    }
                }else if(OtherActorResources[i].GetRType()=="DealDamage"){
                    //Add Decrease Health
                    transform.parent.GetComponent<HealthPoints>().DecreaseHealth(OtherActorResources[i].GetRValue());
                    Destroy(other.gameObject);
                }else{
                    //
                    Debug.Log("Error, Cannot apply any effect: Unknown Actor Resource Type!!!");
                }
            }
        }
        /*else if(other.gameObject.tag.Substring(0,6)=="Pickup"){
            Debug.Log("Trying to start pickup effect..");
                try{
                    var CurrentActorActions = transform.parent.GetComponent<ActorPickupEffects>();
                    var CurrentActorResources = transform.parent.GetComponents<ActorResourse>();
                    if(other.gameObject.tag.Substring(7)=="Heal" && transform.parent.tag=="PlayerCharacter"){
                        Debug.Log("Trying Heal effect");
                        CurrentActorActions.Heal(3);
                    }else if(other.gameObject.tag.Substring(7)=="Timer" && transform.parent.tag=="PlayerCharacter"){
                        Debug.Log("Trying Timer effect");
                        CurrentActorActions.Timer(3);
                    }else if(other.gameObject.tag.Substring(7)=="Trap"){
                        Debug.Log("Trying Trap effect");
                        Debug.Log("Caught in the Trap!");
                        CurrentActorActions.Trap(5);
                    }
                    Destroy(other.gameObject);
                }catch{
                    Debug.Log("Couldn't find ActorPickupEffects Component or Activate Pickup Effect");
                }
        }*/
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
