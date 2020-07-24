using Interactables;
using UnityEngine;

public class FanAnimationController : MonoBehaviour
{
    
    private Animator animator;
    private SwitchInteractable interactable;
    private static readonly int IsActive = Animator.StringToHash("isActive");

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        interactable = GetComponent<SwitchInteractable>();
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetBool(IsActive, interactable.State);
    }
}
