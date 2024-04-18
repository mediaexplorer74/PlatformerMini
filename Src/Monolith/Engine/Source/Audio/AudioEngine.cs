// Decompiled with JetBrains decompiler
// Type: MonolithEngine.AudioEngine
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class AudioEngine
  {
    private static Dictionary<string, AudioEngine.AudioEntry> audioCache = new Dictionary<string, AudioEngine.AudioEntry>();

    internal static void OnAudioConfigChanged()
    {
    }

    public static void AddSound(
      string name,
      string path,
      bool isLooped = false,
      AudioTag audioTag = AudioTag.SOUND_EFFECT,
      bool isMuted = false,
      float maxVolume = 1f)
    {
      if ((double) maxVolume < 0.0 || (double) maxVolume > 1.0)
        throw new Exception("Max volume should be a value between 0 and 1");
      SoundEffectInstance instance = AssetUtil.LoadSoundEffect(path).CreateInstance();
      instance.IsLooped = isLooped;
      AudioEngine.audioCache.Add(name, new AudioEngine.AudioEntry(instance, audioTag, isMuted, maxVolume));
    }

    public static void Pause(string name) => AudioEngine.audioCache[name].SoundEffect.Pause();

    public static void Play(string name, bool waitForFinish = false)
    {
      if (AudioEngine.audioCache[name].IsMuted)
        return;
      SoundEffectInstance soundEffect = AudioEngine.audioCache[name].SoundEffect;
      if (waitForFinish)
      {
        soundEffect.Play();
      }
      else
      {
        if (soundEffect.State == SoundState.Playing)
          soundEffect.Stop();
        soundEffect.Play();
      }
    }

    public static void Stop(string name) => AudioEngine.audioCache[name].SoundEffect.Stop();

    public static void SetVolume(AudioTag tag, float volume)
    {
      foreach (AudioEngine.AudioEntry audioEntry in AudioEngine.audioCache.Values)
      {
        if (audioEntry.Tag == tag)
          audioEntry.SoundEffect.Volume = volume * audioEntry.MaxVolume;
      }
    }

    public static void MuteAll()
    {
      foreach (AudioTag tag in Enum.GetValues(typeof (AudioTag)))
        AudioEngine.ToggleMuteWithTag(tag, true);
    }

    public static void ToggleMuteWithTag(AudioTag tag, bool muted)
    {
      foreach (AudioEngine.AudioEntry audioEntry in AudioEngine.audioCache.Values)
      {
        if (audioEntry.Tag == tag)
          audioEntry.IsMuted = muted;
      }
    }

    public static void UnMuteAll()
    {
      foreach (AudioTag tag in Enum.GetValues(typeof (AudioTag)))
        AudioEngine.ToggleMuteWithTag(tag, false);
    }

    public static void StopSoundEffects()
    {
      foreach (AudioEngine.AudioEntry audioEntry in AudioEngine.audioCache.Values)
      {
        if (audioEntry.Tag == AudioTag.SOUND_EFFECT)
          audioEntry.SoundEffect.Stop();
      }
    }

    private class AudioEntry
    {
      public AudioTag Tag;
      public SoundEffectInstance SoundEffect;
      public bool IsMuted;
      public float MaxVolume;

      public AudioEntry(
        SoundEffectInstance soundEffect,
        AudioTag tag,
        bool isMuted,
        float maxVolume)
      {
        this.Tag = tag;
        this.SoundEffect = soundEffect;
        this.SoundEffect.Volume = maxVolume;
        this.IsMuted = isMuted;
        this.MaxVolume = maxVolume;
      }
    }
  }
}
