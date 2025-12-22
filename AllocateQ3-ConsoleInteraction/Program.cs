using AllocateQ3_ConsoleInteraction;
using AllocateQ3_ConsoleInteraction.Common;
using AllocateQ3_ConsoleInteraction.Common.Interfaces;
using AllocateQ3_ConsoleInteraction.Helpers;
using AllocateQ3_ConsoleInteraction.Interfaces;
using AllocateQ3_ConsoleInteraction.Repositories;
using AllocateQ3_ConsoleInteraction.Repositories.Interfaces;
using AllocateQ3_ConsoleInteraction.Services;
using AllocateQ3_ConsoleInteraction.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var services = new ServiceCollection();

Console.WriteLine("Choose your language: English (en) or Macedonian (mk):");
string? userInput = Console.ReadLine();
var languagePack = LocalizationHelper.GetCurrentLanguage(userInput);

services.AddSingleton<IFilePathProvider, FilePathProvider>();
services.AddScoped(typeof(IFileStorage<>), typeof(FileStorage<>));

services.AddScoped<IAnimalService, AnimalService>();
services.AddScoped<IAnimalRepository, AnimalRepository>();
services.AddSingleton<ISongVerseFormatter>(_ => new OldMcDonaldVerseFormatter(languagePack));
services.AddSingleton(_ => new SongPlayer(
    new OldMcDonaldVerseFormatter(languagePack),
    _.GetRequiredService<IAnimalService>(),
    languagePack
));

using var provider = services.BuildServiceProvider();

var app = provider.GetRequiredService<SongPlayer>();

app.Run();