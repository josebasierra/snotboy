using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class UnderControlHighlighter : Highlighter
    {
        protected override Material DefaultHighlightMaterial => GameManager.Instance().GetUnderControlMaterial();
    }
}