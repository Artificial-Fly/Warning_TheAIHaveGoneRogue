using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActionsAttack : MonoBehaviour
{
    public GameObject AttackZone;
    public delegate void AttackCompleted();
    public event AttackCompleted OnAttackCompleted;
    public void DefaultAttack(){
        Debug.Log("Attacking!");
        AttackZone.SetActive(true);
        if(OnAttackCompleted!=null){
            OnAttackCompleted();
        }
    }
    public void SuicideAttack(){
        Debug.Log("Attacking!");
        AttackZone.SetActive(true);
        Destroy(gameObject, 0.5f);
        if(OnAttackCompleted!=null){
            OnAttackCompleted();
        }
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
