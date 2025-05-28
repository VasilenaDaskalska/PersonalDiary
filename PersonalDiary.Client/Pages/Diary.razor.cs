using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.Client.Pages;

public partial class Diary
{
    [Inject]
    private IDiaryHttpRepository DiaryRepository { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<DiaryBiz> diaryEntries;
    private DiaryBiz editingEntry;
    private DiaryBiz currentEntry = new();
    private bool dialogVisible;
    private string searchString;
    private DateRange dateRange;
    private string sortBy = "date";

    private DialogOptions dialogOptions = new()
    {
        MaxWidth = MaxWidth.Small,
        FullWidth = true
    };

    protected override async Task OnInitializedAsync()
    {
        await this.LoadEntries();
    }

    private async Task LoadEntries()
    {
        this.diaryEntries = (await this.DiaryRepository.GetAllAsync()).ToList();
    }

    private IEnumerable<DiaryBiz> filteredEntries => this.diaryEntries?
        .Where(e => string.IsNullOrWhiteSpace(this.searchString) ||
                    e.Title.Contains(this.searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.Description.Contains(this.searchString, StringComparison.OrdinalIgnoreCase))
        .Where(e => !this.dateRange.Start.HasValue || !this.dateRange.End.HasValue ||
                    (e.CreationDate >= this.dateRange.Start && e.CreationDate <= this.dateRange.End))
        .OrderBy<DiaryBiz, DateTime>(e => this.sortBy == "date" ? e.CreationDate : DateTime.MinValue)
        .ThenBy<DiaryBiz, string>(e => this.sortBy == "title" ? e.Title : string.Empty);

    private void OpenAddDialog()
    {
        this.editingEntry = null;
        this.currentEntry = new DiaryBiz();
        this.dialogVisible = true;
    }

    private void OpenEditDialog(DiaryBiz entry)
    {
        this.editingEntry = entry;
        this.currentEntry = new DiaryBiz
        {
            ID = entry.ID,
            Title = entry.Title,
            Description = entry.Description
        };
        this.dialogVisible = true;
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
}