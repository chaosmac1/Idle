using System;
using System.Collections.Generic;
using Effect;
using Hint;
using Idle.Building;
using Time;
using Unity.Mathematics;
using Save;
using UnityEditor;
using static Idle.Utils.Utils;

using UnityEngine;
#nullable enable
namespace Idle {
    public class Map: MonoBehaviour {
        [SerializeField] public GameObject? taget;
        [SerializeField] public int2 size;
        public Dictionary<ETypeHint, ulong>? Cargo { get; internal set; }
        public MapTime MapTime { get; internal set; }
        private static readonly TimeSpan AutoSaveDelay = TimeSpan.FromMinutes(5);
        private DateTime _lastAutoSave;
        /// <summary> Y,X </summary>
        private Tile[,]? _tiles;
        private Dictionary<Effect.Effect.EEffectName, Effect.Effect>? _effects;
        private Dictionary<Effect.PassiveEffect.EPassiveEffects, Effect.PassiveEffect>? _passiveEffects;

        public IReadOnlyDictionary<IBuilding.EBuildingName, double> Multiplicators { get; internal set; }
        

        public void Start() {
            ThrowIfNull(gameObject, nameof(gameObject));
            if (size.x == 0 || size.y == 0) throw new Exception($"{nameof(size)}. X Or Y is 0");
            if (size.x < 0 || size.y < 0) throw new Exception($"{nameof(size)}. X Or Y is lower then 0");

            if (LoadSaveFile() == false) 
                HardReset();
        } 
            
        
        public void Update() {
            UpdateTime();
            ClearOldStats();

            Dictionary<IBuilding.EBuildingName, List<IBuilding>> tileGroups = GetAllTilesInGroup();

            var calc = new Calc(MapTime.DeltaTime, _effects, _passiveEffects, tileGroups);

            IReadOnlyDictionary<IBuilding.EBuildingName, double> multiplicators = calc.GetMultiplicators()!;
            var allWorkers = GetAllWorkers(tileGroups);

            if (Cargo is null)
                Cargo = new Dictionary<ETypeHint, ulong>();
            
            foreach (var (key, value) in tileGroups) {
                if (value is null || value.Count == 0) continue;
                if (multiplicators.TryGetValue(key, out var multiplicator) == false) multiplicator = 1;

                var building = value[0];
                
                ValueAndHint<ulong> productValue = building.ProductValueAsValueAndHint(allWorkers[key], multiplicator);

                Cargo[productValue.Hint] = Cargo.ContainsKey(productValue.Hint)
                    ? Cargo[productValue.Hint] + productValue.Value
                    : productValue.Value;
            }

            Multiplicators = multiplicators;
        }

        public void SoftReset() {
            if (_passiveEffects is not null && _passiveEffects.ContainsKey(PassiveEffect.EPassiveEffects.Faith)) {
                _passiveEffects[PassiveEffect.EPassiveEffects.Faith].CallEffect(PropMultiplikatorsWorker.FactoryDefault());
            }
            

            throw new NotImplementedException($"TODO Write Lambda {nameof(SoftReset)}");
        }

        public void HardReset() {
            var saveFile = new SaveFile(
                new (ulong Worker, IBuilding.EBuildingName EBuildingName)?[size.y, size.x], 
                new Dictionary<ETypeHint, ulong>(), 
                DateTime.UtcNow, 
                new List<PassiveEffect.EPassiveEffects>());
            
            SaveFileInfo.RemoveAllFiles();
            SaveFileInfo.CreateSaveFile(saveFile);

            if (LoadSaveFile() == false)
                throw new Exception($"{nameof(LoadSaveFile)} Not Work");
        }

        /// <summary> CreateSaveFile And Update _lastAutoSave </summary>
        private void CreateSaveFile() {
            _lastAutoSave = DateTime.UtcNow;
            
            var tiles = new (ulong Worker, IBuilding.EBuildingName EBuildingName)?[size.y, size.x];

            ThrowIfNull(_tiles, nameof(_tiles));
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    var building = _tiles?[y, x].Building;
                    if (building is null) continue;
                    tiles[y, x] = (building.Worker, building.BuildingName);
                }
            }


            ThrowIfNull(_passiveEffects, nameof(_passiveEffects));
            var passiveEffectList = new List<PassiveEffect.EPassiveEffects>(_passiveEffects!.Count);
            
            foreach (var keyValuePair in _passiveEffects) 
                passiveEffectList.Add(keyValuePair.Key);

            var saveFile = new SaveFile(
                tiles, 
                Cargo??new(), 
                _lastAutoSave, 
                passiveEffectList);
        }


        private void ClearOldStats() {
            Multiplicators = new Dictionary<IBuilding.EBuildingName, double>(0);
            foreach (var keyValuePair in _effects) {
                if (keyValuePair.Value.EffectIsActive() == true) continue;
                _effects.Remove(keyValuePair.Key);
            }
        }
        
        private bool LoadSaveFile() {
            var fileInfo = SaveFileInfo.GetLatesFileInfo();
            if (fileInfo.HasValue == false) return false;

            var saveFile = SaveFile.LoadFromFile(fileInfo.Value.FullPath);
            if (saveFile is null) return false;
            
            this.Cargo = saveFile.Cargo;
            this._lastAutoSave = saveFile.LastSave;
            this.MapTime = new MapTime(1, saveFile.LastSave);
            UpdateTime();
            /// <summary> Y,X </summary>
            this._tiles = new Tile[size.y,size.x];

            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    var tile = new Tile(new uint2((uint) x, (uint) y));
                    _tiles[y, x] = tile;
                    var tileFromSaveFile = saveFile.Tiles[y,x];
                    if (tileFromSaveFile.HasValue == false) continue;
                    tile.LoadValues(tileFromSaveFile.Value.EBuildingName, tileFromSaveFile.Value.Worker);
                }
            }
            
            _passiveEffects = new Dictionary<PassiveEffect.EPassiveEffects, PassiveEffect>();

            foreach (var i in saveFile.PassiveEffectsList) 
                _passiveEffects[i] = new PassiveEffect(i);

            return true;
        }

        private void UpdateTime() => MapTime = MapTime.NextUpdate();


        private Dictionary<IBuilding.EBuildingName, List<IBuilding>> GetAllTilesInGroup() {
            var res = new Dictionary<IBuilding.EBuildingName, List<IBuilding>>(16);
            
            foreach (var tile in this._tiles) {
                if (tile.Building is null) continue;

                if (res.TryGetValue(tile.Building.BuildingName, out var buildings)) 
                    buildings.Add(tile.Building);

                res[tile.Building.BuildingName] = new List<IBuilding>() {tile.Building};
            }

            return res;
        }

        private Dictionary<IBuilding.EBuildingName, ulong> GetAllWorkers() => GetAllWorkers(GetAllTilesInGroup());

        private static Dictionary<IBuilding.EBuildingName, ulong> GetAllWorkers(Dictionary<IBuilding.EBuildingName, List<IBuilding>> tiles) {
            var res = new Dictionary<IBuilding.EBuildingName, ulong>();

            foreach (var keyValuePair in tiles) {
                var list = keyValuePair.Value;
                ulong workers = 0;
                
                foreach (var i in list) {
                    if (i is null) continue;
                    workers = i.Worker;
                }
                
                res[keyValuePair.Key] = workers;
            }
            
            return res;
        }
    }
}




























