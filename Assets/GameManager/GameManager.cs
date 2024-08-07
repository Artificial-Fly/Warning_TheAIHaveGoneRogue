using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables go here
    private GameObject PlayerCharacter, CombatManager, HUDManager;
    private HealthPoints PlayerCharacterHP;
    private ActionPoints PlayerCharacterAP;
    //-----------------
    //event dispatchers go here
    //methods go here
    public bool UpdateCombatManageerQueue(GameObject TargetActor, int NextAction){
        if(CombatManager.GetComponent<QueueController>()!=null){
            return CombatManager.GetComponent<QueueController>().UpdateActorsDictionary(TargetActor, NextAction);
        }else{
            return false;
        }
    }
    private void HandleOnCompletedRound(int CurrentCombatRounds){
        if(!(CurrentCombatRounds>0)){
            CombatManager.GetComponent<QueueController>().UpdateCombatStatus(false);
            PlayerCharacterHP.DecreaseHealth(999);
        }
    }
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleActionDecreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleActionReset(int CurrentValue, int MaxValue, float DeltaKoef){}
    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("PlayerCharacter");
        PlayerCharacterHP = GameObject.FindWithTag("PlayerCharacter").GetComponent<HealthPoints>();
        PlayerCharacterAP = GameObject.FindWithTag("PlayerCharacter").GetComponent<ActionPoints>();
        if(PlayerCharacter==null){
            Debug.Log("Can't find PlayerCharacter");
        }else{
            if(PlayerCharacterHP==null || PlayerCharacterAP == null){
            Debug.Log("Can't find PlayerCharacter's entity resources");
            }else if (PlayerCharacterHP!=null && PlayerCharacterAP != null){
                PlayerCharacterHP.OnHealthDecreased += HandleHealthDecreased;
                PlayerCharacterHP.OnHealthIncreased += HandleHealthIncreased;
                PlayerCharacterAP.OnActionDecreased += HandleActionDecreased;
                PlayerCharacterAP.OnActionReset += HandleActionReset;
            } 
        }
        //-----------------
        CombatManager = GameObject.FindWithTag("CombatManager");
        if(CombatManager!=null){
            Debug.Log("Player Character CombatQueue Status: "+UpdateCombatManageerQueue(PlayerCharacter, 0).ToString());
            CombatManager.GetComponent<QueueController>().OnRoundCompleted += HandleOnCompletedRound;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
