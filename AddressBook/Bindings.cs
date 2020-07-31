using AddressBook.Interfaces;
using AddressBook.Repositories;
using Ninject.Modules;

namespace AddressBook
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IPhoneNumberRepository>().To<PhoneNumberRepository>();
        }
    }
}