using System.Collections.Generic;
using Idle.Building;
using UnityEditor;

namespace Effect {
    public readonly struct PropMultiplikatorsWorker {
        public readonly Dictionary<IBuilding.EBuildingName, double> Multiplikators;
        public readonly Dictionary<IBuilding.EBuildingName, double> Worker;

        public PropMultiplikatorsWorker(
            Dictionary<IBuilding.EBuildingName, double> multiplikators, 
            Dictionary<IBuilding.EBuildingName, double> worker) {
            
            Multiplikators = multiplikators;
            Worker = worker;
        }

        public static PropMultiplikatorsWorker FactoryDefault() => new PropMultiplikatorsWorker(
                new Dictionary<IBuilding.EBuildingName, double>(0),
                new Dictionary<IBuilding.EBuildingName, double>(0));
    }
}