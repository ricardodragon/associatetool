using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace associatetool.Model{
    public class ToolTag{

        public ToolTag(){
            
        }
        public int id { get; set; }
        public string codigo { get; set; }        
        public int vida_util { get; set; }
        public int vida_util_unidade { get; set; }
        public bool trigger { get; set; }
    }
}