using System;
using MoveHandler;
using WeaponsHandler;
using ArmorHandler;
namespace EntityHandler
{
    class Entity{
    public int health{get; set;}
    public int healthMax{get; set;}
    private int baseHealth{get; set;}
    public string name{get; set;}
    public Weapon? weapon{get; set;}
    public Move[] moves{get; set;}
    public Armor? armor{get; set;}
    public Entity(string _name, int _health, params Move[] _moves){
        if(_moves != null){
            moves = _moves;
        } else {
        Move[] foo = {new AtkMove("...",0)};
        moves = foo;
        }
        baseHealth = _health;
        health = _health;
        healthMax = _health;
        name = _name;
    }
    public void printEntity(){
        Console.WriteLine(name + " " + health + "/" + healthMax);
        weapon?.printWeapon();
        armor?.printArmor();
        //object? == is null? if so then;
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
    public void calculateFullHealth(int args = 0){
        healthMax = baseHealth;
        if(armor != null){
            healthMax = (healthMax + armor.strength) * armor.rarity;
        }
        if(args == 1){
            health = healthMax;
        }
    }
    }
}