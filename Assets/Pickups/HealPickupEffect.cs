using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickupEffect : MonoBehaviour
{
    [SerializeField]
    private int PickupResourceValue;
    public void ActivatePickupEffect(GameObject TargetActor){
        ActivatePickupSFX();
        TargetActor.GetComponent<HealthPoints>().IncreaseHealth(PickupResourceValue);
        Destroy(transform.gameObject, 0.3f);
    }
    private void ActivatePickupSFX(){
        try{
            var ActorSFXController = transform.Find("ActorSFXController").GetComponent<ActorSFXController>();
            ActorSFXController.StartSFX();
        }
        catch{
            Debug.Log("Error: Couldn't start SFX");
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
