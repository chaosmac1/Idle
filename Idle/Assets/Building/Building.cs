using System;
using System.Collections.Generic;
using Hint;

namespace Idle.Building {
    public abstract class Building: IBuilding {
        internal ulong _worker;

        public ulong Worker {  get => _worker; }
        
        internal IBuilding.EBuildingName _buildingName;
        
        private Action<List<ValueAndHint<ulong>>> _lastResAction;
        Action<List<ValueAndHint<ulong>>> IBuilding.lastResAction => _lastResAction;
        
        public IBuilding.EBuildingName BuildingName { get => _buildingName; set => _buildingName = value; }
        
        public Building(Action<List<ValueAndHint<ulong>>> lastResAction) {
            _lastResAction = lastResAction;
        }

        public Building(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) {
            _lastResAction = lastResAction;
            _worker = worker;
        }
        
        

        
        

        
        

        public void AddWorker(int value) => _worker += (ulong)value;

        public void DropWorker(int value) => _worker -= (ulong) value;

        /// <summary>
        /// Result Value From Workers
        /// workers * (ulong)((multiplication + 1) * 1000) / 1000
        /// </summary>
        /// <param name="multiplication"> multiplication + 1 </param>
        public ulong ProductValue(ulong workers, double multiplication)
            => workers * (ulong) (multiplication * 1000) / 1000;

        public abstract List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers,
            IReadOnlyDictionary<ETypeHint, ulong> productCargo, double multiplication = 0);

        public List<ValueAndHint<ulong>> ProductValueAsValueAndHintSimple(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> multiCargo,
            ETypeHint multiplierHint, ETypeHint multiplierOut, double multiplication) {
            
            ulong factor = 0;
            if (multiCargo.ContainsKey(multiplierHint)) {
                factor = multiCargo[multiplierHint] / 1000;
            }

            var value = this.ProductValue(workers, multiplication) * (1 + factor);
            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(value, multiplierOut)
            };
        }
    }
}