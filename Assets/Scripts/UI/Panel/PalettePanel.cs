using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.UI
{
    public class PalettePanel : BasePanel
    {
        private GameObject _mask;
        private CircleImage _palette;
        private Image _float;
        private Slider _hue;
        private Slider _saturation;
        private Slider _value;
        private Image _result;

        private Color _color = Color.white;

        protected override void Awake()
        {
            base.Awake();

            RegisterEvent();

            CalculateColor();
            CalculateLocalPosition();
        }

        protected override void InitComponent()
        {
            base.InitComponent();

            _mask = transform.Find("Mask").gameObject;
            _palette = transform.Find("Palette").GetComponent<CircleImage>();
            _float = transform.Find("Palette/Float").GetComponent<Image>();
            _hue = transform.Find("HueSlider").GetComponent<Slider>();
            _saturation = transform.Find("SaturationSlider").GetComponent<Slider>();
            _value = transform.Find("ValueSlider").GetComponent<Slider>();
            _result = transform.Find("Result").GetComponent<Image>();
        }

        private void RegisterEvent()
        {
            _mask.AddClickEvent(CloseSelf);
            _hue.onValueChanged.AddListener(OnHueChanged);
            _saturation.onValueChanged.AddListener(OnSaturationChanged);
            _value.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnHueChanged(float val)
        { 
            CalculateColor();
            CalculateLocalPosition();
        }

        private void OnSaturationChanged(float val)
        {
            CalculateColor();
            CalculateLocalPosition();
        }

        private void OnValueChanged(float val)
        {
            _palette.SetValueColor(val);
            CalculateColor();
            CalculateLocalPosition();
        }

        private Color CalculateColor()
        {
            var hueColor = (Color)_palette.GetColor32((int)_hue.value);
            var valueColor = _value.value * Color.white;
            valueColor.a = 1f;
            var saturationColor = Color.Lerp(valueColor, hueColor, _saturation.value);

            _color = saturationColor;
            _result.color = _color;

            return _color;
        }

        private void CalculateLocalPosition()
        {
            var radius = _palette.GetRadius();
            var offset = _palette.GetPivotOffset(radius);

            Vector3 position = _palette.GetPosition((int)_hue.value, radius, offset);
            position = Vector3.Lerp(offset, position, _saturation.value);
            _float.transform.localPosition = position;

            var color = (1f - _value.value) * Color.white;
            color.a = 1f;
            _float.color = color;
        }
    }
}