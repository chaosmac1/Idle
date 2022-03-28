using System.Collections.Generic;
using DefaultNamespace;

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
    }
}