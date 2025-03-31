
namespace _4erp.application.Inbound.Authorization
{
    public class Authorized
    {
        public string? Token { get; set; }

        public Authorized(){}
        public Authorized(string token){
            this.Token = token;
        }
    }
}