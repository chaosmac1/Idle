using System;
using System.Collections.Generic;
using Idle.Effect;
using Hint;
using Idle.Building;
using LambdaTime;
using Unity.Mathematics;
using Save;
using UnityEditor;
using static Idle.Utils.Utils;

using UnityEngine;
#nullable enable
namespace Idle {
    public class Map: MonoBehaviour {
        [SerializeField] public GameObject? taget;
        [SerializeField] public GameObject? matrialHolderObj; 
        [SerializeField] public int2 size;
        [SerializeField] public GameObject tilePop;
        public Dictionary<ETypeHint, ulong>? Cargo { get; private set; }
        public MapTime MapTime { get; internal set; }
        private static readonly TimeSpan AutoSaveDelay = TimeSpan.FromMinutes(5);
        private DateTime _lastAutoSave;
        /// <summary> Y,X </summary>
        private Tile[,]? _tiles;
        private Dictionary<Effect.Effect.EEffectName, Effect.Effect>? _effects;
        private Dictionary<Effect.PassiveEffect.EPassiveEffects, (Effect.PassiveEffect PassiveEffect, int Count)>? _passiveEffects;

        public IReadOnlyDictionary<Effect.Effect.EEffectName, Effect.Effect> Effects 
            => _effects?? new Dictionary<Effect.Effect.EEffectName, Effect.Effect>();

        public void SetEffects(Effect.Effect.EEffectName effectName, DateTime endTime)
            => _effects![effectName] = new Effect.Effect(endTime, effectName);
        
        public IReadOnlyDictionary<PassiveEffect.EPassiveEffects, (PassiveEffect PassiveEffect, int Count)> PassiveEffects
            => _passiveEffects ?? throw new NullReferenceException(nameof(_passiveEffects));

        public void AddCountOrSetPassiveEffects(PassiveEffect.EPassiveEffects passiveEffects) {
            if (!_passiveEffects!.ContainsKey(passiveEffects)) {
                _passiveEffects[passiveEffects] = (new PassiveEffect(passiveEffects), 1);
                return;
            }

            var tuple = _passiveEffects[passiveEffects];
            tuple.Count += 1;
            _passiveEffects[passiveEffects] = tuple;
            return;
        } 
            

        public IReadOnlyDictionary<IBuilding.EBuildingName, double> Multiplicators { get; internal set; }
        

        public void Start() {
            _effects = new Dictionary<Effect.Effect.EEffectName, Effect.Effect>(16);
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
            if (Cargo is null)
                Cargo = new Dictionary<ETypeHint, ulong>();
            
            UpdateBuidings(tileGroups, multiplicators);

            Multiplicators = multiplicators;

            if (new TimeSpan(_lastAutoSave.Ticks + TimeSpan.FromMinutes(1).Ticks) < new TimeSpan(DateTime.UtcNow.Ticks)) {
                this.CreateSaveFile();
                _lastAutoSave = DateTime.UtcNow;
            }
        }

        private void UpdateBuidings(Dictionary<IBuilding.EBuildingName, List<IBuilding>> tileGroups, IReadOnlyDictionary<IBuilding.EBuildingName, double> multiplicators) {
            var productCargoMulity = new Dictionary<ETypeHint, ulong>(16);
            
            void UpdateBuilding(IBuilding.EBuildingName key, IBuilding? building ) {
                if (building is null)
                    throw new NullReferenceException(nameof(building));
                
                if (multiplicators.TryGetValue(key, out var multiplicator) == false)
                    multiplicator = 1;

                var valueAndHints = building.ProductValueAsValueAndHint(building.Worker, productCargoMulity, multiplicator);
                
                bool IsForProductCargoMulity(ETypeHint hint) {
                    return hint switch {
                        ETypeHint.MultiplierForFarm => true,
                        ETypeHint.MultiplierForForest => true,
                        ETypeHint.MultiplierForMine => true,
                        ETypeHint.MultiplierForGaranary => true,
                        ETypeHint.MultiplierForShrine => true,
                        ETypeHint.MultiplierForSchool => true,
                        ETypeHint.MultiplierForSmith => true,
                        ETypeHint.MultiplierForTemple => true,
                        ETypeHint.MultiplierForDocks => true,
                        ETypeHint.MultiplierForMill => true,
                        ETypeHint.MultiplierForFactory => true,
                        ETypeHint.MultiplierForCollege => true,
                        _ => false
                    };
                }
                
                 foreach (var valueAndHint in valueAndHints) {
                    if (IsForProductCargoMulity(valueAndHint.Hint)) {
                        productCargoMulity[valueAndHint.Hint] = productCargoMulity.ContainsKey(valueAndHint.Hint) switch {
                            true => (productCargoMulity[valueAndHint.Hint] + valueAndHint.Value),
                            _ => valueAndHint.Value
                        };
                        continue;
                    }

                    var value = (ulong)((double) valueAndHint.Value * (double) this.MapTime.DeltaTime);
                    Cargo![valueAndHint.Hint] = Cargo.ContainsKey(valueAndHint.Hint)
                        ? Cargo[valueAndHint.Hint] + value
                        : value;
                }
            }

            void UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName key, Dictionary<IBuilding.EBuildingName, List<IBuilding>> tileGroups) {
                if (tileGroups.ContainsKey(key) == false) return;
                var buildings = tileGroups[key];
                if (buildings is null)
                    throw new NullReferenceException(nameof(buildings));
                
                foreach (var building in buildings) {
                    UpdateBuilding(key, building);
                }
            }

            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.College, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Factory, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Mill, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Docks, tileGroups);
            
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.School, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Temple, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Smith, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Shrine, tileGroups);
            
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Granary, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Mine, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Forest, tileGroups);
            UpdateBuildingListFromDicIfExist(IBuilding.EBuildingName.Farm, tileGroups);
        }


        public void SoftReset() {
            if (_passiveEffects is not null && _passiveEffects.ContainsKey(PassiveEffect.EPassiveEffects.PassivFaith)) {
                var tuple = _passiveEffects[PassiveEffect.EPassiveEffects.PassivFaith];
                tuple.Count = 1;
                tuple.PassiveEffect.CallEffect(PropMultiplikatorsWorker.FactoryDefault(), 1);
            }
            

            throw new NotImplementedException($"TODO Write Lambda {nameof(SoftReset)}");
        }

        public void HardReset() {
            var saveFile = new SaveFile(
                new (ulong Worker, IBuilding.EBuildingName EBuildingName)?[size.y, size.x], 
                new Dictionary<ETypeHint, ulong>() {
                    {ETypeHint.Food, 1000},
                    {ETypeHint.Stone, 1000},
                    {ETypeHint.Metal, 1000},
                }, 
                DateTime.UtcNow, 
                new List<(PassiveEffect.EPassiveEffects, int Count)>());
            
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
            var passiveEffectList = new List<(PassiveEffect.EPassiveEffects, int Count)>(_passiveEffects!.Count);
            
            foreach (var (key, (_, count)) in _passiveEffects) 
                passiveEffectList.Add((key, Count: count));

            var saveFile = new SaveFile(
                tiles, 
                Cargo??new(), 
                _lastAutoSave, 
                passiveEffectList);
            SaveFileInfo.CreateSaveFile(saveFile);
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

            
            
            // Tiles
            if (matrialHolderObj is null)
                throw new NullReferenceException(nameof(matrialHolderObj));

            MatrialHolder? matrialHolder = matrialHolderObj.GetComponent<MatrialHolder>();
            if (matrialHolder is null)
                throw new NullReferenceException(nameof(matrialHolderObj) + " Have not Component: " + nameof(MatrialHolder));
            
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    var tile = Tile.Factory(tilePop, matrialHolder, new uint2((uint) x, (uint) y));
                    _tiles[y, x] = tile;
                    var tileFromSaveFile = saveFile.Tiles[y,x];
                    if (tileFromSaveFile.HasValue == false) continue;
                    tile.SetBuildingAndImages(tileFromSaveFile.Value.EBuildingName, tileFromSaveFile.Value.Worker);
                    tile.LoadFromSave(tileFromSaveFile.Value.EBuildingName, tileFromSaveFile.Value.Worker);
                }
            }
            
            _passiveEffects = new Dictionary<PassiveEffect.EPassiveEffects, (PassiveEffect PassiveEffect, int Count)>();

            foreach ((PassiveEffect.EPassiveEffects Name, int Count) i in saveFile.PassiveEffectsList) 
                _passiveEffects[i.Name] = (new PassiveEffect(i.Name), i.Count);

            this._effects = new Dictionary<Effect.Effect.EEffectName, Effect.Effect>();
            
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




























