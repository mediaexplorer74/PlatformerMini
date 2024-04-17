// Decompiled with JetBrains decompiler
// Type: MonolithEngine.UserInputController
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MonolithEngine
{
  public class UserInputController : IUpdatableComponent, IComponent
  {
    private Dictionary<Keys, bool> pressedKeys = new Dictionary<Keys, bool>();
    private Dictionary<Buttons, bool> pressedButtons = new Dictionary<Buttons, bool>();
    private Dictionary<UserInputController.KeyMapping, Action> keyPressActions = new Dictionary<UserInputController.KeyMapping, Action>();
    private Dictionary<Keys, Action> keyReleaseActions = new Dictionary<Keys, Action>();
    private Dictionary<Buttons, Action> buttonReleaseActions = new Dictionary<Buttons, Action>();
    private KeyboardState currentKeyboardState;
    private KeyboardState? prevKeyboardState;
    private GamePadState? prevGamepadState;
    private MouseState mouseState;
    private GamePadState currentGamepadState;
    private int? prevMouseScrollWheelValue = new int?(0);
    private Action mouseWheelUpAction;
    private Action mouseWheelDownAction;
    private float scrollThreshold;
    private Vector2 leftThumbstick = Vector2.Zero;
    private Vector2 rightThumbStick = Vector2.Zero;
    public bool ControlsDisabled;

    public bool UniquePerEntity { get; set; }

    public UserInputController() => this.UniquePerEntity = true;

    public void RegisterKeyPressAction(
      Keys key,
      Buttons controllerButton,
      Action action,
      bool singlePressOnly = false,
      int pressCooldown = 0)
    {
      this.keyPressActions.Add(new UserInputController.KeyMapping(new Keys?(key), new Buttons?(controllerButton), singlePressOnly, pressCooldown), action);
      this.pressedKeys[key] = false;
      this.pressedButtons[controllerButton] = false;
    }

    public void RegisterKeyReleaseAction(Keys key, Buttons controllerButton, Action action)
    {
      this.keyReleaseActions.Add(key, action);
      this.buttonReleaseActions.Add(controllerButton, action);
    }

    public void RegisterKeyReleaseAction(Keys key, Action action)
    {
      this.keyReleaseActions.Add(key, action);
    }

    public void RegisterKeyPressAction(
      Buttons controllerButton,
      Action action,
      bool singlePressOnly = false,
      int pressCooldown = 0)
    {
      this.keyPressActions.Add(new UserInputController.KeyMapping(new Keys?(), new Buttons?(controllerButton), singlePressOnly, pressCooldown), action);
      this.pressedButtons[controllerButton] = false;
    }

    public void RegisterKeyPressAction(
      Keys key,
      Action action,
      bool singlePressOnly = false,
      int pressCooldown = 0)
    {
      this.keyPressActions.Add(new UserInputController.KeyMapping(new Keys?(key), new Buttons?(), singlePressOnly), action);
      this.pressedKeys[key] = false;
    }

    public void RegisterMouseActions(
      Action wheelUpAction,
      Action wheelDownAction,
      float scrollThreshold = 0.0f)
    {
      this.mouseWheelUpAction = wheelUpAction;
      this.mouseWheelDownAction = wheelDownAction;
      this.scrollThreshold = scrollThreshold;
    }

    public bool IsKeyPressed(Keys key) => this.pressedKeys[key];

    public void PreUpdate()
    {
      if (this.ControlsDisabled)
      {
        foreach (Keys key in this.pressedKeys.Keys.ToList<Keys>())
          this.pressedKeys[key] = false;
        foreach (Buttons key in this.pressedButtons.Keys.ToList<Buttons>())
          this.pressedButtons[key] = false;
        this.prevGamepadState = new GamePadState?();
        this.prevKeyboardState = new KeyboardState?();
      }
      else
      {
        this.currentKeyboardState = Keyboard.GetState();
        this.mouseState = Mouse.GetState();
        this.currentGamepadState = GamePad.GetState(PlayerIndex.One);
        foreach (KeyValuePair<UserInputController.KeyMapping, Action> keyPressAction in this.keyPressActions)
        {
          Keys? key = keyPressAction.Key.Key;
          if (key.HasValue)
          {
            if (this.currentKeyboardState.IsKeyDown(key.Value))
            {
              if (!Timer.IsSet("INPUTPRESSED_" + key.Value.ToString()))
              {
                if (keyPressAction.Key.PressCooldown != 0)
                  Timer.SetTimer("INPUTPRESSED_" + key.Value.ToString(), (float) keyPressAction.Key.PressCooldown);
                if (keyPressAction.Key.SinglePressOnly && this.prevKeyboardState.HasValue)
                {
                  KeyboardState? prevKeyboardState = this.prevKeyboardState;
                  KeyboardState currentKeyboardState = this.currentKeyboardState;
                  if ((prevKeyboardState.HasValue ? (prevKeyboardState.HasValue ? (prevKeyboardState.GetValueOrDefault() == currentKeyboardState ? 1 : 0) : 1) : 0) != 0 || this.pressedKeys[key.Value])
                    continue;
                }
                this.pressedKeys[key.Value] = true;
                keyPressAction.Value();
              }
              else
                continue;
            }
            else
            {
              if (this.pressedKeys[key.Value] && this.keyReleaseActions.ContainsKey(key.Value))
                this.keyReleaseActions[key.Value]();
              this.pressedKeys[key.Value] = false;
            }
          }
          Buttons? button = keyPressAction.Key.Button;
          if (button.HasValue)
          {
            if (this.currentGamepadState.IsButtonDown(button.Value))
            {
              if (keyPressAction.Key.SinglePressOnly && this.prevGamepadState.HasValue)
              {
                GamePadState? prevGamepadState = this.prevGamepadState;
                GamePadState currentGamepadState = this.currentGamepadState;
                if ((prevGamepadState.HasValue ? (prevGamepadState.HasValue ? (prevGamepadState.GetValueOrDefault() == currentGamepadState ? 1 : 0) : 1) : 0) != 0 || this.pressedButtons[button.Value])
                  continue;
              }
              if (!Timer.IsSet("INPUTPRESSED_" + button.Value.ToString()))
              {
                if (keyPressAction.Key.PressCooldown != 0)
                  Timer.SetTimer("INPUTPRESSED_" + button.Value.ToString(), (float) keyPressAction.Key.PressCooldown);
                this.pressedButtons[button.Value] = true;
                keyPressAction.Value();
              }
            }
            else
            {
              if (this.pressedButtons[button.Value] && this.buttonReleaseActions.ContainsKey(button.Value))
                this.buttonReleaseActions[button.Value]();
              this.pressedButtons[button.Value] = false;
            }
          }
        }
        this.prevKeyboardState = new KeyboardState?(this.currentKeyboardState);
        this.prevGamepadState = new GamePadState?(this.currentGamepadState);
        int scrollWheelValue1 = this.mouseState.ScrollWheelValue;
        int? scrollWheelValue2 = this.prevMouseScrollWheelValue;
        int valueOrDefault1 = scrollWheelValue2.GetValueOrDefault();
        if (scrollWheelValue1 > valueOrDefault1 & scrollWheelValue2.HasValue)
        {
          if (this.mouseWheelUpAction == null)
            return;
          int scrollWheelValue3 = this.mouseState.ScrollWheelValue;
          int? scrollWheelValue4 = this.prevMouseScrollWheelValue;
          float? nullable = scrollWheelValue4.HasValue ? new float?((float) (scrollWheelValue3 - scrollWheelValue4.GetValueOrDefault())) : new float?();
          float scrollThreshold = this.scrollThreshold;
          if (!((double) nullable.GetValueOrDefault() >= (double) scrollThreshold & nullable.HasValue))
            return;
          this.mouseWheelUpAction();
          this.prevMouseScrollWheelValue = new int?(this.mouseState.ScrollWheelValue);
        }
        else
        {
          int scrollWheelValue5 = this.mouseState.ScrollWheelValue;
          int? scrollWheelValue6 = this.prevMouseScrollWheelValue;
          int valueOrDefault2 = scrollWheelValue6.GetValueOrDefault();
          if (!(scrollWheelValue5 < valueOrDefault2 & scrollWheelValue6.HasValue) || this.mouseWheelDownAction == null)
            return;
          scrollWheelValue6 = this.prevMouseScrollWheelValue;
          int scrollWheelValue7 = this.mouseState.ScrollWheelValue;
          float? nullable = scrollWheelValue6.HasValue ? new float?((float) (scrollWheelValue6.GetValueOrDefault() - scrollWheelValue7)) : new float?();
          float scrollThreshold = this.scrollThreshold;
          if (!((double) nullable.GetValueOrDefault() >= (double) scrollThreshold & nullable.HasValue))
            return;
          this.mouseWheelDownAction();
          this.prevMouseScrollWheelValue = new int?(this.mouseState.ScrollWheelValue);
        }
      }
    }

    public void Update()
    {
    }

    public void PostUpdate()
    {
    }

    public Type GetComponentType() => this.GetType();

    private class KeyMapping
    {
      public Keys? Key;
      public Buttons? Button;
      public bool SinglePressOnly;
      public int PressCooldown;

      public KeyMapping(Keys? key, Buttons? button, bool singlePressOnly = false, int pressCooldown = 0)
      {
        this.Key = key;
        this.Button = button;
        this.SinglePressOnly = singlePressOnly;
        this.PressCooldown = pressCooldown;
      }

      public override bool Equals(object obj)
      {
        if (obj is UserInputController.KeyMapping keyMapping)
        {
          Keys? key1 = this.Key;
          Keys? key2 = keyMapping.Key;
          if (key1.GetValueOrDefault() == key2.GetValueOrDefault() & key1.HasValue == key2.HasValue)
          {
            Buttons? button1 = this.Button;
            Buttons? button2 = keyMapping.Button;
            if (button1.GetValueOrDefault() == button2.GetValueOrDefault() & button1.HasValue == button2.HasValue && this.SinglePressOnly == keyMapping.SinglePressOnly)
              return this.PressCooldown == keyMapping.PressCooldown;
          }
        }
        return false;
      }

      public override int GetHashCode()
      {
        return HashCode.Combine<Keys?, Buttons?, bool, int>(this.Key, this.Button, this.SinglePressOnly, this.PressCooldown);
      }
    }
  }
}
