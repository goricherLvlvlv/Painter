using Painter.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.Manager
{
    public class MGame : ManagerBase<MGame>
    {
        private Game _game;

        public IGame Current
        {
            get 
            {
                if (_game == null)
                {
                    var controller = GameObject.FindGameObjectWithTag("GameController");
                    _game = controller?.GetComponent<Game>();
                }
                return _game;
            }
        }
    }
}