using System;
using System.Collections.Generic;
using Hint;
using Idle.Building;
using Idle.Buildings;
using UnityEngine;

namespace Idle {
    public static class CostTile {
        // TODO Set Cost
        private static Dictionary<IBuilding.EBuildingName, List<ValueAndHint<ulong>>> CostTiles = new Dictionary<IBuilding.EBuildingName, List<ValueAndHint<ulong>>>() {
            {IBuilding.EBuildingName.Farm, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Forest, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Mine, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Granary, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Shrine, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.School, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Smith, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Temple, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Docks, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Mill, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.Factory, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }},
            {IBuilding.EBuildingName.College, new List<ValueAndHint<ulong>>() { ValueAndHint<ulong>.Factory((ulong)2, ETypeHint.Food) }}
        };
        public static IReadOnlyList<ValueAndHint<ulong>> GetCostFor(IBuilding.EBuildingName name) => 
            CostTiles[name] ?? throw new NullReferenceException("name not found in Dictionary");

        public static bool CheckIfCanBuy(IBuilding.EBuildingName name, IReadOnlyDictionary<ETypeHint, ulong> cargo) {
            var costs = GetCostFor(name);
            foreach (ValueAndHint<ulong> cost in costs) {
                if (cargo.ContainsKey(cost.Hint) == false)
                    return false;
                if (cargo[cost.Hint] < cost.Value) return false;
            }

            return true;
        }

        public static void Buy(IBuilding.EBuildingName name, Dictionary<ETypeHint, ulong> cargo) {
            var costs = GetCostFor(name);
            foreach (ValueAndHint<ulong> cost in costs) {
                if (cargo.ContainsKey(cost.Hint) == false)
                    throw new Exception("Key Not Exist:" + cost.Hint.ToString());
                cargo[cost.Hint] -= cost.Value;
            }
        }
    }
}



























