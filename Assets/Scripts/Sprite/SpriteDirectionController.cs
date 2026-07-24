using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class SpriteDirectionController : MonoBehaviour
{

    public float BackAngle = 65f;

    public float SideAngle = 155f;

    public Transform ParentTransform;

    public Animator Animator;


    InputAction tempTest; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ParentTransform = transform.parent;
        Animator = GetComponent<Animator>();


        tempTest = InputSystem.actions.FindAction("Attack"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void LateUpdate()
    {
        var camera = GameManager.Instance.GetCurrentCamera(); 
        if (camera == null )
            return;


        Vector3 forwardCamera = new Vector3(camera.transform.forward.x, 0f, camera.transform.forward.z);

        float signedAngle = Vector3.SignedAngle(ParentTransform.forward, forwardCamera, Vector3.up);

        Vector2 animationDirection;// = new Vector2(0f, 0f); 

        float angle = Mathf.Abs(signedAngle);

        if (angle < BackAngle)
        {
            //back animation
            animationDirection = new Vector2(0, -1f);
        }
        else if (angle < SideAngle)
        {
            if (signedAngle < 0)
            {
                animationDirection = new Vector2(-1f, 0f);
            }
            else
            {
                //side animation, right. 
                animationDirection = new Vector2(1f, 0f);
            }

        }
        else
        {
            //show front animation
            animationDirection = new Vector2(0f, 1f);
        }

        Animator.SetFloat("MoveX", animationDirection.x);
        Animator.SetFloat("MoveY", animationDirection.y);

       

    }
}
