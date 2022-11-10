namespace MonsterCards;

public class Monster
{
    public int HP { get; set; }
    public int MP { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }
    public string Name { get; set; }
    public string Raze { get; set; }
    
    public Monster(string name, string raze, int hp, int mp, int atk, int def)
    {
        this.Name = name;
        this.Raze = raze;
        this.HP = hp;
        this.MP = mp;
        this.ATK = atk;
        this.DEF = def;
    }
}