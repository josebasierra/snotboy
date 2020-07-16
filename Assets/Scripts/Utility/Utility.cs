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
            float yOffset = collider.bounds.extents.y;
            
            var hitDown = Physics2D.Raycast(position, Vector2.down, yOffset + 0.2f);
            var hitDownLeft = Physics2D.Raycast(position, new Vector2(-0.5f, -0.5f), yOffset + 0.2f);
            var hitDownRight = Physics2D.Raycast(position, new Vector2(0.5f, -0.5f), yOffset + 0.2f);

            return hitDown.collider != null || hitDownLeft.collider != null || hitDownRight.collider != null;
        }
    }
}

