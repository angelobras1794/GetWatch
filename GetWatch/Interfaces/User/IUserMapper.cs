using System;

namespace GetWatch.Interfaces.User;

public interface IUserMapper
{
     List<IUser> GetAll();
    IUser? Get(Guid id);
    void Insert(IUser user);

    void Update(IUser user);
    void Remove(Guid id);

}
