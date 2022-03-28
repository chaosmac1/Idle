using System;

namespace DefaultNamespace {
    public interface IBuilding {
        public EBuildingName BuildingName { get; }

        public ulong Worker { get; }

        public void AddWorker(int value);

        public void DropWorker(int value);

        /// <summary>
        /// Result Value From Workers
        /// workers * (ulong)((multiplication + 1) * 1000) / 1000
        /// </summary>
        /// <param name="multiplication"> multiplication + 1 </param>
        /// <exception cref="Exception"></exception>
        public static ulong ProductValue(ulong workers, double multiplication = 0) 
            => workers * (ulong) ((multiplication + 1) * 1000) / 1000;
    
        public enum EBuildingName {
            Farm,
            Forest,
            Mine,
            Granary,
            Shrine,
            School,
            Smith,
            Temple
        }
    }
}