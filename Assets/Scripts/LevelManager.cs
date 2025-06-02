using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;

    [SerializeField] GameObject panelLogin;
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_Dropdown dropdown;

    private void Start()
    {
        StartCoroutine(initDataDropDown());
    }

    public IEnumerator initDataDropDown()
    {
        yield return new WaitForSeconds(1f);
        bool start = true;
        if (dropdown == null) start = false;

        if (start)
        {
            dropdown.ClearOptions();

            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < ASM_MN.Instance.listRegion.Count; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(ASM_MN.Instance.listRegion[i].Name));
            }
            dropdown.AddOptions(options);
        }
    }

    public void login()
    {
        panelLogin.SetActive(true);
        userName.text = "";
    }
    public void closeLogin()
    {
        panelLogin.SetActive(false);
    }

    public void LoadGame()
    {
        if (userName.text.Trim().Length == 0) return;
        ScoreKeeper.Instance.ResetScore(userName.text, dropdown.value);
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
