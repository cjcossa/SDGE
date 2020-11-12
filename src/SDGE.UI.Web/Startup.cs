using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using SDGE.UI.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SDGE.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using SDGE.Infrastructure.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using SDGE.ApplicationCore.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using EmailService;

namespace SDGE.UI.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<ParticipanteContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // services.AddDbContext<ParticipanteContext>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SDGE.UI.Web")));

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);// 2h timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            services.AddControllersWithViews();
            services.AddRazorPages();
            // services.AddMvc(option => option.EnableEndpointRouting = false);
           

            // services.AddSingleton(typeof(IRepository<>), typeof(EFRepository<>));
            // services.AddSingleton<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IEventoParticipanteRepository, EventoParticipanteRepository>();
            services.AddScoped<IMembroRepository, MembroRepository>();
            services.AddScoped<ISubmissaoRepository, SubmissaoRepository>();
            services.AddScoped<ITipoRepository, TipoRepository>();
            services.AddScoped<ICorrecaoRepository, CorrecaoRepository>();
            services.AddScoped<IMembroEventoRepository, MembroEventoRepository>();
            services.AddScoped<IDataImportanteRepository, DataImportanteRepository>();
            services.AddScoped<IComissaoCientificaRepository, ComissaoCientificaRepository>();
            services.AddScoped<IComissaoOrganizadoraRepository, ComissaoOrganizadoraRepository>();
            services.AddScoped<IMembroCientificoRepository, MembroCientificoRepository>();
            services.AddScoped<IMembroOrganizadorRepository, MembroOrganizadorRepository>();
            services.AddScoped<IAlertaRepository, AlertaRepository>();
            services.AddScoped<IFaculdadeRepository, FaculdadeRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();



            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;


                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                //options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            });

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
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

            app.UseRouting();
           
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            //seed
            Seed.SeedData(userManager, roleManager);
            //signInManager.SignOutAsync();

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
                 endpoints.MapRazorPages();
             });
            /*app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Submissao}/{action=Index}/{id?}");
            });*/
            
        }
    }
}
