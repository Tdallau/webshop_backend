namespace Models
{

    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
    }

}