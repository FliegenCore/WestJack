using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public interface IMoveProvider
    {
        event Action<Vector2Int> OnMove;
    }
}
