using System.Collections.Generic;
using UnityEngine;

namespace Play.Block
{
    public class Rotate3DUpdater : MonoBehaviour
    {
        private List<IUpdater> _change = new List<IUpdater>();

        private bool _isActive = false;

        public void AddCheck(IUpdater change)
        {
            _change.Add(change);
            _isActive = true;
        }

        public void RemoveCheck(IUpdater change)
        {
            _change.Remove(change);
            if (_change.Count == 0)
                _isActive = false;
        }

        private void Update()
        {
            if (_isActive)
                foreach (var item in _change)
                    item.Update();
        }
    }
}