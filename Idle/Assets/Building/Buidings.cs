using System;
using System.Collections.Generic;
using Hint;
using Idle.Building;

namespace Idle.Buildings {
    internal class StaticFan {
        public static ulong GetFactorOr0(IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, ETypeHint hint) {
            if (mulityCargo.ContainsKey(hint)) 
                return mulityCargo[hint] / 1000;
            return 0;
        }
    }
    public class Farm: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Farm;
        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication) {
            var res = ProductValueAsValueAndHintSimple(workers, mulityCargo, ETypeHint.MultiplierForFarm, ETypeHint.Food, multiplication);
            return res;
        }

        public Farm(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Farm(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Forest: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Forest;
        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            return ProductValueAsValueAndHintSimple(workers, mulityCargo, ETypeHint.MultiplierForForest, ETypeHint.Wood, multiplication);
        }

        public Forest(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Forest(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Mine: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Mine;
        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            return ProductValueAsValueAndHintSimple(workers, mulityCargo, ETypeHint.MultiplierForMine, ETypeHint.Stone, multiplication);
        }

        public Mine(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Mine(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Granary: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Granary;
        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForGaranary);
            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(this.ProductValue(workers, multiplication) * (1 + factor) * 1000, ETypeHint.MultiplierForFarm)
            };
        }

        public Granary(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Granary(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    
    public class Shrine: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Shrine;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication) {
            return ProductValueAsValueAndHintSimple(workers, mulityCargo, ETypeHint.MultiplierForShrine, ETypeHint.Faith, multiplication);
        }

        public Shrine(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Shrine(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class School: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.School;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, 
            IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForSchool);
            var value = this.ProductValue(workers, multiplication) * (1 + factor) * 1000;

            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForFarm),
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForForest),
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForMine),
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForGaranary)
            };
        }

        public School(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public School(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Smith: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Smith;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication) {
            return ProductValueAsValueAndHintSimple(workers, mulityCargo, ETypeHint.MultiplierForSmith, ETypeHint.Metal, multiplication);
        }

        public Smith(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Smith(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Temple: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Temple;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForTemple);
            var value = this.ProductValue(workers, multiplication) * (1 + factor) * 1000;

            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForShrine)
            };
        }

        public Temple(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Temple(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    
    public class Docks: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Docks;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForDocks);
            var value = this.ProductValue(workers, multiplication) * (1 + factor);

            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(value, ETypeHint.Gold),
                ValueAndHint<ulong>.Factory(value, ETypeHint.Stone),
                ValueAndHint<ulong>.Factory(value, ETypeHint.Food),
                ValueAndHint<ulong>.Factory(value, ETypeHint.Metal),
            };
        }
        public Docks(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Docks(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Mill: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Mill;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForGaranary);
            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(this.ProductValue(workers, multiplication) * (1 + factor) * 1000, ETypeHint.MultiplierForGaranary)
            };
        }

        public Mill(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Mill(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class Factory: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.Factory;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForFactory);
            var value = this.ProductValue(workers, multiplication) * (1 + factor);

            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForSmith)
            };
        }

        public Factory(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public Factory(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
    
    public class College: Building.Building, IBuilding {
        internal IBuilding.EBuildingName _buildingName = IBuilding.EBuildingName.College;

        public override List<ValueAndHint<ulong>> ProductValueAsValueAndHintBuilder(ulong workers, IReadOnlyDictionary<ETypeHint, ulong> mulityCargo, double multiplication = 0) {
            var factor = StaticFan.GetFactorOr0(mulityCargo, ETypeHint.MultiplierForCollege);
            var value = this.ProductValue(workers, multiplication) * (1 + factor);

            return new List<ValueAndHint<ulong>>() {
                ValueAndHint<ulong>.Factory(value, ETypeHint.MultiplierForSchool)
            };
        }

        public College(Action<List<ValueAndHint<ulong>>> lastResAction) : base(lastResAction) {
        }

        public College(Action<List<ValueAndHint<ulong>>> lastResAction, ulong worker) : base(lastResAction, worker) {
        }
    }
}