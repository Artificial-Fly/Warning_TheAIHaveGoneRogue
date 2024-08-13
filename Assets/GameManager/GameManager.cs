using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variables go here
    [SerializeField] 
    private int PlayerActionOnStart=3;
    private int CurrentGameState = 1;//-1=GameOver, 0=Pause, 1=GamePlay
    private GameObject PlayerCharacter, CombatManager, HUDManager;
    private HealthPoints PlayerCharacterHP;
    public string NextLevelName, FirstLevelName;
    //private ActionPoints PlayerCharacterAP;
    //-----------------
    //event dispatchers go here
    public delegate void GameStateChanged(int CurrentGameState, int OldGameState);
    public event GameStateChanged OnGameStateChanged;
    //-----------------
    //methods go here
    public void RestartGame(){
        SceneManager.LoadScene(FirstLevelName);
    }
    public void NextLevel(){
        SceneManager.LoadScene(NextLevelName);
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    //-----------------
    public bool UpdateCombatManageerQueue(GameObject TargetActor, int NextAction){
        if(CombatManager.GetComponent<CombatManager>()!=null){
            return CombatManager.GetComponent<CombatManager>().UpdateActorsDictionary(TargetActor, NextAction);
        }else{
            return false;
        }
    }
    public void AddGameplayTime(int Amount){
        if(CombatManager.GetComponent<CombatManager>()!=null){
            CombatManager.GetComponent<CombatManager>().AddCombatRounds(Amount);
        }
    }
    //----------------
    public void ChangeGameState(int NewGameState){
        if(NewGameState>-2 && NewGameState<2 /*&& CurrentGameState!=-1*/){
            Debug.Log("Changing Game State..");
            var OldGameState = CurrentGameState;
            CurrentGameState = NewGameState;
            if(CurrentGameState==1){
                CombatManager.GetComponent<CombatManager>().UpdateCombatStatus(true);
            }else{
                CombatManager.GetComponent<CombatManager>().UpdateCombatStatus(false);
            }
            if(OnGameStateChanged!=null){
                OnGameStateChanged(CurrentGameState, OldGameState);
                Debug.Log("Changing Game State Has Been Completed");
            }else{
                Debug.Log("Changing Game State Has Been Failed, OnGameStateChanged==null");
            }
        }else{

        }
    }
    private void HandleCompletedRound(int CurrentCombatRounds){
        if(CurrentCombatRounds==0){
            PlayerCharacterHP.DecreaseHealth(999);
        }
    }
    public void MakeGameOver(){
        ChangeGameState(-1);
        CombatManager.GetComponent<CombatManager>().UpdateCombatStatus(false);
    }
    private void UpdatePlayerActionOnStart(){
        if(PlayerCharacter!=null){
            UpdateCombatManageerQueue(PlayerCharacter, PlayerActionOnStart);
        }
    }
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){}
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
                /*PlayerCharacterAP.OnActionDecreased += HandleActionDecreased;
                PlayerCharacterAP.OnActionReset += HandleActionReset;*/
            } 
        }
        //-----------------
        CombatManager = GameObject.FindWithTag("CombatManager");
        if(CombatManager!=null){
            CombatManager.GetComponent<CombatManager>().OnRoundCompleted += HandleCompletedRound;
            CombatManager.GetComponent<CombatManager>().UpdateCombatStatus(true);
            Invoke("UpdatePlayerActionOnStart", 0.15f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
