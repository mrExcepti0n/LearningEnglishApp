using LearningEnglishMobile.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.Navigation
{
    public interface INavigationService
    {

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}