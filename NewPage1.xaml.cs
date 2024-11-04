using Microsoft.Maui;
using System.Reflection;

namespace rmp8l;

public partial class NewPage1 : ContentPage
{
    public NewPage1(string fullName, string gender, string age, bool needsHostel, bool isLeader, ImageSource userPhoto)
    {
        InitializeComponent();

        UserPhoto.Source = userPhoto; // ���������� �����������
        FullNameLabel.Text = fullName;
        GenderLabel.Text = $"���: {gender}";
        AgeLabel.Text = $"�������: {age} ���";
        HostelLabel.Text = $"���������: {(needsHostel ? "��" : "���")}";
        LeaderLabel.Text = $"��������: {(isLeader ? "��" : "���")}";
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}