// Decompiled with JetBrains decompiler
// Type: MonolithEngine.CollisionEngine
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System;
using System.Collections.Generic;
using System.Linq;


namespace MonolithEngine
{
  public class CollisionEngine
  {
    private Dictionary<Type, HashSet<IColliderEntity>> allColliders = new Dictionary<Type, HashSet<IColliderEntity>>();
    private Dictionary<Type, HashSet<Type>> collisionTypeCache = new Dictionary<Type, HashSet<Type>>();
    private Dictionary<IColliderEntity, Dictionary<IColliderEntity, bool>> collisions = new Dictionary<IColliderEntity, Dictionary<IColliderEntity, bool>>();
    private Dictionary<IHasTrigger, Dictionary<string, Dictionary<IGameObject, bool>>> triggers = new Dictionary<IHasTrigger, Dictionary<string, Dictionary<IGameObject, bool>>>();
    private HashSet<IGameObject> changedObjects = new HashSet<IGameObject>();

    public void ObjectChanged(IGameObject entity)
    {
      this.changedObjects.AddIfMissing<IGameObject>(entity);
    }

    public void CheckCollisions(IColliderEntity thisEntity = null)
    {
      if (this.changedObjects.Count == 0 && this.allColliders.Count == 0)
        return;
      this.HandleChangedObjects();
      if (!thisEntity.CollisionsEnabled || !this.collisionTypeCache.ContainsKey(thisEntity.GetType()))
        return;
      foreach (Type key in this.collisionTypeCache[thisEntity.GetType()])
      {
        if (this.allColliders.ContainsKey(key))
        {
          foreach (IColliderEntity otherObject in this.allColliders[key])
          {
            if (otherObject.CanFireTriggers && thisEntity.GetTriggeredAgainst().Contains(otherObject.GetType()))
              this.CheckTriggers(thisEntity, (IGameObject) otherObject);
            if (otherObject.CollisionsEnabled && thisEntity.GetCollisionComponent() != null && thisEntity.GetCollidesAgainst().ContainsKey(otherObject.GetType()) && otherObject.GetCollisionComponent() != null)
              this.CheckCollision(thisEntity, otherObject, thisEntity.GetCollidesAgainst()[otherObject.GetType()]);
          }
        }
      }
      this.InactivateCollisionsAndTriggers(thisEntity);
    }

    private void HandleChangedObjects()
    {
      if (this.changedObjects.Count <= 0)
        return;
      foreach (IColliderEntity changedObject in this.changedObjects)
      {
        if (changedObject.IsDestroyed)
        {
          if (this.allColliders.ContainsKey(changedObject.GetType()))
          {
            this.allColliders[changedObject.GetType()].RemoveIfExists<IColliderEntity>(changedObject);
            if (this.allColliders[changedObject.GetType()].Count == 0)
              this.allColliders.Remove(changedObject.GetType());
          }
        }
        else
        {
          if (changedObject.GetCollisionComponent() != null)
          {
            if (!this.collisions.ContainsKey(changedObject))
              this.collisions[changedObject] = new Dictionary<IColliderEntity, bool>();
            HashSet<IColliderEntity> orDefault1 = this.allColliders.GetOrDefault<Type, HashSet<IColliderEntity>>(changedObject.GetType(), new HashSet<IColliderEntity>());
            orDefault1.Add(changedObject);
            this.allColliders[changedObject.GetType()] = orDefault1;
            foreach (Type key in changedObject.GetCollidesAgainst().Keys)
            {
              HashSet<Type> orDefault2 = this.collisionTypeCache.GetOrDefault<Type, HashSet<Type>>(changedObject.GetType(), new HashSet<Type>());
              orDefault2.Add(key);
              this.collisionTypeCache[changedObject.GetType()] = orDefault2;
            }
          }
          if (changedObject.GetTriggers().Count > 0)
          {
            this.triggers[(IHasTrigger) changedObject] = new Dictionary<string, Dictionary<IGameObject, bool>>();
            foreach (ITrigger trigger in (IEnumerable<ITrigger>) changedObject.GetTriggers())
              this.triggers[(IHasTrigger) changedObject][trigger.GetTag()] = new Dictionary<IGameObject, bool>();
          }
          foreach (Type type in changedObject.GetTriggeredAgainst())
          {
            HashSet<Type> orDefault = this.collisionTypeCache.GetOrDefault<Type, HashSet<Type>>(changedObject.GetType(), new HashSet<Type>());
            orDefault.Add(type);
            this.collisionTypeCache[changedObject.GetType()] = orDefault;
          }
        }
      }
      this.changedObjects.Clear();
    }

    private void CheckCollision(
      IColliderEntity thisEntity,
      IColliderEntity otherObject,
      bool allowOverlap)
    {
      if (!thisEntity.GetCollisionComponent().CollidesWith(otherObject))
        return;
      if (!this.collisions.ContainsKey(thisEntity) || !this.collisions[thisEntity].ContainsKey(otherObject))
        thisEntity.CollisionStarted((IGameObject) otherObject, allowOverlap);
      this.collisions[thisEntity][otherObject] = true;
    }

    private void CheckTriggers(IColliderEntity thisEntity, IGameObject otherObject)
    {
      foreach (ITrigger trigger in (IEnumerable<ITrigger>) thisEntity.GetTriggers())
      {
        if (trigger.IsInsideTrigger(otherObject))
        {
          if (!this.triggers[(IHasTrigger) thisEntity][trigger.GetTag()].ContainsKey(otherObject))
            thisEntity.OnEnterTrigger(trigger.GetTag(), otherObject);
          this.triggers[(IHasTrigger) thisEntity][trigger.GetTag()][otherObject] = true;
        }
      }
    }

    public List<IColliderEntity> GetCollidesWith(IColliderEntity collider)
    {
      List<IColliderEntity> collidesWith = new List<IColliderEntity>();
      if (this.collisions.ContainsKey(collider))
      {
        foreach (IColliderEntity key in this.collisions[collider].Keys)
        {
          if (this.collisions[collider][key])
            collidesWith.Add(key);
        }
      }
      return collidesWith;
    }

    private void InactivateCollisionsAndTriggers(IColliderEntity toUpdate)
    {
      foreach (IColliderEntity key in this.collisions.Keys)
      {
        if (!key.IsDestroyed && (toUpdate == null || key.Equals((object) toUpdate)))
        {
          foreach (IColliderEntity colliderEntity in this.collisions[key].Keys.ToList<IColliderEntity>())
          {
            if (!key.Equals((object) colliderEntity))
            {
              if (!this.collisions[key][colliderEntity])
              {
                key.CollisionEnded((IGameObject) colliderEntity);
                this.collisions[key].Remove(colliderEntity);
              }
              else
                this.collisions[key][colliderEntity] = false;
            }
          }
        }
      }
      foreach (IHasTrigger key1 in this.triggers.Keys)
      {
        if (!key1.IsDestroyed && (toUpdate == null || key1.Equals((object) toUpdate)))
        {
          foreach (string key2 in this.triggers[key1].Keys)
          {
            foreach (IHasTrigger hasTrigger in this.triggers[key1][key2].Keys.ToList<IGameObject>())
            {
              if (!this.triggers[key1][key2][(IGameObject) hasTrigger])
              {
                key1.OnLeaveTrigger(key2, (IGameObject) hasTrigger);
                this.triggers[key1][key2].Remove((IGameObject) hasTrigger);
              }
              else
                this.triggers[key1][key2][(IGameObject) hasTrigger] = false;
            }
          }
        }
      }
    }

    public void PostUpdate()
    {
    }

    public void Destroy() => this.HandleChangedObjects();
  }
}
