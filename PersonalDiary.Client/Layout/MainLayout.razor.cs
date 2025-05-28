using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Client.Shared.State;

namespace PersonalDiary.Client.Layout
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;
        private bool _isDarkMode = true;
        private MudTheme? _theme = null;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private UserState UserState { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this._theme = new()
            {
                PaletteLight = this._lightPalette,
                PaletteDark = this._darkPalette,
                LayoutProperties = new LayoutProperties()
            };

            if (this.UserState.IsAuthenticated)
            {
                this.NavigationManager.NavigateTo("diary");
            }
        }

        private void DrawerToggle()
        {
            this._drawerOpen = !this._drawerOpen;
        }

        private void DarkModeToggle()
        {
            this._isDarkMode = !this._isDarkMode;
        }

        private void GoToUsersPage()
        {
            if (this.UserState.CurrentUser.UserPermissions == Entities.ENUMS.PERMISSIONS.Admin)
            {
                this.NavigationManager.NavigateTo("Users");
            }
        }

        private readonly PaletteLight _lightPalette = new()
        {
            Black = "#1E1E1E", // Base dark for text/contrast
            AppbarText = "#4A4A4A", // Deep neutral gray
            AppbarBackground = "rgba(255, 255, 255, 0.85)", // Soft transparent white
            DrawerBackground = "#F9FAFB", // Ultra-light neutral
            GrayLight = "#DADDE1", // Modern neutral gray
            GrayLighter = "#EFF1F3", // Slightly cool off-white

            Primary = "#A78BFA", // Soft lavender – peaceful and unique
            Surface = "#FFFFFF", // True white for content background
            Background = "#FAFAFA", // Slightly off-white page background
            BackgroundGray = "#F0F0F0", // Very light neutral gray

            ActionDefault = "#6B7280", // Muted slate gray
            ActionDisabled = "#A0A0B033", // Soft light gray with transparency
            ActionDisabledBackground = "#D1D5DB33", // Very light transparent background

            TextPrimary = "#2C2C2C", // Almost-black for high contrast
            TextSecondary = "#6B6B6B", // Subtle, soft gray for secondary
            TextDisabled = "#A0A0A066", // Soft transparent gray
            DrawerIcon = "#6B7280", // Slate tone for icons
            DrawerText = "#4B5563", // Muted but strong drawer text

            Info = "#60A5FA", // Calm blue
            Success = "#34D399", // Minty green
            Warning = "#FBBF24", // Warm amber
            Error = "#F87171", // Soft coral red

            LinesDefault = "#E5E7EB", // Light neutral for dividers/lines
            TableLines = "#E5E7EB",
            Divider = "#E0E0E0",
            OverlayLight = "#FFFFFFAA", // Light semi-transparent overlay
        };

        private readonly PaletteDark _darkPalette = new()
        {
            Primary = "#A78BFA", // Soft lavender for highlights
            Surface = "#2E2A36", // Deep muted purple
            Background = "#1F1B24", // Very dark plum
            BackgroundGray = "#26222C", // Slightly lighter for contrast
            AppbarText = "#BFAEDC", // Muted light lavender
            AppbarBackground = "rgba(31,27,36,0.8)",
            DrawerBackground = "#1F1B24",
            ActionDefault = "#BFAEDC",
            ActionDisabled = "#66667A4D",
            ActionDisabledBackground = "#55556A33",
            TextPrimary = "#EDE6F3", // Soft white-purple
            TextSecondary = "#C7BCD8", // Muted text
            TextDisabled = "#FFFFFF33",
            DrawerIcon = "#C7BCD8",
            DrawerText = "#C7BCD8",
            GrayLight = "#3B3745",
            GrayLighter = "#2E2A36",
            Info = "#81A9F4", // Calm sky blue
            Success = "#7CD3A4", // Gentle green
            Warning = "#FFCC70", // Warm amber
            Error = "#FF6B81", // Soft rose red
            LinesDefault = "#3A3644",
            TableLines = "#3A3644",
            Divider = "#2A2633",
            OverlayLight = "#2E2A3680",
        };

        public string DarkLightModeButtonIcon => this._isDarkMode switch
        {
            true => Icons.Material.Rounded.AutoMode,
            false => Icons.Material.Outlined.DarkMode,
        };
    }
}
