using System;
using System.Collections.Generic;
using DefaultNamespace;
using Effect;
using Hint;

#nullable enable

namespace Save {
    [Serializable]
    public class SaveFile {
        public (ulong Worker, IBuilding.EBuildingName EBuildingName)?[,] Tiles;
        public Dictionary<ETypeHint, ulong> Cargo;
        public DateTime LastSave;
        public Lazy<PassiveEffect.EPassiveEffects> PassiveEffectsList;

        public SaveFile((ulong Worker, IBuilding.EBuildingName EBuildingName)?[,] tiles, Dictionary<ETypeHint, ulong> cargo, DateTime lastSave, Lazy<PassiveEffect.EPassiveEffects> passiveEffectsList) {
            Tiles = tiles;
            Cargo = cargo;
            LastSave = lastSave;
            PassiveEffectsList = passiveEffectsList;
        }
    }
}