using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class TerminalBehaviour : MonoBehaviour
{

    public BoxCollider2D playerTrigger;
    public Animator animator;

    public Animator playerAnimator;

    private bool interactable = false;
    float delay = 1.0f;   //seconds


    IEnumerator sceneTransition()
    {
        //play animation
        playerAnimator.SetBool("activeTerminal", true);
        //wait for the animation to finish
        yield return new WaitForSeconds(delay);
        //load the next scene
        SceneManager.LoadScene("TileSliderScene");
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame  
    void Update()
    {
        if (Input.GetButtonDown("Use") && interactable)
        {
            StartCoroutine("sceneTransition");
        }
    }
    private void OnTriggerEnter2D(Collider2D playerTrigger)
    {
        animator.SetBool("TerminalProxTrigger", true);
        interactable = true;

    }
    private void OnTriggerExit2D(Collider2D playerTrigger)
    {
        animator.SetBool("TerminalProxTrigger", false);
        interactable = false;
    }
}
