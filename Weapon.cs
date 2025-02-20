using System;
// WEAPONS ========================================
//weapon Class
namespace WeaponsHandler
{
    class Weapon {
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
    //inherits since they have litterally the same but simplistity ig?
    class RecWeapon : Weapon{
        public new Type type = Type.REC;
        public RecWeapon(string _name, int _strength, int _rarity) : base(_name,_strength,_rarity){}
    }
    class AtkWeapon : Weapon{
        public new Type type = Type.ATK;
        public AtkWeapon(string _name, int _strength, int _rarity) : base(_name,_strength,_rarity){}
    }
}