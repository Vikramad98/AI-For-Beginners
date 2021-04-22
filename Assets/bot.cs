using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class bot : MonoBehaviour
{
    // Start is called before the first frame update

    NavMeshAgent agent;
    public GameObject target;
    Drive ds;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ds = target.GetComponent<Drive>();
    }

    void seek(Vector3 location)
    {
        agent.SetDestination(location);
    }
    void flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position-fleeVector);
    }

    void pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeheading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(this.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ( (toTarget>90 && relativeheading<20) || ds.currentSpeed < 0.01f)
        {
            seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + ds.currentSpeed);

        seek(target.transform.position + target.transform.forward * lookAhead);
    }
    void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + ds.currentSpeed);
        flee(target.transform.position + target.transform.forward * lookAhead);
    }

    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                        0,
                                        Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        seek(targetWorld);
    }

    // Update is called once per frame
    void Update()
    {
        //flee(target.transform.position);
        Wander();
    }
}
