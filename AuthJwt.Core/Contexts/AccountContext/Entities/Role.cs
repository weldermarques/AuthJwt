using AuthJwt.Core.Contexts.SharedContext.Entities;

namespace AuthJwt.Core.Contexts.AccountContext.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;
    
    public IEnumerable<User> Users { get; set; } = [];
}
