using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Winc.Api.Helpers
{
    public class DiHelper
    {

        //WCF registration
        //https://stackoverflow.com/questions/29460080/autofac-error-no-scope-with-a-tag-matching-autofacwebrequest-is-visible-from


        public static IContainer Configure(IServiceCollection services,
                                        string dllPrefix = "Winc",
                                        bool isWeb = true,
                                        List<Assembly> assemblies = null)
        {

            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();

            var builder = new ContainerBuilder();
            var wincDlls = new List<Assembly>();

            var assem = AppDomain.CurrentDomain.GetAssemblies();
            if (assemblies == null)
            {
                wincDlls = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(t => t.FullName.ToLower().Contains(dllPrefix.Trim().ToLower()))
                    .ToList();
            }
            else
            {
                wincDlls = assemblies;
            }


            //load all our Level 2/3 dependent assemblies having naming in the dllPrefix filter 
            wincDlls
                    .SelectMany(x => x.GetReferencedAssemblies())
                    .Distinct()
                    .Where(y => wincDlls.Any((a) => a.FullName == y.FullName) == false &&
                                y.FullName.ToLower().Contains(dllPrefix.Trim().ToLower())).ToList()
                    .ForEach(x => wincDlls.Add(AppDomain.CurrentDomain.Load(x)));

            var components = builder.RegisterAssemblyTypes(wincDlls.ToArray())
                                            .Where(t =>
                                                t.IsAbstract == false &&
                                                t.IsPublic == true &&
                                                (t.Name.ToLower().Contains("repository") == true ||
                                                t.Name.ToLower().Contains("service") == true)
                                            //t.Name.ContainsAny(types)
                                            ).AsImplementedInterfaces();

            if (isWeb == true)
            {
                //one instance per request/each instance
                components.InstancePerDependency();
            }
            else
            {
                //single instance for the app
                components.InstancePerLifetimeScope();
            }

            //https://stackoverflow.com/questions/50125132/autofac-net-core-register-dbcontext
            var dbContexts = builder.RegisterAssemblyTypes(wincDlls.ToArray())
                            .Where(t =>
                            t.IsAbstract == false &&
                            t.IsPublic == true &&
                            t.Name.ToLower().Contains("dbcontext") == true)
                            .AsSelf();

            if (isWeb == true)
            {
                dbContexts.InstancePerDependency();
            }
            else
            {
                dbContexts.InstancePerLifetimeScope();
            }

            //Loading the default microsoft DI services into Autofac container and handing over to Autofac
            builder.Populate(services);
            var container = builder.Build();

            return container;
        }
    }
}
