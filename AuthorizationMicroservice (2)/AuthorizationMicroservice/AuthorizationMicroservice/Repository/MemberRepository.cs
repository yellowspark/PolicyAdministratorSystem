using AuthorizationMicroservice.DTO;
using AuthorizationMicroservice.Interface;
using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.Repository
{
    public class MemberRepository: IMemberRepository
    {
        public List<AgentDetails> Members;
        public MemberRepository()
        {
            Members = new List<AgentDetails> { 
            new AgentDetails() { Name = "user1",Password="user1" },
            new AgentDetails() { Name = "user2",Password="user2" },
            new AgentDetails() { Name = "user3",Password="user3" },
            new AgentDetails() { Name = "user4",Password="user4" }};

        }
        public AgentDetails AuthenticateUser(MemberLoginDTO member)
        {
            AgentDetails user = Members.FirstOrDefault(u => u.Name == member.UserName && u.Password == member.Password);

            return user;
        }
    }
}
