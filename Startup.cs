using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Locar.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Locar.Models;
using Locar.AcessoDados.Repositorio;
using Locar.AcessoDados.Interface;

namespace Locar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //configurar os cookies da aplicação
            services.ConfigureApplicationCookie(options => 
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50); //Para definir o tempo até o cookie expirar
                options.LoginPath = "/Usuarios/Login";
                options.SlidingExpiration = true; //Depois que o tempo esgota, os cookies são renovados
            });

            services.AddDbContext<LocarDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("myConn")));  
            
            services.AddIdentity<Usuario, NivelAcesso>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<LocarDbContext>();

            services.Configure<IdentityOptions>(options => 
            {
                //Nesta etapa configuro preferências de como a senha precisa ser, do identity.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddScoped<INivelAcesso, NivelAcessoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddScoped<IContaRepositorio, ContaRepositorio>();
            services.AddScoped<ICarroRepositorio, CarroRepositorio>();


            //Por causa das DI que foram feitas no controller Home
            //services.AddScoped<SignInManager<Usuario>,SignInManager<Usuario>>();
            //services.AddScoped<UserManager<Usuario>,UserManager<Usuario>>();
            //services.AddScoped<RoleManager<NivelAcesso>, RoleManager<NivelAcesso>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
