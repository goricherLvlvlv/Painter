using Painter.GamePlay;
using Painter.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.UI
{
    public class GamePanel : MonoBehaviour
    {
        private Game _game => MGame.Fetch().Current;
        private GamePlay.Canvas _canvas => _game?.Canvas;

        private GameObject _undoBtn;
        private GameObject _redoBtn;

        private void Awake()
        {
            _undoBtn = transform.Find("Undo").gameObject;
            _redoBtn = transform.Find("Redo").gameObject;

            _undoBtn.AddClickEvent(Undo);
            _redoBtn.AddClickEvent(Redo);

            _canvas.OnUpdateUndo += OnUpdateUndo;
            _canvas.OnUpdateRedo += OnUpdateRedo;

            OnUpdateUndo();
            OnUpdateRedo();
        }

        private void OnDestroy()
        {
            if (_game != null && _canvas != null)
            {
                _canvas.OnUpdateUndo -= OnUpdateUndo;
                _canvas.OnUpdateRedo -= OnUpdateRedo;
            }
        }

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

        private void Undo()
        {
            _canvas.Undo();
            SetColor(_undoBtn.GetComponent<Image>(), _canvas.CanUndo);
        }

        private void Redo()
        {
            _canvas.Redo();
            SetColor(_redoBtn.GetComponent<Image>(), _canvas.CanRedo);
        }

        private void OnUpdateUndo()
        { 
            SetColor(_undoBtn.GetComponent<Image>(), _canvas.CanUndo);
        }

        private void OnUpdateRedo()
        {
            SetColor(_redoBtn.GetComponent<Image>(), _canvas.CanRedo);
        }
    }
}