using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    bool dead = false;
    GameOverUI ui;

    void Start()
    {
        ui = FindObjectOfType<GameOverUI>(includeInactive: true);
    }

    public void Kill()
    {
        if (dead) return;
        dead = true;
        if (ui) ui.Show();
        else UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); 
    }
}
