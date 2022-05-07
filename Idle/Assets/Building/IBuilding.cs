using System;
using System.Collections.Generic;
using Hint;

namespace Idle.Building {
    public interface IBuilding {
        internal Action<List<ValueAndHint<ulong>>> lastResAction { get; }
        public EBuildingName BuildingName { get; set; }
    
        public ulong Worker { get; }
        
        public void AddWorker(int value);

        public void DropWorker(int value);

        /// <summary>
        /// Result Value From Workers
        /// workers * (ulong)((multiplication + 1) * 1000) / 1000
        /// </summary>
        /// <param name="multiplication"> multiplication + 1 </param>
        public ulong ProductValue(ulong workers, double multiplication = 0); 
            

        public List<Hint.ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers,
            IReadOnlyDictionary<ETypeHint, ulong> productCargo, double multiplication = 0);

        public List<Hint.ValueAndHint<ulong>> ProductValueAsValueAndHint(ulong workers,
            IReadOnlyDictionary<ETypeHint, ulong> productCargo, double multiplication) {
            var res = ProductValueAsValueAndHintBuilder(workers, productCargo, multiplication);
            this.lastResAction(res);
            return res;
        }
            

        public enum EBuildingName {
            Farm,
            Forest,
            Mine,
            Granary,
            Shrine,
            School,
            Smith,
            Temple,
            Docks,
            Mill,
            Factory,
            College,
        }
    }
}