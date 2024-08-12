using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActionsAttack : MonoBehaviour
{
    public GameObject AttackZone;
    public void DefaultAttack(){
        Debug.Log("Attacking!");
        AttackZone.SetActive(true);
    }
    public void SuicideAttack(){
        Debug.Log("Attacking!");
        AttackZone.SetActive(true);
        Destroy(gameObject, 0.5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
