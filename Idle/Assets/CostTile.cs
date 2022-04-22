using Idle.Building;
using UnityEngine;

namespace Idle {
    public class CostTile : MonoBehaviour {
        public (
            IBuilding.EBuildingName Name, 
            int Food,
            int Wood,
            int Stone,
            int Metal,
            int Faith,
            int Gold
            )[] BuildingCost;
    }
}