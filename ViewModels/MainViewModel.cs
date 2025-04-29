using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MoshkovskyiLab2.Models;
using System.Windows;

namespace MoshkovskyiLab2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthDay; 
        private bool _isProcessing;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public DateTime? BirthDay
        {
            get => _birthDay ?? DateTime.Now; 
            set { _birthDay = value; OnPropertyChanged(); }
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set { _isProcessing = value; OnPropertyChanged(); }
        }

        public ICommand ProceedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            ProceedCommand = new RelayCommand(async () => await ProceedAsync(), CanProceed);
        }

        private bool CanProceed() =>
            !string.IsNullOrWhiteSpace(FirstName) &&
            !string.IsNullOrWhiteSpace(LastName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            BirthDay.HasValue && BirthDay.Value != default(DateTime);

        private async Task ProceedAsync()
        {
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsProcessing = true;

            await Task.Delay(1000); 

            if (BirthDay > DateTime.Today || DateTime.Today.Year - BirthDay.Value.Year > 135)
            {
                MessageBox.Show("Invalid birth date.");
                IsProcessing = false;
                return;
            }

            var person = new Person(FirstName, LastName, Email, BirthDay.Value);

            string result = $"Name: {person.FirstName}\n" +
                            $"Surname: {person.LastName}\n" +
                            $"Email: {person.Email}\n" +
                            $"Birth date: {person.BirthDay:d}\n" +
                            $"Adult: {person.IsAdult}\n" +
                            $"Sun sign: {person.SunSign}\n" +
                            $"Chinese sign: {person.ChineseSign}\n" +
                            $"Is birthday today: {person.IsBirthday}";

            MessageBox.Show(result, "Result");
            IsProcessing = false;
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}