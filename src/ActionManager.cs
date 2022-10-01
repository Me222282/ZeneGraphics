using System;
using System.Collections.Generic;

namespace Zene.Graphics
{
    public class ActionManager
    {
        public void Push(Action action)
        {
            lock (_threadRef)
            {
                _actions.Add(action);
            }
        }

        private readonly List<Action> _actions = new List<Action>();
        private readonly object _threadRef = new object();

        public void Flush()
        {
            lock (_threadRef)
            {
                for (int i = 0; i < _actions.Count; i++)
                {
                    _actions[i]?.Invoke();
                }

                _actions.Clear();
            }
        }
    }
}
