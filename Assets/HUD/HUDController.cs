using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //variables go here
    private GameObject PlayerCharacter;
    private HealthPoints PlayerCharacterHP;
    private ActionPoints PlayerCharacterAP;
    //-----------------
    public Slider PlayerCharacterHPSlider;
    public Slider PlayerCharacterAPSlider;
    //event dispatchers go here
    //methods go here
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
        Debug.Log("Health Points Decreased");
        if(PlayerCharacterHPSlider!=null){
            PlayerCharacterHPSlider.value = DeltaKoef;
            Debug.Log(DeltaKoef.ToString());
        } 
    }
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){
        Debug.Log("Health Points Increased");
        if(PlayerCharacterHPSlider!=null){
            PlayerCharacterHPSlider.value = DeltaKoef;
            Debug.Log(DeltaKoef.ToString());
        } 
    }
    private void HandleActionDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
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
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("Player");
        PlayerCharacterHP = GameObject.FindWithTag("Player").GetComponent<HealthPoints>();
        PlayerCharacterAP = GameObject.FindWithTag("Player").GetComponent<ActionPoints>();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
