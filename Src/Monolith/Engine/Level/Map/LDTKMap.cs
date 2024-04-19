// Type: MonolithEngine.LDTKMap

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class LDTKMap
  {
    private readonly string PREFIX = "../"; //"Content/" //look inside "level.json" to undestend assets path :)
    private readonly string EXTENSION_DOT = ".";
    private readonly string BACKGROUND = "Background";
    private readonly string FOREGROUND = "Foreground";
    private readonly string PARALLAX = "Parallax";
    private readonly string COLLIDERS = "Colliders";
    private TileGroup tileGroup;
    private TileGroup mergedBackgroundTileGroup;
    private Layer mergedBackgroundLayer;
    private TileGroup mergedForegroundTileGroup;
    private Layer mergedForegroundLayer;
    private LDTKJson world;

    public LDTKMap(LDTKJson json)
    {
      this.world = json;
      foreach (TilesetDefinition tileset in this.world.Defs.Tilesets)
      {
        string monoGameContentName = this.GetMonoGameContentName(tileset.RelPath);
        Assets.LoadTexture(monoGameContentName, monoGameContentName);
      }
    }

    public List<string> GetLevelNames()
    {
      List<string> levelNames = new List<string>();
      foreach (Level level in this.world.Levels)
        levelNames.Add(level.Identifier);
      levelNames.Sort();
      return levelNames;
    }

    public Vector2 GetLevelSize(string levelID)
    {
      List<string> stringList = new List<string>();
      foreach (Level level in this.world.Levels)
      {
        if (level.Identifier.Equals(levelID))
          return new Vector2((float) level.PxWid, (float) level.PxHei);
      }
      throw new Exception("Level not found.");
    }

    private static int CompareLayerInstances(LayerInstance li1, LayerInstance li2)
    {
      return li1.Identifier.CompareTo(li2.Identifier);
    }

    public HashSet<EntityInstance> ParseLevel(AbstractScene scene)
    {
      Logger.Debug("Parsing level...");
      
      HashSet<EntityInstance> level_entity_instance = new HashSet<EntityInstance>();
      
      this.mergedBackgroundLayer = scene.LayerManager.CreateBackgroundLayer();
      this.mergedForegroundLayer = scene.LayerManager.CreateForegroundLayer();

      foreach (Level world_level in this.world.Levels)
      {
        //RnD
        int pxWid = (int) world_level.PxWid; //1400
        int pxHei = (int) world_level.PxHei;

        scene.SetWidth(pxWid);
        scene.SetHeight(pxHei);

        this.mergedBackgroundTileGroup = new TileGroup(pxWid, pxHei);
        this.mergedForegroundTileGroup = new TileGroup(pxWid, pxHei);

        if (world_level.Identifier.Equals(scene.GetName()))
        {
          Logger.Debug("Parsing level: " + world_level.Identifier);
          float num = 0.0f;
          Array.Sort<LayerInstance>(world_level.LayerInstances, 
              new Comparison<LayerInstance>(LDTKMap.CompareLayerInstances));
          foreach (LayerInstance layerInstance in world_level.LayerInstances)
          {
            Logger.Debug("Parsing layer: " + layerInstance.Identifier);
            foreach (EntityInstance entityInstance in layerInstance.EntityInstances)
              level_entity_instance.Add(entityInstance);
            string identifier = layerInstance.Identifier;
            Layer layer = (Layer) null;
            Texture2D texture;
            char ch;
            if (identifier.StartsWith(this.COLLIDERS) && layerInstance.GridTiles.Length != 0)
            {
              layer = scene.LayerManager.EntityLayer;
              texture = (Texture2D) null;
            }
            else if (identifier.StartsWith(this.BACKGROUND) && layerInstance.GridTiles.Length != 0)
            {
              LayerManager layerManager = scene.LayerManager;
              ch = identifier[identifier.Length - 1];
              int priority = int.Parse(ch.ToString() ?? "");
              layer = layerManager.CreateBackgroundLayer(priority);
              texture = Assets.GetTexture2D(this.GetMonoGameContentName(layerInstance.TilesetRelPath));
            }
            else if (identifier.StartsWith(this.PARALLAX) && layerInstance.GridTiles.Length != 0)
            {
              num += 0.1f;
              LayerManager layerManager = scene.LayerManager;
              ch = identifier[identifier.Length - 1];
              int priority = int.Parse(ch.ToString() ?? "");
              double scrollSpeedMultiplier = (double) num;
              layer = layerManager.CreateParallaxLayer(priority, (float) scrollSpeedMultiplier, true);
              texture = Assets.GetTexture2D(this.GetMonoGameContentName(layerInstance.TilesetRelPath));
              this.tileGroup = new TileGroup(pxWid, pxHei);
            }
            else if (identifier.StartsWith(this.FOREGROUND) && layerInstance.GridTiles.Length != 0)
              texture = Assets.GetTexture2D(this.GetMonoGameContentName(layerInstance.TilesetRelPath));
            else
              continue;
            Logger.Debug("Loading grid tiles...");
            foreach (TileInstance gridTile in layerInstance.GridTiles)
            {
              TileGroup tileGroup = !identifier.StartsWith(this.BACKGROUND) 
                                ? ( !identifier.StartsWith(this.FOREGROUND) 
                                    ? this.tileGroup 
                                    : this.mergedForegroundTileGroup ) 
                                : this.mergedBackgroundTileGroup;

              int grid = Config.GRID;
              Rectangle sourceRectangle = new Rectangle((int) gridTile.Src[0],
                  (int) gridTile.Src[1], grid, grid);
              Vector2 position = new Vector2((float) gridTile.Px[0], (float) gridTile.Px[1]);
              SpriteEffects spriteEffects = SpriteEffects.None;

              if (gridTile.F != 0L)
              {
                if (gridTile.F == 1L)
                {
                  spriteEffects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                  if (gridTile.F != 2L)
                    throw new Exception("This kind of rotation is not supported yet!");
                  spriteEffects = SpriteEffects.FlipVertically;
                }
              }
              tileGroup.AddTile(texture, position, sourceRectangle, spriteEffects);
            }
            if ( layer != null 
                && !identifier.StartsWith(this.BACKGROUND) 
                && !identifier.StartsWith(this.FOREGROUND) )
            {
              Entity entity = new Entity(layer, startPosition: new Vector2(0.0f, 0.0f));
              entity.SetSprite(new MonolithTexture(this.tileGroup.GetTexture()));
              entity.Active = false;
            }
          }

          Logger.Debug("Starting texture merging...");
          if (!this.mergedBackgroundTileGroup.IsEmpty())
          {
            Texture2D texture = this.mergedBackgroundTileGroup.GetTexture();
            Logger.Debug("Merged background layers into one texture: " + texture.Bounds.ToString());
            Entity entity = new Entity(this.mergedBackgroundLayer, startPosition: new Vector2(0.0f, 0.0f));
            entity.SetSprite(new MonolithTexture(texture));
            entity.Active = false;
          }
          if (!this.mergedForegroundTileGroup.IsEmpty())
          {
            Texture2D texture = this.mergedForegroundTileGroup.GetTexture();
            Logger.Debug("Merged foreground layers into one texture: " + texture.Bounds.ToString());
            Entity entity = new Entity(this.mergedForegroundLayer, startPosition: new Vector2(0.0f, 0.0f));
            entity.SetSprite(new MonolithTexture(texture));
            entity.Active = false;
          }
        }
      }
      return level_entity_instance;
    }

    public Dictionary<Vector2, int> GetIntGrid(AbstractScene scene, string gridName)
    {
      Dictionary<Vector2, int> intGrid = new Dictionary<Vector2, int>();
      foreach (Level level in this.world.Levels)
      {
        if (level.Identifier.Equals(scene.GetName()))
        {
          foreach (LayerInstance layerInstance in level.LayerInstances)
          {
            if (layerInstance.Identifier.Equals(gridName))
            {
              for (int index = 0; index < layerInstance.IntGridCsv.Length; ++index)
              {
                int num = (int) layerInstance.IntGridCsv[index];
                if (num != 0)
                {
                  int y = (int) Math.Floor((Decimal) index / (Decimal) layerInstance.CWid);
                  int x = (int) ((long) index - (long) y * layerInstance.CWid);
                  intGrid.Add(new Vector2((float) x, (float) y), num);
                }
              }
            }
          }
        }
      }
      return intGrid;
    }

    public Texture2D GetLayerAsTexture(string levelName, string layerName)
    {
      foreach (Level level in this.world.Levels)
      {
        if (!(level.Identifier != levelName))
        {
          TileGroup tileGroup = new TileGroup((int) level.PxWid, (int) level.PxHei);
          foreach (LayerInstance layerInstance in level.LayerInstances)
          {
            if (layerName == layerInstance.Identifier && layerInstance.GridTiles.Length != 0)
            {
              Texture2D texture2D = Assets.GetTexture2D(this.GetMonoGameContentName(
                  layerInstance.TilesetRelPath));

              foreach (TileInstance gridTile in layerInstance.GridTiles)
              {
                long t = gridTile.T;
                int gridSize = (int) layerInstance.GridSize;
                int grid = Config.GRID;
                int num1 = (int) t - gridSize * (int) Math.Floor((Decimal) (t / (long) gridSize));
                int num2 = (int) Math.Floor((Decimal) t / (Decimal) gridSize);

                Rectangle sourceRectangle = new Rectangle((int) gridTile.Src[0], 
                    (int) gridTile.Src[1], grid, grid);

                Vector2 position = new Vector2((float) gridTile.Px[0], (float) gridTile.Px[1]);
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (gridTile.F != 0L)
                {
                  if (gridTile.F == 1L)
                  {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                  }
                  else
                  {
                    if (gridTile.F != 2L)
                      throw new Exception("This kind of rotation is not supported yet!");
                    spriteEffects = SpriteEffects.FlipVertically;
                  }
                }
                tileGroup.AddTile(texture2D, position, sourceRectangle, spriteEffects);
              }
              return tileGroup.GetTexture();
            }
          }
        }
      }
      return (Texture2D) null;
    }

    private string GetMonoGameContentName(string fullpath)
    {
      string str = fullpath.Substring(fullpath.IndexOf(this.PREFIX) + this.PREFIX.Length);
      return str.Substring(0, str.LastIndexOf(this.EXTENSION_DOT));
    }
  }
}
