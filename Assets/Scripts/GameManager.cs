using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;
   public void Endgame ()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
        Debug.Log("GameOver!");
            //Restarting the game
            Invoke("Restart", restartDelay);       
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
