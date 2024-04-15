using System;
using System.Collections.Generic;
using System.Threading;

namespace Zene.Graphics
{
    public class ActionManager
    {
        public ActionManager() => _temporary = false;
        private ActionManager(bool temp) => _temporary = temp;

        public void Push(Action action)
        {
            if (action == null) { return; }

            if (_temporary || _manageThread == Thread.CurrentThread)
            {
                action.Invoke();
                return;
            }

            lock (_threadRef)
            {
                _actions.Add(action);
            }
        }

        private readonly List<Action> _actions = new List<Action>();
        private readonly object _threadRef = new object();
        private readonly bool _temporary;
        private Thread _manageThread;

        /// <summary>
        /// Determines whether the current thread is the managing thread for this <see cref="ActionManager"/>.
        /// </summary>
        public bool CurrentThread => _manageThread == Thread.CurrentThread;

        public void Flush()
        {
            if (_temporary) { return; }

            lock (_threadRef)
            {
                // Faster method of iteration
                ReadOnlySpan<Action> span = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(_actions);
                for (int i = 0; i < span.Length; i++)
                {
                    span[i].Invoke();
                }

                _actions.Clear();
            }
        }

        public void ThreadChange() => _manageThread = Thread.CurrentThread;

        public static ActionManager Temporary { get; } = new ActionManager(true);
    }
}
