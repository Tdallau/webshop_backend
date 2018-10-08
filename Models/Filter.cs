namespace Models {
    public class Filter {
        public int kind {get; set;}
        public string att {get; set;}
        public object value {get; set;}
        public Filter f1 {get; set;}
        public Filter f2{get; set;}
    }
}