using System;
using Idle.Building;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

#nullable enable
namespace Idle {
    public class Tile: MonoBehaviour {
        public Idle.MatrialHolder tileMaterialMapper;
        private GuiTile _guiTilePosi;

        // Buy
        private GameObject? _freePalceLv1Obj;
        private GameObject? _freePalceLv2Obj;
        private GameObject? _freePalceLv3Obj;
        private GameObject? _freePalceSetBuyLvObj;
        private GameObject? _inUseObj;
        
        // In Use
        private TextMeshPro? _textName;
        private Image? _imageLogo;
        private Image? _imagebg;
        private Image? _buttonImageBg;
        

        public uint2 Posi { get; private set; } = 0;
        public IBuilding? Building { get; }

        public static Tile Factory(GameObject gameObjectCopyFrom, Idle.MatrialHolder matrialHolder, uint2 posi) {
            var tileObj = Object.Instantiate(gameObjectCopyFrom);
            
            if (tileObj is null)
                throw new NullReferenceException(nameof(tileObj));
            
            tileObj.SetActive(true);
            tileObj.gameObject.transform.localPosition = new Vector3(posi.x, posi.y);
            var tile = tileObj.AddComponent<Tile>();
            tile.tileMaterialMapper = matrialHolder;
            tile.Posi = posi;

            tile.SetButtonAndText();
            
            return tile;
        }

        private void SetButtonAndText() {
            var freePlace = GetChild(gameObject, "FreePlace")?? throw new NullReferenceException("FreePlace");
            var freePlaceLv1 = GetChild(freePlace, "lv1")?? throw new NullReferenceException("lv1");
            var freePlaceLv2 = GetChild(freePlace, "lv2")?? throw new NullReferenceException("lv2");
            var freePlaceLv3 = GetChild(freePlace, "lv3")?? throw new NullReferenceException("lv3");
            var freePlaceButtonSetBuyLv = GetChild(freePlace, "ButtonSetBuyLv") ?? throw new NullReferenceException("ButtonSetBuyLv");
            
            var inUse = GetChild(gameObject, "InUse")?? throw new NullReferenceException("InUse");
            var inUseText = GetChild(inUse, "Text") ?? throw new NullReferenceException("Text");
            var inUseButtonAddWorker = GetChild(inUse, "ButtonAddWorker") ?? throw new NullReferenceException("ButtonAddWorker");
            var inUseLogo = GetChild(inUse, "Logo") ?? throw new NullReferenceException("Logo");
            var inUseButtonBreak = GetChild(inUse, "ButtonBreak") ?? throw new NullReferenceException("ButtonBreak");

            
            _freePalceLv1Obj = freePlaceLv1;
            _freePalceLv2Obj = freePlaceLv2;
            _freePalceLv3Obj = freePlaceLv3;
            _freePalceSetBuyLvObj = freePlaceButtonSetBuyLv;
            
            _inUseObj = inUse;
            _imageLogo = inUseLogo.GetComponent<Image>() ?? throw new NullReferenceException("inUseLogo Not Have Component<Image>");
            _imagebg = inUse.GetComponent<Image>() ?? throw new NullReferenceException("inUse Not Have Component<Image>");
            _buttonImageBg = inUseButtonAddWorker.GetComponent<Image>() ?? throw new NullReferenceException("inUseButtonAddWorker Not Have Component<Image>");
            
            
        }

        private static GameObject? GetChild(GameObject obj, string name) {
            Transform? transform = obj.transform.Find(name);
            if (transform is null) return null;

            return transform.gameObject;
        }
        
        
        private void CopyComponent() {
            
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