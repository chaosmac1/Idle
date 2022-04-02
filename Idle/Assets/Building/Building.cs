namespace Idle.Building {
    public abstract class Building {
        internal ulong _worker;
        internal IBuilding.EBuildingName _buildingName;
    
        public IBuilding.EBuildingName BuildingName { get => _buildingName; }

        public void AddWorker(int value) => _worker += (ulong)value;

        public void DropWorker(int value) => _worker -= (ulong) value;
    }
}