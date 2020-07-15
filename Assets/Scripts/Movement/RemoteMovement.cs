using UnityEngine;

namespace Movement
{
    public class RemoteMovement : MonoBehaviour, IMovement
    {
        [SerializeField] public GameObject ControlledObject;

        public void Move(Vector2 direction)
        {
            if (ControlledObject == null) return;

            var movement = ControlledObject.GetComponent<IMovement>();

            movement?.Move(direction);
        }
    }
}