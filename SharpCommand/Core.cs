using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace SharpCommand
{
    public class InputCore
    {
        private readonly string filename = "bindings.txt";

        private IInputModule module;

        private Dictionary<string, string> bindings;
        private List<IInputListener> listeners;

        public InputCore(IInputModule module)
        {
            this.module = module;

            this.bindings = new Dictionary<string, string>();
            this.listeners = new List<IInputListener>();

            if (File.Exists(this.filename))
            {
                IEnumerable<string> binds = File.ReadLines(this.filename);

                foreach (string bind in binds)
                {
                    string action = bind.Split('=')[0].Trim();
                    string key = bind.Split('=')[1].Trim();

                    bindings.Add(key, action);
                }
            }
        }

        public void ChangeModule(IInputModule module)
        {
            this.module = module;
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
            if (this.bindings.ContainsKey(key))
            {
                this.bindings[key] = action;
            }
            else
            {
                this.bindings.Add(key, action);
            }
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
