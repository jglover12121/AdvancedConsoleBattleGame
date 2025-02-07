using System;

enum Type{
  ATK,
  MAG,
  REC
}

public class HelloWorld{
  public static void Main(string[] args){
    AtkMove Slash = new("Slash",30);
    AtkMove Hack = new("Hack",25);
    RecMove Heal = new("Heal", 10);
    Entity plr = new("Player",25,Slash,Hack,Heal);
    plr.weapon = new AtkWeapon("Iron Sword",30,2);
    Entity emy = new("Goblin",30,Slash,Heal);
    //for the love of god tomorrow give this a proper class
    Console.WriteLine("=====================");
    plr.printEntity();
    Console.WriteLine("=====================");
    emy.printEntity();
    Console.WriteLine("=====================");
    string? input = Console.ReadLine();
    if(int.TryParse(input,out int number)){
      plr.useMove(number,ref emy, ref plr);
    }
    Console.WriteLine("\n=====================");
    plr.printEntity();
    Console.WriteLine("=====================");
    emy.printEntity();
    Console.WriteLine("=====================");
    input = Console.ReadLine();
    if(int.TryParse(input,out number)){
      plr.useMove(number,ref emy, ref plr);
    }else{
      plr.useMove(0,ref emy, ref plr);
    }
    Console.WriteLine("\n=====================");
    plr.printEntity();
    
    Console.WriteLine("=====================");
    emy.printEntity();
    Console.WriteLine("=====================");
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
        moves[i].use(ref enemy, ref player);
      }
    } else {
      moves[i].use(ref enemy, ref player);
    }
  }
}
