using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject PerksMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        OptionsMenu.SetActive(true);

        gameObject.SetActive(false);
    }

    public void OpenPerksMenu()
    {
        PerksMenu.SetActive(true);

        gameObject.SetActive(false);
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
