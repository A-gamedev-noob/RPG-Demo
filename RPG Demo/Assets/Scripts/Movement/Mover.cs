using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Camera cam;
    [SerializeField]Transform target;
    public LayerMask terrain;
    Animator animator;

    Ray ray;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            navMeshAgent.SetDestination(GetMousePosition());
        }
        UpdateAnimator();
    }

    Vector3 GetMousePosition(){
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hashit  = Physics.Raycast(ray, out hit,500f,terrain);
        if(hashit)
            return hit.point;
        return transform.position;
    }

    void UpdateAnimator(){
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);              //Converts global space to local space

        animator.SetFloat("ForwardSpeed",localVelocity.z);
    }

}
