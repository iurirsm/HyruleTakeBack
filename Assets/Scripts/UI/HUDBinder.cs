using UnityEngine;
using TMPro;

public class HUDBinder : MonoBehaviour
{
    void Start()
    {
        var t = GetComponent<TMP_Text>();
        if (GameManager.I) GameManager.I.BindCoinText(t);
    }
}
