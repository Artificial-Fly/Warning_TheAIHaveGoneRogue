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
    /*
    private void ActivatePickupSFX(GameObject TargetActor){
        try{
            var ActorSFXController = TargetActor.transform.Find("ActorSFXController").GetComponent<ActorSFXController>();
            ActorSFXController.StartSFX();
        }
        catch{
            Debug.Log("Error: Couldn't start SFX");
        }
    }
    private void ActivatePickupEffect(GameObject TargetActor, string TargetResourceType, int TargetResourceValue){
        if(TargetResourceType=="AddTime"){
                    //Add Combat Rounds but only if Component owned by Player Character
                    if(transform.parent.tag=="PlayerCharacter"){
                        ActivatePickupSFX(TargetActor);
                        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().AddGameplayTime(TargetResourceValue);
                        DestroyPickupOnUse(TargetActor);
                    }
        }else if(TargetResourceType=="Heal"){
                    //Increase Health but only if Component owned by Player Character
                    if(transform.parent.tag=="PlayerCharacter"){
                        ActivatePickupSFX(TargetActor);
                        transform.parent.gameObject.GetComponent<HealthPoints>().IncreaseHealth(TargetResourceValue);
                        DestroyPickupOnUse(TargetActor);
                    }
        }else if(TargetResourceType=="DealDamage"){
                    //Add Decrease Health
                    ActivatePickupSFX(TargetActor);
                    transform.parent.GetComponent<HealthPoints>().DecreaseHealth(TargetResourceValue);
                    DestroyPickupOnUse(TargetActor);
        }else{
                    //No Pickup Effect Found..
                    Debug.Log("Error, Cannot apply any effect: Unknown Actor Resource Type!!!");
        }
    }
    private IEnumerator DestroyPickupOnUse(GameObject TargetActor){
        yield return new WaitForSeconds(0.5f);
        Destroy(TargetActor);
    }*/
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Goal" && transform.parent.tag=="PlayerCharacter"){
            Debug.Log("Player Has Reached The Goal!");
        }else {
            var PickupActor = other.gameObject;
            if(PickupActor.name.Contains("Time") && transform.parent.tag=="PlayerCharacter"){
                try{PickupActor.GetComponent<AddTimePickupEffect>().ActivatePickupEffect(transform.parent.gameObject);}catch{Debug.Log("Error, can't locate pickup effect component");}
            }else if(PickupActor.name.Contains("Heal")&& transform.parent.tag=="PlayerCharacter"){
                try{PickupActor.GetComponent<HealPickupEffect>().ActivatePickupEffect(transform.parent.gameObject);}catch{Debug.Log("Error, can't locate pickup effect component");}
            }else if(PickupActor.name.Contains("Damage")||PickupActor.name.Contains("AttackZone")){
                try{PickupActor.GetComponent<DealDamagePickupEffect>().ActivatePickupEffect(transform.parent.gameObject);}catch{Debug.Log("Error, can't locate pickup effect component");}
            }else{
                //No Pickup Effect Found..
                Debug.Log("Error, Cannot apply any effect: Unknown Actor Resource Type!!!");
            }
            //var CurrentActorPickupEffects = transform.parent.GetComponent<ActorPickupEffects>();
            //Debug.Log("++CHECK OTHER ACTOR RESOURCES++");
            //var OtherActorResources = other.gameObject.GetComponents<ActorResourse>();
            /*for(int i = 0; i < OtherActorResources.Length; i++){
                var PickupResourceType = OtherActorResources[i].GetRType();
                var PickupResourceValue = OtherActorResources[i].GetRValue();
                var PickupActor = other.gameObject;
                if(PickupResourceType=="AddTime"){
                    try{PickupActor.GetComponent<AddTimePickupEffect>().ActivatePickupEffect(transform.parent);}catch{Debug.Log("Error, can't locate pickup effect component");}
                }else if(PickupResourceType=="Heal"){
                    try{PickupActor.GetComponent<HealPickupEffect>().ActivatePickupEffect(transform.parent);}catch{Debug.Log("Error, can't locate pickup effect component");}
                }else if(PickupResourceType=="DealDamage"){
                    try{PickupActor.GetComponent<DealDamagePickupEffect>().ActivatePickupEffect(transform.parent);}catch{Debug.Log("Error, can't locate pickup effect component");}
                }else{
                    //No Pickup Effect Found..
                    Debug.Log("Error, Cannot apply any effect: Unknown Actor Resource Type!!!");
                }
                //ActivatePickupEffect(PickupActor, TargetResourceType, TargetResourceValue);
            }*/
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