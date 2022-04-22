using Idle.Building;
using UnityEngine;

namespace Idle {
    public class MatrialHolder : MonoBehaviour {
        public (IBuilding.EBuildingName name, Material materialBg, Material materialLogo)[] TileMaterialMapper;
    }
}