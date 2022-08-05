namespace Uow.Data.Extensions;

internal static class StringExtensions
{
    /// <summary>
    /// Solves following folders:
    /// %Desktop%
    /// %Programs%
    /// %MyDocuments%
    /// %Personal%
    /// %Favorites%
    /// %Startup%
    /// %Recent%
    /// %SendTo%
    /// %StartMenu%
    /// %MyMusic%
    /// %MyVideos%
    /// %DesktopDirectory%
    /// %MyComputer%
    /// %NetworkShortcuts%
    /// %Fonts%
    /// %Templates%
    /// %CommonStartMenu%
    /// %CommonPrograms%
    /// %CommonStartup%
    /// %CommonDesktopDirectory%
    /// %ApplicationData%
    /// %PrinterShortcuts%
    /// %LocalApplicationData%
    /// %InternetCache%
    /// %Cookies%
    /// %History%
    /// %CommonApplicationData%
    /// %Windows%
    /// %System%
    /// %ProgramFiles%
    /// %MyPictures%
    /// %UserProfile%
    /// %SystemX86%
    /// %ProgramFilesX86%
    /// %CommonProgramFiles%
    /// %CommonProgramFilesX86%
    /// %CommonTemplates%
    /// %CommonDocuments%
    /// %CommonAdminTools%
    /// %AdminTools%
    /// %CommonMusic%
    /// %CommonPictures%
    /// %CommonVideos%
    /// %Resources%
    /// %LocalizedResources%
    /// %CommonOemLinks%
    /// %CDBurning%
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string SolveSpecialFolder(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var names = Enum.GetNames(typeof(Environment.SpecialFolder));
        var values = (Environment.SpecialFolder[])Enum.GetValues(typeof(Environment.SpecialFolder));
        foreach (var name in names)
        {
            if (!value.Contains($"%{name}%"))
            {
                continue;
            }

            var index = names.IndexOf(name);

            value = value.Replace(
                $"%{name}%",
                Environment.GetFolderPath(values[index]));
        }

        return value;
    }
}