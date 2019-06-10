namespace api.appstore.Models
{
    public class QRLoginUserModel
    {
        public QRLoginUserModel()
        {
            deviceid = "0";
            appname = "AppStore";
        }
        public string username { get; set; }
        public string password { get; set; }
        public string deviceid { get; set; }
        public string appname { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string ContactDetails { get; set; }
        public string ContractName { get; set; }
        public string UserCategoryId { get; set; }
        public string UserCategory { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}