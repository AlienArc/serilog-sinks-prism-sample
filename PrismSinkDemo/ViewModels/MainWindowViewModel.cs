using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Serilog;
using Serilog.Sinks.Prism;
using System.Windows.Input;

namespace PrismSinkDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message = string.Empty;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public ICommand DoItCommand { get; }
        public ILogger Logger { get; }
        public IEventAggregator EventAggregator { get; }

        private int number = 0;
        public MainWindowViewModel(ILogger logger, IEventAggregator eventAggregator)
        {
            Logger = logger;
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<EventAggregatorSinkEvent>().Subscribe(OnLogEvent);
            DoItCommand = new DelegateCommand(() => { Title = $"The Number is now {++number}"; Logger.Fatal(Title); });
        }

        private void OnLogEvent(string message)
        {
            Message = message;
        }
    }
}
