using FindMe.Application.Features.Authentication.Commands.ChangePasswrod;
using FindMe.Application.Features.Comments.Commands.Add;
using FindMe.Application.Features.Comments.Commands.Update;
using FindMe.Application.Features.PinPost.Commands.Pin;
using FindMe.Application.Features.PinPost.Commands.UnPinPost;
using FindMe.Application.Features.PinPost.Queries.GetUserPinnedPosts;
using FindMe.Application.Features.Posts.Commands.Create;
using FindMe.Application.Features.Posts.Commands.Update;
using FindMe.Application.Features.Posts.Queries.GetAll;
using FindMe.Application.Features.Profile.Commands;
using FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest;
using FindMe.Application.Features.UserDetail.Commands.AddDetails;
using FindMe.Application.Features.UserDetail.Commands.UpdateDetails;
using FindMe.Application.Features.UserRelative.Commands.UpdateRelatives;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace FindMe.Application.Extention
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMapping()
                    .AddMediator()
                    .AddValidators();

            return services;
        }

        private static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AddDetailsCommand>, AddDetailsCommandValidator>();
            services.AddTransient<IValidator<UpdateDetailsCommand>, UpdateDetailsCommandValidator>();
            services.AddTransient<IValidator<UpdateRelativesCommand>, UpdateRelativesCommandValidator>();
            services.AddTransient<IValidator<EditProfileCommand>, EditProfileCommandValidator>();
            services.AddTransient<IValidator<ChangePasswordCommand>, ChangePasswordCommandValidator>();
            services.AddTransient<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
            services.AddTransient<IValidator<SendRecognitionRequestCommand>, SendRecognitionRequestCommandValidator>();
            services.AddTransient<IValidator<UpdatePostCommand>, UpdatePostCommandValidator>();
            services.AddTransient<IValidator<GetAllPostsQuery>, GetAllPostsQueryValidator>();
            services.AddTransient<IValidator<AddCommentCommand>, AddCommentCommandValidator>();
            services.AddTransient<IValidator<UpdateCommentCommand>, UpdateCommentCommandValidator>();
            services.AddTransient<IValidator<PinPostCommand>, PinPostCommandValidator>();
            services.AddTransient<IValidator<GetUserPinnedPostsCommand>, GetUserPinnedPostsCommandValidator>();
            
            
            
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
