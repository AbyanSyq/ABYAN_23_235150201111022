using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Animator anim;
    private void Awake() {
        Time.timeScale = 1;
    }
    public void startGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartSetUpTankAnimation(){
        Debug.Log("called");
        anim.SetBool("SetUpTank",true);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
