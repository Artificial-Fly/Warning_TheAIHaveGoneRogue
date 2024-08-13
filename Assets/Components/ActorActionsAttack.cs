using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActionsAttack : MonoBehaviour
{
    public GameObject AttackZone;
    //---------
    //methods
    public void DefaultAttack(){
        Debug.Log("Attacking!");
        Invoke("ActivateAttackZone", 0.75f);
    }
    public void SuicideAttack(){
        Debug.Log("Kamikaze Attack!");
        try{
            var ActorSFXController = transform.Find("ActorSFXController").GetComponent<ActorSFXController>();
            ActorSFXController.StartSFX();
        }
        catch{
            Debug.Log("Error: Couldn't start SFX");
        }
        Invoke("ActivateAttackZone", 0.15f);
        Destroy(gameObject, 0.95f);
    }
    private void ActivateAttackZone(){
        AttackZone.SetActive(true);
    }
    //----------
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
