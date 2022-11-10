using MonsterCards;

namespace InterpreterDyZ;

public class Internal
{
    public void ShowLine(string text)
    {
        Console.WriteLine($"> {text}");
    }

    public void IncrementHP(ref Monster monster, int hp)
    {
        monster.HP += hp;
    }

    public void DecrementHP(ref Monster monster, int hp)
    {
        monster.HP -= hp;
    }

    public void IncrementMP(ref Monster monster, int mp)
    {
        monster.MP += mp;
    }

    public void DecrementMP(ref Monster monster, int mp)
    {
        monster.MP -= mp;
    }

    public void IncrementATK(ref Monster monster, int atk)
    {
        monster.ATK += atk;
    }

    public void DecrementATK(ref Monster monster, int atk)
    {
        monster.ATK -= atk;
    }

    public void IncrementDEF(ref Monster monster, int def)
    {
        monster.DEF += def;
    }

    public void DecrementDEF(ref Monster monster, int def)
    {
        monster.DEF -= def;
    }
}