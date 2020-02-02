using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
    public Button MenuButton;
    public GameObject MainCanvas;
    public GameObject HelpCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = MenuButton.GetComponent<Button>();
        btn.onClick.AddListener(onHelpClick);
    }

    private void onHelpClick()
    {
        MainCanvas.SetActive(true);
        HelpCanvas.SetActive(false);
    }
}
