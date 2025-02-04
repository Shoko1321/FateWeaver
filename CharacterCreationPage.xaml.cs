using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace FateWeaver;

public partial class CharacterCreationPage : ContentPage
{
    private List<int> rolls = new List<int>();
    private List<int> availableRolls = new List<int>(); // Track available rolls for selection

    public CharacterCreationPage()
    {
        InitializeComponent();
    }

    private void OnRoll4d6Clicked(object sender, EventArgs e)
    {
        rolls.Clear();
        availableRolls.Clear();
        Random random = new();

        // Roll stats 6 times
        for (int i = 0; i < 6; i++)
        {
            List<int> roll = new List<int>();

            // Roll 4d6
            for (int j = 0; j < 4; j++)
            {
                roll.Add(random.Next(1, 7)); // Simulate a 6-sided die
            }

            roll.Sort(); // Sort to find the lowest
            roll.RemoveAt(0); // Remove the lowest roll
            rolls.Add(roll.Sum()); // Add the sum of the remaining three rolls

            availableRolls.Add(rolls[i]); // Store the available roll to be assigned later
        }

        DisplayRolledStats();
    }

    private void DisplayRolledStats()
    {
        RollsContainer.Children.Clear(); // Clear previous roll display
        int columnIndex = 0;

        foreach (var roll in rolls)
        {
            var rollLabel = new Label
            {
                Text = roll.ToString(),
                FontFamily = "DragonSlayer",
                FontSize = 45,
                TextColor = Color.FromArgb("#800080"), // Magic purple
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            // Add the roll to the container
            RollsContainer.Children.Add(rollLabel);
            columnIndex++; // Move to the next column
        }

        // Update pickers with available rolls
        UpdatePickers();
    }

    private void UpdatePickers()
    {
        // Populate pickers with available rolls
        StrengthPicker.ItemsSource = new List<int>(availableRolls);
        DexterityPicker.ItemsSource = new List<int>(availableRolls);
        ConstitutionPicker.ItemsSource = new List<int>(availableRolls);
        IntelligencePicker.ItemsSource = new List<int>(availableRolls);
        WisdomPicker.ItemsSource = new List<int>(availableRolls);
        CharismaPicker.ItemsSource = new List<int>(availableRolls);
    }

    private void OnStrengthPickerChanged(object sender, EventArgs e)
    {
        AssignRollToStat(StrengthPicker, "Strength");
    }

    private void OnDexterityPickerChanged(object sender, EventArgs e)
    {
        AssignRollToStat(DexterityPicker, "Dexterity");
    }

    private void OnConstitutionPickerChanged(object sender, EventArgs e)
    {
        AssignRollToStat(ConstitutionPicker, "Constitution");
    }

    private void OnIntelligencePickerChanged(object sender, EventArgs e)
    {
        AssignRollToStat(IntelligencePicker, "Intelligence");
    }

    private void OnWisdomPickerChanged(object sender, EventArgs e)
    {
        AssignRollToStat(WisdomPicker, "Wisdom");
    }

    private void OnCharismaPickerChanged(object sender, EventArgs e)
    {
        AssignRollToStat(CharismaPicker, "Charisma");
    }

    private void AssignRollToStat(Picker picker, string stat)
    {
        if (picker.SelectedIndex != -1)
        {
            int selectedRoll = (int)picker.SelectedItem;

            // Remove the selected roll from available rolls
            availableRolls.Remove(selectedRoll);

            // Update the picker to remove the selected roll
            UpdatePickers();

            // Optionally, you could display a message or update a label for the stat
            DisplayAlert($"{stat} Assigned", $"You have assigned {selectedRoll} to {stat}.", "OK");
        }
    }
}
