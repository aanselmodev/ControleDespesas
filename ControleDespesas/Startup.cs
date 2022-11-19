using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ControleDespesas.Database;
using ControleDespesas.Repositories;
using ControleDespesas.Libraries.Sessoes;
using ControleDespesas.Libraries.Login;
using System.Net.Mail;
using System.Net;
using ControleDespesas.Libraries.Email;
using Microsoft.AspNetCore.Http;
using ControleDespesas.Repositories.Contracts;

namespace ControleDespesas
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
            services.AddControllersWithViews();

            #region Sessão

            services.AddMemoryCache();
            services.AddSession(options => {
                options.Cookie.IsEssential = true;   
            });

            #endregion

            #region Repositórios

            services.AddHttpContextAccessor();

            services.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:SMTPServer"),
                    Port = Configuration.GetValue<int>("Email:SMTPPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };

                return smtp;
            });

            services.AddScoped<Email>();
            services.AddScoped<Sessao>();
            services.AddScoped<LoginUsuario>();
            services.AddScoped<Libraries.Cookies.Cookie>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<ITipoDespesaRepository, TipoDespesaRepository>();
            services.AddScoped<IRedefinicaoSenhaRepository, RedefinicaoSenhaRepository>();

            #endregion

            #region Banco de dados

            services.AddDbContext<ControleDespesasContext>(options => options.UseSqlServer(Configuration.GetValue<string>("DatabaseConnectionString")));

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
