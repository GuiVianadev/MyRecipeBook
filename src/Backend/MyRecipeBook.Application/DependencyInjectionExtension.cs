using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration )
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEncrypter(services, configuration);
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
           
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        } 
        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            var keyAdditional = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped(option => new PasswordEncripter(keyAdditional));
        }
    }
}
