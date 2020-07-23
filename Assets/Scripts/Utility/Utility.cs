using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class Physics2DUtility
    {
        public static bool IsOnGround(Vector2 position, Collider2D collider)
        {
            var contactPoints = new List<ContactPoint2D>();
            collider.GetContacts(contactPoints);

            foreach (var contact in contactPoints)
            {
                if (contact.point.y < position.y - 0.1f) return true;
            }

            return false;
        }


        public static float KineticEnergy(float mass, float v_magnitude)
        {
            return 0.5f * mass * v_magnitude * v_magnitude;
        }


        public static float KineticEnergy(float mass, Vector2 v)
        {
            return 0.5f * mass * v.magnitude * v.magnitude;
        }
    }
}

