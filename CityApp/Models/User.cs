using System;

namespace CityApp
{
    public class UserData
    {
        public string name { get; set; }
        public string mobile { get; set; }
        public string location { get; set; }
    }
    public class Target
    {
        public string targetId { get; set; }
        public double title { get; set; }
        public DateTime startDate { get; set; }
        public string days { get; set; }
        public string locationName { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Model<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }

}
