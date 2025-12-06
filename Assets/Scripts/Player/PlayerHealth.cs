using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    bool dead = false;
    GameOverUI ui;

    void Start()
    {
        ui = FindFirstObjectByType<GameOverUI>(FindObjectsInactive.Include);
    }

    public void Kill()
    {
        if (dead) return;
        dead = true;

        if (StarRating.Instance != null)
            StarRating.Instance.OnPlayerDamaged();

        if (ui) ui.Show();
        else UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
