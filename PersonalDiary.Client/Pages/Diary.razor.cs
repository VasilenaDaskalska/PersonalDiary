using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.Client.Shared.Dialogs;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.Client.Pages;

public partial class Diary
{
    [Inject]
    private IDiaryHttpRepository DiaryRepository { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<DiaryBiz> diaryEntries;
    private List<DiaryBiz> filteredEntries = new();
    private DiaryBiz editingEntry;
    private DiaryBiz currentEntry = new();
    private bool dialogVisible;
    private string searchString;
    private DateRange dateRange;
    private string sortBy = "date";

    [Inject]
    private IDialogService DialogService { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    [Inject]
    private UserState UserState { get; set; }

    private DialogOptions dialogOptions = new()
    {
        MaxWidth = MaxWidth.Small,
        FullWidth = true
    };

    protected override async Task OnInitializedAsync()
    {
        await this.LoadEntries();
        this.FilterAndSortEntries();
    }

    private async Task LoadEntries()
    {
        this.diaryEntries = (await this.DiaryRepository.GetAllAsync()).Where(x => x.UserCreatorID == this.UserState.CurrentUser?.ID).ToList();
        this.FilterAndSortEntries();
    }

    private async Task OpenAddDialog()
    {
        this.editingEntry = null;
        this.currentEntry = new DiaryBiz();
        var parameters = new DialogParameters { ["Entry"] = this.currentEntry };
        var dialog = this.DialogService.Show<AddOrEditEntry>("Create your new entry", parameters, new DialogOptions { MaxWidth = MaxWidth.Large, BackdropClick = false });
        this.dialogVisible = true;
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await this.LoadEntries();
        }

        this.dialogVisible = false;
    }

    private async Task OpenEditDialog(DiaryBiz entry)
    {
        this.editingEntry = entry;
        this.currentEntry = new DiaryBiz
        {
            ID = entry.ID,
            Title = entry.Title,
            Description = entry.Description
        };

        var parameters = new DialogParameters { ["Entry"] = this.currentEntry };
        var dialog = this.DialogService.Show<AddOrEditEntry>($"Edit entry {this.currentEntry.Title}", parameters, new DialogOptions { MaxWidth = MaxWidth.Large, BackdropClick = false });

        this.dialogVisible = true;

        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await this.LoadEntries();
        }

        this.dialogVisible = false;
    }

    private async Task OpenViewDialog(DiaryBiz entry)
    {
        try
        {
            var parameters = new DialogParameters { ["Entry"] = entry };
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Large,
                FullWidth = true,
                CloseButton = true,
                BackdropClick = false
            };
            var dialog = this.DialogService.Show<ViewEntry>(entry.Title, parameters, options);
            await dialog.Result;
        }
        catch (Exception ex)
        {
            this.Snackbar.Add("Error opening entry view", Severity.Error);
        }
    }

    private async Task SaveEntry()
    {
        if (this.editingEntry == null)
        {
            await this.DiaryRepository.CreateAsync(this.currentEntry);
        }
        else
        {
            await this.DiaryRepository.UpdateAsync(this.currentEntry);
        }

        await this.LoadEntries();
        this.dialogVisible = false;
    }

    private async Task DeleteEntry(long id)
    {
        await this.DiaryRepository.DeleteAsync(id);
        await this.LoadEntries();
    }

    private void CloseDialog()
    {
        this.dialogVisible = false;
    }

    private void FilterAndSortEntries()
    {
        if (this.diaryEntries == null)
        {
            this.filteredEntries = new List<DiaryBiz>();
            return;
        }

        IEnumerable<DiaryBiz> query = this.diaryEntries;

        // Filter by search
        if (!string.IsNullOrWhiteSpace(this.searchString))
        {
            query = query.Where(e =>
                (e.Title?.Contains(this.searchString, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (e.Description?.Contains(this.searchString, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        // Filter by date range
        if (this.dateRange?.Start != null && this.dateRange?.End != null)
        {
            query = query.Where(e =>
                e.CreationDate >= this.dateRange.Start.Value.Date && e.CreationDate <= this.dateRange.End.Value.Date);
        }

        // Sort
        query = this.sortBy switch
        {
            "title" => query.OrderBy(e => e.Title),
            _ => query.OrderByDescending(e => e.CreationDate), // Default: sort by date descending
        };

        this.filteredEntries = query.ToList();
        this.StateHasChanged();
    }
}