using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables go here
    private GameObject CurrentEnemyCharacter;
    private HealthPoints CurrentEnemyCharacterHP;
    private bool isQuitting=false;
    [SerializeField]
    private GameObject ObstaclePrefab;
    //private ActionPoints CurrentEnemyCharacterAP;
    //-----------------
    //event dispatchers go here
    //methods go here
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy(){
        try{
            GameObject.FindWithTag("CombatManager").GetComponent<CombatManager>().DeleteActorFromActorsDictionary(transform.gameObject);
            if (!isQuitting)
            {
                //Spawn Obstacle at Kamikadze location on destroy
                Instantiate(ObstaclePrefab, transform.position, transform.rotation);
            }
        }
        catch{

        }
    }
    void ResetStats(){
        if (CurrentEnemyCharacterHP!=null){
            CurrentEnemyCharacterHP.IncreaseHealth(999);
            Debug.Log("CurrentEnemyCharacter's Stats Have Been Reset");
        }
    }
    private void HandleHealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef){
        if(!(CurrentValue>CurrentEnemyCharacterHP.MinValue)){
            Destroy(gameObject);
        }
    }
    private void HandleHealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef){}
    // Start is called before the first frame update
    void Start()
    {
        CurrentEnemyCharacter = transform.gameObject;
        CurrentEnemyCharacterHP = transform.gameObject.GetComponent<HealthPoints>();
        if(CurrentEnemyCharacter==null){
            Debug.Log("Can't find CurrentEnemyCharacter");
        }else{
            if(CurrentEnemyCharacterHP==null){
            Debug.Log("Can't find CurrentEnemyCharacter's health points entity resource");
            }else if (CurrentEnemyCharacterHP!=null){
                CurrentEnemyCharacterHP.OnHealthDecreased += HandleHealthDecreased;
                CurrentEnemyCharacterHP.OnHealthIncreased += HandleHealthIncreased;
                Invoke("ResetStats",0.3f);
            } 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
