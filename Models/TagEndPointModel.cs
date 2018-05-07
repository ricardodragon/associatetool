namespace associatetool.Model{
    public class TagEndPointModel {
                        
        public TagEndPointModel(string address, string value, string workstation){
            this.address = address;
            this.workstation = workstation;
            this.value = value;
        }                
        
        public string address { get; set; }
        public string value { get; set;}
        public string workstation { get; set; }

    }
}