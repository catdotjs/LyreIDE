using System.Threading.Tasks;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Linq;

namespace Lyre;
/// <summary>
/// Used for anything file I/O
/// </summary>
static class FileSystem{
    /// <summary>
    /// Starts the folder picker of the current OS system.
    /// </summary>
    /// <param name="window">Current used Window</param>
    /// <param name="title">Title of the selection window(default:"Choose a Folder")</param>
    /// <param name="multipleChoices">Wheter user should be able to choose more than one path(default:false)</param>
    /// <returns>Task<Uri[]></returns>
    public static async Task<Uri[]> GetFolderPath(Window window,string title="Choose a Folder",bool multipleChoices=false){
        TopLevel top = window;
        IReadOnlyList<IStorageFolder> folder = await top.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions{
            Title = title,
            AllowMultiple=multipleChoices,
        });

        Uri[] folderPaths = folder.Select(x=> x.Path).ToArray();
        return folderPaths;
    }
}