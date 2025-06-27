using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace _02_Application.Layer.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
    {
        //public string id { get; set; }
        //public string email { get; set; }
        //public string firstName { get; set; }
        public string IdToken { get; set; }
        //public string name { get; set; }
        //public string lastName { get; set; }
        //public string provider { get; set; }
        //public string photoUrl { get; set; }
    }
}