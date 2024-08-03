using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoints : MonoBehaviour
{
    //variables go here
    private int CurrentValue=0;
    public int MaxValue=3, MinValue=0;
    //-----------------
    //event dispatchers go here
    public delegate void ActionDecreased(int CurrentValue, int MaxValue, float DeltaKoef);
    public event ActionDecreased OnActionDecreased;
    public delegate void ActionReset(int CurrentValue, int MaxValue, float DeltaKoef);
    public event ActionReset OnActionReset;
    //methods go here
    public bool DecreaseAction(int Amount){
        bool success = false;
        if(Amount>0){
            if((CurrentValue-Amount)>MinValue){
                CurrentValue=CurrentValue-Amount;
            }else{
                CurrentValue=MinValue;
            }
            if(OnActionDecreased!=null){
                OnActionDecreased(CurrentValue, MaxValue, DeltaKoef());
                success=true;
            }
        }
        return success;
    }
    public bool ResetAction(){
        bool success = false;
        CurrentValue=MaxValue;
        if(OnActionReset!=null){
                OnActionReset(CurrentValue, MaxValue, DeltaKoef());
                success=true;
            }
        return success;
    }
    public float DeltaKoef(){
        string d = "Action Points: " + CurrentValue.ToString() + " / " + MaxValue.ToString();
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
