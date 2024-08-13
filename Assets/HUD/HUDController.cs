using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //variables go here
    [SerializeField]
    private AudioClip GameOverMusicClip;
    //[SerializeField]
    //private AudioClip PauseMusicClip;
    [SerializeField]
    private AudioClip UnpauseMusicClip;
    [SerializeField]
    private GameObject BackgroundMusicController;
    [SerializeField]
    private GameObject SFXC_ButtonClick_PlayerControl, SFXC_ButtonClick_Menu;
    //-----------------------------
    private GameObject PlayerCharacter;
    public GameObject ControlPanel_PAUSED_OVER,ControlPanel_UNPAUSED, TimerDisplay, HealthPointsDisplay, UnPauseButton;
    private HealthPoints PlayerCharacterHP;
    //private ActionPoints PlayerCharacterAP;
    private GameManager GameManager;
    //-----------------
    public Slider PlayerCharacterHPSlider;
    public Slider PlayerCharacterAPSlider;
    public TMP_Text TimerText, HealthPointsText, GameOverText;  
    //event dispatchers go here
    //methods go here
    private void ActivateSFXForButtonClickPlayerControl(){
        try{
            var SFXController = SFXC_ButtonClick_PlayerControl.GetComponent<ActorSFXController>();
            SFXController.StartSFX();
        }catch{
            Debug.Log("Couldn't Start SFX for SFXC_ButtonClick_PlayerControl..");
        }
    }
    private void ActivateSFXForButtonClickMenu(){
        try{
            var SFXController = SFXC_ButtonClick_Menu.GetComponent<ActorSFXController>();
            SFXController.StartSFX();
        }catch{
            Debug.Log("Couldn't Start SFX for SFXC_ButtonClick_Menu..");
        }
    }
    public void MainMenu(){
        if(GameManager!=null){
            ActivateSFXForButtonClickMenu();
            GameManager.MainMenu();
        }
    }
    public void RestartGame(){
        if(GameManager!=null){
            ActivateSFXForButtonClickPlayerControl();
            GameManager.RestartGame();
        }
    }
    public void PauseGame(){
        try{
            if(GameManager!=null){
                ActivateSFXForButtonClickPlayerControl();
                GameManager.ChangeGameState(0);
            }else{
                Debug.Log("Error: GameManager==null");
                GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                if(GameManager!=null){
                    GameManager.OnGameStateChanged+=HandleGameStateChanged;
                    Debug.Log("HUD Is Now Listening to Game State's Changeds in GameManager Script");
                    ActivateSFXForButtonClickPlayerControl();
                    GameManager.ChangeGameState(0);
                }
            }
        }catch{
            Debug.Log("Failed to PauseGame");
        }
    }
    public void UnPauseGame(){
        try{
            if(GameManager!=null){
                ActivateSFXForButtonClickPlayerControl();
                GameManager.ChangeGameState(1);
            }else{
                Debug.Log("Error: GameManager==null");
                GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                if(GameManager!=null){
                    GameManager.OnGameStateChanged+=HandleGameStateChanged;
                    Debug.Log("HUD Is Now Listening to Game State's Changeds in GameManager Script");
                    ActivateSFXForButtonClickPlayerControl();
                    GameManager.ChangeGameState(1);
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
        if(GameManager!=null){
            ActivateSFXForButtonClickPlayerControl();
            GameManager.UpdateCombatManageerQueue(PlayerCharacter, NextAction);
        }
    }
    private void HandleGameStateChanged(int CurrentGameState, int OldGameState){
        if(CurrentGameState==1){
            Debug.Log("Game State is Unpaused");
            ControlPanel_PAUSED_OVER.gameObject.SetActive(false);
            ControlPanel_UNPAUSED.gameObject.SetActive(true);
            try{
                var BGMContrller = BackgroundMusicController.GetComponent<ActorSFXController>();
                var BGMAudioSource = BackgroundMusicController.GetComponent<AudioSource>();
                BGMAudioSource.loop = true;
                //
                BGMContrller.UpdateSFX(UnpauseMusicClip, null);
                BGMContrller.StartSFX();
            }catch{
                Debug.Log("Couldn't Start Background Music..");
            }
            //--------------
            //TimerDisplay.gameObject.SetActive(true);
            //HealthPointsDisplay.gameObject.SetActive(true);
        }else if(CurrentGameState==0){
            Debug.Log("Game State is Paused");
            GameOverText.SetText("Pause");//gameObject.SetActive(false);
            ControlPanel_PAUSED_OVER.gameObject.SetActive(true);
            ControlPanel_UNPAUSED.gameObject.SetActive(false);
            /*try{
                var BGMContrller = BackgroundMusicController.GetComponent<ActorSFXController>();
                var BGMAudioSource = BackgroundMusicController.GetComponent<AudioSource>();
                BGMAudioSource.loop = false;
                //
                BGMContrller.UpdateSFX(PauseMusicClip, null);
                BGMContrller.StartSFX();
            }catch{
                Debug.Log("Couldn't Start Background Music..");
            }*/
            //--------------
            //TimerDisplay.gameObject.SetActive(true);
            //HealthPointsDisplay.gameObject.SetActive(true);
        }else if(CurrentGameState==-1){
            Debug.Log("Game State is GameOver");
            GameOverText.SetText("Game Over");//gameObject.SetActive(true);
            ControlPanel_PAUSED_OVER.gameObject.SetActive(true);
            ControlPanel_UNPAUSED.gameObject.SetActive(false);
            try{
                var BGMContrller = BackgroundMusicController.GetComponent<ActorSFXController>();
                var BGMAudioSource = BackgroundMusicController.GetComponent<AudioSource>();
                BGMAudioSource.loop = false;
                //
                BGMContrller.UpdateSFX(GameOverMusicClip, null);
                BGMContrller.StartSFX();
            }catch{
                Debug.Log("Couldn't Start Background Music..");
            }
            //--------------
            //TimerDisplay.gameObject.SetActive(false);
            //HealthPointsDisplay.gameObject.SetActive(false);
            UnPauseButton.SetActive(false);
        }
    }
    private void HandleCompletedRound(int CurrentCombatRounds){
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
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(GameManager!=null){
            GameManager.OnGameStateChanged+=HandleGameStateChanged;
            Debug.Log("HUD Is Now Listening to Game State's Changeds in GameManager Script");
        }
        var CombatCombatManager = GameObject.FindWithTag("CombatManager").GetComponent<CombatManager>(); 
        if(CombatCombatManager!=null){
            CombatCombatManager.OnRoundCompleted += HandleCompletedRound;
        }else{
            Debug.Log("CombatCombatManager=null");
        }
        Invoke("UnPauseGame", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
