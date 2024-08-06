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
    //-----------------
    //methods go here
    public bool UpdateActorsDictionary(GameObject TargetActor, int NextAction){//1=up,2=down,3=left,4=right,5=attack,0=idle
        try{
            if(ActorsDictionary.ContainsKey(TargetActor)){
            ActorsDictionary[TargetActor] = NextAction;
            }else{
                ActorsDictionary.Add(TargetActor, NextAction);
            }
            Debug.Log("Success! ActorsDictionary Has Been Updated..");
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
                    return true;
                }else if(Actor.Value==1){
                    //up
                    Debug.Log("Up Action");
                    Actor.Key.transform.Translate(0,10,0);
                    return true;
                }else if(Actor.Value==2){
                    //down
                    Debug.Log("Down Action");
                    Actor.Key.transform.Translate(0,-10,0);
                    return true;
                }else if(Actor.Value==3){
                    //left
                    Debug.Log("Left Action");
                    Actor.Key.transform.Translate(-10,0,0);
                    return true;
                }else if(Actor.Value==4){
                    //right
                    Debug.Log("Right Action");
                    Actor.Key.transform.Translate(10,0,0);
                    return true;
                }else if(Actor.Value==5){
                    //attack
                    Debug.Log("Attack Action");
                    return true;
                }
            }
            return true;
        }catch{
            return false;
        }
    }
    //-----------------
    // Start is called before the first frame update
    void Start()
    {
        CurrentCombatRounds=CombatRounds;
        Invoke("CompleteCombatRound", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
