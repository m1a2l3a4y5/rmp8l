using System.Text.RegularExpressions;// Пространство имен для регулярных выражений
namespace rmp8l
{
    public partial class MainPage : ContentPage
    {
        private List<Student> students = new List<Student>(); //список, который будет хранить объекты типа Student
        private StreamImageSource userPhotoSource; // Для хранения изображения

        public MainPage()
        {
            InitializeComponent();
        }


        private async void ph_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();// Запрос на выбор фото
                if (result != null)
                {
                    // Открываем поток изображения и создаем его источник
                    var stream = await result.OpenReadAsync();
                    userPhotoSource = (StreamImageSource)ImageSource.FromStream(() =>
                    {
                        // Возвращаем новый поток каждый раз
                        return result.OpenReadAsync().Result;
                    });
                    UserPhoto.Source = userPhotoSource; // Отображаем фото на главной странице
                }
                else
                {
                    await DisplayAlert("ВНИМАНИЕ!","Фото не выбрано.","ОК");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ВНИМАНИЕ!", $"Ошибка при выборе фото: {ex.Message}", "ОК");
           
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Проверка на наличие фото
            if (userPhotoSource == null)
            {
                await DisplayAlert("ВНИМАНИЕ!", "Пожалуйста, выберите фото.", "ОК");
                return;
            }

            // Проверка на пустое ФИО и наличие цифр
            if (string.IsNullOrWhiteSpace(FullNameEntry.Text) || !IsValidFullName(FullNameEntry.Text))
            {
                await DisplayAlert("ВНИМАНИЕ!", "Пожалуйста, введите корректное ФИО (только буквы).", "ОК");
                return;
            }

            // Проверка на выбранный пол
            if (GenderPicker.SelectedIndex == -1)
            {
                await DisplayAlert("ВНИМАНИЕ!", "Пожалуйста, выберите пол.", "ОК")  ;
                return;
            }

            // Проверка на возраст
            if (string.IsNullOrWhiteSpace(AgeEntry.Text) || !int.TryParse(AgeEntry.Text, out int age) || age < 0)
            {
                await DisplayAlert("ВНИМАНИЕ!", "Пожалуйста, введите корректный возраст.", "ОК");
                return;
            }

            var student = new Student
            {
                FullName = FullNameEntry.Text,
                Gender = GenderPicker.SelectedItem.ToString(),
                Age = age.ToString(),  // Сохраняем возраст как строку
                NeedsHostel = HostelSwitch.IsToggled,
                IsLeader = LeaderSwitch.IsToggled,
                Photo = userPhotoSource // сохраняем фото
            };

            // Добавляем студента в список
            students.Add(student);
            RecordsPicker.ItemsSource = null; // Сбрасываем источник данных
            RecordsPicker.ItemsSource = students.Select(s => s.FullName).ToList(); // Устанавливаем обновленный источник данных

            await DisplayAlert("ВНИМАНИЕ!", "Данные сохранены!", "ОК");

            // Очищаем все поля
            FullNameEntry.Text = string.Empty;
            GenderPicker.SelectedIndex = -1; // Сбрасываем выбор
            AgeEntry.Text = string.Empty;
            HostelSwitch.IsToggled = false; // Сбрасываем переключатель
            LeaderSwitch.IsToggled = false; // Сбрасываем переключатель
            UserPhoto.Source = null; // Убираем фото
        }
        private async void RecordsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = RecordsPicker.SelectedItem?.ToString();
            if (selectedItem != null)
            {
                var student = students.FirstOrDefault(s => s.FullName == selectedItem);
                if (student != null)
                {
                    // Передаем данные на DetailsPage
                    await Navigation.PushAsync(new NewPage1(
                        student.FullName,
                        student.Gender,
                        student.Age,
                        student.NeedsHostel,
                        student.IsLeader,
                        student.Photo
                    ));
                }
            }
        }
        // Метод для проверки корректности введённого ФИО
        private bool IsValidFullName(string fullName)
        {
            // Регулярное выражение для проверки, что строка содержит только буквы и пробелы
            return Regex.IsMatch(fullName, @"^[А-Яа-яЁёA-Za-z\s\-]+$");
        }
    }
}
