using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActorAround : MonoBehaviour
{
    public int RotateByX=0, RotateByY=0, RotateByZ=0, RotateSpeed=3;
    public void RotateActor(float deltaTime){
        transform.Rotate(RotateByX*deltaTime*RotateSpeed,RotateByY*deltaTime*RotateSpeed,RotateSpeed*deltaTime*RotateSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(false){
            RotateActor(Time.deltaTime);
        }
    }
}
