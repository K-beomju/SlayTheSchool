//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EntityManager : Singleton<EntityManager>
//{
//    protected override void Awake()
//    {
//        base.Awake();
//    }

//    [SerializeField] private GameObject entityPrefab;
//    [SerializeField] private List<Entity> myEntitys;
//    [SerializeField] private Entity myEmptyEntity;


//    private const int MAX_ENTITY_COUNT = 4;
//    public bool IsFullMyEntities => myEntitys.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;
//    private bool ExistMyEmptyEntity => myEntitys.Exists(x => x == myEmptyEntity);
//    private int MyEmptyEntityIndex => myEntitys.FindIndex(x => x == myEmptyEntity);

//    private void EntityAlignMent()
//    {
//        float targetY = 1.5f;

//        for (int i = 0; i < myEntitys.Count; i++)
//        {
//            float targetX = (myEntitys.Count - 1) * -3.4f + i * 3f - 1f;

//            var targetEntity = myEntitys[i];
//            targetEntity.originPos = new Vector3(targetX, targetY, 0);
//            targetEntity.MoveTransform(targetEntity.originPos, false);
//            targetEntity.GetComponent<Order>()?.SetOriginOrder(i);
//            targetEntity.GetComponent<PlayerHealth>()?.ActiveMove();
//        }

//    }

//    public void InsertMyEmptyEntity(float xPos)
//    {
//        if (IsFullMyEntities) 
//            return;

//        if(!ExistMyEmptyEntity)
//            myEntitys.Add(myEmptyEntity);

//        Vector3 emptyEntityPos = myEmptyEntity.transform.position;
//        emptyEntityPos.x = xPos;
//        myEmptyEntity.transform.position = emptyEntityPos;

//        int _emptyEntityIndex = MyEmptyEntityIndex;
//        myEntitys.Sort((entity1,entity2) => entity1.transform.position.x.CompareTo(entity2.transform.position.x));
//        if (MyEmptyEntityIndex != _emptyEntityIndex)
//            EntityAlignMent();
//    }

//    public void RemoveMyEmptyEntity()
//    {
//        if (!ExistMyEmptyEntity) // 내 빈 오브젝트가 있다면 
//            return;

//        myEntitys.RemoveAt(MyEmptyEntityIndex);
//        EntityAlignMent();
//    }

//    public bool SpawnEntity(Item item, Vector3 spawnPos)
//    {
//        if (IsFullMyEntities || !ExistMyEmptyEntity) return false;

//        var entityObject = Instantiate(entityPrefab, spawnPos, Utils.QI);
//        var entity = entityObject.GetComponent<Entity>();

//        myEntitys[MyEmptyEntityIndex] = entity;

//        entity.SetUp(item);
//        EntityAlignMent();

//        return true;
//    }

//}
