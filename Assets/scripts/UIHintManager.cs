using UnityEngine;
using TMPro;

public class UIHintManager : MonoBehaviour
{
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI hintText;

    void Start()
    {
        controlsText.text = "";
        hintText.text = "";
    }

    public void ShowHint(string message)
    {
        hintText.text = message;
    }

    public void HideHint()
    {
        hintText.text = "";
    }
}
