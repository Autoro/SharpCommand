using System;
using Microsoft.Xna.Framework.Input;

namespace SharpCommand
{
    public class XNAInputModule : IInputModule
    {
        public XNAInputModule()
        { }

        public string[] GetPressedKeys()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            string[] translatedKeys = new string[pressedKeys.Length];

            for (int i = 0; i < pressedKeys.Length; i++)
            {
                translatedKeys[i] = pressedKeys[i].ToString();
            }

            return translatedKeys;
        }
    }
}
