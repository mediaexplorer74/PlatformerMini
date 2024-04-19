
// Type: MonolithEngine.IScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System.Collections.Generic;


namespace MonolithEngine
{
  public interface IScene
  {
    void Load();

    void OnStart();

    void Finish();

    void OnEnd();

    void Unload();

    void OnFinished();

    ISceneTransitionEffect GetTransitionEffect();

    ICollection<object> ExportData();

    void ImportData(ICollection<object> state);

    void Update();
  }
}
