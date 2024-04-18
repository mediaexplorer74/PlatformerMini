// Decompiled with JetBrains decompiler
// Type: MonolithEngine.SelectableUIElement
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

#nullable disable
namespace MonolithEngine
{
  internal interface SelectableUIElement
  {
    void OnClick();

    void SetUserInterface(UserInterface userInterface);

    void OnResolutionChanged();
  }
}
