using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class EntityMovement : MonoBehaviour
{

    NavMeshAgent Agent;


    public float Range = 1;


    InputAction Click;

    InputAction Point; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        Agent.speed = 1.0f;//replace with EntityAttributes value when done. 

        Click =  InputSystem.actions.FindAction("RightClick");
        Point = InputSystem.actions.FindAction("Point"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Click.IsPressed()) //left click. 
        {
            Ray ray = GameManager.Instance.GetCurrentCamera().ScreenPointToRay(Point.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out RaycastHit hit, GameManager.GroundLayer))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, Range, NavMesh.AllAreas))
                {
                    Agent.SetDestination(navMeshHit.position); 
                    
                }    
            }

           
        }
    }
}
