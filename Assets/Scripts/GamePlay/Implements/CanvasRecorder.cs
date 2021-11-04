using Painter.Setting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.GamePlay
{
    public partial class Canvas : ICanvas
    {
        #region Variable

        private const int UndoMaxCount = 20;

        private LinkedList<Dictionary<(int, int), Color>> _undoColors = new LinkedList<Dictionary<(int, int), Color>>();
        private LinkedList<Dictionary<(int, int), Color>> _redoColors = new LinkedList<Dictionary<(int, int), Color>>();

        private Dictionary<(int, int), Color> _curChangedColors = new Dictionary<(int, int), Color>();

        public event Action OnUpdateUndo;

        public event Action OnUpdateRedo;

        public bool CanUndo => _undoColors.Count > 0;

        public bool CanRedo => _redoColors.Count > 0;
        #endregion

        #region Interface

        public void Undo()
        {
            if (_undoColors.Count <= 0)
            {
                return;
            }

            var colors = _undoColors.Last.Value;
            _undoColors.RemoveLast();

            var result = ResetChangedColors(colors);

            _redoColors.AddLast(result);

            OnUpdateUndo?.Invoke();
            OnUpdateRedo?.Invoke();
        }

        public void Redo()
        {
            if (_redoColors.Count <= 0)
            {
                return;
            }

            var colors = _redoColors.Last.Value;
            _redoColors.RemoveLast();

            var result = ResetChangedColors(colors);

            _undoColors.AddLast(result);

            OnUpdateUndo?.Invoke();
            OnUpdateRedo?.Invoke();
        }

        public void Clear()
        {
            FillWhiteTexture(_canvas);

            _redoColors.Clear();
            OnUpdateRedo?.Invoke();

            _undoColors.Clear();
            OnUpdateUndo?.Invoke();
        }

        #endregion

        #region Change Color

        private void AddChangedColor(int x, int y, Color oldColor)
        { 
            _curChangedColors[(x, y)] = oldColor;
        }

        private void SaveChangedColor()
        {
            if (_curChangedColors.Count <= 0)
            {
                return;
            }

            _undoColors.AddLast(_curChangedColors);
            _curChangedColors = new Dictionary<(int, int), Color>();

            _redoColors.Clear();

            // Undo列表会有上限, 否则内存会炸穿
            while (_undoColors.Count > UndoMaxCount)
            {
                _undoColors.RemoveFirst();
            }

            OnUpdateUndo?.Invoke();
            OnUpdateRedo?.Invoke();
        }

        private Dictionary<(int, int), Color> ResetChangedColors(Dictionary<(int, int), Color> colors)
        {
            Dictionary<(int, int), Color> reuslt = new Dictionary<(int, int), Color>();
            foreach (var pair in colors)
            {
                var color = _canvas.GetPixel(pair.Key.Item1, pair.Key.Item2);
                _canvas.SetPixel(pair.Key.Item1, pair.Key.Item2, pair.Value);
                reuslt[pair.Key] = color;
            }

            _canvas.Apply();

            return reuslt;
        }

        #endregion
    }
}