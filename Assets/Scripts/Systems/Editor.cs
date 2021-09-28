public interface IDamageble
{
    public float AddDamage();
    public void GetDamage(float damage);
}

namespace GetEnumToGame
{
    public enum Parent
    {
        Player,
        Enemy,
    }

    enum Game
    {
        Is,
        None,
    }
}

namespace AttackType
{
    public enum Type
    {
        Shlash,
        Bullet,

        None,
    }
}

namespace AudioType
{
    public enum BGMType
    {
        Start,
        None,
    }

    public enum ClipType
    {
        Slashing,
        Bullet,
        None,
    }
}

namespace UiType
{
    public enum Ui
    {
        PlayerSlider,
        Game,
    }
}
