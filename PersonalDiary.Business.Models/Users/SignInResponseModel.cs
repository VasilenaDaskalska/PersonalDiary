namespace PersonalDiary.Business.Models.Users
{
    public class SignInResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public UserBiz User { get; set; }
    }
}
