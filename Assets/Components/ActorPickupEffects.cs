using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPickupEffects : MonoBehaviour
{
    //------------------------
    //DEPRECATED COMPONENT----
    //------------------------
    public void Heal(int Amount=10){
        try{
            transform.gameObject.GetComponent<HealthPoints>().IncreaseHealth(Amount);
        }catch{
            Debug.Log("Error: Couldn't Use Heal Pickup Effect");
        }
    }
    public void Timer(int Amount=5){
        try{
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().AddGameplayTime(Amount);
        }catch{
            Debug.Log("Error: Couldn't Use AddTime Pickup Effect");
        }
    }
    public void Trap(int Amount=10){
        try{
            transform.gameObject.GetComponent<HealthPoints>().DecreaseHealth(Amount);
        }catch{
            Debug.Log("Error: Couldn't Use Trap Pickup Effect");
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
