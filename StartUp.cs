using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PikAroomFB.App_Start;
using Owin;

[assembly : OwinStartupAttribute(typeof(PikAroomFB.App_Start.StartUp))]

namespace PikAroomFB.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}