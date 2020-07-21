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
                if (contact.point.y < position.y) return true;
            }

            return false;
        }
    }
}

