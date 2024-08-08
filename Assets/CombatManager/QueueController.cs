using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    //variables go here
    private Dictionary<GameObject, int> ActorsDictionary = new Dictionary<GameObject, int>();
    //-----------------
    public int CombatRounds=10;
    private int CurrentCombatRounds;
    //-----------------
    //event dispatchers go here
    public delegate void RoundCompleted(int CurrentCombatRounds);
    public event RoundCompleted OnRoundCompleted;
    //-----------------
    //methods go here
    public void AddCombatRounds(int Amount){
        if(Amount>0){
            CurrentCombatRounds+=CurrentCombatRounds+Amount;
            if(OnRoundCompleted!=null){
                OnRoundCompleted(CurrentCombatRounds);
                Debug.Log("Add Combat Rounds");
            }
        }
    }
    public void UpdateCombatStatus(bool IsCombatStarted){
        if(IsCombatStarted){
            InvokeRepeating("CompleteCombatRound", 0.5f, 1);
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
                Debug.Log(Actor.Key.ToString()+" initiates "+Actor.Value.ToString());
                if(Actor.Value==0){
                    //idle
                    Debug.Log("Idle Action");
                    //return true;
                }else if(Actor.Value==1){
                    //upt
                    Actor.Key.GetComponent<ActorActions>().MoveUP();
                    //return true;
                }else if(Actor.Value==2){
                    //downt
                    Actor.Key.GetComponent<ActorActions>().MoveDOWN();
                    //return true;
                }else if(Actor.Value==3){
                    //left
                    Actor.Key.GetComponent<ActorActions>().MoveLEFT();
                    //return true;
                }else if(Actor.Value==4){
                    //right
                    Actor.Key.GetComponent<ActorActions>().MoveRIGHT();
                    //return true;
                }else if(Actor.Value==5){
                    //attack
                    Actor.Key.GetComponent<ActorActions>().Attack();
                    //return true;
                }
            }
            if(OnRoundCompleted!=null){
                CurrentCombatRounds--;
                OnRoundCompleted(CurrentCombatRounds);
                Debug.Log("Completed");
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
