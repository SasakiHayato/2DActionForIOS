namespace GetParent
{ 
    public enum Parent
    {
        Player,
        Enemy,
    }
}

namespace EnemyType
{
    public enum Type
    {
        A,
        B,
        C,
    }

    public class EnemyData
    {
        public void Set(Type type, ref float hp, ref float speed)
        {
            switch (type)
            {
                case Type.A:
                    DataA(ref hp, ref speed);
                    break;
                case Type.B:
                    DataB(ref hp, ref speed);
                    break;
                case Type.C:
                    DataC(ref hp, ref speed);
                    break;
                default:
                    break;
            }
        }

        void DataA(ref float hp, ref float speed)
        {
            hp = 1;
            speed = 2;
        }

        void DataB(ref float hp, ref float speed)
        {
            hp = 2;
            speed = 4;
        }

        void DataC(ref float hp, ref float speed)
        {
            hp = 4;
            speed = 2;
        }
    }
}

namespace IManage
{
    public enum Systems
    {
        DiedEnemy,
        TimeRate,

        None,
    }
}

namespace IEnemysBehaviour
{
    public enum ActionType
    {
        Attack,
        Move,

        None,
    }
}



