using Flamingo.GameLoop.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.Minigame
{
    public class MinigameController
    {
        private readonly SignalBus _signalBus;
        private Minigame _current;
        private MemoryPool<Minigame> _pool;
        private List<MinigameData> _minigameData;
        private Dictionary<MinigameView, MinigameView> _viewPool;
        private Dictionary<string, Sprite> _imageLibrary;

        public MinigameController(SignalBus signalBus, Minigame.Pool minigameFactory, List<MinigameData> minigameData)
        {
            _viewPool = new Dictionary<MinigameView, MinigameView>();
            _signalBus = signalBus;
            _pool = minigameFactory;
            _minigameData = minigameData;
        }
        public void OnMinigameRequested(MinigameRequestedSignal args)
        {
            LoadMinigame(args.MinigameIndex);
        }
        public void OnMinigameEnd()
        {
            _pool.Despawn(_current);
            _current = null;
        }
        public void LoadMinigame(int index)
        {
            try
            {
                _current = _pool.Spawn();

                if (index < _minigameData.Count)
                {
                    if (!_viewPool.ContainsKey(_minigameData[index].minigameView))
                    {
                        var view = GameObject.Instantiate(_minigameData[index].minigameView).GetComponent<MinigameView>();
                        _viewPool.Add(_minigameData[index].minigameView, view);
                    }
                    _viewPool[_minigameData[index].minigameView].Initialize(_current);
                    _current.LoadData(_minigameData[index].minigameContent.text, _minigameData[index].score);
                }
                else
                {
                    Debug.LogWarning($"Game loaded Minigame index {index}. Compatible View not found.");
                    _signalBus.Fire<MinigameCompletedSignal>();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

        }
    }
}
