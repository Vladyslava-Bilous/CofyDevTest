using CofyDevTest.Core.API.Auth;
using CofyDevTest.UI.TestData;
using Reqnroll;

namespace CofyDevTest.UI.StepDefinitions
{
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation(@"^(.*User)$")]
        public UserModel GetUserModelByRole(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            var key = value.Trim();

            return key switch
            {
                "RegularUser" => Users.RegularUser,
                "AdminUser" => Users.AdminUser,
                _ => throw new ArgumentException($"Unknown user role '{value}'", nameof(value)),
            };
        }
    }
}
