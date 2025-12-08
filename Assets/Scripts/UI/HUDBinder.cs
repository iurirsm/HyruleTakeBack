using UnityEngine;
using TMPro;

public class HUDBinder : MonoBehaviour
{
    public enum BindType { Coins, Level }

    [SerializeField] BindType bindWhat = BindType.Coins;

    void Start()
    {
        TMP_Text t = GetComponent<TMP_Text>();
        if (GameManager.I == null || t == null) return;

        switch (bindWhat)
        {
            case BindType.Coins:
                GameManager.I.BindCoinText(t);
                break;

            case BindType.Level:
                GameManager.I.BindLevelText(t);
                break;
        }
    }
}
