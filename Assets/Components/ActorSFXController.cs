using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSFXController : MonoBehaviour
{
    [SerializeField]
    private bool StopSFXOnPause_GameOver=true;
    [SerializeField] 
    private AudioClip AudioEffect;
    [SerializeField]
    private ParticleSystem ParticleEffect;
    private bool IsSFXPaused=false;
    private AudioSource AudioSourceComponent;
    //------------------------------
    public bool UpdateSFX(AudioClip NewAudioEffect, ParticleSystem NewParticleEffect){
        try{
            if(NewAudioEffect==null || NewParticleEffect==null){return false;}
            AudioEffect=NewAudioEffect;
            ParticleEffect=NewParticleEffect;
            return true;
        }
        catch{
            return false;
        }
    }
    public void StartSFX(){
        try{
            if(AudioEffect!=null){
                AudioSourceComponent.clip = AudioEffect;
                AudioSourceComponent.Play();
            }
            if(ParticleEffect!=null){
                ParticleEffect.Play();
            }
        }
        catch{

        }
    }
    private void HandleGameStateChanged(int CurrentGameState, int OldGameState){
        try{
            if(CurrentGameState!=OldGameState && StopSFXOnPause_GameOver){
                if(CurrentGameState==1){
                    if(IsSFXPaused){
                        if(AudioSourceComponent.clip!=null){
                            AudioSourceComponent.Play();
                        }
                        if(ParticleEffect!=null){
                            ParticleEffect.Play();
                        }
                        IsSFXPaused=false;
                    }
                }else{
                    if(!IsSFXPaused){
                        if(AudioSourceComponent.clip!=null){
                            AudioSourceComponent.Pause();
                        }
                        if(ParticleEffect!=null){
                            ParticleEffect.Pause();
                        }
                        IsSFXPaused=true;
                    }
                    
                }
            }
        }
        catch{

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(GameManager!=null){
            GameManager.OnGameStateChanged+=HandleGameStateChanged;
        }
        AudioSourceComponent = transform.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
