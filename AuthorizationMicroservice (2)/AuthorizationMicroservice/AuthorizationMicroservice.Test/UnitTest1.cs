using AuthorizationMicroservice.Repository;
using AuthorizationMicroservice.DTO;

namespace AuthorizationMicroservice.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void LoginTest()
        {
            MemberRepository user = new MemberRepository();
            MemberLoginDTO agent = new MemberLoginDTO
            {
                UserName = "user1",
                Password = "user1"
            };
            Assert.AreEqual(user.AuthenticateUser(agent),user.Members.First(x=>x.Name == agent.UserName));
        }
    }
}