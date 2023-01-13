using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // ボールを投げる
    public void Throw(Vector3 direction)
    {
        ball.isKinematic = false;
        ball.AddForce(direction, ForceMode.Impulse);
    }
}
