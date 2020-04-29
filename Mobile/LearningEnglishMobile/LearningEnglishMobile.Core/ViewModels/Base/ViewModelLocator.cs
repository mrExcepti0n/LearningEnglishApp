using LearningEnglishMobile.Core.Services.Identity;
using LearningEnglishMobile.Core.Services.Navigation;
using LearningEnglishMobile.Core.Services.OpenUrl;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.Services.Settings;
using LearningEnglishMobile.Core.Services.User;
using LearningEnglishMobile.Core.Services.Vocabulary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using TinyIoC;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;
        public static readonly BindableProperty AutoWireViewModelProperty =  BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            _container.Register<LoginViewModel>();
            _container.Register<SettingsViewModel>();
            _container.Register<MainViewModel>();
            _container.Register<MasterMainViewModel>();

            // Services - by default, TinyIoC will register interface registrations as singletons.
            _container.Register<IOpenUrlService, OpenUrlService>();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<IIdentityService, IdentityService>();
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IUserService, UserService>();
            _container.Register<IVocabularyService, MockedVocabularyService>();

            _container.Register<IRequestProvider, RequestProvider>();
        }


        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }


        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
