using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //variables go here
    public string CurrentScene, NextScene;
    private Dictionary<GameObject, int> ActorsDictionary = new Dictionary<GameObject, int>();
    //-----------------
    public int CombatRounds=10;
    public float StartCombatDelay=0.5f, CombatRoundDuration=1.1f;
    private int CurrentCombatRounds;
    //-----------------
    //event dispatchers go here
    public delegate void RoundCompleted(int CurrentCombatRounds);
    public event RoundCompleted OnRoundCompleted;
    //-----------------
    //methods go here
    public void AddCombatRounds(int Amount){
        if(Amount>0){
            CurrentCombatRounds+=Amount;
            if(OnRoundCompleted!=null){
                OnRoundCompleted(CurrentCombatRounds);
                Debug.Log("Add Combat Rounds");
            }
        }
    }
    public void UpdateCombatStatus(bool IsCombatStarted){
        if(IsCombatStarted && !IsInvoking("CompleteCombatRound")){
            InvokeRepeating("CompleteCombatRound", StartCombatDelay, CombatRoundDuration);
        }else{
            CancelInvoke();
        }
    }
    public bool UpdateActorsDictionary(GameObject TargetActor, int NextAction){//1=up,2=down,3=left,4=right,5=attack,0=idle
        try{
            if(ActorsDictionary.ContainsKey(TargetActor)){
            ActorsDictionary[TargetActor] = NextAction;
            }else{
                ActorsDictionary.Add(TargetActor, NextAction);
            }
            Debug.Log("Success! ActorsDictionary ("+ TargetActor.ToString()+", "+ NextAction.ToString() +") Has Been Updated..");
            return true;
        }catch{
            Debug.Log("Error! ActorsDictionary Hasn't Been Updated..");
            return  false;
        }
    }
    public bool CompleteCombatRound(){
        try{
            foreach(var Actor in ActorsDictionary){
                //Debug.Log(Actor.Key.ToString()+" initiates "+Actor.Value.ToString());
                if(Actor.Value==0){
                    //idle
                    Debug.Log("Idle Action");
                }else if(Actor.Value==1){
                    //up
                    Actor.Key.GetComponent<ActorActionsMovement>().MoveUP();
                }else if(Actor.Value==2){
                    //downt
                    Actor.Key.GetComponent<ActorActionsMovement>().MoveDOWN();
                }else if(Actor.Value==3){
                    //left
                    Actor.Key.GetComponent<ActorActionsMovement>().MoveLEFT();
                }else if(Actor.Value==4){
                    //right
                    Actor.Key.GetComponent<ActorActionsMovement>().MoveRIGHT();
                    
                }else if(Actor.Value==5){
                    //attack
                    if(Actor.Key.gameObject.tag=="NonPlayerCharacter"){
                        if(Actor.Key.name.Contains("Kamikadze")){
                            Actor.Key.GetComponent<ActorActionsAttack>().SuicideAttack();
                        }else if(!Actor.Key.name.Contains("Kamikadze")){
                            Actor.Key.GetComponent<ActorActionsAttack>().DefaultAttack();
                        }
                    }else{
                        Debug.Log("This ain't NonPlayerCharacter.. Does it have to attack?");
                    }
                    
                }
            }
            if(OnRoundCompleted!=null){
                CurrentCombatRounds--;
                OnRoundCompleted(CurrentCombatRounds);
                //Debug.Log("Completed");
                return true;
            }else{
                return false;
            }
        }catch{
            return false;
        }
    }
    //-----------------
    // Start is called before the first frame update
    void Start()
    {
        CurrentCombatRounds=CombatRounds;
        UpdateCombatStatus(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
