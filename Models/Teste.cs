
using System.Collections.Generic;

namespace associatetool.Model{

    public class Teste{       
        public Teste(string name, int typeId){
            this.name = name;
            this.currentLife = 0;
            this.typeId = typeId;
            this.currentThingId = null;
        }
        //public int toolId { get; set; }     
        public string name { get; set; }                     
        //public double lifeCycle { get; set; }
        public double currentLife { get; set; }
        //public string unitOfMeasurement { get; set; }
        public string description { get; set; }
        public string serialNumber { get; set; }
        //public string code { get; set; }
        //public int? position {get; set;}
        public int typeId { get; set; }
        //public string typeName { get; set; }        
        public string status = "available";
        public int? currentThingId { get; set; }
        public List<string> informations { get; set; }
    }
}