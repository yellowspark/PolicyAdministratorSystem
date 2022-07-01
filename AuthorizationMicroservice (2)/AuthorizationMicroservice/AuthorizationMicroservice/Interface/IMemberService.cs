using AuthorizationMicroservice.DTO;
using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.Interface
{
    public interface IMemberService
    {
        public AgentDetails AuthenticateUser(MemberLoginDTO member);
        public TokenUserDTO CreateJwt(AgentDetails member);
    }
}
