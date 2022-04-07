using System;
using Hint;

namespace Idle.Building {
    public interface IBuilding {
        public EBuildingName BuildingName { get; }

        public ulong Worker { get; }
        
        public void AddWorker(int value);

        public void DropWorker(int value);

        public Hint.ETypeHint Hint { get; }
        
        /// <summary>
        /// Result Value From Workers
        /// workers * (ulong)((multiplication + 1) * 1000) / 1000
        /// </summary>
        /// <param name="multiplication"> multiplication + 1 </param>
        public virtual ulong ProductValue(ulong workers, double multiplication = 0) 
            => workers * (ulong) ((multiplication + 1) * 1000) / 1000;

        public virtual Hint.ValueAndHint<ulong> ProductValueAsValueAndHint(ulong workers, double multiplication = 0)
            => ValueAndHint<ulong>.Factory(ProductValue(workers, multiplication), Hint);

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