using tetris_api;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in
        // production applications
        private readonly UserContext context;

        public UserService(UserContext context) { this.context = context; }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(
                () => this.context.Users.SingleOrDefault(
                    x => x.Username == username && x.HashedPassword == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            // return user.WithoutPassword();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => this.context.Users.WithoutPasswords());
        }
    }
}
