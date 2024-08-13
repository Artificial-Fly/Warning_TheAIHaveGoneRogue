using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string FirstLevelName;
    [SerializeField]
    private GameObject SFXC_ButtonClick_MainMenu;
    //--------------------
    private void ActivateSFXForButtonClickMainMenu(){
        try{
            var SFXController = SFXC_ButtonClick_MainMenu.GetComponent<ActorSFXController>();
            SFXController.StartSFX();
        }catch{
            Debug.Log("Couldn't Start SFX for SFXC_ButtonClick_MainMenu..");
        }
    }
    public void StartGame(){
        ActivateSFXForButtonClickMainMenu();
        SceneManager.LoadScene(FirstLevelName);
    }
    public void QuitGame(){
        ActivateSFXForButtonClickMainMenu();
        Application.Quit();
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
