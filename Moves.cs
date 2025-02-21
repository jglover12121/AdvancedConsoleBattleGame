using System;
using EntityHandler;
//handles moves and all that
namespace MoveHandler{
    class Move{
        public string name{get; set;}
        public int strength{get; set;}
        public Type type{get; set;}
        public Move(string _name,int _strength){
            name = _name;
            strength = _strength;
        }
        //has to be granted by inheritance
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
            //heals
            player.health += strength;
        }
    }
    class AtkMove : Move{
        public AtkMove(string _name, int _strength) : base(_name,_strength){type = Type.ATK;}
        public override void use(ref Entity enemy, ref Entity player){
            //deals damage
            enemy.health -= strength;
        }
    }
}