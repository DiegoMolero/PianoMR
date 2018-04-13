using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A custom handler to show the multiple splashes
/// </summary>
public class InstructionSlate : MonoBehaviour
{
    [Tooltip("List of materials")]
    public List<Material> splashes;

    [Tooltip("Drag Intro_Slate_Card into this game object")]
    public GameObject slate;

    private Animator animator;
    private MeshRenderer slateRenderer;
    private bool isShown;
    private int count;

    // Get references
    private void Awake()
    {
        animator = GetComponent<Animator>();
        slateRenderer = slate.GetComponent<MeshRenderer>();
    }

    // Initialize variables
    private void Start()
    {
        slate.SetActive(true);
        isShown = true;
        count = 0;
    }

    // Play animation
    private void Update()
    {
        if (isShown)
        {
            if(slateRenderer.enabled == false)
            {
                animator.SetBool("Hide", true);
                isShown = false;
            }
        }else
        {
            if (animator.GetBool("Hide"))
            {
                animator.SetBool("Hide", false);
                if(count != splashes.Capacity)
                    slateRenderer.material = splashes[count];
                count++;
            }

            if (count == splashes.Capacity + 1)
                LoadNextScene();

            if (slateRenderer.enabled == true)
                isShown = true;
        }
    }

    /// <summary>
    /// Load next scene
    /// </summary>
    private void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}
