using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator; // Assign your Animator component
    public string animationName; // The name of the animation state you want to play

   
    public void playanimation()
    {
        animator.Play(animationName, -1, 0f);
    }
}
