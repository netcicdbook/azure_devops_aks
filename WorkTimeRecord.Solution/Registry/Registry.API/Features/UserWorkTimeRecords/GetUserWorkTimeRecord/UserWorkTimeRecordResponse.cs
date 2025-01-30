namespace Registry.API.Features.UserWorkTimeRecords.GetUserWorkTimeRecord
{
    public class UserWorkTimeRecordResponse
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime lastRecord { get; set; }
        public string mode { get; set; }
    }
}
