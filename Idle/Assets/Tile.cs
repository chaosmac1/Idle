

#nullable enable
using System;
using System.Collections.Generic;
using Hint;
using Idle.Building;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;
namespace Idle {
    public class Tile: MonoBehaviour {
        public Idle.MatrialHolder tileMaterialMapper;

        public const ulong WorkerCost = 10;
        
        // Buy
        private GameObject? _freePalceLv1Obj;
        private GameObject? _freePalceLv2Obj;
        private GameObject? _freePalceLv3Obj;
        private GameObject? _freePalceSetBuyLvObj;
        private GameObject? _inUseObj;
        
        // In Use
        private TextMeshProUGUI? _textName;
        private UnityEngine.UI.Image? _imageLogo;
        private UnityEngine.UI.Image? _imagebg;
        private UnityEngine.UI.Image? _buttonImageBg;
        private TextMeshProUGUI? _textWorker;
        private TextMeshProUGUI? _textLastOutPut;
        private Button? _buttonAddWorker;

        public uint2 Posi { get; private set; } = 0;
        public IBuilding? Building { get; private set; }

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
            tile.BindTriggers();
            
            
            tileObj!.transform.SetParent(GameObject.FindObjectOfType<Map>().taget!.gameObject.transform);
            
            tile._freePalceLv1Obj!.SetActive(false);
            tile._freePalceLv2Obj!.SetActive(false);
            tile._freePalceLv3Obj!.SetActive(false);
            tile._freePalceSetBuyLvObj!.SetActive(true);
            tile._inUseObj!.SetActive(false);
            
            return tile;
        }

        public void LoadFromSave(IBuilding.EBuildingName buildingName, ulong worker) {
            this.SetBuildingAndImages(buildingName, worker);
            this.Building!.BuildingName = buildingName;
            SwitchToUseMode();
        }
        
        private void SetButtonAndText() {
            var freePlace = GetChild(gameObject, "FreePlace")?? throw new NullReferenceException("FreePlace");
            var freePlaceLv1 = GetChild(freePlace, "lv1")?? throw new NullReferenceException("lv1");
            var freePlaceLv2 = GetChild(freePlace, "lv2")?? throw new NullReferenceException("lv2");
            var freePlaceLv3 = GetChild(freePlace, "lv3")?? throw new NullReferenceException("lv3");
            var freePlaceButtonSetBuyLv = GetChild(freePlace, "ButtonSetBuyLv") ?? throw new NullReferenceException("ButtonSetBuyLv");
            
            var inUse = GetChild(gameObject, "InUse")?? throw new NullReferenceException("InUse");
            var inUseTextObj = GetChild(inUse, "Name") ?? throw new NullReferenceException("Text");
            var inUseButtonAddWorker = GetChild(inUse, "ButtonAddWorker") ?? throw new NullReferenceException("ButtonAddWorker");
            var inUseLogo = GetChild(inUse, "Logo") ?? throw new NullReferenceException("Logo");
            // var inUseButtonBreak = GetChild(inUse, "ButtonBreak") ?? throw new NullReferenceException("ButtonBreak");
            var inUseTextWorkerObj = GetChild(inUse, "Worker") ?? throw new NullReferenceException("Name");
            var inUseTextLastOutPutObj = GetChild(inUse, "LastOutPut") ?? throw new NullReferenceException("LastOutPut");
            var inUseButtonAddWorkerObj = GetChild(inUse, "ButtonAddWorker") ?? throw new NullReferenceException("ButtonAddWorker");
            
            _freePalceLv1Obj = freePlaceLv1;
            _freePalceLv2Obj = freePlaceLv2;
            _freePalceLv3Obj = freePlaceLv3;
            _freePalceSetBuyLvObj = freePlaceButtonSetBuyLv;
            
            _inUseObj = inUse;
            _imageLogo = inUseLogo.GetComponent<UnityEngine.UI.Image>() ?? throw new NullReferenceException("inUseLogo Not Have Component<Image>");;
            
            _imagebg = this.GetComponent<UnityEngine.UI.Image>() ?? throw new NullReferenceException("inUse Not Have Component<Image>");
            _buttonImageBg = inUseButtonAddWorker.GetComponentInChildren<UnityEngine.UI.Image>() ?? throw new NullReferenceException("inUseButtonAddWorker Not Have Component<Image>");
            _textWorker = inUseTextWorkerObj.GetComponent<TextMeshProUGUI>();// ?? throw new NullReferenceException("inUseTextWorkerObj Not Have Component<TextMeshPro>");
            _textLastOutPut = inUseTextLastOutPutObj.GetComponent<TextMeshProUGUI>() ?? throw new NullReferenceException("inUseTextLastOutPutObj Not Have Component<TextMeshPro>");
            _buttonAddWorker = inUseButtonAddWorkerObj.GetComponent<Button>() ?? throw new NullReferenceException("inUseButtonAddWorkerObj Not Have Component<Button>");
            _textName = inUseTextObj.GetComponent<TextMeshProUGUI>() ?? throw new NullReferenceException("inUseText Not Have Component<TextMeshProUGUI>");
        }

        private static GameObject? GetChild(GameObject obj, string name) {
            Transform? transform = obj.transform.Find(name);
            if (transform is null) return null;

            return transform.gameObject;
        }
        
        
        private void CopyComponent() {
            
        }

        public void LateUpdate() {
            if (_textName is null)
                throw new NullReferenceException(nameof(_textName));
            if (_textWorker is null) throw new NullReferenceException(nameof(_textWorker));
            if (this.Building is null) return;

            _textName!.text = this.Building.BuildingName.ToString();
            if (this.Building.Worker.ToString() != this._textWorker.text) {
                this._textWorker.text = "Worker: " + this.Building.Worker.ToString();
            }
            
        } 
            

        
        public void Reset() {
            throw new NotImplementedException($"TODO Write Langer {nameof(Reset)}");
        }

        public void UpdateLastValueGen(List<ValueAndHint<ulong>> values) {
            string GetHintName(ValueAndHint<ulong> value) {
                return value.Hint switch {
                    ETypeHint.None => throw new Exception("ETypeHint.None Can't Use here"),
                    ETypeHint.Food => "Food: ",
                    ETypeHint.Wood => "Wood: ",
                    ETypeHint.Stone => "Stone: ",
                    ETypeHint.Metal => "Metal: ",
                    ETypeHint.Gold => "Gold: ",
                    ETypeHint.Faith => "Faith: ",
                    ETypeHint.MultiplierForFarm => "Multiplier Farm: ",
                    ETypeHint.MultiplierForForest => "Multiplier Forest: ",
                    ETypeHint.MultiplierForMine => "Multiplier Mine: ",
                    ETypeHint.MultiplierForGaranary => "Multiplier Garanary: ",
                    ETypeHint.MultiplierForShrine => "Multiplier Shrine: ",
                    ETypeHint.MultiplierForSchool => "Multiplier School: ",
                    ETypeHint.MultiplierForSmith => "Multiplier Smith: ",
                    ETypeHint.MultiplierForTemple => "Multiplier Temple: ",
                    ETypeHint.MultiplierForDocks => "Multiplier Docks: ",
                    ETypeHint.MultiplierForMill => "Multiplier Mill: ",
                    ETypeHint.MultiplierForFactory => "Multiplier Factory: ",
                    ETypeHint.MultiplierForCollege => "Multiplier College: ",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            ulong ValueFixer(ValueAndHint<ulong> value) {
                return value.Hint switch {
                    ETypeHint.None => throw new Exception("ETypeHint.None Can't Use here"),
                    ETypeHint.Food => value.Value,
                    ETypeHint.Wood => value.Value,
                    ETypeHint.Stone => value.Value,
                    ETypeHint.Metal => value.Value,
                    ETypeHint.Gold => value.Value,
                    ETypeHint.Faith => value.Value,
                    ETypeHint.MultiplierForFarm => value.Value / 1000,
                    ETypeHint.MultiplierForForest => value.Value / 1000,
                    ETypeHint.MultiplierForMine => value.Value / 1000,
                    ETypeHint.MultiplierForGaranary => value.Value / 1000,
                    ETypeHint.MultiplierForShrine => value.Value / 1000,
                    ETypeHint.MultiplierForSchool => value.Value / 1000,
                    ETypeHint.MultiplierForSmith => value.Value / 1000,
                    ETypeHint.MultiplierForTemple => value.Value / 1000,
                    ETypeHint.MultiplierForDocks => value.Value / 1000,
                    ETypeHint.MultiplierForMill => value.Value / 1000,
                    ETypeHint.MultiplierForFactory => value.Value / 1000,
                    ETypeHint.MultiplierForCollege => value.Value / 1000,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            if (values.Count == 0) {
                this._textLastOutPut!.text = "";
                return;
            }

            if (values.Count == 1) {
                this._textLastOutPut!.text = GetHintName(values[0]) + ValueFixer(values[0]).ToString();
                return;
            }

            var newText = string.Empty;
            for (var i = 0; i < values.Count -1; i++) {
                var valueAndHint = values[i];
                this._textLastOutPut!.text = GetHintName(valueAndHint) + ValueFixer(valueAndHint).ToString() + "\n";
            }
            
            this._textLastOutPut!.text = GetHintName(values[^1]) + ValueFixer(values[^1]).ToString();
        }
        
        /// <summary> Call From Map </summary>
        public void SetBuildingAndImages(IBuilding.EBuildingName buildingName, ulong worker) {
            this.Building = buildingName switch {
                IBuilding.EBuildingName.Farm => new Buildings.Farm(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Forest => new Buildings.Forest(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Mine => new Buildings.Mine(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Granary => new Buildings.Granary(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Shrine => new Buildings.Shrine(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.School => new Buildings.School(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Smith => new Buildings.Smith(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Temple => new Buildings.Temple(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Docks => new Buildings.Docks(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Mill => new Buildings.Mill(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.Factory => new Buildings.Factory(UpdateLastValueGen, worker),
                IBuilding.EBuildingName.College => new Buildings.College(UpdateLastValueGen, worker),
                _ => throw new ArgumentOutOfRangeException(nameof(buildingName), buildingName, null)
            };
            foreach ((IBuilding.EBuildingName eBuildingName, Sprite? materialBg, Sprite? materialLogo) in tileMaterialMapper.TileMaterialMapper) {
                if (eBuildingName != buildingName)
                    continue;
               
                this._imagebg!.sprite = materialBg;
                this._imageLogo!.sprite = materialLogo;
            }
        }

        private void SwitchToUseMode() {
            _freePalceLv1Obj!.SetActive(false);
            _freePalceLv2Obj!.SetActive(false);
            _freePalceLv3Obj!.SetActive(false);
            _freePalceSetBuyLvObj!.SetActive(false);
            _inUseObj!.SetActive(true);
        } 
        
       
        /// <summary> Bind GUI Button etc </summary>
        private void BindTriggers() {
            // lv1
            (GetChild(this._freePalceLv1Obj!, "Button Buy Farm") ?? throw new NullReferenceException("Button Farm"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonBuyFarm);
            (GetChild(this._freePalceLv1Obj!, "Button Buy Forest") ?? throw new NullReferenceException("Button Forest"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonBuyForest);
            (GetChild(this._freePalceLv1Obj!, "Button Buy Mine") ?? throw new NullReferenceException("Button Mine"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonBuyMine);
            (GetChild(this._freePalceLv1Obj!, "Button Buy Granary") ?? throw new NullReferenceException("Button Granary"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonBuyGranary);
            (GetChild(this._freePalceLv1Obj!, "Button Close") ?? throw new NullReferenceException("Button Close"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonClose);
            
            // lv2
            (GetChild(this._freePalceLv2Obj!, "Button Buy Shrine") ?? throw new NullReferenceException("Button Shrine"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuyShrine);
            (GetChild(this._freePalceLv2Obj!, "Button Buy School") ?? throw new NullReferenceException("Button School"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuySchool);
            (GetChild(this._freePalceLv2Obj!, "Button Buy Smith") ?? throw new NullReferenceException("Button Smith"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuySmith);
            (GetChild(this._freePalceLv2Obj!, "Button Buy Temple") ?? throw new NullReferenceException("Button Temple"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuyTemple);
            (GetChild(this._freePalceLv2Obj!, "Button Close") ?? throw new NullReferenceException("Button Close"))
                .GetComponent<Button>().onClick.AddListener(ButtonClose);
            
            // lv3
            (GetChild(this._freePalceLv3Obj!, "Button Buy Docks") ?? throw new NullReferenceException("Button Docks"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuyDocks);
            (GetChild(this._freePalceLv3Obj!, "Button Buy Mill") ?? throw new NullReferenceException("Button Mill"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuyMill);
            (GetChild(this._freePalceLv3Obj!, "Button Buy Factory") ?? throw new NullReferenceException("Button Factory"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuyFactory);
            (GetChild(this._freePalceLv3Obj!, "Button Buy College") ?? throw new NullReferenceException("Button College"))
                .GetComponent<Button>().onClick.AddListener(ButtonBuyCollege);
            (GetChild(this._freePalceLv3Obj!, "Button Close") ?? throw new NullReferenceException("Button Close"))
                .GetComponent<Button>().onClick.AddListener(ButtonClose);
            
            // ButtonSetBuyLv
            (GetChild(this._freePalceSetBuyLvObj!, "Button Build Lv.1") ?? throw new NullReferenceException("Button Build Lv.1"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonSwitchToLv1);
            (GetChild(this._freePalceSetBuyLvObj!, "Button Build Lv.2") ?? throw new NullReferenceException("Button Build Lv.2"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonSwitchToLv2);
            (GetChild(this._freePalceSetBuyLvObj!, "Button Build Lv.3") ?? throw new NullReferenceException("Button Build Lv.3"))
                .GetComponent<Button>().onClick.AddListener(this.ButtonSwitchToLv3);

                // inUse
            (GetChild(_inUseObj, "ButtonAddWorker") ?? throw new NullReferenceException("Button Add Worker"))
                .GetComponent<Button>().onClick.AddListener(ButtonAddWorker);
        }

        private void BuyBuilding(IBuilding.EBuildingName buildingName) {
            var cargo = GameObject.FindObjectOfType<Map>().Cargo!;
            if (CostTile.CheckIfCanBuy(buildingName, cargo) == false) {
                Debug.Log("Can Not Buy: " + buildingName.ToString());
                return;
            }

            Debug.Log("Can Buy: " + buildingName.ToString());
            CostTile.Buy(buildingName, cargo);
            this.SetBuildingAndImages(buildingName, 1);
            this.Building!.BuildingName = buildingName;
            SwitchToUseMode();
        }
        
        private void ButtonBuyFarm() => BuyBuilding(IBuilding.EBuildingName.Farm);
        private void ButtonBuyForest() => BuyBuilding(IBuilding.EBuildingName.Forest);
        private void ButtonBuyMine() => BuyBuilding(IBuilding.EBuildingName.Mine);
        private void ButtonBuyGranary() => BuyBuilding(IBuilding.EBuildingName.Granary);
        private void ButtonBuyShrine() => BuyBuilding(IBuilding.EBuildingName.Shrine);
        private void ButtonBuySchool() => BuyBuilding(IBuilding.EBuildingName.School);
        private void ButtonBuySmith() => BuyBuilding(IBuilding.EBuildingName.Smith);
        private void ButtonBuyTemple() => BuyBuilding(IBuilding.EBuildingName.Temple);
        public void ButtonBuyDocks() => BuyBuilding(IBuilding.EBuildingName.Docks);
        public void ButtonBuyMill() => BuyBuilding(IBuilding.EBuildingName.Mill);
        public void ButtonBuyFactory() => BuyBuilding(IBuilding.EBuildingName.Factory);
        public void ButtonBuyCollege() => BuyBuilding(IBuilding.EBuildingName.College);


        public void ButtonSwitchToLv1() {
            _freePalceLv1Obj!.SetActive(true);
            _freePalceLv2Obj!.SetActive(false);
            _freePalceLv3Obj!.SetActive(false);
            _freePalceSetBuyLvObj!.SetActive(false);
            _inUseObj!.SetActive(false);
        }
        
        public void ButtonSwitchToLv2() {
            _freePalceLv1Obj!.SetActive(false);
            _freePalceLv2Obj!.SetActive(true);
            _freePalceLv3Obj!.SetActive(false);
            _freePalceSetBuyLvObj!.SetActive(false);
            _inUseObj!.SetActive(false);
        }
        
        public void ButtonSwitchToLv3() {
            _freePalceLv1Obj!.SetActive(false);
            _freePalceLv2Obj!.SetActive(false);
            _freePalceLv3Obj!.SetActive(true);
            _freePalceSetBuyLvObj!.SetActive(false);
            _inUseObj!.SetActive(false);
        }
        
        private void ButtonClose() {
            _freePalceLv1Obj.SetActive(false);
            _freePalceLv2Obj.SetActive(false);
            _freePalceLv3Obj.SetActive(false);
            _freePalceSetBuyLvObj.SetActive(true);
            _inUseObj.SetActive(false);
        }

        private void ButtonAddWorker() {
            var cargo = GameObject.FindObjectOfType<Map>().Cargo!;

            if (cargo[ETypeHint.Food] < WorkerCost) {
                Debug.Log("Can Not Buy New Worker");
                return;
            }
            cargo[ETypeHint.Food] -= WorkerCost;
                
            this.Building!.AddWorker(1);
        }

        private void ButtonBuildingAddWorker() {
            var map = GameObject.FindObjectOfType<Map>();
            if (map is null)
                throw new NullReferenceException(nameof(map));

            // TODO Change Cost
            if (map.Cargo![ETypeHint.Food] < 100)
                return;
            map.Cargo![ETypeHint.Food] = map.Cargo![ETypeHint.Food] - 100;
            
            this.Building!.AddWorker(1);
            this._textWorker!.text = this.Building.Worker.ToString();
        }
        
        private void ButtonBuildingSell()
            => throw new NotImplementedException($"TODO Write Langer {nameof(ButtonBuildingSell)}");
    }
}