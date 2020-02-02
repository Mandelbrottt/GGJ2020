using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class TerminalBehaviour : MonoBehaviour
{

    public GameObject player;
    public GameObject map;
    public Animator animator;
    public Camera mainCamera;
    private BoxCollider2D playerTrigger;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private bool interactable = false;
    float delay = 2.0f;   //seconds

    Vector3 cameraTarget = new Vector3(0.0f, 0.0f, -10.0f);

    public List<Transform> tileTransforms;

    public bool transitionRunning = false;


    IEnumerator sceneTransitionOut()
    {
        //play animation
        playerAnimator.SetBool("activeTerminal", true);
        playerMovement.enabled = false;
        transitionRunning = true;

        CameraBehaviour cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();

        //set zoom target
        cameraBehaviour.targetOrtho = 20.0f;

        //wait for the animation to finish
        yield return new WaitForSeconds(delay);
       
        transitionRunning = false;
    }
    IEnumerator sceneTransitionIn()
    {
        transitionRunning = true;
        CameraBehaviour cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();

        //play animation
        playerAnimator.SetBool("activeTerminal", false);

        //move the camera back to the player and zoom in
        cameraBehaviour.targetOrtho = 5.0f;


        yield return new WaitForSeconds(0.0f);

        playerMovement.enabled = true;

        transitionRunning = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < map.transform.childCount; i++)
        {
            Transform temp = map.transform.GetChild(i);
            tileTransforms.Add(temp);
        }

        playerTrigger = player.GetComponent<BoxCollider2D>();
        playerAnimator = player.GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        StartCoroutine("sceneTransitionIn");
    }
    // Update is called once per frame  
    void Update()
    {
        CameraBehaviour cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();

        if (Input.GetButtonDown("Use") && interactable)
        {
            cameraBehaviour.transitioning = !cameraBehaviour.transitioning;

            if (cameraBehaviour.transitioning)
                StartCoroutine("sceneTransitionOut");
            else
                StartCoroutine("sceneTransitionIn");

        }
        if (transitionRunning)
            cameraBehaviour.transform.position = Vector3.Lerp(cameraBehaviour.transform.position, cameraTarget, Time.deltaTime * 3.7f);

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, cameraBehaviour.targetOrtho, cameraBehaviour.smoothSpeed * Time.deltaTime);

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
