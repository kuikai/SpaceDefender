using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject OptionPanel;
    public bool IsPausePanelActiv;
    void Start()
    {
        if (PausePanel.activeSelf)
        {
            IsPausePanelActiv = false;
            PausePanel.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (PausePanel.activeSelf)
            {
                IsPausePanelActiv = false;
                PausePanel.SetActive(false);
                GameSession.ResumeGame();
            }
            else
            {
                if (!OptionPanel.activeSelf)
                {
                    GameSession.PauseGame(); 
                    PausePanel.SetActive(true) ;
                    IsPausePanelActiv = true;
                }
            }

            if (OptionPanel.activeSelf)
            {
                OptionPanel.SetActive(false);
                IsPausePanelActiv = false;
                GameSession.ResumeGame();
            }
        }
    }

    public void SetPausePanel(bool Setpanel)
    {
      //  IsPausePanelActiv = true;
        PausePanel.SetActive(Setpanel);
        if(Setpanel == false)
        {
            IsPausePanelActiv = false;
            GameSession.ResumeGame();
        }
        else
        {
            IsPausePanelActiv = true;
        }
    }

    public void GotoOptionsPanel()
    {

        PausePanel.SetActive(false);
        OptionPanel.SetActive(true);

    }

    public void OptionPanelToPausepanel()
    {
        PausePanel.SetActive(true);
        OptionPanel.SetActive(false);   
    }
  
}
