using UnityEngine;
using UnityEngine.UI;

public class InstructionsPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;

    [Header("Settings")]
    [SerializeField] bool hideOnStart = true;

    void Start()
    {
        if (hideOnStart && instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }

        if (openButton != null)
        {
            openButton.onClick.AddListener(ShowInstructions);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideInstructions);
        }
    }

    public void ShowInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
    }

    public void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }

    public void ToggleInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(!instructionsPanel.activeSelf);
        }
    }
}
