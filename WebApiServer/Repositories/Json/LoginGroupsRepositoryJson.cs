using System.Collections.Generic;
using ValueObjects;

namespace Deadlindar.Repositories.Json
{
    public class LoginGroupsRepositoryJson : ILoginGroupsRepository
    {
        private readonly IJsonRepository repository;
        
        public LoginGroupsRepositoryJson(IJsonRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<string> GetByLogin(string login)
        {
            return repository.OpenFile<List<string>>(login);
        }

        public void AddGroup(string login, string groupName)
        {
            var groups = repository.OpenFile<List<string>>(login);
            groups.Add(groupName);
            repository.SaveFile(login, groups);
        }

        public bool RemoveGroup(string login, string groupName)
        {
            var groups = repository.OpenFile<List<string>>(login);
            var answer = false;
            if (groups.Contains(groupName))
            {
                groups.Remove(groupName);
                answer = true;
            }

            repository.SaveFile(login, groups);
            return answer;
        }
    }
}