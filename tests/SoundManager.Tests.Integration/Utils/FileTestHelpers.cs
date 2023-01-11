namespace SoundManager.Tests.Integration.Utils;

public static class FileTestHelpers
{
    public static byte[] ReadTestFile(string name)
    {
        var path = Path.Combine("TestData", name);
        if (!File.Exists(path)) throw new FileNotFoundException($"File {name} not found");
        return File.ReadAllBytes(path);
    }


    public static bool SoundFileExists(string name)
    {
        var path = Path.Combine("TestSounds", name);
        return File.Exists(path);
    }
}