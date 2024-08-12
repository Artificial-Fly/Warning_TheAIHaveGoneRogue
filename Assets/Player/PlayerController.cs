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
    void ResetStats(){
        if (PlayerCharacterHP!=null){
            PlayerCharacterHP.IncreaseHealth(999);
            Debug.Log("PlayerCharacter's Stats Have Been Reset");
        }
    }
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
        if(!(CurrentValue>PlayerCharacterHP.MinValue)){
            Debug.Log("Game Over");
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().MakeGameOver();
            Destroy(gameObject);
        }
    }
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("PlayerCharacter");
        PlayerCharacterHP = GameObject.FindWithTag("PlayerCharacter").GetComponent<HealthPoints>();
        if(PlayerCharacter==null){
            Debug.Log("Can't find PlayerCharacter");
        }else{
            if(PlayerCharacterHP==null){
            Debug.Log("Can't find PlayerCharacter's health points entity resource");
            }else if (PlayerCharacterHP!=null){
                PlayerCharacterHP.OnHealthDecreased += HandleHealthDecreased;
                PlayerCharacterHP.OnHealthIncreased += HandleHealthIncreased;
                Invoke("ResetStats",0.3f);
            } 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
