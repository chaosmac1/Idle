using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

#nullable enable
namespace Idle.GUI {
    public class ButtonOpenShop: MonoBehaviour {
        public GameObject? GUIShopWindow;
        private Button? _button;
        
        public void Start() {
            if (GUIShopWindow is null)
                throw new NullReferenceException(nameof(GUIShopWindow));

            _button = this.GetComponent<Button>() ?? throw new NullReferenceException("Button Component Not Found");

            _button.clicked += this.OnClick;
            
            GUIShopWindow.SetActive(false);   
        }

        public void OnClick() => GUIShopWindow!.gameObject.SetActive(true);
    }
}