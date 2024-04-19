
// Type: MonolithEngine.Camera
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonolithEngine
{
  public class Camera
  {
    private const float MinZoom = 0.01f;
    internal Viewport Viewport;
    private Vector2 origin;
    private Vector2 position;
    private float zoom = 1f;
    private Rectangle? limits;
    public Entity target;
    private Vector2 targetTracingOffset = Vector2.Zero;
    private float friction = 0.89f;
    private float shakePower = 1.5f;
    private float shakeStarted;
    private float shakeDuration;
    private bool easedStop;
    private bool SCROLL = true;
    private bool shake;
    private Vector2 direction;
    private Matrix uiTransofrmMatrix;
    private float scrollSpeedModifier;
    private GraphicsDeviceManager graphicsDeviceManager;
    private float rotation = MathUtil.DegreesToRad(0.0f);
    public CameraMode CameraMode;
    private Vector2 moveToPosition;
    private int cameraNumber;

    public Camera(
      GraphicsDeviceManager graphicsDeviceManager,
      CameraMode cameraMode = CameraMode.SINGLE,
      int cameraNumber = 0)
    {
      this.graphicsDeviceManager = graphicsDeviceManager;
      this.Position = Vector2.Zero;
      this.direction = Vector2.Zero;
      this.Zoom = Math.Abs((Config.SCALE_X + Config.SCALE_Y) / 2);
            this.CameraMode = cameraMode;
      this.cameraNumber = cameraNumber;
      this.Initialize();
    }

    public void Initialize()
    {
      this.Viewport = this.graphicsDeviceManager.GraphicsDevice.Viewport;
      if (this.CameraMode != CameraMode.SINGLE)
      {
        if (this.CameraMode == CameraMode.DUAL_VERTICAL_SPLIT)
        {
          this.Viewport.Width /= 2;
          if (this.cameraNumber == 1)
            this.Viewport.X += this.Viewport.Width;
        }
        else if (this.CameraMode == CameraMode.DUAL_HORIZONTAL_SPLIT)
        {
          this.Viewport.Height /= 2;
          if (this.cameraNumber == 1)
            this.Viewport.Y += this.Viewport.Height;
        }
      }
      Logger.Info("Configuring camera, viewport: [" + this.Viewport.ToString() + "], mode [" + this.CameraMode.ToString() + "], camera number: " + this.cameraNumber.ToString());
      this.origin = new Vector2((float) this.Viewport.Width / 2f, (float) this.Viewport.Height / 2f);
      this.Zoom = Math.Abs((Config.SCALE_X + Config.SCALE_Y) / 2);

      if (this.target != null)
        this.TrackTarget(this.target, true, this.targetTracingOffset);
      this.uiTransofrmMatrix = Matrix.Identity * Matrix.CreateScale(Config.SCALE_X, Config.SCALE_Y, 1f);
    }

    public void Shake(float power = 5f, float duration = 300f, bool easeOut = true)
    {
      this.shakePower = power;
      this.shakeDuration = duration;
      this.shake = true;
      this.easedStop = easeOut;
    }

    public void Update()
    {
      if (!this.SCROLL)
        return;
      float num1 = Math.Max(1f, this.Zoom / 2f);
      float num2 = Config.CAMERA_DEADZONE / num1;
      float y = Globals.ElapsedTime / (float) Config.CAMERA_TIME_MULTIPLIER;
      if (this.target != null || this.moveToPosition != new Vector2())
      {
        Vector2 v2 = this.moveToPosition;
        if (this.target != null)
          v2 = this.target.Transform.Position + this.targetTracingOffset - new Vector2((float) this.Viewport.Width / 2f, (float) this.Viewport.Height / 2f);
        float num3 = Vector2.Distance(this.Position, v2);
        if ((double) num3 >= (double) num2)
        {
          float num4 = MathUtil.RadFromVectors(this.Position, v2);
          this.direction.X += (float) (Math.Cos((double) num4) * ((double) num3 - (double) num2) * ((double) Config.CAMERA_FOLLOW_DELAY / (double) num1)) * y;
          this.direction.Y += (float) (Math.Sin((double) num4) * ((double) num3 - (double) num2) * ((double) Config.CAMERA_FOLLOW_DELAY / (double) num1)) * y;
        }
      }
      this.Position += this.direction * y * num1;
      this.direction *= new Vector2((float) Math.Pow((double) this.friction, (double) y), (float) Math.Pow((double) this.friction, (double) y));
      this.PostUpdate();
    }

    private void PostUpdate()
    {
      if (!this.shake)
        return;
      this.shakeStarted += Globals.ElapsedTime;
      float num = this.shakePower;
      if (this.easedStop)
        num = MathHelper.Lerp(this.shakePower, 0.0f, this.shakeStarted / this.shakeDuration);
      this.Position += new Vector2((float) Math.Cos(Globals.GameTime.TotalGameTime.TotalMilliseconds * 1.1) * num, (float) Math.Sin(0.3 + Globals.GameTime.TotalGameTime.TotalMilliseconds * 1.7) * num);
      if ((double) this.shakeStarted <= (double) this.shakeDuration)
        return;
      this.shake = false;
      this.shakeStarted = 0.0f;
    }

    public void MoveTo(Vector2 position) => this.moveToPosition = position;

    public void MoveBy(Vector2 position) => this.moveToPosition += position;

    public Vector2 ScreenToWorldSpace(Vector2 position)
    {
      return Vector2.Transform(position, Matrix.Invert(this.WorldTranformMatrix));
    }

    public Vector2 WorldToScreenSpace(Vector2 position)
    {
      return Vector2.Transform(position, this.WorldTranformMatrix) / Math.Abs((Config.SCALE_X + Config.SCALE_Y) / 2); 
    }

    public void TrackTarget(Entity entity, bool immediate = false, Vector2 tracingOffset = default (Vector2))
    {
      this.targetTracingOffset = tracingOffset;
      this.target = entity;
      if (!immediate)
        return;
      this.ImmediateRecenter();
    }

    public void StopTracking() => this.target = (Entity) null;

    public void ImmediateRecenter()
    {
      if (this.target == null)
        return;
      this.Position = this.target.Transform.Position + this.targetTracingOffset - new Vector2((float) this.Viewport.Width / 2f, (float) this.Viewport.Height / 2f);
    }

    public Vector2 Position
    {
      get => this.position;
      set
      {
        this.position = value;
        this.ValidatePosition();
      }
    }

    public float Zoom
    {
      get => this.zoom;
      set
      {
        this.zoom = MathHelper.Max(value, 0.01f);
        this.ValidateZoom();
        this.ValidatePosition();
      }
    }

    public Rectangle? Limits
    {
      set
      {
        this.limits = value;
        this.ValidateZoom();
        this.ValidatePosition();
      }
    }

    public Matrix WorldTranformMatrix
    {
      get
      {
        return Matrix.CreateTranslation(new Vector3(-new Vector2(this.position.X * this.scrollSpeedModifier, this.position.Y), 0.0f)) * Matrix.CreateTranslation(new Vector3(-this.origin, 0.0f)) * Matrix.CreateRotationZ(this.rotation) * Matrix.CreateScale(this.zoom, this.zoom, 1f) * Matrix.CreateTranslation(new Vector3(this.origin, 0.0f));
      }
    }

    private void ValidatePosition()
    {
      if (!this.limits.HasValue)
        return;
      Vector2 vector2_1 = Vector2.Transform(Vector2.Zero, Matrix.Invert(this.WorldTranformMatrix));
      Vector2 vector2_2 = new Vector2((float) this.Viewport.Width, (float) this.Viewport.Height) / this.zoom;
      Vector2 min = new Vector2((float) this.limits.Value.Left, (float) this.limits.Value.Top);
      Vector2 vector2_3 = new Vector2((float) this.limits.Value.Right, (float) this.limits.Value.Bottom);
      Vector2 vector2_4 = this.position - vector2_1;
      this.position = Vector2.Clamp(vector2_1, min, vector2_3 - vector2_2) + vector2_4;
    }

    private void ValidateZoom()
    {
      if (!this.limits.HasValue)
        return;
      this.zoom = MathHelper.Max(this.zoom, MathHelper.Max((float) this.Viewport.Width / (float) this.limits.Value.Width, (float) this.Viewport.Height / (float) this.limits.Value.Height));
    }

    public Matrix GetUITransformMatrix() => this.uiTransofrmMatrix;

    public Matrix GetWorldTransformMatrix(float scrollSpeedModifier = 1f, bool lockY = false)
    {
      this.scrollSpeedModifier = scrollSpeedModifier;
      return this.WorldTranformMatrix;
    }
  }
}
