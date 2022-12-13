namespace VersionControl.DTO
{
    public class UserData
    {
        public string? id { get; set; }
        public string? title { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? picture { get; set; }
    }

    public class UserResponseData
    {
        public List<UserData> data { get; set; } = new List<UserData>();
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}