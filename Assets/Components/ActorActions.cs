using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActions : MonoBehaviour
{
    //variables go here
    public int UpLimit=35, DownLimit=-35, LeftLimit=-35, RightLimit=35;
    public int ActorMoveRange = 10;
    //-----------------
    //event dispatchers go here
    //-----------------
    //methods go here
    public void MoveUP(){
        if(transform.position.y+ActorMoveRange<=UpLimit){
            Debug.Log("Up");
            transform.Translate(0,ActorMoveRange,0);
        }
    }
    public void MoveDOWN(){
        if(transform.position.y-ActorMoveRange>=DownLimit){
            Debug.Log("Down");
            transform.Translate(0,-ActorMoveRange,0);
        }
    }
    public void MoveLEFT(){
        if(transform.position.x-ActorMoveRange>=LeftLimit){
            Debug.Log("Left");
            transform.Translate(-ActorMoveRange,0,0);
        }
    }
    public void MoveRIGHT(){
        if(transform.position.x+ActorMoveRange<=RightLimit){
            Debug.Log("Right");
            transform.Translate(ActorMoveRange,0,0);
        }
    }
    public void Attack(){
        Debug.Log("Attack");
    }
    //-----------------
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
