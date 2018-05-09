namespace Action.Common.Events {
    public class UserCreated : IEvent {
        public string Email { get; }
        public string Username { get; }
        public UserCreated () {

        }

        public UserCreated (string email, string username) {
            Username = username;
            Email = email;
        }
    }

}