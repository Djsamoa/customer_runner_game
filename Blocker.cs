using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    private Vector3 leftPosition, rightPosition;
    public float leftDistance, rightDistance;

    private SpriteRenderer spriteRenderer;

    private bool isMovingLeft = true;

    public float speed =1f;
    // Start is called before the first frame update
    void Start()
    {
       leftPosition = transform.position + Vector3.left * leftDistance;
         rightPosition = transform.position + Vector3.right * rightDistance; 
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, isMovingLeft ? leftPosition : rightPosition, speed * Time.deltaTime);
         if(isMovingLeft && Vector3.Distance(transform.position, leftPosition) < 0.1f)
         {
              isMovingLeft = false;
              spriteRenderer.flipX = true;
         }
          else if(!isMovingLeft && Vector3.Distance(transform.position, rightPosition) < 0.1f)
          {
                isMovingLeft = true;
                spriteRenderer.flipX = false;
          }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftPosition, rightPosition);
        Gizmos.DrawSphere(leftPosition, 0.1f);
        Gizmos.DrawSphere(rightPosition, 0.1f);
    }
}
