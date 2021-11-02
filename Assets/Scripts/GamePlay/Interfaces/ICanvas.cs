using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.GamePlay
{
    public interface ICanvas
    {
        public event Action OnUpdateUndo;

        public event Action OnUpdateRedo;

        public bool CanUndo { get; }

        public bool CanRedo { get; }

        public void Undo();

        public void Redo();

        public void Clear();

    }
}