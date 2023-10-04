using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StudentCrud.Auth;
using StudentCrud.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCrudTest
{
    //trouble using Moq directly on UserManager<IdentityUser>
    //creating this instead
    public class InMemoryUserManager : UserManager<IdentityUser>
    {
        public InMemoryUserManager()
            : base(new Mock<IUserStore<IdentityUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<IdentityUser>>().Object,
                   new IUserValidator<IdentityUser>[0],
                   new IPasswordValidator<IdentityUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }

        public override Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(new IdentityUser { UserName = userName });
        }

        public override Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            // Simplified password check for the purpose of the example
            return Task.FromResult(user.PasswordHash == password);
        }
    }


    [TestClass]
    public class AuthenticateTest
    {
        [DataTestMethod]
        [DataRow("abc1", "xyz3")]
        [DataRow("abc2", "xyz2")]
        [DataRow("abc3", "xyz1")]
        public async Task VerifyCredentialsTest(string uname, string upassword)
        {
            //prepare data
            var user = new IdentityUser { UserName = uname, PasswordHash = upassword };
            var userManagerMock = new InMemoryUserManager();
            LoginModel model = new LoginModel { upassword = upassword };

            //act
            AuthenticateManager manager = new AuthenticateManager();
            bool validCreds = await manager.VerifyCredentials(user, userManagerMock, model);

            //assert
            Assert.IsTrue(validCreds);

        }

    }
}
