using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

#nullable enable
namespace Idle.GUI {
    public class ButtonOpenShop: MonoBehaviour {
        public GameObject? GUIShopWindow;

        public void Start() {
            if (GUIShopWindow is null)
                throw new NullReferenceException(nameof(GUIShopWindow));

            var button = this.GetComponent<UnityEngine.UI.Button>() ?? throw new NullReferenceException("Button Component Not Found");

            button.onClick.AddListener(this.OnClick);

            GUIShopWindow.SetActive(false);   
        }

        public void OnClick() => GUIShopWindow!.gameObject.SetActive(true);
    }
}