using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables go here
    private GameObject PlayerCharacter;
    private HealthPoints PlayerCharacterHP;
    //private ActionPoints PlayerCharacterAP;
    //-----------------
    //event dispatchers go here
    //methods go here
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
        if(!(CurrentValue>PlayerCharacterHP.MinValue)){
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    /*private void HandleActionDecreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleActionReset(int CurrentValue, int MaxValue, float DeltaKoef){}*/
    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("PlayerCharacter");
        PlayerCharacterHP = GameObject.FindWithTag("PlayerCharacter").GetComponent<HealthPoints>();
        //PlayerCharacterAP = GameObject.FindWithTag("PlayerCharacter").GetComponent<ActionPoints>();
        if(PlayerCharacter==null){
            Debug.Log("Can't find PlayerCharacter");
        }else{
            if(PlayerCharacterHP==null /*|| PlayerCharacterAP == null*/){
            Debug.Log("Can't find PlayerCharacter's entity resources");
            }else if (PlayerCharacterHP!=null /*&& PlayerCharacterAP != null*/){
                PlayerCharacterHP.OnHealthDecreased += HandleHealthDecreased;
                PlayerCharacterHP.OnHealthIncreased += HandleHealthIncreased;
                //PlayerCharacterAP.OnActionDecreased += HandleActionDecreased;
                //PlayerCharacterAP.OnActionReset += HandleActionReset;
                //first time dispatchers run
                //PlayerCharacterHP.DecreaseHealth(999);
                Invoke("ResetStats",0.3f);
            } 
        }
    }
    void ResetStats(){
        if (PlayerCharacterHP!=null /*&& PlayerCharacterAP != null*/){
            PlayerCharacterHP.IncreaseHealth(999);
            //PlayerCharacterAP.ResetAction();
            Debug.Log("PlayerCharacter's Stats Have Been Reset");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
