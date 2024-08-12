using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActorAround : MonoBehaviour
{
    private bool RotateActorStatus=true;
    public int RotateByX=0, RotateByY=0, RotateByZ=0, RotateSpeed=3;
    public void RotateActor(float deltaTime){
        transform.Rotate(RotateByX*deltaTime*RotateSpeed,RotateByY*deltaTime*RotateSpeed,RotateSpeed*deltaTime*RotateSpeed);
    }
    private void HandleGameStateChanged(int CurrentGameState, int OldGameState){
        if(CurrentGameState==1){
            RotateActorStatus=true;
        }else{
            RotateActorStatus=false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var GameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(GameManagerScript!=null){
            GameManagerScript.OnGameStateChanged+=HandleGameStateChanged;
        }
        if(RotateActorStatus){
            RotateActor(Time.deltaTime);
        }
    }
}
