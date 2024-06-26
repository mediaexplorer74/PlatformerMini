﻿// Type: ForestPlatformerExample.EntityParser
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace ForestPlatformerExample
{
    class EntityParser
    {
        private LDTKMap world;

        private Hero hero;

        public EntityParser(LDTKMap world)
        {
            this.world = world;
        }

        public void LoadEntities(AbstractScene scene, string levelID)
        {
            Vector2 heroPosition = Vector2.Zero;
            List<(Vector2, int, int)> movingPlatforms = new List<(Vector2, int, int)>();

            foreach (EntityInstance entity in world.ParseLevel(scene))
            {

                Logger.Debug("Parsing entity: " + entity.Identifier);

                Vector2 position = new Vector2(entity.Px[0], entity.Px[1]);
                Vector2 pivot = new Vector2((float)entity.Pivot[0], (float)entity.Pivot[1]);
                if (entity.Identifier.Equals("Hero"))
                {
                    heroPosition = position;
                    hero = new Hero(scene, heroPosition);
                }
                else if (entity.Identifier.Equals("Coin"))
                {
                    bool hasGravity = true;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {
                        if (field.Identifier == "hasGravity")
                        {
                            hasGravity = (bool)field.Value;
                        }
                    }
                    Coin c = new Coin(scene, position);
                    c.HasGravity = hasGravity;
                }
                else if (entity.Identifier.Equals("MovingPlatform"))
                {
                    movingPlatforms.Add((position, (int)entity.Width, (int)entity.Height));
                }
                else if (entity.Identifier.Equals("Spring"))
                {
                    int power = -1;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {
                        if (field.Identifier == "power")
                        {
                            //RnD
                            try
                            {
                                power = (int)field.Value;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("[ex] power cast bug: " + ex.Message);
                                power = 15;
                            }
                        }
                    }
                    Spring spring = new Spring(scene, position, (int)(power));
                }

                else if (entity.Identifier.Equals("Box"))
                {
                    new Box(scene, position);
                }
                else if (entity.Identifier.Equals("Ladder"))
                {
                    new Ladder(scene, position, (int)entity.Width, (int)entity.Height);
                }
                else if (entity.Identifier.Equals("MovingPlatformTurn"))
                {
                    Direction dir = default;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {
                        if (field.Identifier == "Direction")
                        {
                            dir = (Direction)Enum.Parse(typeof(Direction), field.Value.ToString());
                        }

                        new MovingPlatformTurner(scene, position, dir);
                    }
                }
                else if (entity.Identifier.Equals("SlideWall"))
                {
                    new SlideWall(scene, position, (int)entity.Width, (int)entity.Height);
                }
                else if (entity.Identifier.Equals("Spikes"))
                {
                    Direction dir = Direction.EAST;//default;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {
                        if (field.Identifier == "Direction")
                        {
                            try
                            {
                                if (field.Value != null)
                                  dir = (Direction)Enum.Parse(typeof(Direction), field.Value.ToString());
                            }
                            catch
                            {
                                dir = Direction.EAST;
                            }
                        }
                    }
                    float size = entity.Width > entity.Height ? entity.Width : entity.Height;
                    new Spikes(scene, position, (int)size, dir);
                }
                else if (entity.Identifier.Equals("RespawnPoint"))
                {
                    new RespawnPoint(scene, 256, 256, position);
                }
                else if (entity.Identifier.Equals("IceTrigger"))
                {
                    new IceTrigger(scene, (int)entity.Width, (int)entity.Height, position);
                }
                else if (entity.Identifier.Equals("NextLevelTrigger"))
                {
                    new NextLevelTrigger(scene, position, (int)entity.Width, (int)entity.Height);
                }
                else if (entity.Identifier.Equals("EnemyTrunk"))
                {
                    new Trunk(scene, position, Direction.WEST);
                }
                else if (entity.Identifier.Equals("EnemySpikedTurtle"))
                {
                    new SpikedTurtle(scene, position, Direction.WEST);
                }
                else if (entity.Identifier.Equals("EnemyCarrot"))
                {
                    int speed = -1;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {

                        if (field.Identifier == "speed")
                        {
                            try
                            {
                                object v = field.Value;
                                speed = (int)Math.Round((decimal)v);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("[ex] speed cast bug: " + ex.Message);
                                speed = 0;//1;
                            }
                        }
                    }
                    Carrot carrot = new Carrot(scene, position, Direction.EAST);
                }
                else if (entity.Identifier.Equals("EnemyIceCream"))
                {
                    new IceCream(scene, position);
                }
                else if (entity.Identifier.Equals("Saw"))
                {
                    bool horizontal = true;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {

                        if (field.Identifier == "HorizontalMovement")
                        {
                            horizontal = (bool)field.Value;
                        }
                    }
                    new Saw(scene, position, horizontal, pivot);
                }
                else if (entity.Identifier.Equals("SawPath"))
                {
                    new SawPath(scene, position, (int)entity.Width, (int)entity.Height);
                }
                else if (entity.Identifier.Equals("Fan"))
                {
                    int forceFeildHeight = -1;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {

                        if (field.Identifier == "forceFeildHeight")
                        {
                            try
                            {
                                forceFeildHeight = (int)field.Value;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("[ex] EntityParser - forceFeildHeight error: " + ex.Message);
                                //PLAN B
                                forceFeildHeight = 256;
                            }
                        }
                    }
                    new Fan(scene, position, forceFeildHeight);
                }
                else if (entity.Identifier.Equals("EnemyRock"))
                {
                    RockSize size = (RockSize)RockSize.SMALL;//default;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {

                        if (field.Identifier == "RockSize")
                        {
                            try
                            {
                                if (field.Value != null)
                                  size = (RockSize)Enum.Parse(typeof(RockSize), field.Value.ToString());
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("RockSize parsing error: " + ex.Message);
                                size = (RockSize)RockSize.SMALL;
                            }
                        }
                    }
                    new Rock(scene, position, size);
                }
                else if (entity.Identifier.Equals("EnemyGhost"))
                {
                    new Ghost(scene, position);
                }
                else if (entity.Identifier.Equals("GameFinishedTrophy"))
                {
                    new GameFinishTrophy(scene, position, pivot);
                }
                else if (entity.Identifier.Equals("PopupTextTrigger"))
                {
                    string textName = null;
                    foreach (FieldInstance field in entity.FieldInstances)
                    {

                        if (field.Identifier == "TextName")
                        {
                            textName = field.Value.ToString();
                        }
                    }
                    new PopupTrigger(scene, position, (int)entity.Width, (int)entity.Height, textName);
                }
            }

            foreach ((Vector2, int, int) mp in movingPlatforms)
            {
                new MovingPlatform(scene, mp.Item1, mp.Item2, mp.Item3);
            }

#if false//DEBUG
            
             PhysicalEntity collisionTest = new PhysicalEntity(scene.LayerManager.EntityLayer, null, new Vector2(17, 37) * Config.GRID)
             {
                 HasGravity = false
             };
              collisionTest.SetSprite(default);//(Assets.CreateRectangle(64, 64, Color.Yellow));
             collisionTest.AddTag("Mountable");
             //collisionTest.AddComponent(new BoxCollisionComponent(collisionTest, 32, 32, new Vector2(-16, -16)));
             collisionTest.AddComponent(new BoxCollisionComponent(collisionTest, 64, 64, Vector2.Zero));
             //(collisionTest.GetCollisionComponent() as BoxCollisionComponent).DEBUG_DISPLAY_COLLISION = true;
#endif

        }

        public void LoadIntGrid(AbstractScene scene)
        {
            foreach (KeyValuePair<Vector2, int> entry in world.GetIntGrid(scene, "Colliders"))
            {
                Vector2 position = new Vector2(entry.Key.X, entry.Key.Y);
                StaticCollider e = new StaticCollider(scene, position);

                switch (entry.Value)
                {
                    case 1:
                        e.AddTag("Collider");
                        break;
                    case 2:
                        e.AddTag("SlideWall");
                        break;
                    case 3:
                        //e.AddTag("Platform");
                        break;
                    case 4:
                        //e.AddTag("Ladder");
                        //e.BlocksMovement = false;
                        break;
                    case 5:
                        e.AddTag("Platform");
                        e.AddBlockedDirection(Direction.WEST);
                        break;
                    case 6:
                        e.AddTag("Platform");
                        e.AddBlockedDirection(Direction.EAST);
                        break;
                    case 7:
                        e.AddTag("Platform");
                        e.AddBlockedDirection(Direction.NORTH);
                        break;
                    case 8:
                        e.AddTag("Platform");
                        e.AddBlockedDirection(Direction.SOUTH);
                        break;
                }
            }
        }

        public Hero GetHero()
        {
            return hero;
        }
    }
}
