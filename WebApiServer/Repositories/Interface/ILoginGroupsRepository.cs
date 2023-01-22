using System.Collections.Generic;

namespace Deadlindar.Repositories
{
    public interface ILoginGroupsRepository
    {
        public IEnumerable<string> GetByLogin(string login);
        public void AddGroup(string login, string groupName);
        public bool RemoveGroup(string login, string groupName);
    }
}