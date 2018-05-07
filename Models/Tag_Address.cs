using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace associatetool.Model{

    public class Tag_Address{

        public int id { get; set; }
        public string codigo { get; set; }        
        public string vida_util { get; set; }
        public string vida_util_unidade { get; set; }
        public string trigger { get; set; }
        public string nome_ferramenta { get; set; }
        public string vida_util_acumulado { get; set; }
        public string vida_util_status { get; set; }
        public string vida_util_max { get; set; }
        public string ferramenta_id { get; set; }
    }
}