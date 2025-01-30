namespace Registry.API.Domain
{
    public class UserWorkTimeRecord
    {        
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastRecord { get; set; }
        public string Mode { get; set; }
    }
}
