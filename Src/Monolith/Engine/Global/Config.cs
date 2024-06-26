﻿
// Type: MonolithEngine.Config
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System;


namespace MonolithEngine
{
  public class Config
  {
    public static float CHARACTER_SPEED = 80f;
    public static int GRID = 16;
    public static float HORIZONTAL_FRICTION = 0.4f;
    public static float VERTICAL_FRICTION = 0.8f;
    public static float BUMP_FRICTION = 0.7f;

    public static bool GRAVITY_ON = true;
    public static float GRAVITY_FORCE = 2f;//8f;

    public static float JUMP_FORCE = 6f;//3f;
    public static float GRAVITY_T_MULTIPLIER = 1f;
    public static float DYNAMIC_COLLISION_CHECK_FREQUENCY = 1f;
    public static bool INCREASING_GRAVITY = true;

    public static float SPRITE_DRAW_OFFSET = 0.0f;
    public static float SPRITE_COLLISION_OFFSET = 0.0f;

    public static float TIME_OFFSET = 0.1f;
    public static int FIXED_UPDATE_FPS = 30;
    public static int PIVOT_RADIUS = 10;

    public static int CAMERA_TIME_MULTIPLIER = 1;
    public static float CAMERA_DEADZONE = 100f;
    public static float CAMERA_FRICTION = 0.89f;
    public static float CAMERA_FOLLOW_DELAY = 0.0005f;
    public static float CAMERA_ZOOM = 1f;

    public static float SCALE_X = 1f;
    public static float SCALE_Y = 1f;

    public static Action ExitAction;
  }
}
