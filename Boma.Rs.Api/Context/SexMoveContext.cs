using Boma.RedeSocial.Domain.Context.Interfaces;
using System;
using System.Web;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Crosscut.Exceptions;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace Boma.Rs.Api.Context
{
    public class SexMoveContext: ISexMoveContext
    {
        public SexMoveContext()
        {
            Id = Guid.NewGuid();
            Connection = ResolveConnection();
        }
        
        public Guid Id { get; set; }
        public IUser User { get; set; }
        public DbConnection Connection { get; set; }

        public static IUser ResolveUser()
        {
            var auth = HttpContext.Current.GetOwinContext().Authentication;
            var user = auth.User;

            if (user != null && auth.User.Identity.IsAuthenticated)
            {
                
                DateTime createDate;
                DateTime.TryParse(user.FindFirst("nibouser:createDate").Value, out createDate);

                return new User(Guid.Parse(user.FindFirst("Id").Value),  user.FindFirst("Email").Value, user.FindFirst("UserName").Value);
            }

            throw new DomainException("Usuário deve ser do contexto da aplicação");


            // Se não resolver usando esse context do owin, usar esse igual do provider do oauth
            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);
            //if (user == null)

        }

        public static DbConnection ResolveConnection() 
            => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        
    }
}