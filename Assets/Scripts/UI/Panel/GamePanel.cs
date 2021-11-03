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
        private GameObject _penBtn;

        private GameObject _paletteBtn;

        #endregion

        #region Override

        protected override void Awake()
        {
            base.Awake();

            RegisterEvent();

            OnUpdateUndo();
            OnUpdateRedo();

            OnClickPen();
        }

        private void OnDestroy()
        {
            if (_game != null && _canvas != null)
            {
                _canvas.OnUpdateUndo -= OnUpdateUndo;
                _canvas.OnUpdateRedo -= OnUpdateRedo;
            }
        }

        protected override void InitComponent()
        {
            base.InitComponent();

            _undoBtn = transform.Find("Undo").gameObject;
            _redoBtn = transform.Find("Redo").gameObject;
            _clearBtn = transform.Find("Clear").gameObject;

            _eraserBtn = transform.Find("Eraser").gameObject;
            _penBtn = transform.Find("Pen").gameObject;

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
            _game.ChoosePen("Eraser.prefab");

            SetColor(_penBtn.GetComponent<Image>(), true);
            SetColor(_eraserBtn.GetComponent<Image>(), false);
        }

        private void OnClickPen()
        {
            _game.ChoosePen("Pen.prefab");

            SetColor(_penBtn.GetComponent<Image>(), false);
            SetColor(_eraserBtn.GetComponent<Image>(), true);
        }

        private void OnClickPalette()
        {
            _game.Root.LoadPrefabAtUILayer("PalettePanel.prefab");
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