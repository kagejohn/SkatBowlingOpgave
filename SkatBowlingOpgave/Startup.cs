﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkatBowlingOpgave.Startup))]
namespace SkatBowlingOpgave
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
