using Core.World;
using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public interface IMoveProvider
    {
        event Action<Vector2Int> OnMove;
        FloorController FloorController { get; }

        void Init(FloorController floorController);
    }
}
