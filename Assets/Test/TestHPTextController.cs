using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestHPTextController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<HealthPoints>().OnHealthDecreased += HandleHealthDecreased;
        GameObject.FindWithTag("Player").GetComponent<HealthPoints>().OnHealthIncreased += HandleHealthIncreased;
        Debug.Log("HPTEXT_START");
        GameObject.FindWithTag("Player").GetComponent<HealthPoints>().DecreaseHealth(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void HandleHealthDecreased(int CurrentHealth, int MaxHealth, float HealthKoef){
        //this.GetComponent<TextMeshPro>().SetText("HP: {0}/{1}",CurrentHealth,MaxHealth);
        Debug.Log("HPTEXT_HPDEC");
    }
    private void HandleHealthIncreased(int CurrentHealth, int MaxHealth, float HealthKoef){
        //this.GetComponent<TextMeshPro>().SetText("HP: {0}/{1}",CurrentHealth,MaxHealth);
        Debug.Log("HPTEXT_HPINC");
    }
}
