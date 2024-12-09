using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>()
    .Build();

var sessionCookie = configuration["SessionCookie"];
if (string.IsNullOrEmpty(sessionCookie))
{
    Console.WriteLine("Session cookie not found in user secrets. Please add it and try again.");
    return;
}

if (args.Length == 0)
{
    Console.WriteLine("Please provide the day as an argument.");
    return;
}

var day = args[0];
const string year = "2024";
var baseDir = Path.Combine(@"C:\Users\punsvbjo\personal\advent-of-code", year, $"Day{day}");
Directory.CreateDirectory(baseDir);

CreateProgramFile();
CreateTestFile();
CreateExampleFile();
await CreateInputFile();

Console.WriteLine($"Scaffolding complete for Day {day}");
return;

void CreateProgramFile ()
{
    var content = $$"""
                    namespace AoC{{year}}.Day{{day}};
                    
                    internal static class Day{{day}}
                    {
                        public static string Part1(string input)
                        {
                            return string.Empty;
                        }
                        public static string Part2(string input)
                        {
                            return string.Empty;
                        }
                    }
                    """;
    var filePath = Path.Combine(baseDir, $"Day{day}.cs");
    CreateAndWriteToFile(filePath, content);
}

void CreateTestFile()
{
    var content = $$"""
                    using Xunit;
                    using Xunit.Abstractions;
                    
                    namespace AoC{{year}}.Day{{day}};
                    
                    public class Day{{day}}Tests(ITestOutputHelper testOutputHelper)
                    {
                        private const int Day = {{day}};
                    
                        public static IEnumerable<object[]> TestData =>
                        new List<object[]>
                        {
                            new object[] { Day{{day}}.Part1, "example.txt", "-1" },
                            new object[] { Day{{day}}.Part1, "input.txt", "-1" },
                            new object[] { Day{{day}}.Part2, "example.txt", "-2" },
                            new object[] { Day{{day}}.Part2, "input.txt", "-2" }
                        };
                    
                        [Theory, MemberData(nameof(TestData))]
                        public void Test(Func<string, string> fn, string file, string expectedResult)
                        {
                            var input = File.ReadAllText($"../../../Day{Day}/{file}");
                            var result = fn(input);
                            Assert.Equal(expectedResult, result);
                            testOutputHelper.WriteLine(result);
                        }
                    }
                    """;
    var filePath = Path.Combine(baseDir, $"Day{day}Tests.cs");
    CreateAndWriteToFile(filePath, content);
}

async Task CreateInputFile()
{
    var inputUrl = $"https://adventofcode.com/{year}/day/{day}/input";
    var filePath = Path.Combine(baseDir, "input.txt");


    try
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");

        var content = await client.GetStringAsync(inputUrl);
        await File.WriteAllTextAsync(filePath, content.Trim());

        Console.WriteLine($"Downloaded input file at {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error downloading input file: {ex.Message}");
    }
}

void CreateExampleFile()
{
    const string exampleFileContent = "";
    var exampleFilePath = Path.Combine(baseDir, "example.txt");
    CreateAndWriteToFile(exampleFilePath, exampleFileContent);
}

static void CreateAndWriteToFile(string filePath, string content)
{
    try
    {
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Successfully written to the file at {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
    }
}