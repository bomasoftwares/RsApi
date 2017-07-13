using Boma.RedeSocial.Domain.Context.Interfaces;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using Boma.RedeSocial.AppService.Users.Interfaces;
using System.Web;

namespace Boma.Rs.Api.Context
{
    public class SexMoveContext : ISexMoveContext
    {
        private IUserAppService UserAppService { get; set; }
        public SexMoveContext()
        {
            Id = Guid.NewGuid();
            Connection = ResolveConnection();
            UserContext = HttpContext.Current.Request.GetOwinContext().Authentication.User.Identity.Name;
        }

        public Guid Id { get; set; }

        public string UserContext { get; set; }
        public DbConnection Connection { get; set; }

        public static DbConnection ResolveConnection() 
            => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public void ResolveUser(string userName) => UserContext = userName;

        
    }
}