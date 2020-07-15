using System;
using UnityEngine;

namespace Movement
{
    public class FlyingMovement : MonoBehaviour, IMovement
    {
        [SerializeField] float MaxHeight = 3;
        [SerializeField] float Speed = 5;
        [SerializeField] private bool CanMoveItself = false;

        private Rigidbody2D myRigidbody;
        private float initialHeight; 
        
        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            
            initialHeight = gameObject.transform.position.y;
        }

        private bool ControlledDirectly()
        {
            var controller = gameObject.GetComponent<Controllable>();

            return controller != null && controller.IsBeignControlled();
        }

        private void OnDrawGizmos()
        {
            Vector2 start = gameObject.transform.position;
            Vector2 end = (Vector2)gameObject.transform.position + new Vector2(0, MaxHeight);
            Debug.DrawLine(start, end, Color.magenta);
        }

        public void Move(Vector2 direction)
        {
            if (ControlledDirectly() && !CanMoveItself) return;

            myRigidbody.velocity = direction.normalized * Speed;

            var maxHeight = initialHeight + MaxHeight;
            if (gameObject.transform.position.y > maxHeight)
            {
                var o = gameObject;
                o.transform.position = new Vector2(o.transform.position.x, maxHeight);
            }
        }
    }
}