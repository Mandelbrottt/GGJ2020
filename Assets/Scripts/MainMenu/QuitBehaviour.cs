using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBehaviour : MonoBehaviour
{
    public Button QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = QuitButton.GetComponent<Button>();
        btn.onClick.AddListener(quit);
    }
    private void quit() 
    {
        Application.Quit();
    }
}
