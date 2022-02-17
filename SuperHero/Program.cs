using Newtonsoft.Json;
public class SuperHeroes
{
    public string squadName { get; set; }
    public string homeTown { get; set; }
    public int formed { get; set; }
    public string secretBase { get; set; }
    public bool active { get; set; }
    public Members[] members { get; set; }
}

public class Members
{
    public string name { get; set; }
    public int age { get; set; }
    public string secretIdentity { get; set; }
    public string[] powers { get; set; }
}

class SuperHero
{
    public static void Main(string[] args)
    {
        FileWorker.FileConvert(@"D:\superhero.json");
    }
}

class FileWorker
{
    public static void FileConvert(string filePath)
    {
        string json = File.ReadAllText(filePath);
        SuperHeroes superHeroes = JsonConvert.DeserializeObject<SuperHeroes>(json);
        string path = $"{System.Environment.CurrentDirectory}\\{superHeroes.squadName}";
        DirectoryCreate(path);
        FileCreate(path, superHeroes);
    }

    public static void DirectoryCreate(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
        else
        {
            Directory.CreateDirectory(path);
        }
    }
    
    public static void FileCreate(string path, SuperHeroes superHeroes)
    {
        for (var i = 0; i < superHeroes.members.Length; i++)
        {
            int powerNum = 0;
            FileStream fstream = File.Open($"{path}\\{superHeroes.members[i].name}.csv", FileMode.OpenOrCreate);
            fstream.Close();
            for (var c = 0; c < superHeroes.members[i].powers.Length; c++)
            {
                powerNum++;
                File.AppendAllText($"{path}\\{superHeroes.members[i].name}.csv", $"{powerNum}\t{superHeroes.members[i].powers[c]}\n");
            }
        }
    }
}

