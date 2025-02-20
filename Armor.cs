using System; 

namespace ArmorHandler{
    interface IArmor{
        string name{get; set;}
        int strength{get; set;}
        int rarity{get; set;}
        void printArmor();
    }

    class Armor : IArmor{
        public string name{get; set;}
        public int strength{get; set;}
        public int rarity{get; set;}
        public Armor(string _name, int _strength, int _rarity){
            name = _name;
            strength = _strength;
            rarity = _rarity;
        }
        public void printArmor(){
            Console.WriteLine(name + ": " + strength + "(" + rarity + ")");
        }
        
    }
}