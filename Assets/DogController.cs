using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    public GameObject TargetBall;
    public GameObject TargetGoal;
    private GameObject target;
    private bool retrieve;
    private  GameObject ball;
    private bool readyToThrow;
    NavMeshAgent navmeshagent;
    private GameObject child;


    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        navmeshagent = GetComponent<NavMeshAgent>();
        target = TargetBall;
        readyToThrow = true;
        retrieve = false;
    }

    // Update is called once per frame
    void Update()
    {
        //navmeshagent.SetDestination(target.transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            if (readyToThrow)
            {
                ball.GetComponent<BallController>().Throw(new Vector3(Random.Range(-4.0f,4.0f), Random.Range(7.0f,14.0f), Random.Range(7.0f,14.0f)));
                readyToThrow = false;
            }
            retrieve = true;
            if ((child != null) && (child.tag != "BallTag"))
            {
                Destroy(child);
                child = new GameObject();
            }
        }
        if (retrieve)
        {
            Retrieve();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == TargetBall && !(other.gameObject.tag == "GoalTag"))
        {
            if (other.gameObject.tag == "BallTag")
            {
                ball.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (other.gameObject.tag == "BallTag" || other.gameObject.tag == "BarTag" || other.gameObject.tag == "CanTag")
            {
                target = TargetGoal;
                other.gameObject.transform.parent = this.gameObject.transform;
                other.gameObject.transform.localPosition = new Vector3(0, 0, 0.7f);
            }
        }
        else if (target == TargetGoal && other.gameObject.tag == "GoalTag")
        {
            child = transform.GetChild(1).gameObject;
            child.transform.parent = null;
            child.transform.position = new Vector3(0, 0.1f, -26.0f);
            target = TargetBall;
            if (child.tag == "BallTag")
            {
                readyToThrow = true;
            }
            retrieve = false;
        }
    }

    private void Retrieve()
    {
        navmeshagent.SetDestination(target.transform.position);
    }
}
