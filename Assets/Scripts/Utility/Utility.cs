using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class Physics2DUtility
    {

        //TODO: Pass layer
        public static bool IsOnGround(Vector2 position, Collider2D collider)
        {
            float xOffset = collider.bounds.extents.x;
            float yOffset = collider.bounds.extents.y;

            var hitDown = Physics2D.Raycast(position + new Vector2(0, -yOffset), Vector2.down, 0.1f);
            var hitDownLeft = Physics2D.Raycast(position + new Vector2(-xOffset, -yOffset), Vector2.down, 0.1f);
            var hitDownRight = Physics2D.Raycast(position + new Vector2(xOffset, -yOffset), Vector2.down, 0.1f);

            return hitDown.collider != null || hitDownLeft.collider != null || hitDownRight.collider != null;
        }
    }
}

