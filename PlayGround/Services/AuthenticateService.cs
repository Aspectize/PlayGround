using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;

namespace PlayGround
{

    [Service(Name = "AuthenticateService")]
    public class AuthenticateService : IAuthentication, IPersistentAuthentication, IUserProfile //, IInitializable, ISingleton
    {

        AspectizeUser IAuthentication.Authenticate(string userName, string secret, AuthenticationProtocol protocol, HashHelper.Algorithm algorithm, string challenge)
        {
            return AspectizeUser.GetUnAuthenticatedUser();
        }


        bool IPersistentAuthentication.ValidateUser(AspectizeUser user)
        {
            return false;
        }

        DataSet IUserProfile.GetUserProfile()
        {
            return null;
        }

    }

}
