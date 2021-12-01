using Prism.Navigation;
using System;
using Shiny;

namespace reb.ViewModels
{
    public class MotorListPageViewModel : ViewModel
    {
        private INavigationService _navigationService;
        
        public MotorListPageViewModel(
            INavigationService navigationService
            )
        {
            _navigationService = navigationService;
        }
    }
}
