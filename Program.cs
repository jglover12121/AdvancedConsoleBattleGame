using System;
using EntityHandler;
using MoveHandler;
using WeaponsHandler;
using ArmorHandler;

enum Type{
  ATK,
  MAG,
  REC
}

public class Program{
  public static void Main(string[] args){
    AtkMove Slash = new("Slash",5);
    AtkMove Hack = new("Hack",7);
    RecMove Heal = new("Heal", 10);
    Entity plr = new("Player",20,Slash,Hack,Heal);
    plr.armor = new Armor("Silver Armor",15,1);
    plr.weapon = new AtkWeapon("Iron Sword",10,1);
    plr.calculateFullHealth();
    Entity emy = new("Goblin",20,Slash,Heal);
    //emy.weapon = new AtkWeapon("God Slayer",50,3);
    //for the love of god tomorrow give this a proper class
    Battle.begin(ref emy, ref plr);
  }
}
//=================================================

//Battle handle(no class i didnt deem itnecessary)
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
