using System;

public class GameObject
{
    private int id;
    private string name;
    private int x;
    private int y;

    public GameObject(int id, string name, int x, int y)
    {
        this.id = id;
        this.name = name;
        this.x = x;
        this.y = y;
    }

    public int getId() => id;

    public string getName() => name;

    public int getX() => x;

    public int getY() => y;
}

public class Unit : GameObject
{
    private bool alive;
    private float hp;

    public Unit(int id, string name, int x, int y, float hp) : base(id, name, x, y)
    {
        this.hp = hp;
        alive = true;
    }

    public bool isAlive() => alive;

    public float getHp() => hp;

    public void receiveDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) alive = false;
    }
}

public interface Attacker
{
    void attack(Unit unit);
}

public interface Moveable
{
    void move(int newX, int newY);
}

public class Archer : Unit, Attacker, Moveable
{
    public Archer(int id, string name, int x, int y, float hp) : base(id, name, x, y, hp)
    {
    }

    public void attack(Unit unit)
    {
        unit.receiveDamage(10); 
    }

    public void move(int newX, int newY)
    {
        Console.WriteLine($"{getName()} перемещается с ({getX()}, {getY()}) на ({newX}, {newY})");
    }
}

public class Building : GameObject
{
    private bool built;

    public Building(int id, string name, int x, int y) : base(id, name, x, y)
    {
        built = false;
    }

    public bool isBuilt() => built;
}

public class Fort : Building, Attacker
{
    public Fort(int id, string name, int x, int y) : base(id, name, x, y)
    {
    }

    public void attack(Unit unit)
    {
        unit.receiveDamage(20);
    }
}

public class MobileHome : Building, Moveable
{
    public MobileHome(int id, string name, int x, int y) : base(id, name, x, y)
    {
    }

    public void move(int newX, int newY)
    {
        Console.WriteLine($"{getName()} перемещается с ({getX()}, {getY()}) на ({newX}, {newY})");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Archer archer = new Archer(1, "Лучник", 0, 0, 100);
        Fort fort = new Fort(2, "Крепость", 10, 10);
        MobileHome mobileHome = new MobileHome(3, "Дом на колесах", 20, 20);

        Console.WriteLine($"{archer.getName()} на позиции ({archer.getX()}, {archer.getY()}) с HP: {archer.getHp()}");

        fort.attack(archer);
        Console.WriteLine($"После атаки крепости, {archer.getName()} HP: {archer.getHp()}");

        archer.move(5, 5);
        mobileHome.move(15, 15);
    }
}
