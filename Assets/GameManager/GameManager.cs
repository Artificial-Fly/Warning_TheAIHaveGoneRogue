using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables go here
    private GameObject PlayerCharacter;
    private HealthPoints PlayerCharacterHP;
    private ActionPoints PlayerCharacterAP;
    //-----------------
    //event dispatchers go here
    //methods go here
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleActionDecreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    private void HandleActionReset(int CurrentValue, int MaxValue, float DeltaKoef){}
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
