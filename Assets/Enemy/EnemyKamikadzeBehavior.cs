using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikadzeBehavior : MonoBehaviour
{
    //variables go here
    public GameObject ActorSenses;
    private GameManager GameManagerScript;
    private CombatManager CombatManagerScript;
    private GameObject CurrentEnemyCharacter;
    private bool IsPatrolling=true;//true=patrol, false=battle
    private int CurrentActionIndex=0;
    private static int ActionsAmount = 10;
    public string[] PatrolActions = new string[ActionsAmount];
    //public string[] BattleActions = new string[ActionsAmount];
    private Dictionary<string,int> ActionStringToInt = new Dictionary<string,int>();
    //-----------------
    //event dispatchers go here
    //-----------------
    //methods go here
    void HandleSensesTriggered(bool UpSense, bool DownSense, bool LeftSense, bool RightSense, string LastTriggeredActorTag, int lastTriggeredActorSense){
        Debug.Log("Non Player Character (Enemy) has spoted something..");
        if(LastTriggeredActorTag=="Hitbox"){
            Debug.Log("Non Player Character (Enemy) has spoted [the player character?] -> preparing suicide attack!!");
            UpdateNextAction(5);//next action is attack
        }
    }
    void HandleCompletedRound(int CurrentCombatRounds){
        CheckCurrentEnemyCharacterCombatStatus();
    }
    void CheckCurrentEnemyCharacterCombatStatus(){
        if(CurrentActionIndex<ActionsAmount){
            CurrentActionIndex++;
        }else{
            CurrentActionIndex=0;
        }
        if(IsPatrolling){
            Debug.Log("Non Player Character (Enemy) State is Patrolling");//.\n Enemy's next action is "+PatrolActions[CurrentActionIndex]+"("+ActionStringToInt[PatrolActions[CurrentActionIndex]].ToString()+")");
            UpdateNextAction(ActionStringToInt[PatrolActions[CurrentActionIndex]]);
            //UpdateNextAction(1);
        }else{
            Debug.Log("Non Player Character (Enemy) State is Battling");
            UpdateNextAction(5);//next action is attack
        }
    }
    bool ConfigureReferences(){
        try{
            ActorSenses.GetComponent<ActorSensesBase>().OnSensesTriggered+=HandleSensesTriggered;
            CombatManagerScript = GameObject.FindWithTag("CombatManager").GetComponent<CombatManager>();
            CombatManagerScript.OnRoundCompleted += HandleCompletedRound;
            CurrentEnemyCharacter = transform.gameObject;
            GameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            return true;
        }
        catch{
            return false;
        }
            
    }
    void UpdateNextAction(int NextAction){
        if(GameManagerScript!=null && CurrentEnemyCharacter!=null){
            GameManagerScript.UpdateCombatManageerQueue(CurrentEnemyCharacter, NextAction);
        }else{
            if(!ConfigureReferences()){
                Debug.Log("Failed To Update Next Action: Error During Configurating Of This Enemy Character!");
            }else{
                UpdateNextAction(NextAction);
            }
        }
    }
    void ConfigureActionDictionary(){
        ActionStringToInt.Add("idle", 0);
        ActionStringToInt.Add("up", 1);
        ActionStringToInt.Add("down", 2);
        ActionStringToInt.Add("left", 3);
        ActionStringToInt.Add("right", 4);
        ActionStringToInt.Add("attack", 5);
    }
    // Start is called before the first frame update
    void Start()
    {
        ConfigureActionDictionary();
        ConfigureReferences();
        CheckCurrentEnemyCharacterCombatStatus();
        /*ActorSenses.GetComponent<ActorSensesBase>().OnSensesTriggered+=HandleSensesTriggered;
        CombatManagerScript = GameObject.FindWithTag("CombatManager").GetComponent<CombatManager>();
        CombatManagerScript.OnRoundCompleted += HandleCompletedRound;
        CurrentEnemyCharacter = transform.gameObject;
        GameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
