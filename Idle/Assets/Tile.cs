using System;
using Idle.Building;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

#nullable enable
namespace Idle {
    public class Tile: MonoBehaviour {
        private GuiTile _guiTilePosi;
        private GameObject _gameObject;
        
        
        public uint2 Posi { get; }
        public IBuilding? Building { get; }

        public Tile(uint2 posi) {
            Posi = posi;
            throw new NotImplementedException($"TODO Write Langer {nameof(Tile)} (Create GameObject AND Building And Gui)");
        }

        public void LateUpdate() 
            => throw new NotImplementedException($"TODO Write Langer {nameof(LateUpdate)}");

        
        public void Reset() {
            throw new NotImplementedException($"TODO Write Langer {nameof(Reset)}");
        }

        /// <summary> Call From Map </summary>
        public void LoadValues(IBuilding.EBuildingName buildingName, ulong worker) {
            
        }
        
        private GameObject CreateGameObject() 
            => throw new NotImplementedException($"TODO Write Langer {nameof(CreateGameObject)}");

        /// <summary> Bind GUI Button etc </summary>
        private void BindTriggers()
            => throw new NotImplementedException($"TODO Write Langer {nameof(BindTriggers)}");

        private void CreateGui()
            => throw new NotImplementedException($"TODO Write Langer {nameof(CreateGui)}");

        private void ButtonBuyFarm()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuyFarm)}");

        private void ButtonBuyForest()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuyForest)}");
        
        private void ButtonBuyMine()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuyMine)}");
        
        private void ButtonBuyGranary()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuyGranary)}");
        
        private void ButtonBuyShrine()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuyShrine)}");
        
        private void ButtonBuySchool()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuySchool)}");
        
        private void ButtonBuySmith()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuySmith)}");
        
        private void ButtonBuyTemple()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuyTemple)}");
        
        private void ButtonClose()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonClose)}");
        
        private void ButtonBuildingSell()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuildingSell)}");
        
        private void ButtonBuildingAddWorker()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuildingAddWorker)}");
        
        private void ButtonBreakBuilding()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBreakBuilding)}");
        
        public readonly struct GuiTile {
            public readonly GameObject? LayerBuildingLevel1;
            public readonly GameObject? LayerBuildingLevel2;
            public readonly GameObject? LayerBuildingLevel3;

            public readonly GameObject? SwitchLayer;
            public readonly GameObject? BuildingImage;
            public readonly GameObject? BuildingTextWorker;
            public readonly GameObject? BuildingTextValueIncome;

            private GuiTile(GameObject? layerBuildingLevel1, GameObject? layerBuildingLevel2, GameObject? layerBuildingLevel3, 
                GameObject? switchLayer, GameObject? buildingImage, GameObject? buildingTextWorker, 
                GameObject? buildingTextValueIncome) {
                LayerBuildingLevel1 = layerBuildingLevel1;
                LayerBuildingLevel2 = layerBuildingLevel2;
                LayerBuildingLevel3 = layerBuildingLevel3;
                SwitchLayer = switchLayer;
                BuildingImage = buildingImage;
                BuildingTextWorker = buildingTextWorker;
                BuildingTextValueIncome = buildingTextValueIncome;
            }

            /// <param name="gameObject"> Parent </param>
            public GuiTile Factory(GameObject gameObject, IBuilding? building) {
                throw new NotImplementedException($"TODO Write Langer {nameof(Factory)}");
            }

            public GuiTile SwitchBuilding(IBuilding? building) {
                throw new NotImplementedException($"TODO Write Langer {nameof(SwitchLayer)}");
            }
        }
    }
}