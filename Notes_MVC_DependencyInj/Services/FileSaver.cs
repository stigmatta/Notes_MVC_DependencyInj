using Notes_MVC_DependencyInj.Models;
using System.Text.Json;

namespace Notes_MVC_DependencyInj.Services
{
    public class FileNoteHandler : INoteHandler
    {
        private readonly string _filePath = "notes.json";

        public async Task<List<Note>> Load()
        {
            List<Note> notes = new();

            if (File.Exists(_filePath))
            {
                string existingData = await File.ReadAllTextAsync(_filePath);
                if (!string.IsNullOrWhiteSpace(existingData))
                    notes = JsonSerializer.Deserialize<List<Note>>(existingData) ?? new List<Note>();
            }

            return notes;
        }

        public async Task Save(Note note)
        {
            List<Note> notes = await Load();

            notes.Add(note);

            string json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
