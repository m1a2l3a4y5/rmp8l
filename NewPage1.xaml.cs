using Microsoft.Maui;
using System.Reflection;

namespace rmp8l;

public partial class NewPage1 : ContentPage
{
    public NewPage1(string fullName, string gender, string age, bool needsHostel, bool isLeader, ImageSource userPhoto)
    {
        InitializeComponent();

        UserPhoto.Source = userPhoto; // Отображаем изображение
        FullNameLabel.Text = fullName;
        GenderLabel.Text = $"Пол: {gender}";
        AgeLabel.Text = $"Возраст: {age} лет";
        HostelLabel.Text = $"Общежитие: {(needsHostel ? "Да" : "Нет")}";
        LeaderLabel.Text = $"Староста: {(isLeader ? "Да" : "Нет")}";
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}