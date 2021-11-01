using Painter.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.Manager
{
    public class MGame : ManagerBase<MGame>
    {
        private Game _game;

        public Game Current
        {
            get 
            {
                if (_game == null)
                {
                    _game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
                }
                return _game;
            }
        }
    }
}