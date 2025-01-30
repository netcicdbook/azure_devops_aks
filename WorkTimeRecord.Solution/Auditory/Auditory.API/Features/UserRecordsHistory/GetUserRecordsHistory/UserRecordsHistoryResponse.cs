namespace Auditory.API.Features.UserRecordsHistory.GetUserRecordsHistory
{
    public class UserRecordsHistoryResponse
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime lastRecord { get; set; }
        public string mode { get; set; }
    }
}