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

    public enum Game
    {
        Main,
        Titel,
        Result,
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
        
        None,
    }
}

namespace UiType
{
    public enum Ui
    {
        PlayerSlider,
        PlayerHp,
        Game,
    }
}
