using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    //variables go here
    private int CurrentValue=0;
    public int MaxValue=10, MinValue=0;
    //-----------------
    //event dispatchers go here
    public delegate void HealthDecreased(int CurrentValue, int MaxValue, float DeltaKoef);
    public event HealthDecreased OnHealthDecreased;
    public delegate void HealthIncreased(int CurrentValue, int MaxValue, float DeltaKoef);
    public event HealthIncreased OnHealthIncreased;
    //methods go here
    public bool DecreaseHealth(int Amount){
        //Debug.Log("DecreaseHealth start");
        bool success = false;
        if(Amount>0){
            if((CurrentValue-Amount)>MinValue){
                    CurrentValue=CurrentValue-Amount;
                }else{
                    CurrentValue=MinValue;
                }
                if (OnHealthDecreased != null)
                {
                    OnHealthDecreased(CurrentValue, MaxValue, DeltaKoef());
                    success=true;
                }
                //Debug.Log("Success!");
        }
        //Debug.Log("DecreaseHealth end");
        return success;
    }

    public bool IncreaseHealth(int Amount){
        //
        Debug.Log("IncreaseHealth start");
        bool success = false;
        if(Amount>0){
            if((CurrentValue+Amount)>MaxValue){
                    CurrentValue=MaxValue;
                }else{
                    CurrentValue=CurrentValue+Amount;
                }
                if (OnHealthIncreased != null)
                {
                    OnHealthIncreased(CurrentValue, MaxValue, DeltaKoef());
                    success=true;
                }
                //Debug.Log("Success!");
        }
        //Debug.Log("IncreaseHealth end");
        return success;
    }

    public float DeltaKoef(){
        string d = "Health Points: " + CurrentValue.ToString() + " / " + MaxValue.ToString();
        Debug.Log(d);
        return CurrentValue/MaxValue;
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
