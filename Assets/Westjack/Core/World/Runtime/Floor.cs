using System.Collections.Generic;
using UnityEngine;

namespace Core.World
{
    public class Floor : MonoBehaviour
    {
        [SerializeField] private List<Tile> m_Tiles;

        public IEnumerable<Tile> Tiles => m_Tiles;
    }
}
