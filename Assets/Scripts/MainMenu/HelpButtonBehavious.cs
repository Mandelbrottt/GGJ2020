using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonBehavious : MonoBehaviour
{
    public Button helpButton;
    public GameObject MainCanvas;
    public GameObject HelpCanvas;
    // Start is called before the first frame update
    void Start()
    {
        HelpCanvas.SetActive(false);
        Button btn = helpButton.GetComponent<Button>();
        btn.onClick.AddListener(onHelpClick);
    }

    private void onHelpClick()
    {
        MainCanvas.SetActive(false);
        HelpCanvas.SetActive(true);

    }
}
