using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.UI
{
    public class PalettePanel : BasePanel
    {
        private GameObject _mask;
        private PalleteImage _palette;
        private Image _float;
        private Slider _hue;
        private Slider _saturation;
        private Slider _value;
        private Image _result;

        private Color _color = Color.white;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                _game.Pen.Color = _color;
            }
        }

        protected override void Init<ArgType>(ArgType arg)
        {
            base.Init(arg);

            if (arg is PalettePanelArg colorArg)
            {
                _color = colorArg.color;
            }

            InitComponent();
            RGB2HSV(_color);
            RegisterEvent();

            CalculateColor();
            CalculateLocalPosition();
        }

        private void RGB2HSV(Color color)
        {
            var hue = _palette.GetHue(color);
            _hue.value = hue;

            float max = Mathf.Max(color.r, color.g, color.b);
            float min = Mathf.Min(color.r, color.g, color.b);

            var saturation = max == 0 ? max : 1f - min / max;
            _saturation.value = saturation;

            var value = max;
            _value.value = value;

            _palette.SetValueColor(value);
        }

        private void InitComponent()
        {
            _mask = transform.Find("Mask").gameObject;
            _palette = transform.Find("Palette").GetComponent<PalleteImage>();
            _float = transform.Find("Palette/Float").GetComponent<Image>();
            _hue = transform.Find("HueSlider").GetComponent<Slider>();
            _saturation = transform.Find("SaturationSlider").GetComponent<Slider>();
            _value = transform.Find("ValueSlider").GetComponent<Slider>();
            _result = transform.Find("Result").GetComponent<Image>();
        }

        private void RegisterEvent()
        {
            _mask.AddClickEvent(Close);
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
            var saturationColor = Color.Lerp(Color.white, hueColor, _saturation.value);
            var valueColor = Color.Lerp(Color.black, saturationColor, _value.value);
            valueColor.a = 1f;

            Color = valueColor;
            _result.color = Color;

            return Color;
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
