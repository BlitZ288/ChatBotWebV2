using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatWithBotWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppIdentityDbContex>(option => option.UseNpgsql(
                   Configuration["Data:ChatBotWebIdentity:ConnectionString"]
              ));
            services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(
                     Configuration["Data:ChatBotWeb:ConnectionString"]

                ));

            services.AddIdentity<User, ApplicationRole>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppIdentityDbContex>()
                .AddDefaultTokenProviders();


            services.AddScoped<IChatService, ChatServise>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserSercvice>();
            services.AddScoped<IBotService, BotServices>();

            services.AddScoped<IUnitOfWorck, EFUnitOfWork>();


            services.AddMemoryCache();
            services.AddSession();


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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // ��������������
            app.UseAuthorization();     // �����������
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
