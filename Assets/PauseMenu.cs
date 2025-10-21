using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);     // Megjelenik a menü
        Time.timeScale = 0f;             // Megállítja a játékot
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);    // Eltűnik a menü
        Time.timeScale = 1f;             // Folytatja a játékot
        isPaused = false;
    }

    public void Fomenu()
    {
        Time.timeScale = 1f; // <<< kulcsfontosságú
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // <<< FONTOS!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
