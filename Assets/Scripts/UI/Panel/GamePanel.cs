using Painter.GamePlay;
using Painter.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.UI
{
    public class GamePanel : BasePanel
    {
        #region Variable

        private ICanvas _canvas => _game?.Canvas;

        private GameObject _undoBtn;
        private GameObject _redoBtn;
        private GameObject _clearBtn;

        private GameObject _eraserBtn;
        private Text _eraserLevel;
        private Slider _eraserSlider;

        private GameObject _penBtn;
        private Text _penLevel;
        private Slider _penSlider;

        private GameObject _paletteBtn;

        #endregion

        #region Override

        private void OnDestroy()
        {
            if (_game != null && _canvas != null)
            {
                _canvas.OnUpdateUndo -= OnUpdateUndo;
                _canvas.OnUpdateRedo -= OnUpdateRedo;
            }
        }

        protected override void Init<ArgType>(ArgType arg)
        {
            base.Init(arg);

            InitComponent();
            RegisterEvent();

            OnUpdateUndo();
            OnUpdateRedo();

            OnClickPen();
        }

        private void InitComponent()
        {
            _undoBtn = transform.Find("Undo").gameObject;
            _redoBtn = transform.Find("Redo").gameObject;
            _clearBtn = transform.Find("Clear").gameObject;

            _eraserBtn = transform.Find("Eraser").gameObject;
            _eraserLevel = _eraserBtn.transform.Find("Level/Text").GetComponent<Text>();
            _eraserSlider = _eraserBtn.transform.Find("Slider").GetComponent<Slider>();

            _penBtn = transform.Find("Pen").gameObject;
            _penLevel = _penBtn.transform.Find("Level/Text").GetComponent<Text>();
            _penSlider = _penBtn.transform.Find("Slider").GetComponent<Slider>();

            _paletteBtn = transform.Find("Palette").gameObject;
        }

        private void RegisterEvent()
        {
            _undoBtn.AddClickEvent(OnClickUndo);
            _redoBtn.AddClickEvent(OnClickRedo);
            _clearBtn.AddClickEvent(OnClickClear);
            _eraserBtn.AddClickEvent(OnClickEraser);
            _penBtn.AddClickEvent(OnClickPen);
            _paletteBtn.AddClickEvent(OnClickPalette);

            _penSlider.onValueChanged.AddListener(OnPenLevelChanged);
            _eraserSlider.onValueChanged.AddListener(OnEraserLevelChanged);

            _canvas.OnUpdateUndo += OnUpdateUndo;
            _canvas.OnUpdateRedo += OnUpdateRedo;
        }

        #endregion

        #region Image Status

        private void SetColor(Image image, bool enable)
        {
            if (enable)
            {
                SetEnable(image);
            }
            else
            {
                SetDisable(image);
            }
        }

        private void SetDisable(Image image)
        {
            image.color = Color.gray;
        }

        private void SetEnable(Image image)
        {
            image.color = Color.black;
        }

        #endregion

        #region Event

        private void OnClickUndo()
        {
            _canvas.Undo();
            SetColor(_undoBtn.GetComponent<Image>(), _canvas.CanUndo);
        }

        private void OnClickRedo()
        {
            _canvas.Redo();
            SetColor(_redoBtn.GetComponent<Image>(), _canvas.CanRedo);
        }

        private void OnClickClear()
        {
            _canvas.Clear();
        }

        private void OnClickEraser()
        {
            _game.ChoosePen("Eraser");

            SetColor(_penBtn.GetComponent<Image>(), true);
            SetColor(_eraserBtn.GetComponent<Image>(), false);
        }

        private void OnClickPen()
        {
            _game.ChoosePen("Pen");

            SetColor(_penBtn.GetComponent<Image>(), false);
            SetColor(_eraserBtn.GetComponent<Image>(), true);
        }

        private void OnClickPalette()
        {
            BasePanel.Open<PalettePanel, PalettePanelArg>(new PalettePanelArg() { color = _game.Pen.Color });
        }

        private void OnPenLevelChanged(float val)
        {
            _penLevel.text = ((int)val).ToString();
            _game.PenLevel = (int)val;
        }

        private void OnEraserLevelChanged(float val)
        {
            _eraserLevel.text = ((int)val).ToString();
            _game.EraserLevel = (int)val;
        }

        private void OnUpdateUndo()
        { 
            SetColor(_undoBtn.GetComponent<Image>(), _canvas.CanUndo);
        }

        private void OnUpdateRedo()
        {
            SetColor(_redoBtn.GetComponent<Image>(), _canvas.CanRedo);
        }

        #endregion
    }
}