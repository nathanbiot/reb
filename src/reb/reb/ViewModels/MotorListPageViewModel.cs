using System.Reactive;
using System.Reactive.Linq;

namespace reb.ViewModels;

public class MotorListPageViewModel : ViewModel
{
    #region {Private fields

    private INavigationService _navigationService;

    #endregion

    #region {CTOR}

    public MotorListPageViewModel(
        INavigationService navigationService
        )
    {
        _navigationService = navigationService;

        SelectedMotorChanged = ReactiveCommand.CreateFromTask<Unit, Unit>(ExecuteSelectedMotorChanged);
    }

    #endregion

    #region {LifeCycle}

    public override Task InitializeAsync(INavigationParameters parameters)
    {
        var motors = new List<Motor>();
        for (int i = 0; i < 20; i++)
        {
            motors.Add(GetFakeMotor());
        }
        Motors = motors;

        return base.InitializeAsync(parameters);
    }

    private Motor GetFakeMotor()
    {
        return new Motor
        {
            Brand = "Test",
            NotchesNumber = 10,
            AddedOn = DateTime.Now,
            Nameplate = new MotorNameplate
            {
                PhasesNumber = 3,
                SerialNumber = "0123456789",
                Weight = 16,
                MotorSetups = new List<MotorSetup>
                    {
                        new MotorSetup
                        {
                            ConnectionType = ConnectionType.Star,
                            Current = 4.77f,
                            Frequency = 50,
                            Power = 2.2f,
                            Voltage = 400,
                            PowerFactor = 0.8f,
                            Speed = 2885
                        }
                    }
            }
        };
    }

    #endregion

    #region {Properties}

    //Doesn't work, TODO, moving on with RaiseAndSetIfChanged for now
    //[Reactive]
    //public List<Motor> Motors { get; set; }

    private List<Motor> _motors;
    public List<Motor> Motors
    {
        get => _motors;
        set => this.RaiseAndSetIfChanged(ref _motors, value);
    }


    private Motor _selectedMotor;
    public Motor SelectedMotor
    {
        get => _selectedMotor;
        set => this.RaiseAndSetIfChanged(ref _selectedMotor, value);
    }


    #endregion

    #region {Commands}

    public ReactiveCommand<Unit, Unit> SelectedMotorChanged { get; }

    #endregion

    #region {Methods}

    private async Task<Unit> ExecuteSelectedMotorChanged(Unit args)
    {
        if (_selectedMotor == null)
            return default;

        var navParams = new NavigationParameters
        {
            { NavigationKeys.MotorDetailsNavParamMotor, _selectedMotor }
        };
        await _navigationService.NavigateAsync(NavigationKeys.MotorDetails, navParams);

        return default;
    }

    #endregion

}
