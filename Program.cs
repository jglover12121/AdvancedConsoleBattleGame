using System;

enum Type{
  ATK,
  MAG,
  REC
}

public class HelloWorld{
  public static void Main(string[] args){
    AtkMove Slash = new("Slash",5);
    AtkMove Hack = new("Hack",7);
    RecMove Heal = new("Heal", 10);
    Entity plr = new("Player",40,Slash,Hack,Heal);
    plr.weapon = new AtkWeapon("Iron Sword",10,2);
    Entity emy = new("Goblin",50,Slash,Heal);
    emy.weapon = new AtkWeapon("God Slayer",50,3);
    //for the love of god tomorrow give this a proper class
    Battle.begin(ref emy, ref plr);
  }
}
//=================================================
//moves interface
interface IMove{
  string name{get; set;}
  Type type{get; set;}
  int strength{get; set;}
  void use(ref Entity enemy, ref Entity player);
  void printMove();
}
// MOVES ==========================================
//implements the interface
class Move : IMove{
  public string name{get; set;}
  public int strength{get; set;}
  public Type type{get; set;}
  public Move(string _name,int _strength){
    name = _name;
    strength = _strength;
  }
  public virtual void use(ref Entity enemy, ref Entity player){}
  //left empty since its base class
  public void printMove(){
    Console.WriteLine(name + ": " + strength + " | " + type);
  }
}
//inherits since they have litterally the same
class RecMove : Move{
  public RecMove(string _name, int _strength) : base(_name,_strength){type = Type.REC;}
  public override void use(ref Entity enemy, ref Entity player){
    player.health += strength;
  }
}
class AtkMove : Move{
  public AtkMove(string _name, int _strength) : base(_name,_strength){type = Type.ATK;}
  public override void use(ref Entity enemy, ref Entity player){
    enemy.health -= strength;
  }
}
// WEAPONS ========================================
//weapon interface
interface IWeapon{
  string name{get; set;}
  int strength{get; set;}
  int rarity{get; set;}
  Type type{get; set;}
  void printWeapon();
}
//weapon implementation base class
class Weapon : IWeapon{
  public string name{get; set;}
  public int strength{get; set;}
  public int rarity{get; set;}
  public Type type{get; set;}
  public Weapon(string _name, int _strength, int _rarity){
    name = _name;
    strength = _strength;
    rarity = _rarity;
  }
  public void printWeapon(){
    Console.WriteLine(name + ": " + strength + "(" + rarity + ") | " + type);
  }
}
//inherits since they have litterally the same
class RecWeapon : Weapon{
  public new Type type = Type.REC;
  public RecWeapon(string _name, int _strength, int _rarity) : base(_name,_strength,_rarity){}
}
class AtkWeapon : Weapon{
  public new Type type = Type.ATK;
  public AtkWeapon(string _name, int _strength, int _rarity) : base(_name,_strength,_rarity){}
}
//Armor class 
interface IArmor{
    public string name;
    public int strength;
    public int rarity;
    public void printArmor();
    public void calculateDamage();
}

class Armor{
  public string name{get; set;}
  public int strength{get; set;}
  public int rarity{get; set;}
  public Armor(string _name, int _strength, int _rarity){
    
  }
  public void printWeapon(){
    Console.WriteLine(name + ": " + strength + "(" + rarity + ")");
  }
}


//entity implementation
class Entity{
  public int health{get; set;}
  public int healthMax{get; set;}
  public string name{get; set;}
  public IWeapon? weapon{get; set;}
  public IMove[] moves{get; set;}
  public Entity(string _name, int _health, params IMove[] _moves){
    if(_moves != null){
      moves = _moves;
    } else {
      IMove[] foo = {new AtkMove("...",0)};
      moves = foo;
    }
    health = _health;
    healthMax = _health;
    name = _name;
  }
  public void printEntity(){
    Console.WriteLine(name + " " + health + "/" + healthMax);
    if(weapon != null){
      weapon.printWeapon();
    }
    printMoves();
  }

  public void printMoves(){
    for(int i=0;i < moves.Length; i++){
      moves[i].printMove();
    }
  }
  public void useMove(int i, ref Entity enemy, ref Entity player){
    if(weapon != null){
      if(weapon.type == moves[i].type){
        int storedStrength = moves[i].strength;
        moves[i].strength += weapon.strength;
        moves[i].strength *= weapon.rarity;
        Console.WriteLine("Used: " + moves[i].name + " | " + moves[i].type + " | " + moves[i].strength);
        moves[i].use(ref enemy, ref player);
        moves[i].strength = storedStrength;
      } else {
        Console.WriteLine("Used: " + moves[i].name + " | " + moves[i].type + " | " + moves[i].strength);
        moves[i].use(ref enemy, ref player);
      }
    } else {
      Console.WriteLine("Used: " + moves[i].name + " | " + moves[i].type + " | " + moves[i].strength);
      moves[i].use(ref enemy, ref player);
    }
  }
}


class Battle{
  public static void begin(ref Entity enemy, ref Entity player){
    string nextPhase = "player";
    bool battleOver = false;
    Random rnd = new();
    while(!battleOver){
      switch (nextPhase)
      {
        case "player":
          player.printEntity();
          Console.WriteLine("===========================");
          string? input = Console.ReadLine();
          Console.WriteLine("===========================");
          if(input == null) { input = "1";}
          if(int.TryParse(input,out int num)){
            player.useMove(num-1, ref enemy, ref player);
          }
          Console.WriteLine("===========================");
          if(enemy.health <= 0){
            battleOver = true;
            nextPhase = "finished";
          }
          nextPhase = "enemy";
          break;
        case "enemy":
          enemy.printEntity();
          Console.WriteLine("===========================");
          enemy.useMove(0,ref player, ref enemy);
          Console.WriteLine("===========================");
          if(player.health <= 0){
            battleOver = true;
            nextPhase = "finished";
          }
          nextPhase = "player";
          break;
          case "finished":
          battleOver = true;
          Console.WriteLine(enemy.health);
          break;
          default:
          battleOver = false;
          break;
      }
    }
  }
}
