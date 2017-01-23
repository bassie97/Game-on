using UnityEngine;
using System;

public class MentorControllerScript : MonoBehaviour {

    [SerializeField]
    private Sprite[] bubbles;
    [SerializeField]
    private Sprite[] bubblesInverted;

    // Variables
    private Rigidbody2D mentorRigidbody;
    private SpriteRenderer speechBubble;
    private Vector2 target;
    private bool facingRight;
    private int timerSpeech;

    // Use this for initialization
    void Start ()
    {
        mentorRigidbody = GetComponent<Rigidbody2D>();
        speechBubble = GetComponentsInChildren<SpriteRenderer>()[1];
        facingRight = true;
        SetTimerSpeech();
    }
	
	// Update is called once per frame
	void Update () {
        if (PositionReached())
        {
            GetComponent<Animator>().SetBool("isWalking", false);
            mentorRigidbody.velocity = new Vector2(0, mentorRigidbody.velocity.y);
        } 
        if (SpeechTimedOut())
        {
            speechBubble.enabled = false;
        }
    }

    /* 
     * Set the timer for the speech bubble to time out.
     * This is the amount of time that the system has been running plus 10 seconds.
     */
    private void SetTimerSpeech()
    {
        timerSpeech = Environment.TickCount + 10000;
    }

   /* 
    * Method that is called when the mentor reaches a trigger point. 
    * The following happens in this method:
    * 1. The target that the mentor has to walk to is set.
    * 2. The mentor walks to this target.
    * 3. The speech bubble is set and displayed.
    * (Note: Callled from MentorTriggerScript.cs)
    */
    public void Act(Vector2 position, int index)
    {
        target = position;
        Walk();
        Speak(index);        
    }

  /* 
   * Method that is called from the act method. This method causes the mentor to walk 
   * The following happens in this method:
   * 1. The isWalking boolean in the animator component is set to true.
   * 2. The target position is checked and the mentor is flipped if he needs to be.
   */
    private void Walk()
    {
        GetComponent<Animator>().SetBool("isWalking", true);

        if (target.x > mentorRigidbody.position.x)
        {
            if (!facingRight) Flip();
            mentorRigidbody.velocity = new Vector2(2, mentorRigidbody.velocity.y);
        } else if (target.x < mentorRigidbody.position.x)
        {
            if (facingRight) Flip();
            mentorRigidbody.velocity = new Vector2(-2, mentorRigidbody.velocity.y);
        }
    }

  /* 
   * Method that is called from the Update() method.
   * This method checks if the target position is reached.
   */
    private bool PositionReached()
    {
        if (target == null) return false;
        return (Math.Abs(mentorRigidbody.position.x - target.x) <= 1);        
    }

    /* 
     * Method for flipping the mentor's animation
     */
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        facingRight = !facingRight;
        scale.x *= -1;
        transform.localScale = scale;
    }

    /* 
     * Method that makes the metor speaks. 
     * Checks if the speech bubble has to be flipped and does so if this is the case.
     */
    private void Speak(int index)
    {
        
        Transform transformers = GetComponentInChildren<Transform>();
        foreach (Transform transformer in transformers)
        {
            if (transformer.gameObject != this.gameObject)
            {
                if (facingRight && transformer.localScale.x > 0 || !facingRight && transformer.localScale.x < 0)
                    break;
                Vector3 scale = transformer.localScale;
                scale.x *= -1;
                transformer.localScale = scale;
            }
        }

        if (facingRight)
            speechBubble.sprite = bubbles[index];
        else
            speechBubble.sprite = bubblesInverted[index];
        speechBubble.enabled = true;
        SetTimerSpeech();
    }

    /* 
     * Method that is called from the Update() method.
     * This methos checks if the speech's time out timer is reached. (Yeah, this's proper English) 
     */
    private bool SpeechTimedOut()
    {
        return (Environment.TickCount == timerSpeech);
    }
}
