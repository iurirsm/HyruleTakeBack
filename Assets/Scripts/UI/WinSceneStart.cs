using UnityEngine;

public class WinSceneStart : MonoBehaviour
{
    [SerializeField] WinUI winUI;

    void Start()
    {
        winUI.Show();
    }

}
