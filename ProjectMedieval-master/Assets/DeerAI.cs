using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]

public class DeerAI : MonoBehaviour
{
    public bool ismoving = true;
	public float speed = 1;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;
    public float timer = 0;
    CharacterController controller;
    float heading;
    Vector3 targetRotation;
    public Animation an;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
       
        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        an = GetComponent<Animation>();
        StartCoroutine(NewHeading());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10 && ismoving == true)
        {
            speed = 0;
            an.Stop("WalkCycle");
            an.Play("GrazeCycle");
            ismoving = false;
        }
        else if (timer >= 20 && ismoving == false)
        {
            an.Stop("GrazeCycle");
            an.Play("WalkCycle");
            speed = 1;
            timer = 0;
            ismoving = true;
 
        }



        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}
