using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //variables go here
    private GameObject PlayerCharacter;
    public GameObject ControlPanel_PAUSED_OVER,ControlPanel_UNPAUSED, TimerDisplay, HealthPointsDisplay;
    private HealthPoints PlayerCharacterHP;
    //private ActionPoints PlayerCharacterAP;
    private GameManager GameManagerScript;
    //-----------------
    public Slider PlayerCharacterHPSlider;
    public Slider PlayerCharacterAPSlider;
    public TMP_Text TimerText, HealthPointsText, GameOverText;  
    //event dispatchers go here
    //methods go here
    public void MainMenu(){
        if(GameManagerScript!=null){
            GameManagerScript.MainMenu();
        }
    }
    public void RestartGame(){
        if(GameManagerScript!=null){
            GameManagerScript.RestartGame();
        }
    }
    public void PauseGame(){
        try{
            if(GameManagerScript!=null){
                GameManagerScript.ChangeGameState(0);
            }else{
                Debug.Log("Error: GameManagerScript==null");
                GameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                if(GameManagerScript!=null){
                    GameManagerScript.OnGameStateChanged+=HandleGameStateChanged;
                    Debug.Log("HUD Is Now Listening to Game State's Changeds in GameManager Script");
                    GameManagerScript.ChangeGameState(0);
                }
            }
        }catch{
            Debug.Log("Failed to PauseGame");
        }
    }
    public void UnPauseGame(){
        try{
            if(GameManagerScript!=null){
                GameManagerScript.ChangeGameState(1);
            }else{
                Debug.Log("Error: GameManagerScript==null");
                GameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                if(GameManagerScript!=null){
                    GameManagerScript.OnGameStateChanged+=HandleGameStateChanged;
                    Debug.Log("HUD Is Now Listening to Game State's Changeds in GameManager Script");
                    GameManagerScript.ChangeGameState(1);
                }
            }
        }catch{
            Debug.Log("Failed to UnPauseGame");
        }
    }
    //---------------
    private void UpdateTimerText(int CurrentCombatRounds){
        if(CurrentCombatRounds<0){
            TimerText.SetText("99");
        }else{
            TimerText.SetText(CurrentCombatRounds.ToString());
        }
    }
    public void UpdatePlayerCharacterNextAction(int NextAction){
        if(GameManagerScript!=null){
            GameManagerScript.UpdateCombatManageerQueue(PlayerCharacter, NextAction);
        }
    }
    private void HandleGameStateChanged(int CurrentGameState, int OldGameState){
        if(CurrentGameState==1){
            Debug.Log("Game State is Unpaused");
            ControlPanel_PAUSED_OVER.gameObject.SetActive(false);
            ControlPanel_UNPAUSED.gameObject.SetActive(true);
            //--------------
            TimerDisplay.gameObject.SetActive(true);
            HealthPointsDisplay.gameObject.SetActive(true);
        }else if(CurrentGameState==0){
            Debug.Log("Game State is Paused");
            GameOverText.SetText("Pause");//gameObject.SetActive(false);
            ControlPanel_PAUSED_OVER.gameObject.SetActive(true);
            ControlPanel_UNPAUSED.gameObject.SetActive(false);
            //--------------
            TimerDisplay.gameObject.SetActive(true);
            HealthPointsDisplay.gameObject.SetActive(true);
        }else if(CurrentGameState==-1){
            Debug.Log("Game State is GameOver");
            GameOverText.SetText("Game Over");//gameObject.SetActive(true);
            ControlPanel_PAUSED_OVER.gameObject.SetActive(true);
            ControlPanel_UNPAUSED.gameObject.SetActive(false);
            //--------------
            TimerDisplay.gameObject.SetActive(false);
            HealthPointsDisplay.gameObject.SetActive(false);
        }
    }
    private void HandleOnCompletedRound(int CurrentCombatRounds){
        Debug.Log("Round Has Been Completed");
        UpdateTimerText(CurrentCombatRounds);
    }
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
        Debug.Log("Health Points Decreased");
        if(PlayerCharacterHPSlider!=null){
            //PlayerCharacterHPSlider.value = DeltaKoef;
            HealthPointsText.SetText(CurrentValue.ToString());
            Debug.Log(DeltaKoef.ToString());
        } 
    }
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){
        Debug.Log("Health Points Increased");
        if(PlayerCharacterHPSlider!=null){
            //PlayerCharacterHPSlider.value = DeltaKoef;
            HealthPointsText.SetText(CurrentValue.ToString());
            Debug.Log(DeltaKoef.ToString());
        } 
    }
    /*private void HandleActionDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
        Debug.Log("Action Points Decreased");
        if(PlayerCharacterAPSlider!=null){
            PlayerCharacterAPSlider.value = DeltaKoef;
            Debug.Log(DeltaKoef.ToString());
        } 
    }
    private void HandleActionReset(int CurrentValue, int MaxValue, float DeltaKoef){
        Debug.Log("Action Points Reset");
        if(PlayerCharacterAPSlider!=null){
            PlayerCharacterAPSlider.value = DeltaKoef;
            Debug.Log(DeltaKoef.ToString());
        } 
    }*/

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
                Debug.Log("HUD is listening to PlayerCharacter's Stats..");
            } 
        }
        GameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(GameManagerScript!=null){
            GameManagerScript.OnGameStateChanged+=HandleGameStateChanged;
            Debug.Log("HUD Is Now Listening to Game State's Changeds in GameManager Script");
        }
        var CombatQueueController = GameObject.FindWithTag("CombatManager").GetComponent<QueueController>(); 
        if(CombatQueueController!=null){
            CombatQueueController.OnRoundCompleted += HandleOnCompletedRound;
        }else{
            Debug.Log("CombatQueueController=null");
        }
        UnPauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
