using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource AudioEffect;
    public ParticleSystem ParticleEffect;
    private bool IsSFXPaused=false;
    private bool IsSFXStarted=false;
    public void StartSFX(){
        if(AudioEffect!=null){
            AudioEffect.Play();
        }
        if(ParticleEffect!=null){
            ParticleEffect.Play();
        }
        IsSFXStarted=true;
    }
    private void HandleGameStateChanged(int CurrentGameState, int OldGameState){
        if(CurrentGameState!=OldGameState){
            if(CurrentGameState==1){
                if(IsSFXPaused && IsSFXStarted){
                    if(AudioEffect!=null){
                        AudioEffect.Play();
                    }
                    if(ParticleEffect!=null){
                        ParticleEffect.Play();
                    }
                    IsSFXPaused=false;
                }
            }else{
                if(!IsSFXPaused && IsSFXStarted){
                    if(AudioEffect!=null){
                        AudioEffect.Pause();
                    }
                    if(ParticleEffect!=null){
                        ParticleEffect.Pause();
                    }
                    IsSFXPaused=true;
                }
                
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(GameManager!=null){
            GameManager.OnGameStateChanged+=HandleGameStateChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
