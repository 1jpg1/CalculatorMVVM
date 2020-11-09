using Calculator.Commands;
using Calculator.Model;
using Calculator.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        public ObservableCollection<MathExpression> ExpressionHistory { get; set; }

        public MathExpression CurrentExpression { get; set; }
        
        private MathExpression _SelectedExpression;
        public MathExpression SelectedExpression
        {
            get { return _SelectedExpression; }
            set 
            {
                Set(ref _SelectedExpression, value);

                CurrentExpression = new MathExpression(_SelectedExpression.Content);
                OnPropertyChanged("CurrentExpression");

            }
        }

        #endregion

        #region Commands

        public ICommand ButtonPressCommand { get; }
        private bool CanButtonPressCommandExecute(object p) => true;
        private void OnButtonPressCommandExecute(object p)
        {
            CurrentExpression.AddChar(p);
            OnPropertyChanged("CurrentExpression");
        }
        
        public ICommand EqualPressCommand { get; }
        private bool CanEqualPressCommandExecute(object p) => true;
        private void OnEqualPressCommandExecute(object p)
        {
            if (CurrentExpression.Content != "")
            {
                CurrentExpression.CalculateResult();
                ExpressionHistory.Add(CurrentExpression);
                OnPropertyChanged("CurrentExpression");

                CurrentExpression = new MathExpression();
            }
        }        

        public ICommand ClearPressCommand { get; }
        private bool CanClearPressCommandExecute(object p) => true;
        private void OnClearPressCommandExecute(object p)
        {
            CurrentExpression.Clear();
            OnPropertyChanged("CurrentExpression");
        }

        public ICommand DeletePressCommand { get; }
        private bool CanDeletePressCommandExecute(object p) => true;
        private void OnDeletePressCommandExecute(object p)
        {
            CurrentExpression.Delete();
            OnPropertyChanged("CurrentExpression");
        }

        public ICommand DotPressCommand { get; }
        private bool CanDotPressCommandExecute(object p) => false;
        private void OnDotPressCommandExecute(object p)
        {
            
        }

        #endregion

        public MainWindowViewModel()
        {
            
            ButtonPressCommand = new RelayCommand(OnButtonPressCommandExecute, CanButtonPressCommandExecute);
            EqualPressCommand = new RelayCommand(OnEqualPressCommandExecute, CanEqualPressCommandExecute);
            ClearPressCommand = new RelayCommand(OnClearPressCommandExecute, CanClearPressCommandExecute);
            DeletePressCommand = new RelayCommand(OnDeletePressCommandExecute, CanDeletePressCommandExecute);
            DotPressCommand = new RelayCommand(OnDotPressCommandExecute, CanDotPressCommandExecute);

            ExpressionHistory = new ObservableCollection<MathExpression>();
            CurrentExpression = new MathExpression();
        }
    }
}
