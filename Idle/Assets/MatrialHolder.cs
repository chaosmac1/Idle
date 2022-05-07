using System;
using Idle.Building;
using UnityEngine;

namespace Idle {
    public class MatrialHolder : MonoBehaviour {
        [SerializeField]
        public IBuilding.EBuildingName[] Name;
        [SerializeField]
        public Sprite[] MaterialBg;
        [SerializeField]
        public Sprite[] MaterialLogo;
        
        public (IBuilding.EBuildingName name, Sprite materialBg, Sprite materialLogo)[] TileMaterialMapper {
            get {
                if (Name.Length != MaterialBg.Length || MaterialBg.Length != MaterialLogo.Length) {
                    throw new Exception("MatrialHolder Not Eq Size");
                } 
                var res = new (IBuilding.EBuildingName name, Sprite materialBg, Sprite materialLogo)[Name.Length];

                for (var i = 0; i < res.Length; i++) {
                    res[i] = (Name[i], MaterialBg[i], MaterialLogo[i]);
                }
                
                return res;
            } }
    }
}