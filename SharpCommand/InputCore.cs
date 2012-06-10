using System;
using System.Collections.Generic;
using System.IO;

namespace SharpCommand
{
    public class InputCore
    {
        private readonly string fileName;
        private readonly IInputModule module;

        private readonly Dictionary<string, string> bindings;
        private readonly List<IInputListener> listeners;

        public InputCore(string fileName, IInputModule module)
        {
            this.fileName = fileName;
            this.module = module;

            this.bindings = new Dictionary<string, string>();
            this.listeners = new List<IInputListener>();

            foreach (string line in File.ReadLines(this.fileName))
            {
                string action = line.Split('=')[0].Trim();
                string key = line.Split('=')[1].Trim();

                this.BindKey(key, action);
            }
        }

        public void AddListener(IInputListener listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IInputListener listener)
        {
            this.listeners.Remove(listener);
        }

        public void BindKey(string key, string action)
        {
            this.bindings[key] = action;
        }

        public void UnbindKey(string key)
        {
            if (this.bindings.ContainsKey(key))
            {
                this.bindings[key] = String.Empty;
            }
        }

        public void Update()
        {
            foreach (string key in this.module.GetPressedKeys())
            {
                if (this.bindings.ContainsKey(key))
                {
                    foreach (IInputListener listener in this.listeners)
                    {
                        listener.PerformAction(this.bindings[key]);
                    }
                }
            }
        }
    }
}
