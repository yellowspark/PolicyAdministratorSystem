using AuthorizationMicroservice.DTO;
using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.Interface
{
    public interface IMemberRepository
    {
        public AgentDetails AuthenticateUser(MemberLoginDTO member);
    }
}
